# CLAUDE.md

## Project

The GoPay .NET SDK (NuGet package `GoPay.NET`), a client library for the GoPay payment gateway REST API (https://doc.gopay.com). The library targets `netstandard2.0`. The test project targets `net5.0` and uses MSTest.

## Commands

These match CI ([.github/workflows](.github/workflows/), which runs on windows-latest). The build needs the .NET 10 SDK, pinned by [global.json](global.json). Running the tests also needs the .NET 5 runtime.

```bash
dotnet restore
dotnet build --no-restore
dotnet test --no-build --verbosity normal

# Single test / single class
dotnet test --filter "FullyQualifiedName~GoPay.Tests.CreatePaymentTests.<MethodName>"
dotnet test --filter "ClassName=GoPay.Tests.CreatePaymentTests"

# Formatting (CI runs the --verify-no-changes variant)
dotnet format whitespace
dotnet format whitespace --verify-no-changes

# Public API compatibility vs. a released package (CI does this against API_BASELINE_VERSION)
dotnet tool restore
dotnet apicompat --left <released GoPay.net-sdk.dll> --right GoPay.net-sdk/bin/Release/netstandard2.0/GoPay.net-sdk.dll
```

## Code quality tooling

Backward compatibility of the published package comes first. The tooling is set up so that it never changes the public API:

- `LangVersion` is pinned to `8.0` and `Nullable` is `warnings`, which analyses null-safety without emitting annotations into the public API. Don't switch to `enable` outside a major release.
- Roslyn analyzers run at the pinned `AnalysisLevel` `10.0-recommended`. [.editorconfig](.editorconfig) groups the rules: rules whose fix would break the API are `none`, and rules whose fix changes behavior are `suggestion`. Don't "fix" a disabled rule by renaming or changing public members.
- In CI (`CI=true`) the library treats warnings as errors, except the NuGet audit warnings NU1901–NU1904. A new warning has to be fixed or explicitly classified in `.editorconfig`.
- Formatting rules are in the top part of [.editorconfig](.editorconfig) and are checked in CI with `dotnet format whitespace --verify-no-changes`. Only the `whitespace` mode is used: `dotnet format style` and `dotnet format analyzers` can rename or change public members, so don't run them across the whole project. Files use LF, which [.gitattributes](.gitattributes) enforces. Don't set `charset`, because files are mixed UTF-8 with and without BOM. Add bulk reformat commits to [.git-blame-ignore-revs](.git-blame-ignore-revs).
- The CI ApiCompat step fails if public API is removed or changed compared to `API_BASELINE_VERSION` in the workflow. Bump it after each release.

To package for NuGet, build Release and pack with [Package.nuspec](Package.nuspec). It takes the DLL from `GoPay.net-sdk\bin\Release\netstandard2.0\` (Windows-style paths).

## Tests are integration tests

All tests in [GoPay.net-sdkTests/src/Tests/](GoPay.net-sdkTests/src/Tests/) call the live GoPay **sandbox** (`https://gw.sandbox.gopay.com/api`) with credentials from [TestUtils.cs](GoPay.net-sdkTests/src/Tests/TestUtils.cs). There are no mocks. A test needs network access, and its result depends on sandbox state.

Most `[TestMethod()]` attributes are commented out on purpose. Many tests use hard-coded payment, card or batch IDs that only work for a specific sandbox account. When you add or run a test, enable it locally. Don't re-enable the whole suite.

## Architecture

- **[GPConnector.cs](GoPay.net-sdk/src/GPConnector.cs) is the whole public API surface.** Every gateway operation is a method on this one class: payments, refunds, recurrence, preauthorization, QR payment, payment instruments, account statements, EET, Supercash and cards. Many operations come as a sync/`Async` pair. To add an endpoint:
  1. Add a method that calls `CreateRestRequest(url, contentType[, param, method])`. This adds `Accept` and `Authorization: Bearer <AccessToken.Token>`.
  2. Serialize the body with `serializeToJson`, which uses Newtonsoft and ignores nulls.
  3. Pass the response through `ProcessResponse<T>`.
- **Auth flow:** `GetAppToken(scope)` POSTs `/oauth2/token` with HTTP Basic auth and stores `AccessToken` on the connector. It returns `this`, so callers chain it: `connector.GetAppToken().CreatePayment(...)`. Nothing refreshes the token automatically (it lasts 30 minutes).
- **Shared static `RestClient`:** `Client` is `static`, and every constructor reassigns it. The last connector constructed sets the base URL and User-Agent for every instance in the process. Keep this in mind when changing connector construction.
- **Error handling:** `Deserialize<T>` first parses the body as `APIError`. If `ErrorMessages` is non-null, it throws `GPClientException` with `.Error` set. Any other parse failure is wrapped in `GPClientException` together with the status code and the content. `processComplex<T>` is used for array responses such as refund history.
- **`IncomingDataEventHandler`** is raised with the raw response data in `ProcessResponse`. `GetAppToken` raises it too.
- **Serialization:** models are Newtonsoft POCOs that map snake_case JSON through `[JsonProperty]`. Enums use `StringEnumConverter`, or `SafeJsonEnumStringConvertor` (file `DefensiveJsonEnumStringConvertor.cs`), which falls back to a given "unknown" value instead of throwing. Dates use `GPDateAdapter`. `GetPaymentInstruments` rewrites some hyphenated keys before it deserializes (`ProcessPaymentInstrumentRootContent`).

## Namespace quirks

The project folder layout doesn't match the namespaces:

- The connector, `Utils` and `GPClientException` live in `GoPay`.
- Most payment models are in `GoPay.Model.Payments` (plural). A few are in `GoPay.Model.Payment`.
- `Card`, `Refund` and `Utils` use the legacy `gopay_dotnet_standard_api.src[.Model.Payment]` namespace. That name comes from the csproj `RootNamespace`.
- The other groups are `GoPay.Common`, `GoPay.Account`, `GoPay.EETProp` and `GoPay.Supercash`.

Changing any of these namespaces breaks consumers. Match the namespace already used by sibling files.

## Releasing / versioning

The version is set in two places that must agree: `<version>` in [GoPay.net-sdk.csproj](GoPay.net-sdk/GoPay.net-sdk.csproj) and `<version>` in [Package.nuspec](Package.nuspec). Update `<releaseNotes>` in the nuspec too. The nuspec `<dependencies>` list is maintained by hand and must match the csproj `PackageReference` versions. The default User-Agent (`GoPay .NET <assembly version>`) is built from the assembly version. After publishing, bump `API_BASELINE_VERSION` in [.github/workflows/dotnet.yml](.github/workflows/dotnet.yml) to the new version.
