using Dimensional.TinyReturns.Core;
using Microsoft.Extensions.Configuration;

namespace CitiFileImporterConsole
{
    public class CitiFileImporterConsoleSettings : ITinyReturnsDatabaseSettings
    {
        private readonly IConfiguration _configuration;

        public CitiFileImporterConsoleSettings(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ReturnsDatabaseConnectionString
        {
            get { return _configuration.GetConnectionString("TinyReturnsDatabase"); }
        }
    }
}