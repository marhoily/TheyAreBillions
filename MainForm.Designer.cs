namespace TheyAreBillions
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            _match = new Label();
            _name = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 24);
            label1.Name = "label1";
            label1.Size = new Size(195, 57);
            label1.TabIndex = 0;
            label1.Text = "F5 - Save";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 91);
            label2.Name = "label2";
            label2.Size = new Size(309, 57);
            label2.TabIndex = 0;
            label2.Text = "F8 - LoadGame";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 165);
            label3.Name = "label3";
            label3.Size = new Size(559, 57);
            label3.TabIndex = 0;
            label3.Text = "Crl+Alt+R - remove last save";
            // 
            // _match
            // 
            _match.AutoSize = true;
            _match.Location = new Point(22, 231);
            _match.Name = "_match";
            _match.Size = new Size(136, 57);
            _match.TabIndex = 1;
            _match.Text = "label4";
            // 
            // _name
            // 
            _name.AutoSize = true;
            _name.Location = new Point(22, 297);
            _name.Name = "_name";
            _name.Size = new Size(136, 57);
            _name.TabIndex = 1;
            _name.Text = "label4";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(23F, 57F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(_name);
            Controls.Add(_match);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Backup Catcher";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label _match;
        private Label _name;
    }
}