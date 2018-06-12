namespace Active_Directory_Management
{
	partial class GroupSelector
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cdCheck = new System.Windows.Forms.CheckBox();
			this.usbDiskCheck = new System.Windows.Forms.CheckBox();
			this.usbDeviceCheck = new System.Windows.Forms.CheckBox();
			this.cloudCheck = new System.Windows.Forms.CheckBox();
			this.internetLabel = new System.Windows.Forms.Label();
			this.internetCombo = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// cdCheck
			// 
			this.cdCheck.BackColor = System.Drawing.SystemColors.Control;
			this.cdCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdCheck.Location = new System.Drawing.Point(88, 38);
			this.cdCheck.Name = "cdCheck";
			this.cdCheck.Size = new System.Drawing.Size(168, 28);
			this.cdCheck.TabIndex = 16;
			this.cdCheck.Text = "Доступ к CD/DVD";
			this.cdCheck.UseVisualStyleBackColor = false;
			// 
			// usbDiskCheck
			// 
			this.usbDiskCheck.Location = new System.Drawing.Point(88, 72);
			this.usbDiskCheck.Name = "usbDiskCheck";
			this.usbDiskCheck.Size = new System.Drawing.Size(168, 28);
			this.usbDiskCheck.TabIndex = 17;
			this.usbDiskCheck.Text = "Доступ к USB дискам";
			this.usbDiskCheck.UseVisualStyleBackColor = true;
			// 
			// usbDeviceCheck
			// 
			this.usbDeviceCheck.Location = new System.Drawing.Point(88, 106);
			this.usbDeviceCheck.Name = "usbDeviceCheck";
			this.usbDeviceCheck.Size = new System.Drawing.Size(168, 28);
			this.usbDeviceCheck.TabIndex = 18;
			this.usbDeviceCheck.Text = "Доступ к USB устройствам";
			this.usbDeviceCheck.UseVisualStyleBackColor = true;
			// 
			// cloudCheck
			// 
			this.cloudCheck.Enabled = false;
			this.cloudCheck.Location = new System.Drawing.Point(88, 140);
			this.cloudCheck.Name = "cloudCheck";
			this.cloudCheck.Size = new System.Drawing.Size(168, 28);
			this.cloudCheck.TabIndex = 19;
			this.cloudCheck.Text = "Личная папка (Диск K:\\)";
			this.cloudCheck.UseVisualStyleBackColor = true;
			// 
			// internetLabel
			// 
			this.internetLabel.AutoSize = true;
			this.internetLabel.Location = new System.Drawing.Point(85, 204);
			this.internetLabel.Name = "internetLabel";
			this.internetLabel.Size = new System.Drawing.Size(104, 13);
			this.internetLabel.TabIndex = 15;
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
			this.internetCombo.Location = new System.Drawing.Point(195, 201);
			this.internetCombo.Name = "internetCombo";
			this.internetCombo.Size = new System.Drawing.Size(121, 21);
			this.internetCombo.TabIndex = 14;
			// 
			// GroupSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cdCheck);
			this.Controls.Add(this.usbDiskCheck);
			this.Controls.Add(this.usbDeviceCheck);
			this.Controls.Add(this.cloudCheck);
			this.Controls.Add(this.internetLabel);
			this.Controls.Add(this.internetCombo);
			this.Name = "GroupSelector";
			this.Size = new System.Drawing.Size(712, 410);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox cdCheck;
		private System.Windows.Forms.CheckBox usbDiskCheck;
		private System.Windows.Forms.CheckBox usbDeviceCheck;
		private System.Windows.Forms.CheckBox cloudCheck;
		private System.Windows.Forms.Label internetLabel;
		private System.Windows.Forms.ComboBox internetCombo;
	}
}
