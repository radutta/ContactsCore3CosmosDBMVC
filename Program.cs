using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ContactsCore3CosmosDBMVC
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
              if (!hostingContext.HostingEnvironment.IsDevelopment())
              {
                SetupKeyVault(hostingContext, config);
              }
              SetupKeyVault(hostingContext, config); // comment this line before deployment to azure
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });

    private static void SetupKeyVault(HostBuilderContext hostingContext, IConfigurationBuilder config)
    {
      var buildConfig = config.Build();
      var keyVaultEndpoint = buildConfig["MNKeyVault"];
      if (!string.IsNullOrEmpty(keyVaultEndpoint))
      {
        var azureServiceTokenProvider = new AzureServiceTokenProvider();
        var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
        config.AddAzureKeyVault(keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
      }
    }
  }
}
