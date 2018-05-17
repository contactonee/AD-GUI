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
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Node8");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Node9");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Node10");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Node3", new System.Windows.Forms.TreeNode[] {
            treeNode22,
            treeNode23,
            treeNode24,
            treeNode25});
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Node11");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Node12");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Node13");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Node4", new System.Windows.Forms.TreeNode[] {
            treeNode27,
            treeNode28,
            treeNode29});
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode21,
            treeNode26,
            treeNode30});
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Node17");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("Node18");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Node19");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("Node14", new System.Windows.Forms.TreeNode[] {
            treeNode33,
            treeNode34,
            treeNode35});
            System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("Node15 test");
            System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("Node16");
            System.Windows.Forms.TreeNode treeNode39 = new System.Windows.Forms.TreeNode("Node6", new System.Windows.Forms.TreeNode[] {
            treeNode36,
            treeNode37,
            treeNode38});
            System.Windows.Forms.TreeNode treeNode40 = new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
            treeNode32,
            treeNode39});
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
            treeNode21.Name = "Node2";
            treeNode21.Text = "Node2";
            treeNode22.Name = "Node7";
            treeNode22.Text = "Node7";
            treeNode23.Name = "Node8";
            treeNode23.Text = "Node8";
            treeNode24.Name = "Node9";
            treeNode24.Text = "Node9";
            treeNode25.Name = "Node10";
            treeNode25.Text = "Node10";
            treeNode26.Name = "Node3";
            treeNode26.Text = "Node3";
            treeNode27.Name = "Node11";
            treeNode27.Text = "Node11";
            treeNode28.Name = "Node12";
            treeNode28.Text = "Node12";
            treeNode29.Name = "Node13";
            treeNode29.Text = "Node13";
            treeNode30.Name = "Node4";
            treeNode30.Text = "Node4";
            treeNode31.Name = "Node0";
            treeNode31.Text = "Node0";
            treeNode32.Name = "Node5";
            treeNode32.Text = "Node5";
            treeNode33.Name = "Node17";
            treeNode33.Text = "Node17";
            treeNode34.Name = "Node18";
            treeNode34.Text = "Node18";
            treeNode35.Name = "Node19";
            treeNode35.Text = "Node19";
            treeNode36.Name = "Node14";
            treeNode36.Text = "Node14";
            treeNode37.Name = "Node15";
            treeNode37.Text = "Node15 test";
            treeNode38.Name = "Node16";
            treeNode38.Text = "Node16";
            treeNode39.Name = "Node6";
            treeNode39.Text = "Node6";
            treeNode40.Name = "Node1";
            treeNode40.Text = "Node1";
            this.treeBox.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode31,
            treeNode40});
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
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}