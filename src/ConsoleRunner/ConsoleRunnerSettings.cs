using Microsoft.Extensions.Configuration;
using TinyReturns.SharedKernel;

namespace TinyReturns.ConsoleRunner
{
    public class ConsoleRunnerSettings : ITinyReturnsDatabaseSettings
    {
        private readonly IConfiguration _configuration;

        public ConsoleRunnerSettings(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ReturnsDatabaseConnectionString
        {
            get
            {
                return _configuration.GetConnectionString("TinyReturnsDatabase");
            }
        }
    }
}