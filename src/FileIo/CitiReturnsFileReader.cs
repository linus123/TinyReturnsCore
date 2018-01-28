using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Dimensional.TinyReturns.Core;
using Dimensional.TinyReturns.Core.CitiFileImport;

namespace TinyReturns.FileIo
{
    public class CitiReturnsFileReader : ICitiReturnsFileReader
    {
        private readonly ISystemLog _systemLog;

        public CitiReturnsFileReader(
            ISystemLog systemLog)
        {
            _systemLog = systemLog;
        }

        public CitiMonthlyReturnsDataFileRecord[] ReadFile(
            string filePath)
        {
            var streamReader = new StreamReader(filePath);

            var csv = new CsvReader(streamReader);

            csv.Configuration.RegisterClassMap<CitiMonthlyReturnsDataFileRecordMap>();

            var records = csv.GetRecords<CitiMonthlyReturnsDataFileRecord>();

            return records.ToArray();
        }

        public class CitiMonthlyReturnsDataFileRecordMap : ClassMap<CitiMonthlyReturnsDataFileRecord>
        {
            public CitiMonthlyReturnsDataFileRecordMap()
            {
                Map(m => m.ExternalId).Name("ExternalID");
                Map(m => m.EndDate).Name("EndDate");
                Map(m => m.Value).Name("Value");
            }
        }

    }
}