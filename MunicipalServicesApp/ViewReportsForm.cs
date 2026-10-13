
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class ViewReportsForm : Form
    {
        private DataGridView reportsGrid;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblReportCount;
        private TextBox txtReportDetails;

        // APPLICATION COLOURS
        private readonly Color navyColor =
            Color.FromArgb(16, 42, 67);

        private readonly Color tealColor =
            Color.FromArgb(0, 166, 166);

        private readonly Color lightBackground =
            Color.FromArgb(244, 248, 251);

        private readonly Color secondaryBackground =
            Color.FromArgb(230, 238, 245);

        public ViewReportsForm()
        {
            InitializeComponent();
            CreateReportInterface();
            LoadReports();
        }

        // CREATE THE MAIN INTERFACE
        private void CreateReportInterface()
        {
            SuspendLayout();

            Text = "Municipal Connect - Submitted Reports";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1100, 750);
            MinimumSize = new Size(850, 600);
            BackColor = lightBackground;
            Font = new Font("Segoe UI", 10F);

            Controls.Clear();

            TableLayoutPanel mainLayout =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 5,
                    Padding = new Padding(15),
                    BackColor = lightBackground
                };

            mainLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 85F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 160F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 75F));

            // HEADER
            Label heading = new Label
            {
                Text = "Submitted Municipal Reports",
                Dock = DockStyle.Fill,
                BackColor = navyColor,
                ForeColor = Color.White,
                Font = new Font(
                    "Segoe UI",
                    22F,
                    FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0)
            };

            // REPORT COUNT
            lblReportCount = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font(
                    "Segoe UI",
                    11F,
                    FontStyle.Bold),
                ForeColor = navyColor,
                Padding = new Padding(10, 0, 0, 0)
            };

            // REPORTS TABLE
            reportsGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = true,

                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,

                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,

                MultiSelect = false,
                RowHeadersVisible = false,

                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,

                EnableHeadersVisualStyles = false,
                ColumnHeadersHeight = 42,
                ColumnHeadersHeightSizeMode =
                    DataGridViewColumnHeadersHeightSizeMode.DisableResizing,

                RowTemplate = { Height = 38 },

                GridColor =
                    Color.FromArgb(225, 232, 238),

                Margin = new Padding(0, 0, 0, 10)
            };

            // COLUMN HEADERS
            reportsGrid.ColumnHeadersDefaultCellStyle.BackColor =
                navyColor;

            reportsGrid.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            reportsGrid.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold);

            reportsGrid.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // TABLE ROWS
            reportsGrid.DefaultCellStyle.BackColor =
                Color.White;

            reportsGrid.DefaultCellStyle.ForeColor =
                navyColor;

            reportsGrid.DefaultCellStyle.Font =
                new Font("Segoe UI", 10F);

            reportsGrid.DefaultCellStyle.SelectionBackColor =
                tealColor;

            reportsGrid.DefaultCellStyle.SelectionForeColor =
                Color.White;

            reportsGrid.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(240, 247, 250);

            reportsGrid.AlternatingRowsDefaultCellStyle.ForeColor =
                navyColor;

            reportsGrid.AlternatingRowsDefaultCellStyle.SelectionBackColor =
                tealColor;

            reportsGrid.AlternatingRowsDefaultCellStyle.SelectionForeColor =
                Color.White;

            reportsGrid.SelectionChanged +=
                ReportsGrid_SelectionChanged;

            // REPORT DETAILS PANEL
            TableLayoutPanel detailsPanel =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 2,
                    BackColor = Color.White,
                    Padding = new Padding(10),
                    Margin = new Padding(0)
                };

            detailsPanel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 32F));

            detailsPanel.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            Label lblDetails = new Label
            {
                Text = "Report Details",
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI",
                    12F,
                    FontStyle.Bold),
                ForeColor = navyColor,
                TextAlign = ContentAlignment.MiddleLeft
            };

            txtReportDetails = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.White,
                ForeColor = navyColor,
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.None
            };

            detailsPanel.Controls.Add(
                lblDetails, 0, 0);

            detailsPanel.Controls.Add(
                txtReportDetails, 0, 1);

            // ACTION BUTTONS
            FlowLayoutPanel buttonPanel =
                new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false,
                    BackColor = lightBackground,
                    Padding = new Padding(5),
                    AutoScroll = true
                };

            btnRefresh = new Button();
            btnClose = new Button();

            StyleActionButton(
                btnRefresh,
                "Refresh Reports",
                tealColor,
                Color.White);

            StyleActionButton(
                btnClose,
                "Back to Main Menu",
                secondaryBackground,
                navyColor);

            btnRefresh.Click +=
                (sender, e) => LoadReports();

            btnClose.Click +=
                (sender, e) => Close();

            buttonPanel.Controls.Add(btnRefresh);
            buttonPanel.Controls.Add(btnClose);

            // CENTRE ACTION BUTTONS
            buttonPanel.Layout += (sender, e) =>
            {
                int totalWidth =
                    btnRefresh.Width +
                    btnClose.Width +
                    btnRefresh.Margin.Horizontal +
                    btnClose.Margin.Horizontal;

                int leftPadding = Math.Max(
                    5,
                    (buttonPanel.ClientSize.Width -
                     totalWidth) / 2);

                if (buttonPanel.Padding.Left != leftPadding)
                {
                    buttonPanel.Padding =
                        new Padding(leftPadding, 5, 5, 5);
                }
            };

            // ASSEMBLE LAYOUT
            mainLayout.Controls.Add(heading, 0, 0);
            mainLayout.Controls.Add(lblReportCount, 0, 1);
            mainLayout.Controls.Add(reportsGrid, 0, 2);
            mainLayout.Controls.Add(detailsPanel, 0, 3);
            mainLayout.Controls.Add(buttonPanel, 0, 4);

            Controls.Add(mainLayout);

            ResumeLayout(true);
        }

        // STYLE BUTTONS
        private void StyleActionButton(
            Button button,
            string buttonText,
            Color backgroundColor,
            Color foregroundColor)
        {
            button.Text = buttonText;
            button.Size = new Size(175, 45);

            button.Dock = DockStyle.None;
            button.BackColor = backgroundColor;
            button.ForeColor = foregroundColor;

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            button.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold);

            button.TextAlign =
                ContentAlignment.MiddleCenter;

            button.Cursor = Cursors.Hand;
            button.Margin = new Padding(8);

            button.UseVisualStyleBackColor = false;
        }

        // LOAD REPORTS FROM REPOSITORY
        private void LoadReports()
        {
            // Clear the previous selection before rebinding.
            reportsGrid.DataSource = null;
            txtReportDetails.Clear();

            var reports = ReportRepository.Reports
                .OrderByDescending(r => r.DateSubmitted)
                .Select(r => new
                {
                    r.ReportId,
                    r.Location,
                    r.Category,
                    r.Description,
                    r.DateSubmitted,
                    r.Status,
                    r.AttachmentPath
                })
                .ToList();

            reportsGrid.DataSource = reports;

            lblReportCount.Text =
                "Total Submitted Reports: " + reports.Count;

            if (reportsGrid.Columns.Count > 0)
            {
                reportsGrid.Columns["ReportId"].HeaderText =
                    "Report ID";

                reportsGrid.Columns["DateSubmitted"].HeaderText =
                    "Date Submitted";

                reportsGrid.Columns["AttachmentPath"].HeaderText =
                    "Attachment";

                reportsGrid.Columns["DateSubmitted"]
                    .DefaultCellStyle.Format =
                    "dd MMM yyyy HH:mm";

                // ADJUST COLUMN WIDTHS
                reportsGrid.Columns["ReportId"].FillWeight = 65;
                reportsGrid.Columns["Location"].FillWeight = 120;
                reportsGrid.Columns["Category"].FillWeight = 130;
                reportsGrid.Columns["Description"].FillWeight = 180;
                reportsGrid.Columns["DateSubmitted"].FillWeight = 130;
                reportsGrid.Columns["Status"].FillWeight = 90;
                reportsGrid.Columns["AttachmentPath"].FillWeight = 110;
            }

            // SHOW FIRST REPORT AUTOMATICALLY
            if (reports.Count > 0)
            {
                reportsGrid.ClearSelection();

                reportsGrid.Rows[0].Selected = true;

                ShowReportDetails(
                    Convert.ToInt32(
                        reportsGrid.Rows[0]
                            .Cells["ReportId"].Value));
            }
            else
            {
                txtReportDetails.Text =
                    "No reports have been submitted yet.";
            }
        }

        // HANDLE REPORT SELECTION
        private void ReportsGrid_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (reportsGrid.CurrentRow == null ||
                !reportsGrid.Columns.Contains("ReportId"))
                return;

            object reportIdValue =
                reportsGrid.CurrentRow.Cells[
                    "ReportId"].Value;

            if (reportIdValue == null)
                return;

            int reportId =
                Convert.ToInt32(reportIdValue);

            ShowReportDetails(reportId);
        }

        // DISPLAY COMPLETE REPORT DETAILS
        private void ShowReportDetails(int reportId)
        {
            IssueReport selectedReport =
                ReportRepository.Reports
                    .FirstOrDefault(r =>
                        r.ReportId == reportId);

            if (selectedReport == null)
            {
                txtReportDetails.Text =
                    "The selected report could not be found.";
                return;
            }

            string attachmentName =
                string.IsNullOrWhiteSpace(
                    selectedReport.AttachmentPath)
                ? "No attachment"
                : System.IO.Path.GetFileName(
                    selectedReport.AttachmentPath);

            txtReportDetails.Text =
                "REPORT ID: " + selectedReport.ReportId +
                Environment.NewLine +
                "LOCATION: " + selectedReport.Location +
                Environment.NewLine +
                "CATEGORY: " + selectedReport.Category +
                Environment.NewLine +
                "DATE SUBMITTED: " +
                selectedReport.DateSubmitted.ToString(
                    "dd MMM yyyy HH:mm") +
                Environment.NewLine +
                "STATUS: " + selectedReport.Status +
                Environment.NewLine +
                "ATTACHMENT: " + attachmentName +
                Environment.NewLine +
                Environment.NewLine +
                "DESCRIPTION:" +
                Environment.NewLine +
                selectedReport.Description;
        }
    }
}
