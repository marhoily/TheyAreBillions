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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._match = new System.Windows.Forms.Label();
            this._name = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 57);
            this.label1.TabIndex = 0;
            this.label1.Text = "F5 - Save";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(309, 57);
            this.label2.TabIndex = 0;
            this.label2.Text = "F8 - LoadGame";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(559, 57);
            this.label3.TabIndex = 0;
            this.label3.Text = "Crl+Alt+R - remove last save";
            // 
            // _match
            // 
            this._match.AutoSize = true;
            this._match.Location = new System.Drawing.Point(22, 231);
            this._match.Name = "_match";
            this._match.Size = new System.Drawing.Size(136, 57);
            this._match.TabIndex = 1;
            this._match.Text = "label4";
            // 
            // _name
            // 
            this._name.AutoSize = true;
            this._name.Location = new System.Drawing.Point(22, 297);
            this._name.Name = "_name";
            this._name.Size = new System.Drawing.Size(136, 57);
            this._name.TabIndex = 1;
            this._name.Text = "label4";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(23F, 57F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._name);
            this.Controls.Add(this._match);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Backup Catcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label _match;
        private Label _name;
    }
}