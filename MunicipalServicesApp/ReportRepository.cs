using System.Collections.Generic;

namespace MunicipalServicesApp
{
    public static class ReportRepository
    {
        public static List<IssueReport> Reports { get; }
            = new List<IssueReport>();

        public static int GetNextReportId()
        {
            return Reports.Count + 1;
        }

        public static void AddReport(IssueReport report)
        {
            Reports.Add(report);
        }
    }
}