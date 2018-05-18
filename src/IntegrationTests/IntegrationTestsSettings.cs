using System.IO;
using Microsoft.Extensions.Configuration;
using TinyReturns.SharedKernel;

namespace TinyReturns.IntegrationTests
{
    public class IntegrationTestsSettings : ITinyReturnsDatabaseSettings
    {
        public string ReturnsDatabaseConnectionString
        {
            get
            {
                var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                var configurationRoot = configurationBuilder.Build();

                return configurationRoot.GetConnectionString("TinyReturnsDatabase");
            }
        }
    }
}