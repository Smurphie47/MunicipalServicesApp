
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class LocalEventsForm : Form
    {
        // INTERFACE CONTROLS
        private DataGridView eventsGrid;
        private TextBox txtDescription;
        private TextBox txtSearch;
        private ComboBox cmbCategory;
        private DateTimePicker dtpEventDate;
        private CheckBox chkFilterDate;
        private Label lblEventCount;
        private Label lblSearchHistory;
        private Label lblRecommendationCount;
        private ListBox lstRecommendations;

        // ANNOUNCEMENT CONTROLS
        private Label lblAnnouncement;
        private Label lblAnnouncementCount;
        private Button btnNextAnnouncement;

        // ADVANCED DATA STRUCTURES
        private static readonly Stack<string> searchHistory =
            new Stack<string>();

        private static readonly Dictionary<string, int>
            categorySearchCount =
            new Dictionary<string, int>(
                StringComparer.OrdinalIgnoreCase);

        // APPLICATION COLOURS
        private readonly Color navyColor =
            Color.FromArgb(16, 42, 67);

        private readonly Color tealColor =
            Color.FromArgb(0, 166, 166);

        private readonly Color lightBackground =
            Color.FromArgb(244, 248, 251);

        private readonly Color secondaryBackground =
            Color.FromArgb(230, 238, 245);

        public LocalEventsForm()
        {
            InitializeComponent();

            CreateInterface();
            LoadCategories();
            LoadEvents();
            UpdateSearchHistory();
            UpdateRecommendations();
            LoadAnnouncementPreview();
        }

        // MAIN INTERFACE
        private void CreateInterface()
        {
            SuspendLayout();

            Text = "Municipal Connect - Local Events";
            StartPosition = FormStartPosition.CenterScreen;

            Size = new Size(1250, 900);
            MinimumSize = new Size(1000, 740);

            BackColor = lightBackground;
            Font = new Font("Segoe UI", 10F);

            Controls.Clear();

            TableLayoutPanel mainLayout =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 7,
                    Padding = new Padding(15),
                    BackColor = lightBackground
                };

            mainLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 80F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 105F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 40F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 155F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 120F));

            mainLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65F));

            mainLayout.Controls.Add(
                CreateHeader(), 0, 0);

            mainLayout.Controls.Add(
                CreateSearchPanel(), 0, 1);

            lblEventCount = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font(
                    "Segoe UI", 11F, FontStyle.Bold),
                ForeColor = navyColor,
                Padding = new Padding(5, 0, 0, 0)
            };

            mainLayout.Controls.Add(
                lblEventCount, 0, 2);

            mainLayout.Controls.Add(
                CreateContentPanel(), 0, 3);

            mainLayout.Controls.Add(
                CreateDetailsPanel(), 0, 4);

            mainLayout.Controls.Add(
                CreateAnnouncementPanel(), 0, 5);

            mainLayout.Controls.Add(
                CreateBottomPanel(), 0, 6);

            Controls.Add(mainLayout);

            ResumeLayout(true);
        }

        // NAVY HEADER
        private Control CreateHeader()
        {
            Label heading = new Label
            {
                Text = "Local Events & Announcements",
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI", 23F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = navyColor,
                ForeColor = Color.White,
                Margin = new Padding(0)
            };

            return heading;
        }

        // SEARCH AND FILTER PANEL
        private Control CreateSearchPanel()
        {
            TableLayoutPanel searchPanel =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 4,
                    RowCount = 2,
                    Padding = new Padding(12),
                    BackColor = Color.White,
                    Margin = new Padding(0, 10, 0, 5)
                };

            searchPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 30F));

            searchPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 24F));

            searchPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 19F));

            searchPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 27F));

            searchPanel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 25F));

            searchPanel.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            // SEARCH INPUT
            Label lblSearch = CreateLabel("Search Events");

            txtSearch = new TextBox
            {
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 11F),
                Margin = new Padding(5, 6, 8, 0)
            };

            txtSearch.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SearchEvents();
                }
            };

            // CATEGORY FILTER
            Label lblCategory =
                CreateLabel("Select Category");

            cmbCategory = new ComboBox
            {
                Dock = DockStyle.Top,
                DropDownStyle =
                    ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 11F),
                Margin = new Padding(5, 6, 8, 0)
            };

            // DATE FILTER
            chkFilterDate = new CheckBox
            {
                Text = "Filter by Date",
                Dock = DockStyle.Fill,
                Checked = false,
                Font = new Font("Segoe UI", 10F),
                ForeColor = navyColor
            };

            dtpEventDate = new DateTimePicker
            {
                Dock = DockStyle.Top,
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today,
                Enabled = false,
                Margin = new Padding(5, 6, 8, 0)
            };

            chkFilterDate.CheckedChanged +=
                (sender, e) =>
                {
                    dtpEventDate.Enabled =
                        chkFilterDate.Checked;
                };

            // SEARCH BUTTONS
            TableLayoutPanel searchButtons =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 1,
                    Margin = new Padding(3, 3, 0, 0)
                };

            searchButtons.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50F));

            searchButtons.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50F));

            Button btnSearch = new Button();

            StyleButton(
                btnSearch,
                "Search",
                tealColor,
                Color.White);

            btnSearch.Dock = DockStyle.Fill;

            btnSearch.Click += (sender, e) =>
                SearchEvents();

            Button btnClear = new Button();

            StyleButton(
                btnClear,
                "Clear Filters",
                secondaryBackground,
                navyColor);

            btnClear.Dock = DockStyle.Fill;

            btnClear.Click += (sender, e) =>
                ClearFilters();

            searchButtons.Controls.Add(
                btnSearch, 0, 0);

            searchButtons.Controls.Add(
                btnClear, 1, 0);

            searchPanel.Controls.Add(lblSearch, 0, 0);
            searchPanel.Controls.Add(txtSearch, 0, 1);

            searchPanel.Controls.Add(lblCategory, 1, 0);
            searchPanel.Controls.Add(cmbCategory, 1, 1);

            searchPanel.Controls.Add(chkFilterDate, 2, 0);
            searchPanel.Controls.Add(dtpEventDate, 2, 1);

            searchPanel.Controls.Add(
                searchButtons, 3, 1);

            return searchPanel;
        }

        // STANDARD FORM LABEL
        private Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font(
                    "Segoe UI", 10F, FontStyle.Bold),
                ForeColor = navyColor,
                Margin = new Padding(5, 0, 0, 0)
            };
        }

        // EVENTS TABLE AND RECOMMENDATIONS
        private Control CreateContentPanel()
        {
            TableLayoutPanel contentPanel =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 1,
                    Margin = new Padding(0, 0, 0, 10)
                };

            contentPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 65F));

            contentPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 35F));

            // EVENTS TABLE
            eventsGrid = new DataGridView
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
                    DataGridViewColumnHeadersHeightSizeMode
                        .DisableResizing,

                GridColor =
                    Color.FromArgb(225, 232, 238),

                Margin = new Padding(0, 0, 12, 0)
            };

            eventsGrid.RowTemplate.Height = 36;

            // NAVY TABLE HEADERS
            eventsGrid.ColumnHeadersDefaultCellStyle.BackColor =
                navyColor;

            eventsGrid.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            eventsGrid.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI", 10F, FontStyle.Bold);

            // TABLE ROWS
            eventsGrid.DefaultCellStyle.BackColor =
                Color.White;

            eventsGrid.DefaultCellStyle.ForeColor =
                navyColor;

            eventsGrid.DefaultCellStyle.Font =
                new Font("Segoe UI", 10F);

            eventsGrid.DefaultCellStyle.SelectionBackColor =
                tealColor;

            eventsGrid.DefaultCellStyle.SelectionForeColor =
                Color.White;

            eventsGrid.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(240, 247, 250);

            eventsGrid.AlternatingRowsDefaultCellStyle.ForeColor =
                navyColor;

            eventsGrid.AlternatingRowsDefaultCellStyle
                .SelectionBackColor = tealColor;

            eventsGrid.AlternatingRowsDefaultCellStyle
                .SelectionForeColor = Color.White;

            eventsGrid.SelectionChanged +=
                EventsGrid_SelectionChanged;

            // RECOMMENDATIONS PANEL
            TableLayoutPanel recommendationsPanel =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 3,
                    Padding = new Padding(15),
                    BackColor = Color.White,
                    Margin = new Padding(0)
                };

            recommendationsPanel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 42F));

            recommendationsPanel.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            recommendationsPanel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 40F));

            Label lblRecommendations = new Label
            {
                Text = "Recommended Events",
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI", 14F, FontStyle.Bold),
                ForeColor = navyColor,
                TextAlign = ContentAlignment.MiddleLeft
            };

            lstRecommendations = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11F),
                DisplayMember = "Title",
                HorizontalScrollbar = true,
                IntegralHeight = false,
                BorderStyle = BorderStyle.FixedSingle,
                ItemHeight = 24
            };

            lstRecommendations.SelectedIndexChanged +=
                Recommendations_SelectedIndexChanged;

            lblRecommendationCount = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.DimGray,
                TextAlign = ContentAlignment.MiddleLeft
            };

            recommendationsPanel.Controls.Add(
                lblRecommendations, 0, 0);

            recommendationsPanel.Controls.Add(
                lstRecommendations, 0, 1);

            recommendationsPanel.Controls.Add(
                lblRecommendationCount, 0, 2);

            contentPanel.Controls.Add(eventsGrid, 0, 0);

            contentPanel.Controls.Add(
                recommendationsPanel, 1, 0);

            return contentPanel;
        }

        // EVENT DETAILS SECTION
        private Control CreateDetailsPanel()
        {
            TableLayoutPanel detailsPanel =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 2,
                    BackColor = Color.White,
                    Padding = new Padding(12),
                    Margin = new Padding(0, 0, 0, 10)
                };

            detailsPanel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 30F));

            detailsPanel.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            Label lblDetails = new Label
            {
                Text = "Event Details",
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI", 12F, FontStyle.Bold),
                ForeColor = navyColor,
                TextAlign = ContentAlignment.MiddleLeft
            };

            txtDescription = new TextBox
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

            detailsPanel.Controls.Add(lblDetails, 0, 0);
            detailsPanel.Controls.Add(txtDescription, 0, 1);

            return detailsPanel;
        }

        // ANNOUNCEMENT QUEUE SECTION
        private Control CreateAnnouncementPanel()
        {
            TableLayoutPanel announcementPanel =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 2,
                    Padding = new Padding(12),
                    BackColor =
                        Color.FromArgb(235, 245, 249),
                    Margin = new Padding(0, 0, 0, 10)
                };

            announcementPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 72F));

            announcementPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 28F));

            announcementPanel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 35F));

            announcementPanel.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            Label lblAnnouncementHeading = new Label
            {
                Text = "Municipal Announcement Queue",
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI", 12F, FontStyle.Bold),
                ForeColor = navyColor,
                TextAlign = ContentAlignment.MiddleLeft
            };

            lblAnnouncement = new Label
            {
                Text = "No announcements available.",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                ForeColor = navyColor,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };

            lblAnnouncementCount = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI", 10F, FontStyle.Bold),
                ForeColor = navyColor,
                TextAlign = ContentAlignment.MiddleCenter
            };

            btnNextAnnouncement = new Button();

            StyleButton(
                btnNextAnnouncement,
                "Next Announcement",
                tealColor,
                Color.White);

            btnNextAnnouncement.Dock = DockStyle.Fill;
            btnNextAnnouncement.Margin = new Padding(5);

            btnNextAnnouncement.Click +=
                BtnNextAnnouncement_Click;

            announcementPanel.Controls.Add(
                lblAnnouncementHeading, 0, 0);

            announcementPanel.Controls.Add(
                lblAnnouncement, 0, 1);

            announcementPanel.Controls.Add(
                lblAnnouncementCount, 1, 0);

            announcementPanel.Controls.Add(
                btnNextAnnouncement, 1, 1);

            return announcementPanel;
        }

        // BOTTOM NAVIGATION
        private Control CreateBottomPanel()
        {
            TableLayoutPanel bottomPanel =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 1,
                    Margin = new Padding(0)
                };

            bottomPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 70F));

            bottomPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 30F));

            lblSearchHistory = new Label
            {
                Text = "Recent Search: None",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                ForeColor = navyColor,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };

            Button btnBack = new Button();

            StyleButton(
                btnBack,
                "Back to Main Menu",
                secondaryBackground,
                navyColor);

            btnBack.Dock = DockStyle.None;
            btnBack.Size = new Size(185, 43);
            btnBack.Anchor = AnchorStyles.None;

            btnBack.Click += (sender, e) => Close();

            bottomPanel.Controls.Add(
                lblSearchHistory, 0, 0);

            bottomPanel.Controls.Add(btnBack, 1, 0);

            return bottomPanel;
        }

        // STANDARD BUTTON STYLING
        private void StyleButton(
            Button button,
            string buttonText,
            Color backgroundColor,
            Color foregroundColor)
        {
            button.Text = buttonText;
            button.BackColor = backgroundColor;
            button.ForeColor = foregroundColor;

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            button.Font = new Font(
                "Segoe UI", 10F, FontStyle.Bold);

            button.TextAlign =
                ContentAlignment.MiddleCenter;

            button.Cursor = Cursors.Hand;
            button.Margin = new Padding(5);

            button.UseVisualStyleBackColor = false;
        }

        // LOAD UNIQUE CATEGORIES FROM HASHSET
        private void LoadCategories()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All Categories");

            foreach (string category in
                EventRepository.Categories.OrderBy(c => c))
            {
                cmbCategory.Items.Add(category);
            }

            cmbCategory.SelectedIndex = 0;
        }

        // LOAD ALL EVENTS
        private void LoadEvents()
        {
            DisplayEvents(EventRepository.GetAllEvents());
        }

        // DISPLAY EVENTS IN THE TABLE
        private void DisplayEvents(
            List<MunicipalEvent> events)
        {
            eventsGrid.DataSource = null;

            var displayData = events.Select(e => new
            {
                e.EventId,
                e.Title,
                e.Category,
                e.Location,
                e.EventDate,
                e.EventType
            }).ToList();

            eventsGrid.DataSource = displayData;

            lblEventCount.Text =
                "Events and Announcements Found: " +
                events.Count;

            if (eventsGrid.Columns.Count > 0)
            {
                eventsGrid.Columns["EventId"].HeaderText =
                    "Event ID";

                eventsGrid.Columns["EventDate"].HeaderText =
                    "Date";

                eventsGrid.Columns["EventType"].HeaderText =
                    "Type";

                eventsGrid.Columns["EventDate"]
                    .DefaultCellStyle.Format = "dd MMM yyyy";

                // IMPROVED COLUMN WIDTHS
                eventsGrid.Columns["EventId"].FillWeight = 55;
                eventsGrid.Columns["Title"].FillWeight = 200;
                eventsGrid.Columns["Category"].FillWeight = 120;
                eventsGrid.Columns["Location"].FillWeight = 110;
                eventsGrid.Columns["EventDate"].FillWeight = 110;
                eventsGrid.Columns["EventType"].FillWeight = 105;
            }

            if (events.Count == 0)
            {
                txtDescription.Text =
                    "No events match your search criteria.";
            }
            else
            {
                EventsGrid_SelectionChanged(
                    this, EventArgs.Empty);
            }
        }

        // SEARCH AND FILTER EVENTS
        private void SearchEvents()
        {
            string keyword = txtSearch.Text.Trim();

            string selectedCategory =
                cmbCategory.SelectedItem?.ToString()
                ?? "All Categories";

            IEnumerable<MunicipalEvent> results =
                EventRepository.GetAllEvents();

            // KEYWORD SEARCH
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                results = results.Where(e =>
                    ContainsText(e.Title, keyword) ||
                    ContainsText(e.Description, keyword) ||
                    ContainsText(e.Location, keyword) ||
                    ContainsText(e.Category, keyword));
            }

            // CATEGORY FILTER
            if (selectedCategory != "All Categories")
            {
                results = results.Where(e =>
                    string.Equals(
                        e.Category,
                        selectedCategory,
                        StringComparison.OrdinalIgnoreCase));
            }

            // DATE FILTER
            if (chkFilterDate.Checked)
            {
                DateTime selectedDate =
                    dtpEventDate.Value.Date;

                results = results.Where(e =>
                    e.EventDate.Date == selectedDate);
            }

            DisplayEvents(
                results.OrderBy(e => e.EventDate).ToList());

            RecordSearch(keyword, selectedCategory);
            UpdateRecommendations();
        }

        // CASE-INSENSITIVE TEXT MATCHING
        private bool ContainsText(
            string source, string keyword)
        {
            if (string.IsNullOrWhiteSpace(source) ||
                string.IsNullOrWhiteSpace(keyword))
            {
                return false;
            }

            return source.IndexOf(
                keyword,
                StringComparison.OrdinalIgnoreCase) >= 0;
        }

        // RECORD USER SEARCHES
        private void RecordSearch(
            string keyword,
            string selectedCategory)
        {
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                searchHistory.Push(keyword);
            }

            if (selectedCategory != "All Categories")
            {
                if (!categorySearchCount.ContainsKey(
                    selectedCategory))
                {
                    categorySearchCount[selectedCategory] = 0;
                }

                categorySearchCount[selectedCategory]++;
            }

            UpdateSearchHistory();
        }

        // DISPLAY RECENT SEARCH
        private void UpdateSearchHistory()
        {
            lblSearchHistory.Text = searchHistory.Count > 0
                ? "Recent Search: " + searchHistory.Peek()
                : "Recent Search: None";
        }

        // GENERATE EVENT RECOMMENDATIONS
        private void UpdateRecommendations()
        {
            List<MunicipalEvent> recommendations =
                RecommendationEngine.GetRecommendations(
                    EventRepository.GetAllEvents(),
                    categorySearchCount,
                    searchHistory,
                    5);

            lstRecommendations.BeginUpdate();

            try
            {
                lstRecommendations.Items.Clear();

                foreach (MunicipalEvent item in recommendations)
                {
                    lstRecommendations.Items.Add(item);
                }
            }
            finally
            {
                lstRecommendations.EndUpdate();
            }

            lblRecommendationCount.Text =
                recommendations.Count +
                " recommended event(s)";
        }

        // SELECT A RECOMMENDED EVENT
        private void Recommendations_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            MunicipalEvent selectedEvent =
                lstRecommendations.SelectedItem as MunicipalEvent;

            if (selectedEvent != null)
            {
                ShowEventDetails(selectedEvent);
            }
        }

        // SELECT AN EVENT FROM THE TABLE
        private void EventsGrid_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (eventsGrid.CurrentRow == null ||
                !eventsGrid.Columns.Contains("EventId"))
            {
                return;
            }

            object idValue =
                eventsGrid.CurrentRow.Cells["EventId"].Value;

            if (idValue == null)
                return;

            int eventId = Convert.ToInt32(idValue);

            MunicipalEvent selectedEvent;

            if (EventRepository.EventsById.TryGetValue(
                eventId, out selectedEvent))
            {
                ShowEventDetails(selectedEvent);
            }
        }

        // DISPLAY COMPLETE EVENT DETAILS
        private void ShowEventDetails(
            MunicipalEvent selectedEvent)
        {
            txtDescription.Text =
                "EVENT: " + selectedEvent.Title +
                Environment.NewLine +
                "CATEGORY: " + selectedEvent.Category +
                Environment.NewLine +
                "LOCATION: " + selectedEvent.Location +
                Environment.NewLine +
                "DATE: " +
                selectedEvent.EventDate.ToString("dd MMM yyyy") +
                Environment.NewLine +
                "TYPE: " + selectedEvent.EventType +
                Environment.NewLine +
                Environment.NewLine +
                "DESCRIPTION:" +
                Environment.NewLine +
                selectedEvent.Description;
        }

        // SHOW NEXT ANNOUNCEMENT IN QUEUE
        private void LoadAnnouncementPreview()
        {
            MunicipalEvent next =
                EventRepository.PeekNextAnnouncement();

            lblAnnouncementCount.Text =
                EventRepository.GetAnnouncementCount() +
                " in queue";

            if (next == null)
            {
                lblAnnouncement.Text =
                    "No announcements available.";

                btnNextAnnouncement.Enabled = false;
                return;
            }

            lblAnnouncement.Text =
                "Next: " + next.Title +
                " | " + next.Location +
                " | " +
                next.EventDate.ToString("dd MMM yyyy");

            btnNextAnnouncement.Enabled = true;
        }

        // PROCESS QUEUE USING FIFO
        private void BtnNextAnnouncement_Click(
            object sender,
            EventArgs e)
        {
            MunicipalEvent announcement =
                EventRepository.ProcessNextAnnouncement();

            if (announcement == null)
            {
                LoadAnnouncementPreview();
                return;
            }

            ShowEventDetails(announcement);

            MunicipalEvent next =
                EventRepository.PeekNextAnnouncement();

            lblAnnouncement.Text =
                "Now viewing: " + announcement.Title +
                Environment.NewLine +
                "Next in queue: " +
                (next != null ? next.Title : "None");

            lblAnnouncementCount.Text =
                EventRepository.GetAnnouncementCount() +
                " in queue";
        }

        // CLEAR ALL FILTERS
        private void ClearFilters()
        {
            txtSearch.Clear();
            cmbCategory.SelectedIndex = 0;

            chkFilterDate.Checked = false;
            dtpEventDate.Value = DateTime.Today;

            LoadEvents();
            UpdateRecommendations();
        }
    }
}
