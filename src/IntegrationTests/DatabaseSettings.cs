using System.IO;
using Microsoft.Extensions.Configuration;
using TinyReturns.Core;

namespace TinyReturns.IntegrationTests
{
    public class DatabaseSettings : ITinyReturnsDatabaseSettings
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