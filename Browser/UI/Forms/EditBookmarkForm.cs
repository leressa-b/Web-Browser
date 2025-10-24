using Browser.Core.Models;

namespace Browser.UI.Forms
{
    public partial class EditBookmarkForm : Form
    {
        public Bookmark updatedBookmark { get; private set; }
        public bool isDeleteAction { get; private set; } = false;

        private readonly Bookmark originalBookmark;
        private readonly bool isCreatingNew;

        // Controls
        private TextBox txtName;
        private TextBox txtUrl;
        private Button btnDelete;

        // Accepts an existing bookmark (for edit) or null (for create)
        public EditBookmarkForm(Bookmark bookmarkToEdit)
        {
            originalBookmark = bookmarkToEdit;
            isCreatingNew = originalBookmark.id == Guid.Empty;

            // --- Form Setup ---
            this.Text = isCreatingNew ? "Add New Bookmark" : "Edit Bookmark";
            this.Width = 500;
            this.Height = 220;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            InitializeControls();
        }

        private void InitializeControls()
        {
            this.Controls.Add(new Label() { Text = "Name:", Left = 10, Top = 10, Width = 80 });
            txtName = new TextBox()
            {
                Left = 90,
                Top = 10,
                Width = 370,
                Text = originalBookmark.name ?? ""
            };
            this.Controls.Add(txtName);

            this.Controls.Add(new Label() { Text = "URL:", Left = 10, Top = 40, Width = 80 });
            txtUrl = new TextBox()
            {
                Left = 90,
                Top = 40,
                Width = 370,
                Text = originalBookmark.url,
                ReadOnly = true
            };
            this.Controls.Add(txtUrl);

            Button btnSave = new Button()
            {
                Text = isCreatingNew ? "Add" : "Save",
                Left = 280,
                Top = 130,
                Width = 85,
                DialogResult = DialogResult.OK
            };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);
            this.AcceptButton = btnSave;

            btnDelete = new Button()
            {
                Text = "Delete",
                Left = 375,
                Top = 130,
                Width = 85
            };
            btnDelete.Click += BtnDelete_Click;

            // Only show the Delete button if editing an existing bookmark
            if (!isCreatingNew)
            {
                this.Controls.Add(btnDelete);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None; // Prevent closing
                return;
            }

            // Create the Bookmark object to return
            updatedBookmark = new Bookmark
            {
                // Use existing ID if editing, generate a temporary new one if adding
                id = originalBookmark.id,
                name = txtName.Text.Trim(),
                url = txtUrl.Text.Trim()
            };
            // DialogResult = DialogResult.OK will close the form
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            isDeleteAction = true;
            this.DialogResult = DialogResult.Abort; // Use Abort for a non OK action
        }
    }
}