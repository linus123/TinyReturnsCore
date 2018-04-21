using System;
using System.IO;
using Dimensional.TinyReturns.Database;
using Microsoft.Extensions.Configuration;
using TinyReturns.Core;
using TinyReturns.Core.CitiFileImport;
using TinyReturns.FileIo;

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

            var citiFileImportInteractor = CreateCitiFileImportInteractor(configuration);

            var citiFileImportRequestModel = new CitiFileImportRequestModel(args);

            citiFileImportInteractor.ImportFiles(citiFileImportRequestModel);
        }

        private static CitiFileImportInteractor CreateCitiFileImportInteractor(
            IConfigurationRoot configuration)
        {
            var citiFileImporterConsoleSettings = new ConsoleRunnerSettings(configuration);
            var systemLogNoOp = new SystemLogNoOp();

            var tinyReturnsDatabase = new TinyReturnsDatabase(citiFileImporterConsoleSettings, systemLogNoOp);
            var citiReturnsFileReader = new CitiReturnsFileReader(systemLogNoOp);

            var citiReturnSeriesImporter = new CitiReturnSeriesImporter(
                tinyReturnsDatabase,
                citiReturnsFileReader,
                tinyReturnsDatabase);

            var citiFileImportInteractor = new CitiFileImportInteractor(
                citiReturnSeriesImporter);

            return citiFileImportInteractor;
        }
    }
}
