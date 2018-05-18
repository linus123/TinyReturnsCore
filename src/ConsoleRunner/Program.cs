using System.IO;
using Microsoft.Extensions.Configuration;
using TinyReturns.SharedKernel.CitiFileImport;

namespace TinyReturns.ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var serviceLocator = new ServiceLocatorForConsoleRunner(configuration);

            var citiFileImportInteractor = serviceLocator.GetService<CitiFileImportInteractor>();

            var citiFileImportRequestModel = new CitiFileImportRequestModel(args);

            citiFileImportInteractor.ImportFiles(citiFileImportRequestModel);
        }
    }
}
