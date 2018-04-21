namespace TinyReturns.Core.PerformanceReport
{
    public interface IPerformanceReportExcelReportView
    {
        void RenderReport(
            PerformanceReportExcelReportModel model,
            string fullFilePath);
    }
}