
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class MainMenuForm : Form
    {
        // APPLICATION COLOURS
        private readonly Color navyColor =
            Color.FromArgb(16, 42, 67);

        private readonly Color tealColor =
            Color.FromArgb(0, 166, 166);

        private readonly Color lightBackground =
            Color.FromArgb(244, 248, 251);

        private readonly Color disabledBackground =
            Color.FromArgb(220, 228, 234);

        private readonly Color disabledText =
            Color.FromArgb(83, 101, 117);

        public MainMenuForm()
        {
            InitializeComponent();
            ApplyModernStyle();
        }

        private void ApplyModernStyle()
        {
            SuspendLayout();

            Text = "Municipal Connect";
            StartPosition = FormStartPosition.CenterScreen;

            Size = new Size(1000, 760);
            MinimumSize = new Size(780, 650);

            BackColor = lightBackground;
            Font = new Font("Segoe UI", 10F);

            // Rebuild the visual layout while
            // reusing existing designer controls.
            Controls.Clear();

            TableLayoutPanel mainLayout =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 3,
                    BackColor = lightBackground,
                    Margin = new Padding(0),
                    Padding = new Padding(0)
                };

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 180F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 80F));

            mainLayout.Controls.Add(
                CreateHeaderPanel(), 0, 0);

            mainLayout.Controls.Add(
                CreateBodySection(), 0, 1);

            mainLayout.Controls.Add(
                CreateFooterPanel(), 0, 2);

            Controls.Add(mainLayout);

            ResumeLayout(true);
        }

        // HEADER SECTION
        private Panel CreateHeaderPanel()
        {
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = navyColor,
                Margin = new Padding(0),
                Padding = new Padding(15)
            };

            TableLayoutPanel headerLayout =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 2,
                    BackColor = navyColor,
                    Margin = new Padding(0)
                };

            headerLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 65F));

            headerLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 35F));

            lblHeading.Text = "MUNICIPAL CONNECT";
            lblHeading.Dock = DockStyle.Fill;
            lblHeading.AutoSize = false;

            lblHeading.TextAlign =
                ContentAlignment.BottomCenter;

            lblHeading.Font = new Font(
                "Segoe UI", 30F, FontStyle.Bold);

            lblHeading.ForeColor = Color.White;
            lblHeading.BackColor = navyColor;
            lblHeading.Margin = new Padding(0);

            Label taglineLabel = new Label
            {
                Text = "Your city. Your voice.",
                Dock = DockStyle.Fill,
                AutoSize = false,
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font(
                    "Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(244, 162, 97),
                BackColor = navyColor,
                Margin = new Padding(0, 5, 0, 0)
            };

            headerLayout.Controls.Add(
                lblHeading, 0, 0);

            headerLayout.Controls.Add(
                taglineLabel, 0, 1);

            headerPanel.Controls.Add(headerLayout);

            return headerPanel;
        }

        // MAIN MENU BODY
        private Control CreateBodySection()
        {
            TableLayoutPanel bodyLayout =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 2,
                    BackColor = lightBackground,
                    Padding = new Padding(15)
                };

            bodyLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));

            bodyLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 75F));

            bodyLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            lblInstruction.Text =
                "How can we assist you today?";

            lblInstruction.Dock = DockStyle.Fill;
            lblInstruction.AutoSize = false;

            lblInstruction.TextAlign =
                ContentAlignment.MiddleCenter;

            lblInstruction.Font = new Font(
                "Segoe UI", 20F, FontStyle.Bold);

            lblInstruction.ForeColor = navyColor;
            lblInstruction.BackColor = lightBackground;
            lblInstruction.Margin = new Padding(0);

            // Fixed-size container keeps all buttons
            // together instead of spreading them apart.
            TableLayoutPanel buttonLayout = new TableLayoutPanel
            {
                Dock = DockStyle.None,

                 // Move buttons upward, but keep them centred horizontally
                Anchor = AnchorStyles.Top,

                Size = new Size(390, 272),
                ColumnCount = 1,
                RowCount = 4,
                BackColor = lightBackground,

                // Small top margin so it sits just below the heading
                Margin = new Padding(0, 10, 0, 0),

                Padding = new Padding(0)
            };

            buttonLayout.ColumnStyles.Add(
                new ColumnStyle(
                    SizeType.Percent, 100F));

            // Each row is 68 pixels:
            // 50px button + 18px available spacing.
            for (int i = 0; i < 4; i++)
            {
                buttonLayout.RowStyles.Add(
                    new RowStyle(
                        SizeType.Absolute, 68F));
            }

            // REPORT AN ISSUE
            StyleMenuButton(
                btnReportIssues,
                "Report an Issue",
                true);

            // LOCAL EVENTS AND ANNOUNCEMENTS
            StyleMenuButton(
                btnLocalEvents,
                "Local Events && Announcements",
                true);

            btnLocalEvents.Click -=
                BtnLocalEvents_Click;

            btnLocalEvents.Click +=
                BtnLocalEvents_Click;

            // SERVICE REQUEST STATUS
            // Disabled until Part 3.
            StyleMenuButton(
                btnServiceStatus,
                "Service Request Status",
                false);

            // VIEW SUBMITTED REPORTS
            Button btnViewReports = new Button();

            StyleMenuButton(
                btnViewReports,
                "View Submitted Reports",
                true);

            btnViewReports.Click +=
                BtnViewReports_Click;

            buttonLayout.Controls.Add(
                btnReportIssues, 0, 0);

            buttonLayout.Controls.Add(
                btnLocalEvents, 0, 1);

            buttonLayout.Controls.Add(
                btnServiceStatus, 0, 2);

            buttonLayout.Controls.Add(
                btnViewReports, 0, 3);

            bodyLayout.Controls.Add(
                lblInstruction, 0, 0);

            bodyLayout.Controls.Add(
                buttonLayout, 0, 1);

            return bodyLayout;
        }

        // CONSISTENT BUTTON STYLING
        private void StyleMenuButton(
            Button button,
            string buttonText,
            bool isEnabled)
        {
            button.Text = buttonText;

            button.Dock = DockStyle.None;
            button.Anchor = AnchorStyles.None;

            // All four buttons have equal dimensions.
            button.Size = new Size(340, 50);

            button.Enabled = isEnabled;

            // All active buttons use teal.
            button.BackColor = isEnabled
                ? tealColor
                : disabledBackground;

            button.ForeColor = isEnabled
                ? Color.White
                : disabledText;

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            button.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold);

            button.TextAlign =
                ContentAlignment.MiddleCenter;

            button.Cursor = isEnabled
                ? Cursors.Hand
                : Cursors.Default;

            // A small uniform margin.
            button.Margin = new Padding(6);

            button.UseVisualStyleBackColor = false;
        }

        // FOOTER SECTION
        private Panel CreateFooterPanel()
        {
            Panel footerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = navyColor,
                Margin = new Padding(0),
                Padding = new Padding(10)
            };

            TableLayoutPanel footerLayout =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 2,
                    BackColor = navyColor
                };

            footerLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F));

            footerLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F));

            Label footerHeading = new Label
            {
                Text =
                    "Stronger communities start with conversations.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font(
                    "Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = navyColor
            };

            Label footerMessage = new Label
            {
                Text =
                    "Thank you for helping us build a better city.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font(
                    "Segoe UI", 10F, FontStyle.Regular),
                ForeColor = tealColor,
                BackColor = navyColor
            };

            footerLayout.Controls.Add(
                footerHeading, 0, 0);

            footerLayout.Controls.Add(
                footerMessage, 0, 1);

            footerPanel.Controls.Add(footerLayout);

            return footerPanel;
        }

        // REPORT ISSUES NAVIGATION
        private void btnReportIssues_Click(
            object sender,
            EventArgs e)
        {
            using (ReportIssueForm reportForm =
                new ReportIssueForm())
            {
                Hide();

                try
                {
                    reportForm.ShowDialog();
                }
                finally
                {
                    Show();
                }
            }
        }

        // LOCAL EVENTS NAVIGATION
        private void BtnLocalEvents_Click(
            object sender,
            EventArgs e)
        {
            using (LocalEventsForm eventsForm =
                new LocalEventsForm())
            {
                Hide();

                try
                {
                    eventsForm.ShowDialog();
                }
                finally
                {
                    Show();
                }
            }
        }

        // VIEW SUBMITTED REPORTS NAVIGATION
        private void BtnViewReports_Click(
            object sender,
            EventArgs e)
        {
            using (ViewReportsForm reportsForm =
                new ViewReportsForm())
            {
                Hide();

                try
                {
                    reportsForm.ShowDialog();
                }
                finally
                {
                    Show();
                }
            }
        }
    }
}
