namespace TinyReturns.Core.CitiFileImport
{
    public interface ICitiReturnsFileReader
    {
        CitiMonthlyReturnsDataFileRecord[] ReadFile(
            string filePath);
    }
}