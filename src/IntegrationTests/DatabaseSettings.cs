using System.IO;
using Dimensional.TinyReturns.Core;
using Microsoft.Extensions.Configuration;

namespace Dimensional.TinyReturns.IntegrationTests
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