# channel-engine
ChannelEngine API consumer with console &amp; mvc endpoints.

# introduction
Data oriented monolith application which is mainly responsible for calling third-party and process some business logic.  
The ChannelEngine swagger documentation ```https://api-dev.channelengine.net/api/```  

Some high level overview:  
![highlevel](https://github.com/omelianlevkovych/channel-engine/blob/main/assets/overview.png)

# buid, test
In order to build a solution:
```dotnet
cd [project-root-dir]
dotnet build
```

To run a tests:
```dotnet
cd [project-root-dir]
dotnet test
```

You will have to provide ```ApiKey``` throw environment variable or by changing appsettings.json value.

# todo
- add more tests; play with fluet validator
- add mvc
- add error hadling
- add serilog
- add polly for retries


# notes
- patch endpoint only accepts array. In case I have single patch operation like:
```json
{
    "op": "replace",
    "value": 1256,
    "path": "Stock"
}
```
You will get validation exception in this case with the message:
```json
The JSON patch document was malformed and could not be parsed.
```

- /orders endpoint does not have ```Name``` property for products. Therefore you have to call the /products endpoint to fetch it.
It hits performance and creates some unnecessary complexity.
- It is not clear how to merge the products from /orders endpoint (I decided to merge them based on MerchantProductNo), however
there are some drawbacks. What to do in case we do not have consistent data, for example ```Gtin```.
- The /orders endpoint lacks validation for status query parameter dublicates; its hard to rely on some behaviour.

