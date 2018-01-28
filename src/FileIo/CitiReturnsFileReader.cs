using System.IO;
using System.Linq;
using CsvHelper;
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

            var records = csv.GetRecords<CitiMonthlyReturnsDataFileRecord>();

            return records.ToArray();
        }
    }
}