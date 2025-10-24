using Browser.Core.Interfaces;
using Browser.Core.Models;

namespace Browser.UI.Forms
{
    public partial class MainForm : Form
    {
        private readonly IBrowserService browserService;
        private readonly IHomePageManager homePageManager;
        private readonly IBookmarkManager bookmarkManager;
        private readonly IHistoryManager historyManager;
        private readonly Stack<string> backStack = new Stack<string>();
        private readonly Stack<string> forwardStack = new Stack<string>();
        private string lastLoadedUrl = "";

        public MainForm(IBrowserService browserService, IHomePageManager homePageManager, IBookmarkManager bookmarkManager, IHistoryManager historyManager)
        {
            InitializeComponent();
            this.browserService = browserService;
            this.homePageManager = homePageManager;
            this.bookmarkManager = bookmarkManager;
            this.historyManager = historyManager;
        }

        private async Task HandleNavigationRequestAsync(string inputUrl, bool isNavigatingBackOrForward = false)
        {
            // 1. URL Cleanup and UI Update for Address Bar
            string cleanUrl = CleanAndValidateUrl(inputUrl);
            string previousUrl = this.lastLoadedUrl;
            txtAddress.Text = cleanUrl; // Update the address bar with the cleaned URL

            // Only push the previous URL if the user initiated a new navigation (not back/forward)
            if (!isNavigatingBackOrForward && !string.IsNullOrEmpty(previousUrl) && previousUrl != cleanUrl)
            {
                // Push the page we are leaving onto the back stack
                backStack.Push(previousUrl);

                // Clear the forward stack (a new navigation path always invalidates forward history)
                forwardStack.Clear();
            }

            // 2. Set UI State to Loading
            btnSendHTTPRequest.Enabled = false;
            btnReload.Enabled = false;
            listLinks.Items.Clear();
            HTMLViewer.Text = "loading...";
            this.Text = "Loading...";

            try
            {
                // 3. Delegate to Browser Engine
                PageContent page = await browserService.LoadPageAsync(cleanUrl);

                // 4. Display Results and Persist URL
                DisplayPageContent(page);

                await historyManager.AddHistoryEntryAsync(cleanUrl, page.title);

                await RefreshHistoryList();

            }
            catch (Exception ex)
            {
                // 5. Handle Exceptions (Network, Parsing, etc.)
                HTMLViewer.Text = $"Fatal Error: Could not complete request to {cleanUrl}.\n\nDetails: {ex.Message}";
                this.Text = "Error";
            }
            finally
            {
                lastLoadedUrl = cleanUrl;
                // 6. Reset Button State
                btnSendHTTPRequest.Enabled = true;
                btnReload.Enabled = true;
                UpdateNavigationButtons();
            }
        }

        // --- Private Helper Methods ---

        // Responsibility: Cleans user input to ensure it's a valid, loadable URL
        private string CleanAndValidateUrl(string url)
        {
            string cleanUrl = url.Trim();
            if (!cleanUrl.StartsWith("http://") && !cleanUrl.StartsWith("https://"))
                cleanUrl = "https://" + cleanUrl;
            return cleanUrl;
        }

        // Responsibility: Updates all display elements based on the PageContent model
        private void DisplayPageContent(PageContent page)
        {
            string windowTitle = $"[{page.statusCode}]";
            string displayContent = $"Status: {page.statusCode}\n";

            if (!string.IsNullOrEmpty(page.title))
            {
                windowTitle += $" {page.title}";
                displayContent += $"Title: {page.title}\n";
            }

            this.Text = windowTitle;
            displayContent += $"\n{page.rawHtml}";
            HTMLViewer.Text = displayContent;

            foreach (string link in page.links)
            {
                listLinks.Items.Add(link);
            }
        }

        // Responsibility: Builds a new menu
        private ContextMenuStrip CreateHomepageContextMenu()
        {
            var contextMenu = new ContextMenuStrip();

            // 1. Set current page as homepage item
            var setAsHomepageItem = new ToolStripMenuItem("Set Current Page as Homepage");
            // Attach the existing logic
            setAsHomepageItem.Click += async (s, args) => await SetCurrentPageAsHomepage();
            contextMenu.Items.Add(setAsHomepageItem);

            // 2. Edit homepage URL item
            var editHomepageItem = new ToolStripMenuItem("Edit Homepage URL...");
            // Attach the existing logic
            editHomepageItem.Click += async (s, args) => await EditHomepageUrl();
            contextMenu.Items.Add(editHomepageItem);

            return contextMenu;
        }

        // --- Event Handlers ---
        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Load the settings (which contains the required default or the saved URL)
                var homePageSetting = await homePageManager.LoadHomePageAsync();
                var bookmarkList = await bookmarkManager.LoadBookmarksAsync();

                // Use the loaded URL to initialize the address bar
                if (!string.IsNullOrEmpty(homePageSetting.url))
                {
                    txtAddress.Text = homePageSetting.url;

                    // Automatically navigate to the home page on load
                    await HandleNavigationRequestAsync(homePageSetting.url);
                }

                await RefreshBookmarksList();
                await RefreshHistoryList();
            }
            catch (Exception ex)
            {
                // Handle cases where the repository fails to load, even the default
                MessageBox.Show(this, $"Could not load home page settings: {ex.Message}", "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSendHTTPRequest_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAddress.Text))
                    await HandleNavigationRequestAsync(txtAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Application Error in button click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Prevent the beep sound

                try
                {
                    if (!string.IsNullOrEmpty(txtAddress.Text))
                        await HandleNavigationRequestAsync(txtAddress.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Application Error on Enter key: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                txtAddress.Text = lastLoadedUrl;
                await HandleNavigationRequestAsync(lastLoadedUrl, isNavigatingBackOrForward: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Application Error during reload: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void listLinks_Click(object sender, EventArgs e)
        {
            if (listLinks.SelectedItem != null)
            {
                string selectedUrl = listLinks.SelectedItem.ToString();
                txtAddress.Text = selectedUrl;

                try
                {
                    await HandleNavigationRequestAsync(selectedUrl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Application Error in link click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnHome_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Left click - Navigate to homepage
                try
                {
                    var homePageSetting = await homePageManager.LoadHomePageAsync();
                    if (!string.IsNullOrEmpty(homePageSetting.url))
                    {
                        await HandleNavigationRequestAsync(homePageSetting.url);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Failed to navigate to homepage: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Right click - Delegate creation to the helper
                var contextMenu = CreateHomepageContextMenu();

                // Show context menu at cursor position
                contextMenu.Show(btnHome.Owner.PointToScreen(new Point(e.X, e.Y)));
            }
        }

        // Helper method to set current page as homepage
        private async Task SetCurrentPageAsHomepage()
        {
            try
            {
                if (!string.IsNullOrEmpty(lastLoadedUrl))
                {
                    await homePageManager.SaveHomePageAsync(lastLoadedUrl);
                    MessageBox.Show(this, $"Homepage set to: {lastLoadedUrl}", "Homepage Updated",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "No page is currently loaded to set as homepage.", "No Page Loaded",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to save homepage: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to edit homepage URL
        private async Task EditHomepageUrl()
        {
            try
            {
                var currentHomepage = await homePageManager.LoadHomePageAsync();
                string initialUrl = currentHomepage?.url ?? "https://www.hw.ac.uk";

                // Use the dedicated form for input
                using (var inputForm = new EditHomePageForm("Edit Homepage", initialUrl))
                {
                    if (inputForm.ShowDialog(this) == DialogResult.OK)
                    {
                        string newUrl = inputForm.newUrl;
                        if (!string.IsNullOrEmpty(newUrl))
                        {
                            string cleanUrl = CleanAndValidateUrl(newUrl);
                            await homePageManager.SaveHomePageAsync(cleanUrl);
                            MessageBox.Show(this, $"Homepage updated to: {cleanUrl}", "Homepage Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to update homepage: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnBookmark_Click(object sender, EventArgs e)
        {
            try
            {
                Bookmark existingBookmark = bookmarkManager.GetBookmarkByUrl(lastLoadedUrl);

                // Prepare the Bookmark object to pass to the form
                Bookmark bookmarkToEdit;

                if (existingBookmark != null)
                {
                    bookmarkToEdit = new Bookmark
                    {
                        id = existingBookmark.id,
                        name = existingBookmark.name,
                        url = existingBookmark.url
                    };
                }
                else
                {
                    bookmarkToEdit = new Bookmark
                    {
                        // Use Guid.Empty to signal new to the form
                        id = Guid.Empty,
                        name = this.Text, // Current window title as default name
                        url = lastLoadedUrl
                    };
                }

                // Instantiate the new dedicated form
                using (var form = new EditBookmarkForm(bookmarkToEdit))
                {
                    DialogResult result = form.ShowDialog(this);

                    if (result == DialogResult.OK)
                    {
                        Bookmark updatedData = form.updatedBookmark;

                        if (existingBookmark != null)
                        {
                            await bookmarkManager.EditBookmarkAsync(updatedData);
                            MessageBox.Show($"Bookmark '{updatedData.name}' updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Create a new bookmark.
                            await bookmarkManager.CreateBookmarkAsync(updatedData.name, updatedData.url);
                            MessageBox.Show($"Bookmark '{updatedData.name}' added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (result == DialogResult.Abort && form.isDeleteAction)
                    {
                        // Use the ID from the original bookmark (which was stored in bookmarkToEdit)
                        if (existingBookmark != null)
                        {
                            await bookmarkManager.DeleteBookmarkAsync(existingBookmark.id);
                            MessageBox.Show($"Bookmark removed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                await RefreshBookmarksList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to add bookmark: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // helper: To populate and refresh the list box (listBookmarks)
        private async Task RefreshBookmarksList()
        {
            listBookmarks.Items.Clear();
            List<Bookmark> bookmarks = bookmarkManager.GetAllBookmarks();

            foreach (var bookmark in bookmarks)
            {
                listBookmarks.Items.Add(bookmark);
            }
        }
        private async void listBookmarks_Click(object sender, EventArgs e)
        {
            if (listBookmarks.SelectedItem is Bookmark selectedBookmark)
            {
                string selectedUrl = selectedBookmark.url;
                txtAddress.Text = selectedUrl;

                try
                {
                    await HandleNavigationRequestAsync(selectedUrl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Application Error in link click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // helper: To populate and refresh the list box (listHistory)
        private async Task RefreshHistoryList()
        {
            listHistory.Items.Clear();
            List<History> history = historyManager.GetAllHistory();

            foreach (var historyentry in history)
            {
                listHistory.Items.Add(historyentry);
            }
        }

        private async void listHistory_Click(object sender, EventArgs e)
        {
            if (listHistory.SelectedItem is History selectedEntry)
            {
                string selectedUrl = selectedEntry.url;
                txtAddress.Text = selectedUrl;

                try
                {
                    await HandleNavigationRequestAsync(selectedUrl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Application Error navigating to history entry: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnBack_Click(object sender, EventArgs e)
        {
            if (backStack.Count > 0)
            {
                // 1. Take the URL we are leaving (the current page) and push it to the forward stack
                forwardStack.Push(lastLoadedUrl);

                // 2. Get the URL we are going to (the previous page)
                string previousUrl = backStack.Pop();

                // 3. Navigate, setting the flag to prevent stack changes
                await HandleNavigationRequestAsync(previousUrl, isNavigatingBackOrForward: true);
            }
        }

        private async void btnForward_Click(object sender, EventArgs e)
        {
            if (forwardStack.Count > 0)
            {
                // 1. Take the URL we are leaving (the current page) and push it to the back stack
                backStack.Push(lastLoadedUrl);

                // 2. Get the URL we are going to (the next page)
                string nextUrl = forwardStack.Pop();

                // 3. Navigate, setting the flag to prevent stack changes
                await HandleNavigationRequestAsync(nextUrl, isNavigatingBackOrForward: true);
            }
        }
        private void UpdateNavigationButtons()
        {
            btnBack.Enabled = backStack.Count > 0;
            btnForward.Enabled = forwardStack.Count > 0;
        }
    }
}
