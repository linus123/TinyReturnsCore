using System.IO;
using TinyReturns.Core.CitiFileImport;
using Xunit;

namespace TinyReturns.IntegrationTests.Core.CitiFileImport
{
    public class CitiReturnsFileReaderTests : DatabaseTestBase
    {
        [Fact]
        public void ShouldReadCorrectNumberOfRecordsGivenValidReturnsFile()
        {
            var results = ReadTestFile();

            Assert.Equal(9, results.Length);
        }

        [Fact]
        public void ShouldReadCorrectPropertiesGivenValidReturnsFile()
        {
            var results = ReadTestFile();

            Assert.Equal("100", results[0].ExternalId);
            Assert.Equal("10/31/2013", results[0].EndDate);
            Assert.Equal("4.40055", results[0].Value);
        }

        private CitiMonthlyReturnsDataFileRecord[] ReadTestFile()
        {
            var file = GetNetReturnsTestFilePath();

            var serviceLocator = new ServiceLocatorForIntegrationTests();
            var reader = serviceLocator.GetService<ICitiReturnsFileReader>();

            var results = reader.ReadFile(file);
            return results;
        }

        private string GetNetReturnsTestFilePath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            var targetFile = currentDirectory
                 + @"\Core\TestNetReturnsForEntity100.csv";

            return targetFile;
        }

    }
}