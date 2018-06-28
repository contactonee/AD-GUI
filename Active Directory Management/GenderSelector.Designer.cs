namespace Active_Directory_Management
{
	partial class GenderSelector
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
			this.maleBtn = new System.Windows.Forms.RadioButton();
			this.femaleBtn = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// maleBtn
			// 
			this.maleBtn.Appearance = System.Windows.Forms.Appearance.Button;
			this.maleBtn.Location = new System.Drawing.Point(0, 0);
			this.maleBtn.Name = "maleBtn";
			this.maleBtn.Size = new System.Drawing.Size(23, 23);
			this.maleBtn.TabIndex = 0;
			this.maleBtn.Text = "М";
			this.maleBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.maleBtn.UseVisualStyleBackColor = true;
			// 
			// femaleBtn
			// 
			this.femaleBtn.Appearance = System.Windows.Forms.Appearance.Button;
			this.femaleBtn.Location = new System.Drawing.Point(26, 0);
			this.femaleBtn.Name = "femaleBtn";
			this.femaleBtn.Size = new System.Drawing.Size(23, 23);
			this.femaleBtn.TabIndex = 1;
			this.femaleBtn.Text = "Ж";
			this.femaleBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.femaleBtn.UseVisualStyleBackColor = true;
			// 
			// GenderSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.femaleBtn);
			this.Controls.Add(this.maleBtn);
			this.Name = "GenderSelector";
			this.Size = new System.Drawing.Size(50, 24);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RadioButton maleBtn;
		private System.Windows.Forms.RadioButton femaleBtn;
	}
}
