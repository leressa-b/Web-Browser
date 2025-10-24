namespace Browser.UI.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            navToolStrip = new ToolStrip();
            btnHome = new ToolStripButton();
            btnBack = new ToolStripButton();
            btnForward = new ToolStripButton();
            btnReload = new ToolStripButton();
            txtAddress = new ToolStripTextBox();
            btnBookmark = new ToolStripButton();
            btnSendHTTPRequest = new ToolStripButton();
            bodyDisplay = new SplitContainer();
            listLinks = new ListBox();
            labelLinks = new Label();
            splitHTMLTab = new SplitContainer();
            HTMLViewer = new RichTextBox();
            tabControl = new TabControl();
            tabBookmarks = new TabPage();
            listBookmarks = new ListBox();
            tabHistory = new TabPage();
            listHistory = new ListBox();
            navToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bodyDisplay).BeginInit();
            bodyDisplay.Panel1.SuspendLayout();
            bodyDisplay.Panel2.SuspendLayout();
            bodyDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitHTMLTab).BeginInit();
            splitHTMLTab.Panel1.SuspendLayout();
            splitHTMLTab.Panel2.SuspendLayout();
            splitHTMLTab.SuspendLayout();
            tabControl.SuspendLayout();
            tabBookmarks.SuspendLayout();
            tabHistory.SuspendLayout();
            SuspendLayout();
            // 
            // navToolStrip
            // 
            navToolStrip.BackColor = Color.LightGray;
            navToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            navToolStrip.ImageScalingSize = new Size(20, 20);
            navToolStrip.Items.AddRange(new ToolStripItem[] { btnHome, btnBack, btnForward, btnReload, txtAddress, btnBookmark, btnSendHTTPRequest });
            navToolStrip.Location = new Point(0, 0);
            navToolStrip.Name = "navToolStrip";
            navToolStrip.Padding = new Padding(5);
            navToolStrip.RenderMode = ToolStripRenderMode.System;
            navToolStrip.Size = new Size(800, 47);
            navToolStrip.Stretch = true;
            navToolStrip.TabIndex = 0;
            navToolStrip.Text = "toolStrip1";
            // 
            // btnHome
            // 
            btnHome.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnHome.Image = (Image)resources.GetObject("btnHome.Image");
            btnHome.ImageTransparentColor = Color.Magenta;
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(29, 34);
            btnHome.Text = "Home";
            btnHome.ToolTipText = "Go to Home Page";
            btnHome.MouseDown += btnHome_MouseDown;
            // 
            // btnBack
            // 
            btnBack.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnBack.ImageTransparentColor = Color.Magenta;
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(29, 34);
            btnBack.Text = "<";
            btnBack.ToolTipText = "Go back";
            btnBack.Click += btnBack_Click;
            // 
            // btnForward
            // 
            btnForward.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnForward.ImageTransparentColor = Color.Magenta;
            btnForward.Name = "btnForward";
            btnForward.Size = new Size(29, 34);
            btnForward.Text = ">";
            btnForward.ToolTipText = "Go Forward";
            btnForward.Click += btnForward_Click;
            // 
            // btnReload
            // 
            btnReload.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnReload.Image = (Image)resources.GetObject("btnReload.Image");
            btnReload.ImageTransparentColor = Color.Magenta;
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(29, 34);
            btnReload.Text = "Reload";
            btnReload.Click += btnReload_Click;
            // 
            // txtAddress
            // 
            txtAddress.Margin = new Padding(5);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(580, 27);
            txtAddress.ToolTipText = "Enter a URL and press Enter or Go";
            txtAddress.KeyPress += txtAddress_KeyPress;
            // 
            // btnBookmark
            // 
            btnBookmark.Alignment = ToolStripItemAlignment.Right;
            btnBookmark.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnBookmark.Image = (Image)resources.GetObject("btnBookmark.Image");
            btnBookmark.ImageTransparentColor = Color.Magenta;
            btnBookmark.Name = "btnBookmark";
            btnBookmark.Size = new Size(29, 34);
            btnBookmark.Text = "Bookmark this tab";
            btnBookmark.Click += btnBookmark_Click;
            // 
            // btnSendHTTPRequest
            // 
            btnSendHTTPRequest.Alignment = ToolStripItemAlignment.Right;
            btnSendHTTPRequest.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnSendHTTPRequest.ImageTransparentColor = Color.Magenta;
            btnSendHTTPRequest.Name = "btnSendHTTPRequest";
            btnSendHTTPRequest.Size = new Size(32, 34);
            btnSendHTTPRequest.Text = "Go";
            btnSendHTTPRequest.ToolTipText = "Navigate to address";
            btnSendHTTPRequest.Click += btnSendHTTPRequest_Click;
            // 
            // bodyDisplay
            // 
            bodyDisplay.Dock = DockStyle.Fill;
            bodyDisplay.Location = new Point(0, 47);
            bodyDisplay.Name = "bodyDisplay";
            // 
            // bodyDisplay.Panel1
            // 
            bodyDisplay.Panel1.BackColor = Color.MistyRose;
            bodyDisplay.Panel1.Controls.Add(listLinks);
            bodyDisplay.Panel1.Controls.Add(labelLinks);
            // 
            // bodyDisplay.Panel2
            // 
            bodyDisplay.Panel2.Controls.Add(splitHTMLTab);
            bodyDisplay.Size = new Size(800, 403);
            bodyDisplay.SplitterDistance = 200;
            bodyDisplay.TabIndex = 2;
            // 
            // listLinks
            // 
            listLinks.BackColor = Color.MistyRose;
            listLinks.Cursor = Cursors.Hand;
            listLinks.Dock = DockStyle.Fill;
            listLinks.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point, 0);
            listLinks.ForeColor = SystemColors.MenuHighlight;
            listLinks.FormattingEnabled = true;
            listLinks.HorizontalScrollbar = true;
            listLinks.Location = new Point(0, 30);
            listLinks.Name = "listLinks";
            listLinks.Size = new Size(200, 373);
            listLinks.TabIndex = 1;
            listLinks.Click += listLinks_Click;
            // 
            // labelLinks
            // 
            labelLinks.AutoSize = true;
            labelLinks.BackColor = Color.MistyRose;
            labelLinks.Dock = DockStyle.Top;
            labelLinks.Location = new Point(0, 0);
            labelLinks.Name = "labelLinks";
            labelLinks.Padding = new Padding(5);
            labelLinks.Size = new Size(90, 30);
            labelLinks.TabIndex = 0;
            labelLinks.Text = "Page Links:";
            labelLinks.TextAlign = ContentAlignment.TopCenter;
            // 
            // splitHTMLTab
            // 
            splitHTMLTab.Dock = DockStyle.Fill;
            splitHTMLTab.Location = new Point(0, 0);
            splitHTMLTab.Name = "splitHTMLTab";
            // 
            // splitHTMLTab.Panel1
            // 
            splitHTMLTab.Panel1.Controls.Add(HTMLViewer);
            // 
            // splitHTMLTab.Panel2
            // 
            splitHTMLTab.Panel2.Controls.Add(tabControl);
            splitHTMLTab.Size = new Size(596, 403);
            splitHTMLTab.SplitterDistance = 400;
            splitHTMLTab.TabIndex = 0;
            // 
            // HTMLViewer
            // 
            HTMLViewer.BackColor = Color.LavenderBlush;
            HTMLViewer.Dock = DockStyle.Fill;
            HTMLViewer.Location = new Point(0, 0);
            HTMLViewer.Name = "HTMLViewer";
            HTMLViewer.ReadOnly = true;
            HTMLViewer.Size = new Size(400, 403);
            HTMLViewer.TabIndex = 0;
            HTMLViewer.Text = "";
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabBookmarks);
            tabControl.Controls.Add(tabHistory);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(192, 403);
            tabControl.TabIndex = 0;
            // 
            // tabBookmarks
            // 
            tabBookmarks.Controls.Add(listBookmarks);
            tabBookmarks.Location = new Point(4, 29);
            tabBookmarks.Name = "tabBookmarks";
            tabBookmarks.Padding = new Padding(3);
            tabBookmarks.Size = new Size(184, 370);
            tabBookmarks.TabIndex = 0;
            tabBookmarks.Text = "Bookmarks";
            tabBookmarks.UseVisualStyleBackColor = true;
            // 
            // listBookmarks
            // 
            listBookmarks.Dock = DockStyle.Fill;
            listBookmarks.FormattingEnabled = true;
            listBookmarks.HorizontalScrollbar = true;
            listBookmarks.Location = new Point(3, 3);
            listBookmarks.Name = "listBookmarks";
            listBookmarks.Size = new Size(178, 364);
            listBookmarks.TabIndex = 0;
            listBookmarks.Click += listBookmarks_Click;
            // 
            // tabHistory
            // 
            tabHistory.Controls.Add(listHistory);
            tabHistory.Location = new Point(4, 29);
            tabHistory.Name = "tabHistory";
            tabHistory.Padding = new Padding(3);
            tabHistory.Size = new Size(184, 370);
            tabHistory.TabIndex = 1;
            tabHistory.Text = "History";
            tabHistory.UseVisualStyleBackColor = true;
            // 
            // listHistory
            // 
            listHistory.Dock = DockStyle.Fill;
            listHistory.FormattingEnabled = true;
            listHistory.HorizontalScrollbar = true;
            listHistory.Location = new Point(3, 3);
            listHistory.Name = "listHistory";
            listHistory.Size = new Size(178, 364);
            listHistory.TabIndex = 0;
            listHistory.Click += listHistory_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(bodyDisplay);
            Controls.Add(navToolStrip);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            navToolStrip.ResumeLayout(false);
            navToolStrip.PerformLayout();
            bodyDisplay.Panel1.ResumeLayout(false);
            bodyDisplay.Panel1.PerformLayout();
            bodyDisplay.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bodyDisplay).EndInit();
            bodyDisplay.ResumeLayout(false);
            splitHTMLTab.Panel1.ResumeLayout(false);
            splitHTMLTab.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitHTMLTab).EndInit();
            splitHTMLTab.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabBookmarks.ResumeLayout(false);
            tabHistory.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip navToolStrip;
        private ToolStripButton btnBack;
        private ToolStripButton btnHome;
        private ToolStripButton btnForward;
        private ToolStripButton btnReload;
        private ToolStripTextBox txtAddress;
        private ToolStripButton btnSendHTTPRequest;
        private ToolStripButton btnBookmark;
        private SplitContainer bodyDisplay;
        private Label labelLinks;
        private SplitContainer splitHTMLTab;
        private ListBox listLinks;
        private RichTextBox HTMLViewer;
        private TabControl tabControl;
        private TabPage tabBookmarks;
        private TabPage tabHistory;
        private ListBox listBookmarks;
        private ListBox listHistory;
    }
}