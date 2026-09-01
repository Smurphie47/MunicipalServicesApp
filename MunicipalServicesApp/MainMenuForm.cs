using System;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class MainMenuForm : Form
    {
        private readonly Color navyColor =
            Color.FromArgb(16, 42, 67);

        private readonly Color tealColor =
            Color.FromArgb(0, 166, 166);

        private readonly Color blueColor =
            Color.FromArgb(22, 119, 200);

        private readonly Color lightBackground =
            Color.FromArgb(244, 248, 251);

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

            Controls.Clear();

            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = lightBackground,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 220F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 100F));

            Panel headerPanel = CreateHeaderPanel();
            Control bodySection = CreateBodySection();
            Panel footerPanel = CreateFooterPanel();

            mainLayout.Controls.Add(headerPanel, 0, 0);
            mainLayout.Controls.Add(bodySection, 0, 1);
            mainLayout.Controls.Add(footerPanel, 0, 2);

            Controls.Add(mainLayout);

            ResumeLayout(true);
        }

        private Panel CreateHeaderPanel()
        {
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = navyColor,
                Margin = new Padding(0),
                Padding = new Padding(20)
            };

            TableLayoutPanel headerLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = navyColor
            };

            headerLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 65F));

            headerLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 35F));

            lblHeading.Text = "MUNICIPAL CONNECT";
            lblHeading.Dock = DockStyle.Fill;
            lblHeading.AutoSize = false;
            lblHeading.TextAlign = ContentAlignment.BottomCenter;
            lblHeading.Font =
                new Font("Segoe UI", 34F, FontStyle.Bold);
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
                    "Segoe UI",
                    18F,
                    FontStyle.Bold),
                ForeColor = Color.FromArgb(244, 162, 97),
                BackColor = navyColor,
                Margin = new Padding(0, 8, 0, 0)
            };

            headerLayout.Controls.Add(lblHeading, 0, 0);
            headerLayout.Controls.Add(taglineLabel, 0, 1);

            headerPanel.Controls.Add(headerLayout);

            return headerPanel;
        }

        private Control CreateBodySection()
        {
            TableLayoutPanel bodyLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 2,
                BackColor = lightBackground,
                Padding = new Padding(20)
            };

            bodyLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 15F));

            bodyLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 70F));

            bodyLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 15F));

            bodyLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 90F));

            bodyLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            lblInstruction.Text =
                "How can we assist you today?";

            lblInstruction.Dock = DockStyle.Fill;
            lblInstruction.AutoSize = false;
            lblInstruction.TextAlign =
                ContentAlignment.MiddleCenter;

            lblInstruction.Font =
                new Font("Segoe UI", 24F, FontStyle.Bold);

            lblInstruction.ForeColor = navyColor;
            lblInstruction.BackColor = lightBackground;
            lblInstruction.Margin = new Padding(0);

            TableLayoutPanel buttonLayout =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 3,
                    BackColor = lightBackground,
                    Padding = new Padding(15, 5, 15, 15)
                };

            buttonLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 33.33F));

            buttonLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 33.33F));

            buttonLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 33.34F));

            StyleMenuButton(
                btnReportIssues,
                "Report an Issue",
                tealColor,
                true);

            StyleMenuButton(
                btnLocalEvents,
                "Local Events && Announcements",
                blueColor,
                false);

            StyleMenuButton(
                btnServiceStatus,
                "Service Request Status",
                blueColor,
                false);

            buttonLayout.Controls.Add(
                btnReportIssues, 0, 0);

            buttonLayout.Controls.Add(
                btnLocalEvents, 0, 1);

            buttonLayout.Controls.Add(
                btnServiceStatus, 0, 2);

            bodyLayout.Controls.Add(
                lblInstruction, 1, 0);

            bodyLayout.Controls.Add(
                buttonLayout, 1, 1);

            return bodyLayout;
        }

        private void StyleMenuButton(
            Button button,
            string buttonText,
            Color backgroundColor,
            bool isEnabled)
        {
            button.Text = buttonText;
            button.Dock = DockStyle.Fill;
            button.Enabled = isEnabled;
            button.BackColor = backgroundColor;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            button.Font = new Font(
                "Segoe UI",
                16F,
                FontStyle.Bold);

            button.Cursor = isEnabled
                ? Cursors.Hand
                : Cursors.Default;

            button.Margin = new Padding(10);
            button.UseVisualStyleBackColor = false;
        }

        private Panel CreateFooterPanel()
        {
            Panel footerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = navyColor,
                Margin = new Padding(0),
                Padding = new Padding(20)
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
                    "Segoe UI",
                    11F,
                    FontStyle.Regular),
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
                    "Segoe UI",
                    11F,
                    FontStyle.Regular),
                ForeColor = tealColor,
                BackColor = navyColor
            };

            footerLayout.Controls.Add(footerHeading, 0, 0);
            footerLayout.Controls.Add(footerMessage, 0, 1);

            footerPanel.Controls.Add(footerLayout);

            return footerPanel;
        }

        private void btnReportIssues_Click(
            object sender,
            EventArgs e)
        {
            ReportIssueForm reportForm =
                new ReportIssueForm();

            Hide();
            reportForm.ShowDialog();
            Show();
        }
    }
}