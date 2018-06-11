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
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// maleBtn
			// 
			this.maleBtn.Appearance = System.Windows.Forms.Appearance.Button;
			this.maleBtn.Location = new System.Drawing.Point(39, 3);
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
			this.femaleBtn.Location = new System.Drawing.Point(66, 3);
			this.femaleBtn.Name = "femaleBtn";
			this.femaleBtn.Size = new System.Drawing.Size(23, 23);
			this.femaleBtn.TabIndex = 1;
			this.femaleBtn.Text = "Ж";
			this.femaleBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.femaleBtn.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(27, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Пол";
			// 
			// GenderSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.femaleBtn);
			this.Controls.Add(this.maleBtn);
			this.Name = "GenderSelector";
			this.Size = new System.Drawing.Size(99, 33);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton maleBtn;
		private System.Windows.Forms.RadioButton femaleBtn;
		private System.Windows.Forms.Label label1;
	}
}
