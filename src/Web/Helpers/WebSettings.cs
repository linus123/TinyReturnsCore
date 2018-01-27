using Dimensional.TinyReturns.Core;
using Microsoft.Extensions.Configuration;

namespace TinyReturnsCore.Helpers
{
    public class WebSettings : ITinyReturnsDatabaseSettings
    {
        private readonly IConfiguration _configuration;

        public WebSettings(
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