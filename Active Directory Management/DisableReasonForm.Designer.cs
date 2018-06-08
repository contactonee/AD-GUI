namespace Active_Directory_Management
{
    partial class DisableReasonForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.reasonTextBox = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(272, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Пожалуйста, укажите причину блокировки аккаунта";
			// 
			// reasonTextBox
			// 
			this.reasonTextBox.Location = new System.Drawing.Point(15, 70);
			this.reasonTextBox.Name = "reasonTextBox";
			this.reasonTextBox.Size = new System.Drawing.Size(267, 20);
			this.reasonTextBox.TabIndex = 1;
			this.reasonTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.reasonTextBox_KeyUp);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(101, 107);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(94, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Заблокировать";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// DisableReasonForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(296, 142);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.reasonTextBox);
			this.Controls.Add(this.label1);
			this.Name = "DisableReasonForm";
			this.Text = "DisableReasonForm";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.TextBox reasonTextBox;
    }
}