namespace NetSaha
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ConnectButton = new Button();
            IPTextBox = new TextBox();
            NameButton = new Button();
            NameTextBox = new TextBox();
            CreateSahaButton = new Button();
            LogListBox = new ListBox();
            SuspendLayout();
            // 
            // ConnectButton
            // 
            ConnectButton.Location = new Point(713, 38);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(75, 23);
            ConnectButton.TabIndex = 0;
            ConnectButton.Text = "Connect";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // IPTextBox
            // 
            IPTextBox.Location = new Point(501, 38);
            IPTextBox.Name = "IPTextBox";
            IPTextBox.Size = new Size(206, 23);
            IPTextBox.TabIndex = 1;
            // 
            // NameButton
            // 
            NameButton.Location = new Point(713, 9);
            NameButton.Name = "NameButton";
            NameButton.Size = new Size(75, 23);
            NameButton.TabIndex = 3;
            NameButton.Text = "Enter";
            NameButton.UseVisualStyleBackColor = true;
            NameButton.Click += NameButton_Click;
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(501, 10);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(206, 23);
            NameTextBox.TabIndex = 4;
            // 
            // CreateSahaButton
            // 
            CreateSahaButton.Location = new Point(501, 410);
            CreateSahaButton.Name = "CreateSahaButton";
            CreateSahaButton.Size = new Size(287, 28);
            CreateSahaButton.TabIndex = 5;
            CreateSahaButton.Text = "Create Saha";
            CreateSahaButton.UseVisualStyleBackColor = true;
            CreateSahaButton.Click += CreateSahaButton_Click;
            // 
            // LogListBox
            // 
            LogListBox.FormattingEnabled = true;
            LogListBox.Location = new Point(501, 67);
            LogListBox.Name = "LogListBox";
            LogListBox.Size = new Size(287, 334);
            LogListBox.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LogListBox);
            Controls.Add(CreateSahaButton);
            Controls.Add(NameTextBox);
            Controls.Add(NameButton);
            Controls.Add(IPTextBox);
            Controls.Add(ConnectButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ConnectButton;
        private TextBox IPTextBox;
        private Button NameButton;
        private TextBox NameTextBox;
        private Button CreateSahaButton;
        private ListBox LogListBox;
    }
}
