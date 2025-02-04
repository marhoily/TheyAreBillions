﻿namespace UI
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
            _frameName = new Label();
            _folderName = new Label();
            _errorLabel = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // _frameName
            // 
            _frameName.AutoSize = true;
            _frameName.Dock = DockStyle.Fill;
            _frameName.Font = new Font("SimSun", 72F, FontStyle.Regular, GraphicsUnit.Point);
            _frameName.Location = new Point(757, 591);
            _frameName.Name = "_frameName";
            _frameName.Size = new Size(748, 591);
            _frameName.TabIndex = 1;
            _frameName.Text = "007";
            _frameName.TextAlign = ContentAlignment.TopCenter;
            // 
            // _errorLabel
            // 
            _errorLabel.AutoSize = true;
            _errorLabel.Dock = DockStyle.Fill;
            _errorLabel.Font = new Font("Arial", 24F, FontStyle.Regular, GraphicsUnit.Point);
            _errorLabel.ForeColor = Color.OrangeRed;
            _errorLabel.Location = new Point(757, 591);
            _errorLabel.Name = "_errorLabel";
            _errorLabel.Size = new Size(748, 591);
            _errorLabel.TabIndex = 2;
            _errorLabel.Text = "007";
            _errorLabel.TextAlign = ContentAlignment.BottomCenter;
            // 
            // _folderName
            // 
            _folderName.AutoSize = true;
            _folderName.Dock = DockStyle.Fill;
            _folderName.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            _folderName.Location = new Point(757, 0);
            _folderName.Name = "_folderName";
            _folderName.Size = new Size(748, 591);
            _folderName.TabIndex = 1;
            _folderName.Text = "ilya45";
            _folderName.TextAlign = ContentAlignment.BottomCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.Controls.Add(_folderName, 1, 0);
            tableLayoutPanel1.Controls.Add(_frameName, 1, 1);
            tableLayoutPanel1.Controls.Add(_errorLabel, 1, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 45.4545441F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 45.45455F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.090908F));
            tableLayoutPanel1.Size = new Size(2264, 1301);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(23F, 57F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(2264, 1301);
            Controls.Add(tableLayoutPanel1);
            ForeColor = Color.FromArgb(224, 224, 224);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Backup Catcher";
            Load += MainForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label _errorLabel;
        private Label _frameName;
        private Label _folderName;
        private TableLayoutPanel tableLayoutPanel1;
    }
}