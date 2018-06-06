namespace Active_Directory_Management
{
    partial class MainView
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
			this.searchBox = new System.Windows.Forms.TextBox();
			this.createBtn = new System.Windows.Forms.Button();
			this.firstBox = new System.Windows.Forms.TextBox();
			this.lastBox = new System.Windows.Forms.TextBox();
			this.internetLabel = new System.Windows.Forms.Label();
			this.internetCombo = new System.Windows.Forms.ComboBox();
			this.cloudCheck = new System.Windows.Forms.CheckBox();
			this.usbDeviceCheck = new System.Windows.Forms.CheckBox();
			this.usbDiskCheck = new System.Windows.Forms.CheckBox();
			this.cdCheck = new System.Windows.Forms.CheckBox();
			this.detailBtn = new System.Windows.Forms.Button();
			this.saveBtn = new System.Windows.Forms.Button();
			this.listBox = new System.Windows.Forms.ListBox();
			this.treeView = new System.Windows.Forms.TreeView();
			this.switchPanel = new System.Windows.Forms.Panel();
			this.disableBtn = new System.Windows.Forms.Button();
			this.updBtn = new System.Windows.Forms.Button();
			this.switchPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// searchBox
			// 
			this.searchBox.Location = new System.Drawing.Point(12, 12);
			this.searchBox.Name = "searchBox";
			this.searchBox.Size = new System.Drawing.Size(339, 20);
			this.searchBox.TabIndex = 0;
			this.searchBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
			// 
			// createBtn
			// 
			this.createBtn.Location = new System.Drawing.Point(273, 183);
			this.createBtn.Name = "createBtn";
			this.createBtn.Size = new System.Drawing.Size(75, 23);
			this.createBtn.TabIndex = 2;
			this.createBtn.Text = "Создать";
			this.createBtn.UseVisualStyleBackColor = true;
			this.createBtn.Click += new System.EventHandler(this.CreateBtn_Click);
			// 
			// firstBox
			// 
			this.firstBox.Location = new System.Drawing.Point(724, 23);
			this.firstBox.Name = "firstBox";
			this.firstBox.ReadOnly = true;
			this.firstBox.Size = new System.Drawing.Size(165, 20);
			this.firstBox.TabIndex = 3;
			// 
			// lastBox
			// 
			this.lastBox.Location = new System.Drawing.Point(539, 23);
			this.lastBox.Name = "lastBox";
			this.lastBox.ReadOnly = true;
			this.lastBox.Size = new System.Drawing.Size(167, 20);
			this.lastBox.TabIndex = 4;
			// 
			// internetLabel
			// 
			this.internetLabel.AutoSize = true;
			this.internetLabel.Location = new System.Drawing.Point(3, 164);
			this.internetLabel.Name = "internetLabel";
			this.internetLabel.Size = new System.Drawing.Size(104, 13);
			this.internetLabel.TabIndex = 11;
			this.internetLabel.Text = "Доступ в Интернет";
			// 
			// internetCombo
			// 
			this.internetCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.internetCombo.FormattingEnabled = true;
			this.internetCombo.Items.AddRange(new object[] {
            "Отсутствует",
            "Ограниченный",
            "Полный"});
			this.internetCombo.Location = new System.Drawing.Point(113, 161);
			this.internetCombo.Name = "internetCombo";
			this.internetCombo.Size = new System.Drawing.Size(121, 21);
			this.internetCombo.TabIndex = 10;
			// 
			// cloudCheck
			// 
			this.cloudCheck.Appearance = System.Windows.Forms.Appearance.Button;
			this.cloudCheck.Enabled = false;
			this.cloudCheck.Location = new System.Drawing.Point(3, 105);
			this.cloudCheck.Name = "cloudCheck";
			this.cloudCheck.Size = new System.Drawing.Size(168, 28);
			this.cloudCheck.TabIndex = 9;
			this.cloudCheck.Text = "Личная папка (Диск K:\\)";
			this.cloudCheck.UseVisualStyleBackColor = true;
			// 
			// usbDeviceCheck
			// 
			this.usbDeviceCheck.Appearance = System.Windows.Forms.Appearance.Button;
			this.usbDeviceCheck.Location = new System.Drawing.Point(3, 71);
			this.usbDeviceCheck.Name = "usbDeviceCheck";
			this.usbDeviceCheck.Size = new System.Drawing.Size(168, 28);
			this.usbDeviceCheck.TabIndex = 8;
			this.usbDeviceCheck.Text = "Доступ к USB устройствам";
			this.usbDeviceCheck.UseVisualStyleBackColor = true;
			// 
			// usbDiskCheck
			// 
			this.usbDiskCheck.Appearance = System.Windows.Forms.Appearance.Button;
			this.usbDiskCheck.Location = new System.Drawing.Point(3, 37);
			this.usbDiskCheck.Name = "usbDiskCheck";
			this.usbDiskCheck.Size = new System.Drawing.Size(168, 28);
			this.usbDiskCheck.TabIndex = 7;
			this.usbDiskCheck.Text = "Доступ к USB дискам";
			this.usbDiskCheck.UseVisualStyleBackColor = true;
			// 
			// cdCheck
			// 
			this.cdCheck.Appearance = System.Windows.Forms.Appearance.Button;
			this.cdCheck.Location = new System.Drawing.Point(3, 3);
			this.cdCheck.Name = "cdCheck";
			this.cdCheck.Size = new System.Drawing.Size(168, 28);
			this.cdCheck.TabIndex = 6;
			this.cdCheck.Text = "Доступ к CD/DVD";
			this.cdCheck.UseVisualStyleBackColor = true;
			// 
			// detailBtn
			// 
			this.detailBtn.Location = new System.Drawing.Point(308, 37);
			this.detailBtn.Name = "detailBtn";
			this.detailBtn.Size = new System.Drawing.Size(100, 38);
			this.detailBtn.TabIndex = 12;
			this.detailBtn.Text = "Полное редактирование";
			this.detailBtn.UseVisualStyleBackColor = true;
			this.detailBtn.Click += new System.EventHandler(this.DetailBtn_Click);
			// 
			// saveBtn
			// 
			this.saveBtn.Location = new System.Drawing.Point(154, 218);
			this.saveBtn.Name = "saveBtn";
			this.saveBtn.Size = new System.Drawing.Size(118, 62);
			this.saveBtn.TabIndex = 13;
			this.saveBtn.Text = "Сохранить";
			this.saveBtn.UseVisualStyleBackColor = true;
			this.saveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
			// 
			// listBox
			// 
			this.listBox.FormattingEnabled = true;
			this.listBox.HorizontalScrollbar = true;
			this.listBox.Items.AddRange(new object[] {
            "abacaba",
            "param 1",
            "param 2",
            "test "});
			this.listBox.Location = new System.Drawing.Point(12, 38);
			this.listBox.Name = "listBox";
			this.listBox.ScrollAlwaysVisible = true;
			this.listBox.Size = new System.Drawing.Size(320, 407);
			this.listBox.Sorted = true;
			this.listBox.TabIndex = 16;
			// 
			// treeView
			// 
			this.treeView.Location = new System.Drawing.Point(12, 38);
			this.treeView.Name = "treeView";
			this.treeView.Size = new System.Drawing.Size(420, 407);
			this.treeView.TabIndex = 17;
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
			this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
			// 
			// switchPanel
			// 
			this.switchPanel.Controls.Add(this.cdCheck);
			this.switchPanel.Controls.Add(this.usbDiskCheck);
			this.switchPanel.Controls.Add(this.usbDeviceCheck);
			this.switchPanel.Controls.Add(this.cloudCheck);
			this.switchPanel.Controls.Add(this.saveBtn);
			this.switchPanel.Controls.Add(this.internetCombo);
			this.switchPanel.Controls.Add(this.detailBtn);
			this.switchPanel.Controls.Add(this.internetLabel);
			this.switchPanel.Enabled = false;
			this.switchPanel.Location = new System.Drawing.Point(492, 63);
			this.switchPanel.Name = "switchPanel";
			this.switchPanel.Size = new System.Drawing.Size(427, 280);
			this.switchPanel.TabIndex = 19;
			// 
			// disableBtn
			// 
			this.disableBtn.Enabled = false;
			this.disableBtn.Location = new System.Drawing.Point(800, 170);
			this.disableBtn.Name = "disableBtn";
			this.disableBtn.Size = new System.Drawing.Size(100, 38);
			this.disableBtn.TabIndex = 14;
			this.disableBtn.Text = "Отключить аккаунт";
			this.disableBtn.UseVisualStyleBackColor = true;
			this.disableBtn.Click += new System.EventHandler(this.DisableBtn_Click);
			// 
			// updBtn
			// 
			this.updBtn.Location = new System.Drawing.Point(357, 10);
			this.updBtn.Name = "updBtn";
			this.updBtn.Size = new System.Drawing.Size(75, 23);
			this.updBtn.TabIndex = 20;
			this.updBtn.Text = "Обновить";
			this.updBtn.UseVisualStyleBackColor = true;
			this.updBtn.Click += new System.EventHandler(this.UpdBtn_Click);
			// 
			// MainView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(934, 459);
			this.Controls.Add(this.disableBtn);
			this.Controls.Add(this.updBtn);
			this.Controls.Add(this.switchPanel);
			this.Controls.Add(this.treeView);
			this.Controls.Add(this.listBox);
			this.Controls.Add(this.lastBox);
			this.Controls.Add(this.firstBox);
			this.Controls.Add(this.createBtn);
			this.Controls.Add(this.searchBox);
			this.Name = "MainView";
			this.Text = "Active Directory ";
			this.switchPanel.ResumeLayout(false);
			this.switchPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button createBtn;
        private System.Windows.Forms.TextBox firstBox;
        private System.Windows.Forms.TextBox lastBox;
        private System.Windows.Forms.Label internetLabel;
        private System.Windows.Forms.ComboBox internetCombo;
        private System.Windows.Forms.CheckBox cloudCheck;
        private System.Windows.Forms.CheckBox usbDeviceCheck;
        private System.Windows.Forms.CheckBox usbDiskCheck;
        private System.Windows.Forms.CheckBox cdCheck;
        private System.Windows.Forms.Button detailBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Panel switchPanel;
        private System.Windows.Forms.Button updBtn;
        private System.Windows.Forms.Button disableBtn;
	}
}