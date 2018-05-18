namespace TinyReturns.SharedKernel.CitiFileImport
{
    public interface ICitiReturnsFileReader
    {
        CitiMonthlyReturnsDataFileRecord[] ReadFile(
            string filePath);
    }
}