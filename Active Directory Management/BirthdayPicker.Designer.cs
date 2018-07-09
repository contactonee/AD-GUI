namespace Active_Directory_Management
{
	partial class BirthdayPicker
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
            this.dayLabel = new System.Windows.Forms.Label();
            this.monthLabel = new System.Windows.Forms.Label();
            this.yearLabel = new System.Windows.Forms.Label();
            this.dayBox = new System.Windows.Forms.ComboBox();
            this.monthBox = new System.Windows.Forms.ComboBox();
            this.yearBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // dayLabel
            // 
            this.dayLabel.AutoSize = true;
            this.dayLabel.Location = new System.Drawing.Point(3, -1);
            this.dayLabel.Name = "dayLabel";
            this.dayLabel.Size = new System.Drawing.Size(34, 13);
            this.dayLabel.TabIndex = 3;
            this.dayLabel.Text = "День";
            // 
            // monthLabel
            // 
            this.monthLabel.AutoSize = true;
            this.monthLabel.Location = new System.Drawing.Point(65, -1);
            this.monthLabel.Name = "monthLabel";
            this.monthLabel.Size = new System.Drawing.Size(40, 13);
            this.monthLabel.TabIndex = 4;
            this.monthLabel.Text = "Месяц";
            // 
            // yearLabel
            // 
            this.yearLabel.AutoSize = true;
            this.yearLabel.Location = new System.Drawing.Point(133, -1);
            this.yearLabel.Name = "yearLabel";
            this.yearLabel.Size = new System.Drawing.Size(25, 13);
            this.yearLabel.TabIndex = 5;
            this.yearLabel.Text = "Год";
            // 
            // dayBox
            // 
            this.dayBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.dayBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.dayBox.IntegralHeight = false;
            this.dayBox.Location = new System.Drawing.Point(0, 15);
            this.dayBox.MaxDropDownItems = 10;
            this.dayBox.Name = "dayBox";
            this.dayBox.Size = new System.Drawing.Size(50, 21);
            this.dayBox.TabIndex = 6;
            // 
            // monthBox
            // 
            this.monthBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.monthBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.monthBox.IntegralHeight = false;
            this.monthBox.Location = new System.Drawing.Point(60, 15);
            this.monthBox.MaxDropDownItems = 10;
            this.monthBox.Name = "monthBox";
            this.monthBox.Size = new System.Drawing.Size(50, 21);
            this.monthBox.TabIndex = 7;
            // 
            // yearBox
            // 
            this.yearBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.yearBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.yearBox.IntegralHeight = false;
            this.yearBox.Location = new System.Drawing.Point(120, 15);
            this.yearBox.MaxDropDownItems = 10;
            this.yearBox.Name = "yearBox";
            this.yearBox.Size = new System.Drawing.Size(50, 21);
            this.yearBox.TabIndex = 8;
            // 
            // BirthdayPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.yearBox);
            this.Controls.Add(this.monthBox);
            this.Controls.Add(this.dayBox);
            this.Controls.Add(this.yearLabel);
            this.Controls.Add(this.monthLabel);
            this.Controls.Add(this.dayLabel);
            this.Name = "BirthdayPicker";
            this.Size = new System.Drawing.Size(172, 38);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label dayLabel;
		private System.Windows.Forms.Label monthLabel;
		private System.Windows.Forms.Label yearLabel;
		private System.Windows.Forms.ComboBox dayBox;
		private System.Windows.Forms.ComboBox monthBox;
		private System.Windows.Forms.ComboBox yearBox;
	}
}
