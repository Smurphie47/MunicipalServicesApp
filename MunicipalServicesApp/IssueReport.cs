using System;

namespace MunicipalServicesApp
{
    public class IssueReport
    {
        public int ReportId { get; set; }

        public string Location { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string AttachmentPath { get; set; }

        public DateTime DateSubmitted { get; set; }

        public string Status { get; set; }
    }
}