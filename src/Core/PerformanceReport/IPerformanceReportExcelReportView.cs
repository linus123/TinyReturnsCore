namespace TinyReturns.SharedKernel.PerformanceReport
{
    public interface IPerformanceReportExcelReportView
    {
        void RenderReport(
            PerformanceReportExcelReportModel model,
            string fullFilePath);
    }
}