
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public static class ReportRepository
    {
        private static readonly string DataFolder =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "MunicipalConnect");

        private static readonly string FilePath =
            Path.Combine(DataFolder, "reports.json");

        public static List<IssueReport> Reports { get; private set; }
            = new List<IssueReport>();

        static ReportRepository()
        {
            LoadReports();
        }

        public static int GetNextReportId()
        {
            return Reports.Count == 0
                ? 1
                : Reports.Max(r => r.ReportId) + 1;
        }

        public static void AddReport(IssueReport report)
        {
            if (report == null)
                throw new ArgumentNullException(nameof(report));

            Reports.Add(report);

            try
            {
                SaveReports();
            }
            catch
            {
                Reports.Remove(report);
                throw;
            }
        }

        public static void SaveReports()
        {
            Directory.CreateDirectory(DataFolder);

            string temporaryFile = FilePath + ".tmp";

            var serializer =
                new DataContractJsonSerializer(
                    typeof(List<IssueReport>));

            using (FileStream stream =
                new FileStream(
                    temporaryFile,
                    FileMode.Create,
                    FileAccess.Write))
            {
                serializer.WriteObject(stream, Reports);
            }

            if (File.Exists(FilePath))
            {
                File.Replace(
                    temporaryFile,
                    FilePath,
                    null);
            }
            else
            {
                File.Move(temporaryFile, FilePath);
            }
        }

        public static void LoadReports()
        {
            if (!File.Exists(FilePath))
            {
                Reports = new List<IssueReport>();
                return;
            }

            try
            {
                var serializer =
                    new DataContractJsonSerializer(
                        typeof(List<IssueReport>));

                using (FileStream stream =
                    new FileStream(
                        FilePath,
                        FileMode.Open,
                        FileAccess.Read))
                {
                    Reports =
                        serializer.ReadObject(stream)
                        as List<IssueReport>
                        ?? new List<IssueReport>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load saved reports.\n\n" +
                    ex.Message,
                    "Report Storage Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                throw;
            }
        }
    }
}
