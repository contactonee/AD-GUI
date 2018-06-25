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
			this.groupBox = new System.Windows.Forms.CheckedListBox();
			this.SuspendLayout();
			// 
			// groupBox
			// 
			this.groupBox.CheckOnClick = true;
			this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox.FormattingEnabled = true;
			this.groupBox.Location = new System.Drawing.Point(0, 0);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(250, 264);
			this.groupBox.TabIndex = 0;
			// 
			// GroupSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox);
			this.Name = "GroupSelector";
			this.Size = new System.Drawing.Size(250, 264);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckedListBox groupBox;
	}
}
