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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node8");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node9");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node10");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node3", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node11");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node12");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node13");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Node4", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode6,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Node17");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Node18");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Node19");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Node14", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14,
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Node15 test");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Node16");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Node6", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17,
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode19});
            this.searchBox = new System.Windows.Forms.TextBox();
            this.treeBox = new System.Windows.Forms.TreeView();
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
            this.treeBoxCache = new System.Windows.Forms.TreeView();
            this.searchBoxPlaceholder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(12, 12);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(239, 20);
            this.searchBox.TabIndex = 0;
            this.searchBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            // 
            // treeBox
            // 
            this.treeBox.Location = new System.Drawing.Point(12, 38);
            this.treeBox.Name = "treeBox";
            treeNode1.Name = "Node2";
            treeNode1.Text = "Node2";
            treeNode2.Name = "Node7";
            treeNode2.Text = "Node7";
            treeNode3.Name = "Node8";
            treeNode3.Text = "Node8";
            treeNode4.Name = "Node9";
            treeNode4.Text = "Node9";
            treeNode5.Name = "Node10";
            treeNode5.Text = "Node10";
            treeNode6.Name = "Node3";
            treeNode6.Text = "Node3";
            treeNode7.Name = "Node11";
            treeNode7.Text = "Node11";
            treeNode8.Name = "Node12";
            treeNode8.Text = "Node12";
            treeNode9.Name = "Node13";
            treeNode9.Text = "Node13";
            treeNode10.Name = "Node4";
            treeNode10.Text = "Node4";
            treeNode11.Name = "Node0";
            treeNode11.Text = "Node0";
            treeNode12.Name = "Node5";
            treeNode12.Text = "Node5";
            treeNode13.Name = "Node17";
            treeNode13.Text = "Node17";
            treeNode14.Name = "Node18";
            treeNode14.Text = "Node18";
            treeNode15.Name = "Node19";
            treeNode15.Text = "Node19";
            treeNode16.Name = "Node14";
            treeNode16.Text = "Node14";
            treeNode17.Name = "Node15";
            treeNode17.Text = "Node15 test";
            treeNode18.Name = "Node16";
            treeNode18.Text = "Node16";
            treeNode19.Name = "Node6";
            treeNode19.Text = "Node6";
            treeNode20.Name = "Node1";
            treeNode20.Text = "Node1";
            this.treeBox.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode20});
            this.treeBox.Size = new System.Drawing.Size(320, 353);
            this.treeBox.TabIndex = 1;
            this.treeBox.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeBox_AfterSelect);
            // 
            // createBtn
            // 
            this.createBtn.Location = new System.Drawing.Point(257, 10);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(75, 23);
            this.createBtn.TabIndex = 2;
            this.createBtn.Text = "Создать";
            this.createBtn.UseVisualStyleBackColor = true;
            this.createBtn.Click += new System.EventHandler(this.CreateBtn_Click);
            // 
            // firstBox
            // 
            this.firstBox.Location = new System.Drawing.Point(405, 12);
            this.firstBox.Name = "firstBox";
            this.firstBox.ReadOnly = true;
            this.firstBox.Size = new System.Drawing.Size(100, 20);
            this.firstBox.TabIndex = 3;
            // 
            // lastBox
            // 
            this.lastBox.Location = new System.Drawing.Point(533, 12);
            this.lastBox.Name = "lastBox";
            this.lastBox.ReadOnly = true;
            this.lastBox.Size = new System.Drawing.Size(100, 20);
            this.lastBox.TabIndex = 4;
            // 
            // internetLabel
            // 
            this.internetLabel.AutoSize = true;
            this.internetLabel.Enabled = false;
            this.internetLabel.Location = new System.Drawing.Point(402, 268);
            this.internetLabel.Name = "internetLabel";
            this.internetLabel.Size = new System.Drawing.Size(104, 13);
            this.internetLabel.TabIndex = 11;
            this.internetLabel.Text = "Доступ в Интернет";
            // 
            // internetCombo
            // 
            this.internetCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.internetCombo.Enabled = false;
            this.internetCombo.FormattingEnabled = true;
            this.internetCombo.Items.AddRange(new object[] {
            "Отсутствует",
            "Ограниченный",
            "Полный"});
            this.internetCombo.Location = new System.Drawing.Point(512, 265);
            this.internetCombo.Name = "internetCombo";
            this.internetCombo.Size = new System.Drawing.Size(121, 21);
            this.internetCombo.TabIndex = 10;
            // 
            // cloudCheck
            // 
            this.cloudCheck.AutoSize = true;
            this.cloudCheck.Enabled = false;
            this.cloudCheck.Location = new System.Drawing.Point(405, 203);
            this.cloudCheck.Name = "cloudCheck";
            this.cloudCheck.Size = new System.Drawing.Size(150, 17);
            this.cloudCheck.TabIndex = 9;
            this.cloudCheck.Text = "Личная папка (Диск K:\\)";
            this.cloudCheck.UseVisualStyleBackColor = true;
            // 
            // usbDeviceCheck
            // 
            this.usbDeviceCheck.AutoSize = true;
            this.usbDeviceCheck.Enabled = false;
            this.usbDeviceCheck.Location = new System.Drawing.Point(405, 180);
            this.usbDeviceCheck.Name = "usbDeviceCheck";
            this.usbDeviceCheck.Size = new System.Drawing.Size(165, 17);
            this.usbDeviceCheck.TabIndex = 8;
            this.usbDeviceCheck.Text = "Доступ к USB устройствам";
            this.usbDeviceCheck.UseVisualStyleBackColor = true;
            // 
            // usbDiskCheck
            // 
            this.usbDiskCheck.AutoSize = true;
            this.usbDiskCheck.Enabled = false;
            this.usbDiskCheck.Location = new System.Drawing.Point(405, 157);
            this.usbDiskCheck.Name = "usbDiskCheck";
            this.usbDiskCheck.Size = new System.Drawing.Size(138, 17);
            this.usbDiskCheck.TabIndex = 7;
            this.usbDiskCheck.Text = "Доступ к USB дискам";
            this.usbDiskCheck.UseVisualStyleBackColor = true;
            // 
            // cdCheck
            // 
            this.cdCheck.AutoSize = true;
            this.cdCheck.Enabled = false;
            this.cdCheck.Location = new System.Drawing.Point(405, 134);
            this.cdCheck.Name = "cdCheck";
            this.cdCheck.Size = new System.Drawing.Size(118, 17);
            this.cdCheck.TabIndex = 6;
            this.cdCheck.Text = "Доступ к CD/DVD";
            this.cdCheck.UseVisualStyleBackColor = true;
            // 
            // detailBtn
            // 
            this.detailBtn.Enabled = false;
            this.detailBtn.Location = new System.Drawing.Point(405, 333);
            this.detailBtn.Name = "detailBtn";
            this.detailBtn.Size = new System.Drawing.Size(100, 38);
            this.detailBtn.TabIndex = 12;
            this.detailBtn.Text = "Полное редактирование";
            this.detailBtn.UseVisualStyleBackColor = true;
            this.detailBtn.Click += new System.EventHandler(this.DetailBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Enabled = false;
            this.saveBtn.Location = new System.Drawing.Point(558, 341);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 13;
            this.saveBtn.Text = "Сохранить";
            this.saveBtn.UseVisualStyleBackColor = true;
            // 
            // treeBoxCache
            // 
            this.treeBoxCache.Location = new System.Drawing.Point(23, 333);
            this.treeBoxCache.Name = "treeBoxCache";
            this.treeBoxCache.Size = new System.Drawing.Size(121, 97);
            this.treeBoxCache.TabIndex = 14;
            this.treeBoxCache.Visible = false;
            // 
            // searchBoxPlaceholder
            // 
            this.searchBoxPlaceholder.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.searchBoxPlaceholder.AutoSize = true;
            this.searchBoxPlaceholder.BackColor = System.Drawing.SystemColors.Window;
            this.searchBoxPlaceholder.ForeColor = System.Drawing.SystemColors.GrayText;
            this.searchBoxPlaceholder.Location = new System.Drawing.Point(16, 15);
            this.searchBoxPlaceholder.Name = "searchBoxPlaceholder";
            this.searchBoxPlaceholder.Size = new System.Drawing.Size(48, 13);
            this.searchBoxPlaceholder.TabIndex = 15;
            this.searchBoxPlaceholder.Text = "Поиск...";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.searchBoxPlaceholder);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.detailBtn);
            this.Controls.Add(this.internetLabel);
            this.Controls.Add(this.internetCombo);
            this.Controls.Add(this.cloudCheck);
            this.Controls.Add(this.usbDeviceCheck);
            this.Controls.Add(this.usbDiskCheck);
            this.Controls.Add(this.cdCheck);
            this.Controls.Add(this.lastBox);
            this.Controls.Add(this.firstBox);
            this.Controls.Add(this.createBtn);
            this.Controls.Add(this.treeBox);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.treeBoxCache);
            this.Name = "MainView";
            this.Text = "Active Directory ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.TreeView treeBox;
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
        private System.Windows.Forms.TreeView treeBoxCache;
        private System.Windows.Forms.Label searchBoxPlaceholder;
    }
}