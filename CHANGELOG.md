# Changelog

## Unreleased

- Build uses the .NET 10 SDK, pinned by `global.json`; the library still targets `netstandard2.0`
- Nullable analysis enabled in warnings-only mode (`LangVersion` 8.0, `Nullable` `warnings`), no nullable annotations in the public API
- Enabled Roslyn analyzers (`AnalysisLevel` `10.0-recommended`), rules classified in `.editorconfig`, public API unchanged
- Library warnings are treated as errors in CI (except NuGet audit warnings NU1901–NU1904)
- CI checks public API compatibility against the last released package with ApiCompat
- Added formatting rules to `.editorconfig`, applied `dotnet format whitespace` (formatting only), formatting checked in CI
- Added Husky.Net pre-commit hook formatting staged `.cs` files, installed automatically on `dotnet restore`
- CI tests on .NET 5, 8 and 10, `actions/checkout` bumped to v7 and `actions/setup-dotnet` to v6
- CI `GITHUB_TOKEN` restricted to read-only repository contents
- Added Dependabot configuration for GitHub Actions and NuGet
- Tests use payments and card tokens from the current sandbox account, updated sandbox credentials
- Added CLAUDE.md and CHANGELOG.md

## 1.4.0

- Added QR payment feature (`GetQrPayment`)

## 1.3.3

- Fixed `BankAccount.ToString()` (added `AccountToken`)
- Removed debug console output from `Bnpl`

## 1.3.2

- Updated `Newtonsoft.Json` to 13.0.3, `RestSharp` and `RestSharp.Serializers.NewtonsoftJson` to 112.1.0

## 1.3.1

- Added default `User-Agent` with SDK version, constructor overload with custom `User-Agent`
- Renamed `Payer.PaymendCard` to `Payer.PaymentCard` (breaking change)

## 1.3.0

- Added BNPL payment method
- Added `BnplType` enum, `allowed_bnpl_types` and `default_bnpl_type`

## 1.2.8

- Added history of refunds (`GetRefundsHistory`)

## 1.2.7

- Added card detail (`GetCardDetail`) and card removal (`DeleteCard`)

## 1.2.6

- Fixed language codes (`EE` → `ET`, `SI` → `SL`)

## 1.2.5

- No changes recorded in the repository

## 1.2.4

- Fixed refund call headers

## 1.2.3

- Added `card_fingerprint`
- GET requests are sent without `Content-Type`

## 1.2.2

- No changes recorded in the repository

## 1.2.1

- Removed `HRK` currency
- `Lang` made public, added languages `UK`, `EE`, `LT`, `LV`, `SI`, `PT`
- Bumped `Newtonsoft.Json` from 13.0.1 to 13.0.2

## 1.2.0

- Updated `RestSharp` to 108.0.2 (may contain breaking changes, see https://restsharp.dev/v107/)
- Added `_5050` substate
- Added MCOF params (`card_id`, `request_card_token`, `masked_pan`)

## 1.1.20

- Dependency updates

## 1.1.19

- Fixed refund call content type (#20)

## 1.1.18

- Added `_5049` substate

## 1.1.17

- Added `CLICK_TO_PAY` payment instrument
- Added `payer.bank_account.account_token` property

## 1.1.16

- Added `_5048` substate

## 1.1.15

- Library targets .NET Standard 2.0 (previously .NET Framework 4.5)
- Updated `Newtonsoft.Json` to 13.0.1, `RestSharp` and `RestSharp.Serializers.NewtonsoftJson` to 106.12.0

## 1.1.14

- Added Apple Pay payment instrument
- Updated supported languages
- Updated `Newtonsoft.Json` to 12.0.3
- Fixed error handling
- Added package icon

## 1.1.13

- Unreadable server responses are thrown as `GPClientException` with the HTTP status code and content

## 1.1.12

- Added server event listener

## 1.1.11

- No changes recorded in the repository

## 1.1.10

- Unknown payment instruments no longer throw on deserialization

## 1.1.9

- Added Google Pay payment instrument
- Added payment substates `_5001`, `_5020`, `_5040`, `_5041`

## 1.1.8

- Updated currencies list
- TLS 1.2 is used by default (`setSecurityProtocol` constructor parameter)

## 1.1.7

- Added partial capture

## 1.1.6

- Added card token payment

## 1.1.5

- Minimal target framework lowered to .NET Framework 4.5
- Updated `Newtonsoft.Json` to 9.0.1

## 1.1.4

- Added missing payment substates
- Added superCASH methods

## 1.1.1 – 1.1.3

- No changes recorded in the repository

## 1.1.0

- Added code lists, list of enabled payment instruments, account statement and EET receipts
- Fixed nullable enums and `ToString()` methods

## 1.0.1

- Fixed payment status method

## 1.0.0

- First release
