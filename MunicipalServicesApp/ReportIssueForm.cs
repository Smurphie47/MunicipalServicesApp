using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class ReportIssueForm : Form
    {
        private string selectedFilePath = string.Empty;

        private readonly Color navyColor =
            Color.FromArgb(16, 42, 67);

        private readonly Color tealColor =
            Color.FromArgb(0, 166, 166);

        private readonly Color lightBackground =
            Color.FromArgb(244, 248, 251);

        public ReportIssueForm()
        {
            InitializeComponent();

            txtLocation.TextChanged += InputFieldsChanged;
            cmbCategory.SelectedIndexChanged += InputFieldsChanged;
            rtbDescription.TextChanged += InputFieldsChanged;

            ApplyModernStyle();
            UpdateProgress();
        }

        private void ApplyModernStyle()
        {
            SuspendLayout();

            Text = "Municipal Connect - Report an Issue";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1050, 800);
            MinimumSize = new Size(850, 700);
            BackColor = lightBackground;
            Font = new Font("Segoe UI", 10F);
            AutoScroll = true;

            Controls.Clear();

            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = lightBackground,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 155F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            mainLayout.Controls.Add(CreateHeader(), 0, 0);
            mainLayout.Controls.Add(CreateFormSection(), 0, 1);

            Controls.Add(mainLayout);

            ResumeLayout(true);
        }

        private Panel CreateHeader()
        {
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = navyColor,
                Margin = new Padding(0),
                Padding = new Padding(30)
            };

            TableLayoutPanel headerLayout =
                new TableLayoutPanel
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

            Label titleLabel = new Label
            {
                Text = "Report a Municipal Issue",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font(
                    "Segoe UI",
                    26F,
                    FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = navyColor
            };

            Label subtitleLabel = new Label
            {
                Text = "Help us improve your community",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font(
                    "Segoe UI",
                    13F,
                    FontStyle.Bold),
                ForeColor = tealColor,
                BackColor = navyColor,
                Margin = new Padding(0, 5, 0, 0)
            };

            headerLayout.Controls.Add(titleLabel, 0, 0);
            headerLayout.Controls.Add(subtitleLabel, 0, 1);

            headerPanel.Controls.Add(headerLayout);

            return headerPanel;
        }

        private Control CreateFormSection()
        {
            TableLayoutPanel outerLayout =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 3,
                    RowCount = 1,
                    BackColor = lightBackground,
                    Padding = new Padding(15)
                };

            outerLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 8F));

            outerLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 84F));

            outerLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 8F));

            TableLayoutPanel formLayout =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 6,
                    BackColor = Color.White,
                    Padding = new Padding(25),
                    Margin = new Padding(0)
                };

            formLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 27F));

            formLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 73F));

            formLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55F));

            formLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55F));

            formLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 175F));

            formLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65F));

            formLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 90F));

            formLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            PrepareInputControls();

            formLayout.Controls.Add(
                CreateFieldLabel("Issue Location *"), 0, 0);

            formLayout.Controls.Add(txtLocation, 1, 0);

            formLayout.Controls.Add(
                CreateFieldLabel("Issue Category *"), 0, 1);

            formLayout.Controls.Add(cmbCategory, 1, 1);

            formLayout.Controls.Add(
                CreateFieldLabel("Issue Description *"), 0, 2);

            formLayout.Controls.Add(rtbDescription, 1, 2);

            formLayout.Controls.Add(
                CreateFieldLabel(
                    "Supporting evidence\n(Optional)"),
                0,
                3);

            formLayout.Controls.Add(
                CreateAttachmentSection(),
                1,
                3);

            Control progressSection = CreateProgressSection();

            formLayout.Controls.Add(progressSection, 0, 4);
            formLayout.SetColumnSpan(progressSection, 2);

            Control actionSection = CreateActionSection();

            formLayout.Controls.Add(actionSection, 0, 5);
            formLayout.SetColumnSpan(actionSection, 2);

            outerLayout.Controls.Add(formLayout, 1, 0);

            return outerLayout;
        }

        private void PrepareInputControls()
        {
            txtLocation.Dock = DockStyle.Fill;
            txtLocation.Font =
                new Font("Segoe UI", 11F);
            txtLocation.Margin =
                new Padding(8, 8, 8, 8);

            cmbCategory.Dock = DockStyle.Fill;
            cmbCategory.Font =
                new Font("Segoe UI", 11F);
            cmbCategory.Margin =
                new Padding(8, 8, 8, 8);
            cmbCategory.DropDownStyle =
                ComboBoxStyle.DropDownList;

            rtbDescription.Dock = DockStyle.Fill;
            rtbDescription.Font =
                new Font("Segoe UI", 11F);
            rtbDescription.Margin =
                new Padding(8);
            rtbDescription.BorderStyle =
                BorderStyle.FixedSingle;
        }

        private Label CreateFieldLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold),
                ForeColor = navyColor,
                BackColor = Color.White,
                Margin = new Padding(5)
            };
        }

        private Control CreateAttachmentSection()
        {
            TableLayoutPanel attachmentLayout =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 1,
                    BackColor = Color.White,
                    Margin = new Padding(0)
                };

            attachmentLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 70F));

            attachmentLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 30F));

            lblSelectedFile.Dock = DockStyle.Fill;
            lblSelectedFile.AutoSize = false;
            lblSelectedFile.AutoEllipsis = true;
            lblSelectedFile.TextAlign =
                ContentAlignment.MiddleLeft;
            lblSelectedFile.Font =
                new Font("Segoe UI", 10F);
            lblSelectedFile.ForeColor =
                Color.DimGray;
            lblSelectedFile.Margin =
                new Padding(8);

            btnAttachFile.Text = "Attach File";
            btnAttachFile.Dock = DockStyle.Fill;
            btnAttachFile.BackColor =
                Color.FromArgb(230, 238, 245);
            btnAttachFile.ForeColor = navyColor;
            btnAttachFile.FlatStyle =
                FlatStyle.Flat;
            btnAttachFile.FlatAppearance.BorderColor =
                navyColor;
            btnAttachFile.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold);
            btnAttachFile.Cursor = Cursors.Hand;
            btnAttachFile.Margin =
                new Padding(8);

            attachmentLayout.Controls.Add(
                lblSelectedFile, 0, 0);

            attachmentLayout.Controls.Add(
                btnAttachFile, 1, 0);

            return attachmentLayout;
        }

        private Control CreateProgressSection()
        {
            TableLayoutPanel progressLayout =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 2,
                    BackColor = Color.White,
                    Padding = new Padding(5)
                };

            progressLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F));

            progressLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F));

            lblProgressMessage.Dock = DockStyle.Fill;
            lblProgressMessage.AutoSize = false;
            lblProgressMessage.TextAlign =
                ContentAlignment.BottomCenter;
            lblProgressMessage.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold);
            lblProgressMessage.ForeColor =
                navyColor;
            lblProgressMessage.Margin =
                new Padding(5);

            prgReportProgress.Dock = DockStyle.Fill;
            prgReportProgress.Minimum = 0;
            prgReportProgress.Maximum = 100;
            prgReportProgress.Margin =
                new Padding(40, 8, 40, 8);

            progressLayout.Controls.Add(
                lblProgressMessage, 0, 0);

            progressLayout.Controls.Add(
                prgReportProgress, 0, 1);

            return progressLayout;
        }

        private Control CreateActionSection()
        {
            TableLayoutPanel actionLayout =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 3,
                    RowCount = 1,
                    BackColor = Color.White,
                    Padding = new Padding(5)
                };

            actionLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));

            actionLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 210F));

            actionLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 210F));

            StyleActionButton(
                btnBack,
                "Back to Main Menu",
                Color.FromArgb(230, 238, 245),
                navyColor);

            StyleActionButton(
                btnSubmit,
                "Submit Report",
                tealColor,
                Color.White);

            actionLayout.Controls.Add(btnBack, 1, 0);
            actionLayout.Controls.Add(btnSubmit, 2, 0);

            return actionLayout;
        }

        private void StyleActionButton(
            Button button,
            string text,
            Color backgroundColor,
            Color foregroundColor)
        {
            button.Text = text;
            button.Dock = DockStyle.Fill;
            button.BackColor = backgroundColor;
            button.ForeColor = foregroundColor;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.Margin = new Padding(8);
            button.UseVisualStyleBackColor = false;
        }

        private void InputFieldsChanged(
            object sender,
            EventArgs e)
        {
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            int progress = 0;

            if (!string.IsNullOrWhiteSpace(
                txtLocation.Text))
            {
                progress += 34;
            }

            if (cmbCategory.SelectedIndex != -1)
            {
                progress += 33;
            }

            if (!string.IsNullOrWhiteSpace(
                rtbDescription.Text))
            {
                progress += 33;
            }

            prgReportProgress.Value = progress;

            if (progress == 0)
            {
                lblProgressMessage.Text =
                    "Start by entering the issue location.";
            }
            else if (progress < 67)
            {
                lblProgressMessage.Text =
                    "Good start. Complete the remaining details.";
            }
            else if (progress < 100)
            {
                lblProgressMessage.Text =
                    "Almost finished. Complete the final field.";
            }
            else
            {
                lblProgressMessage.Text =
                    "Your report is ready to submit.";
            }
        }

        private void btnAttachFile_Click(
            object sender,
            EventArgs e)
        {
            using (OpenFileDialog openFileDialog =
                new OpenFileDialog())
            {
                openFileDialog.Title =
                    "Select supporting evidence";

                openFileDialog.Filter =
                    "Supported files|" +
                    "*.jpg;*.jpeg;*.png;*.gif;" +
                    "*.pdf;*.doc;*.docx|" +
                    "Image files|" +
                    "*.jpg;*.jpeg;*.png;*.gif|" +
                    "PDF documents|*.pdf|" +
                    "Word documents|*.doc;*.docx";

                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() ==
                    DialogResult.OK)
                {
                    selectedFilePath =
                        openFileDialog.FileName;

                    lblSelectedFile.Text =
                        Path.GetFileName(
                            selectedFilePath);
                }
            }
        }

        private void btnSubmit_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtLocation.Text))
            {
                MessageBox.Show(
                    "Please enter the location of the issue.",
                    "Missing Location",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtLocation.Focus();
                return;
            }

            if (cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select an issue category.",
                    "Missing Category",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbCategory.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(
                rtbDescription.Text))
            {
                MessageBox.Show(
                    "Please provide a description of the issue.",
                    "Missing Description",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                rtbDescription.Focus();
                return;
            }

            IssueReport newReport = new IssueReport
            {
                ReportId =
                    ReportRepository.GetNextReportId(),

                Location =
                    txtLocation.Text.Trim(),

                Category =
                    cmbCategory.SelectedItem.ToString(),

                Description =
                    rtbDescription.Text.Trim(),

                AttachmentPath =
                    selectedFilePath,

                DateSubmitted =
                    DateTime.Now,

                Status =
                    "Submitted"
            };

            ReportRepository.AddReport(newReport);

            MessageBox.Show(
                "Your issue was submitted successfully.\n\n" +
                "Report number: " +
                newReport.ReportId +
                "\nStatus: " +
                newReport.Status,
                "Report Submitted",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ClearForm();
        }

        private void ClearForm()
        {
            txtLocation.Clear();
            cmbCategory.SelectedIndex = -1;
            rtbDescription.Clear();

            selectedFilePath = string.Empty;
            lblSelectedFile.Text = "No file selected";

            txtLocation.Focus();
            UpdateProgress();
        }

        private void btnBack_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }

        private void label1_Click(
            object sender,
            EventArgs e)
        {
            // Existing Designer event.
        }

        private void lblCategory_Click(
            object sender,
            EventArgs e)
        {
            // Existing Designer event.
        }

        private void lblDescription_Click(
            object sender,
            EventArgs e)
        {
            // Existing Designer event.
        }
    }
}