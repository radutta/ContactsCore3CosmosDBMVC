# Azure CosmosDB

- Using DotNetCore 3.1 MVC for the Demo

#### DotNet commands

- dotnet new web --name ContactsCore3CosmosDBMVC -f netcoreasp3.1
- DotNet Packages for the Project
  - Using .Net 3.0
    - dotnet add package Microsoft.Azure.Cosmos -v 3.6.0 for using CosmosClient as the class
  - Using .Net 2.0
    - dotnet add package Microsoft.Azure.DocumentDB.Core -v 2.9.2 DocumentClient as the class
      - namespace tobe used Micorsoft.Azure.Documents.Client
  - For Swagger
    - dotnet add package Swashbuckle.AspNetCore -v 5.0.0
  - For KeyVault
    - dotnet add package Microsoft.Azure.Services.AppAuthentication -Version 1.4.0 (or latest)
    - dotnet add package Microsoft.Azure.KeyVault -Version 3.0.5 (or latest)
    - dotnet add package Microsoft.Extensions.Configuration.AzureKeyVault -Version 3.1.2 (or latest)
  - For Redis Cache
    - dotnet add package Microsoft.Extensions.Caching.Redis -Version 2.2.0 (Use this one)
  - For Azure Application Insights
    - dotnet add package Microsoft.ApplicationInsights.AspNetCore -Version 2.13.1 or (latest)
    - dotnet add package Microsoft.Extensions.Logging.ApplicationInsights -Version 2.13.1 (for logging ILogger user defined logs in ApplicationInsights)

#### KeyVault

"CosmosConnectionString--CosmosEndpoint"="Enter the URI"
"CosmosConnectionString--CosmosKey"="Enter the Key"
"ApplicationInsights--InstrumentationKey"="Enter the Key"
"ConnectionStrings--RedisConnection"="Enter the ConnectionString"
