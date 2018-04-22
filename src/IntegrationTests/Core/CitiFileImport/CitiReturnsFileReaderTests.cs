﻿using System.IO;
using TinyReturns.Core;
using TinyReturns.Core.CitiFileImport;
using Xunit;

namespace TinyReturns.IntegrationTests.Core.CitiFileImport
{
    public class CitiReturnsFileReaderTests : IntegrationTestBase
    {
        [Fact]
        public void ShouldReadCorrectNumberOfRecordsGivenValidReturnsFile()
        {
            var results = ReadTestFile();

            Assert.Equal(results.Length, 9);
        }

        [Fact]
        public void ShouldReadCorrectPropertiesGivenValidReturnsFile()
        {
            var results = ReadTestFile();

            Assert.Equal(results[0].ExternalId, "100");
            Assert.Equal(results[0].EndDate, "10/31/2013");
            Assert.Equal(results[0].Value, "4.40055");
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