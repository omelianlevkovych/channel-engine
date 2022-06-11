# Table of Content
- [channel-engine](#channel-engine)
- [introduction](#introduction)
- [technologies & other](#technologies--other)
- [buid & test](#buid--test)
- [notes](#notes)

# channel-engine
ChannelEngine API consumer with console &amp; MVC endpoints.

# introduction
Data-oriented monolith application is mainly responsible for calling third-party and processing some business logic.  
The ChannelEngine swagger documentation ```https://api-dev.channelengine.net/api/```  

Some high level overview:  
![highlevel](https://github.com/omelianlevkovych/channel-engine/blob/main/assets/overview.png)

# technologies & other
- **dotnet6** & C#
- **Polly** for HTTP retires
- **Serilog** for persistent logging
- **git**, gitflow
- xUnit, **FluentAssertion**, **NSubstitute**
- TDD & **DDD**, SOLID & best practices
- github actions

# buid & test
In order to build a solution:
```dotnet
cd [project-root-dir]
dotnet build
```

To run tests:
```dotnet
cd [project-root-dir]
dotnet test
```

You will **have to provide** ```ApiKey``` by changing the environment variable or by changing the appsettings.json value.

# notes
- patch endpoint only accepts arrays. In case I have a single patch operation like:
```JSON
{
    "op": "replace",
    "value": 1256,
    "path": "Stock"
}
```
You will get a validation exception in this case with the message:
```JSON
The JSON patch document was malformed and could not be parsed.
```

- /orders endpoint does not have ```Name``` property for products. Therefore you have to call the /products endpoint to fetch it.
It hits performance and creates some unnecessary complexity.
- It is not clear how to merge the products from the /orders endpoint (I decided to merge them based on MerchantProductNo), however
there are some drawbacks. What to do in case we do not have consistent data, for example ```Gtin```.
- The /orders endpoint lacks validation for status query parameter duplicates; it's hard to rely on some behaviour.

