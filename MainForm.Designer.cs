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
            _isDirty = new Label();
            _frameName = new Label();
            _folderName = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // _isDirty
            // 
            _isDirty.AutoSize = true;
            _isDirty.Dock = DockStyle.Fill;
            _isDirty.Location = new Point(757, 866);
            _isDirty.Name = "_isDirty";
            _isDirty.Size = new Size(748, 435);
            _isDirty.TabIndex = 1;
            _isDirty.Text = "Clean";
            _isDirty.TextAlign = ContentAlignment.TopCenter;
            // 
            // _frameName
            // 
            _frameName.AutoSize = true;
            _frameName.Dock = DockStyle.Fill;
            _frameName.Font = new Font("SimSun", 72F, FontStyle.Regular, GraphicsUnit.Point);
            _frameName.Location = new Point(757, 433);
            _frameName.Name = "_frameName";
            _frameName.Size = new Size(748, 433);
            _frameName.TabIndex = 1;
            _frameName.Text = "007";
            _frameName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _folderName
            // 
            _folderName.AutoSize = true;
            _folderName.Dock = DockStyle.Fill;
            _folderName.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            _folderName.Location = new Point(757, 0);
            _folderName.Name = "_folderName";
            _folderName.Size = new Size(748, 433);
            _folderName.TabIndex = 1;
            _folderName.Text = "ilya45";
            _folderName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.Controls.Add(_folderName, 1, 0);
            tableLayoutPanel1.Controls.Add(_frameName, 1, 1);
            tableLayoutPanel1.Controls.Add(_isDirty, 1, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
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
        private Label _isDirty;
        private Label _frameName;
        private Label _folderName;
        private TableLayoutPanel tableLayoutPanel1;
    }
}