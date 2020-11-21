using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace API.Infrastructure
{
    public class ApiConfiguration: IApiConfiguration
    {
        private readonly IConfiguration configuration;

        public ApiConfiguration(IConfiguration configuration)
        {
            string keyVaultUrlPattern = configuration["KeyVaultUrlPattern"];
            string avgKeyVaultName = configuration["AvgKeyVaultName"];
            string avgKeyVaultUrl = string.Format(keyVaultUrlPattern, avgKeyVaultName);

            var configBuilder = new ConfigurationBuilder()
                        .AddConfiguration(configuration)
                        .AddAzureKeyVault(new AzureKeyVaultConfigurationOptions(avgKeyVaultUrl)
                        {
                            ReloadInterval = new System.TimeSpan(0, 30, 0)
                        });

            this.configuration = configBuilder.Build();
        }

        public string AvgConnectionString => configuration["AvgConnectionString"];
    }
}