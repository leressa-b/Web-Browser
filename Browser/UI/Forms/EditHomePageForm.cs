namespace Browser.UI.Forms
{
    public partial class EditHomePageForm : Form
    {
        public string newUrl { get; set; }

        private TextBox inputBox;

        public EditHomePageForm(string title, string initialUrl)
        {
            this.Text = title;
            this.Width = 500;
            this.Height = 150;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Label
            Label textLabel = new Label()
            {
                Left = 10,
                Top = 15,
                Width = 460,
                Text = "Enter the URL:"
            };

            // Input Box (stores the initial URL)
            inputBox = new TextBox()
            {
                Left = 10,
                Top = 40,
                Width = 460,
                Text = initialUrl
            };

            // OK Button
            Button okButton = new Button()
            {
                Text = "OK",
                Left = 310,
                Width = 75,
                Top = 70,
                DialogResult = DialogResult.OK
            };

            // Cancel Button
            Button cancelButton = new Button()
            {
                Text = "Cancel",
                Left = 395,
                Width = 75,
                Top = 70,
                DialogResult = DialogResult.Cancel
            };


            // When OK is clicked, store the value before closing
            okButton.Click += (sender, e) =>
            {
                this.newUrl = inputBox.Text; // Capture the result
                this.Close();
            };

            cancelButton.Click += (sender, e) => { this.Close(); };

            // Add Controls
            this.Controls.Add(textLabel);
            this.Controls.Add(inputBox);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);

            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;
        }
    }
}
