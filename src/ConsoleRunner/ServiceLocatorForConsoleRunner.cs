using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TinyReturns.Core;
using TinyReturns.Core.CitiFileImport;
using TinyReturns.Core.DataRepositories;
using TinyReturns.Database;
using TinyReturns.FileIo;

namespace TinyReturns.ConsoleRunner
{
    public class ServiceLocatorForConsoleRunner
    {
        private readonly IDictionary<object, object> _services;

        internal ServiceLocatorForConsoleRunner(
            IConfigurationRoot configuration)
        {
            _services = new Dictionary<object, object>();

            _services.Add(typeof(SystemLogNoOp), new SystemLogNoOp());
            _services.Add(typeof(ConsoleRunnerSettings), new ConsoleRunnerSettings(configuration));

            _services.Add(typeof(ISystemLog), GetService<SystemLogNoOp>());
            _services.Add(typeof(ITinyReturnsDatabaseSettings), GetService<ConsoleRunnerSettings>());

            _services.Add(typeof(TinyReturnsDatabase), new TinyReturnsDatabase(GetService<ITinyReturnsDatabaseSettings>(), GetService<ISystemLog>()));

            _services.Add(typeof(IReturnsSeriesDataGateway), GetService<TinyReturnsDatabase>());
            _services.Add(typeof(IMonthlyReturnsDataGateway), GetService<TinyReturnsDatabase>());

            _services.Add(typeof(CitiReturnsFileReader), new CitiReturnsFileReader(GetService<ISystemLog>()));

            _services.Add(
                typeof(CitiReturnSeriesImporter),
                new CitiReturnSeriesImporter(
                    GetService<IReturnsSeriesDataGateway>(),
                    GetService<CitiReturnsFileReader>(),
                    GetService<IMonthlyReturnsDataGateway>()));

            _services.Add(
                typeof(CitiFileImportInteractor),
                new CitiFileImportInteractor(
                    GetService<CitiReturnSeriesImporter>()));

        }

        public T GetService<T>()
        {
            try
            {
                return (T)_services[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException("The requested service is not registered");
            }
        }
    }
}