# GoPay .NET API #


Detailed guide: [https://doc.gopay.com](https://doc.gopay.com)


# Requirements

 - .NET Standard 2.0

# NuGet #

This library is avalaible from NuGet Package Manager 

```
PM> Install-Package GOPAY.NET
```

# Dependencies #

- Newtonsoft.Json
- Restsharp
- Restsharp.Newtonsoft.Json

# Namespace #

```cs
  using GoPay.Common;
  using GoPay.Model;
  using GoPay.Payment;
```


# Basic usage 

## Creating an instance of GPConnector

```cs
var connector = new GPConnector(<API_URL>,<USER_SECRET>, <USER_ID>);
```

The connector provides methods for interacting with our gateway.


## OAuth
 
 
To be able to communicate with our gateway it's required to create an auth token.
 
 ```cs
 var connector = new GPConnector(<API_URL>,<USER_ID>, <USER_SECRET>);
 connector.GetAppToken(); 
 ```
 
 The token gets cached in an instance of GPConnector and its lifetime is 30 minutes. The method ` GetAppToken()` creates token in a scope `"payment-create"`. If you would like to create a token in a different scope call method `GetAppToken(<SCOPE>)` Once the token expires its required to obtain a new one by calling the method getAppToken again.
     
 
## Avalaible methods 
  
| Method        | API Action    |
| :------------ |:--------------|
| [CreatePayment](#create) | https://doc.gopay.com/#payment-creation |
| [PaymentStatus](#status) | https://doc.gopay.com/#payment-status |
| [RefundPayment](#refund) | https://doc.gopay.com/#payment-refund |
| [CreateRecurrentPayment](#createrec) | https://doc.gopay.com/#recurring-payments |
| [CreateRecurrentPayment](#createrecdem) | https://doc.gopay.com/#recurring-on-demand |
| [VoidRecurrency](#voidrec) | https://doc.gopay.com/#recurring-payment-cancellation |
| [PreauthorizedPazment](#preauth) | https://doc.gopay.com/#preauthorized-payment-creation |
| [VoidAuthorization](#voidauth) | https://doc.gopay.com/#cancelling-a-preauthorized-payment |
| [CapturePayment](#capt) | https://doc.gopay.com/#capturing-a-preauthorized-payment |
 
 
###### Create a payment <a id="create">

```cs
 var payment = new BasePayment()
            {
                Currency = <Currency>,
                Lang = "ENG",
                OrderNumber = "789456167879",
                Amount = 7500,
                Target = new Target()
                {
                    GoId = <GOID>,
                    Type = Target.TargetType.ACCOUNT
                },
                Callback = new Callback()
                {
                    NotificationUrl = <NOTIFICATION_URL>,
                    ReturnUrl = <RETURN_URL>
                },
                Payer = new Payer()
                {
                    Contact = new PayerContact()
                    {
                        Email = "test@test.gopay.cz"
                    },
                    DefaultPaymentInstrument = PaymentInstrument.PAYMENT_CARD
                }
                
            };


try {
    var result = connector.CreatePayment(payment);
} catch (GPClientException e) {
    //
}
 ```

###### Payment status <a id="status">

```cs
try {
    var payment = connector.PaymentStatus(<PAYMENT_ID>);
} catch (GPClientException e) {
     //
}
```
 
###### Payment refund <a id="refund">

```cs
try {
      var result = connector.RefundPayment(<PAYMENT_ID>, <AMOUNT>);
} catch (GPClientException e) {
      //
}
```
 
###### Create preauthorized payment <a id="preauth">

```cs
var payment = new BasePayment() 
{
  PreAuthorize = true,
  ...
};
try {
    connector.CreatePayment(payment);
} catch (GPClientException ex) {
    //
}
```

###### Void authorization <a id="voidauth">

```cs
try {
    var result = connector.VoidAuthorization(<ID>);
} catch (GPClientException ex) {
    //
}
```

###### Recurrent payment on demand <a id="createrecdem">

```cs
var recurrence = new NextPayment() 
{
 Amount = <Amount>,
 Currency = <Currency>,
 OrderNumber = <OrderNumber>
};

try {
    connector.CreateRecurrentPayment(payment);
} catch {GPClientException e) {
    //
}
```

###### Recurrent payment <a id="createrec">

```cs
var recurrence = new Recurrence() 
{
 Cycle = RecurrenceCycle.DAY,
 DateTo = new DateTime(2020, 12, 12),
 Period = 5
};
var payment = new BasePayment();
payment.Recurrence = recurrence;

try {
    connector.CreatePayment(payment);
} catch {GPClientException e) {
    //
}
```

###### Capture payment <a id="capt">

```cs
try {
    var capture = connector.CapturePayment(<ID>);
} catch (GPClientException ex) {
    //
}
```

###### Void recurrency <a id="voidrec">

 ```cs
try {
    var voidRecurrency = connector.VoidRecurrency(<ID>);
} catch (GPClientException ex) {
     //
}
```

All methods above throw checked exception GPClientException on a failure.

```cs
try {
    connector.getAppToken().CreatePayment(payment);
} catch (GPClientException e) {
    var err = exception.Error;
    var date = err.DateIssued;
    foreach (var element in err.ErrorMessages)
    {
        //
    }
}
```

## Contributing

Contributions from others would be very much appreciated! Send pull request/ issue. Thanks!

## License

Copyright (c) 2016 GoPay.com. MIT Licensed, see LICENSE for details.
