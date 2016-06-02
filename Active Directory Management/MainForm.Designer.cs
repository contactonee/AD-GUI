namespace Active_Directory_Management
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.first = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.last = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.firstTranslit = new System.Windows.Forms.TextBox();
            this.lastTranslit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.birthday = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.city = new System.Windows.Forms.ComboBox();
            this.department = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.create = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.settingsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(200, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(340, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Создание новой учетной записи";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // first
            // 
            this.first.Location = new System.Drawing.Point(104, 61);
            this.first.Name = "first";
            this.first.Size = new System.Drawing.Size(197, 20);
            this.first.TabIndex = 1;
            this.first.TextChanged += new System.EventHandler(this.first_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Имя";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // last
            // 
            this.last.Location = new System.Drawing.Point(104, 87);
            this.last.Name = "last";
            this.last.Size = new System.Drawing.Size(197, 20);
            this.last.TabIndex = 3;
            this.last.TextChanged += new System.EventHandler(this.last_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Фамилия";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // firstTranslit
            // 
            this.firstTranslit.Location = new System.Drawing.Point(551, 57);
            this.firstTranslit.Name = "firstTranslit";
            this.firstTranslit.Size = new System.Drawing.Size(221, 20);
            this.firstTranslit.TabIndex = 5;
            this.firstTranslit.TabStop = false;
            // 
            // lastTranslit
            // 
            this.lastTranslit.Location = new System.Drawing.Point(554, 83);
            this.lastTranslit.Name = "lastTranslit";
            this.lastTranslit.Size = new System.Drawing.Size(218, 20);
            this.lastTranslit.TabIndex = 6;
            this.lastTranslit.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(467, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Имя транслит";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(440, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Фамилия транслит";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Дата рождения";
            // 
            // birthday
            // 
            this.birthday.Location = new System.Drawing.Point(104, 154);
            this.birthday.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.birthday.MinDate = new System.DateTime(1850, 1, 1, 0, 0, 0, 0);
            this.birthday.Name = "birthday";
            this.birthday.Size = new System.Drawing.Size(137, 20);
            this.birthday.TabIndex = 10;
            this.birthday.Value = new System.DateTime(2016, 6, 1, 0, 0, 0, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(268, 160);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Филиал";
            // 
            // city
            // 
            this.city.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.city.FormattingEnabled = true;
            this.city.Items.AddRange(new object[] {
            "Актау",
            "Алматы",
            "Астана",
            "Москва",
            "Уральск"});
            this.city.Location = new System.Drawing.Point(322, 157);
            this.city.Name = "city";
            this.city.Size = new System.Drawing.Size(95, 21);
            this.city.Sorted = true;
            this.city.TabIndex = 12;
            // 
            // department
            // 
            this.department.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.department.FormattingEnabled = true;
            this.department.Items.AddRange(new object[] {
            "Департамент 1",
            "Департамент 2",
            "Департамент 3",
            "Департамент 4",
            "Департамент 5",
            "Департамент 6",
            "Департамент 7"});
            this.department.Location = new System.Drawing.Point(526, 157);
            this.department.Name = "department";
            this.department.Size = new System.Drawing.Size(95, 21);
            this.department.Sorted = true;
            this.department.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(444, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Департамент";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(697, 410);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Выход";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // create
            // 
            this.create.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.create.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.create.Location = new System.Drawing.Point(349, 391);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(131, 42);
            this.create.TabIndex = 18;
            this.create.Text = "СОЗДАТЬ!";
            this.create.UseVisualStyleBackColor = true;
            this.create.Click += new System.EventHandler(this.create_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(705, 157);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(67, 21);
            this.comboBox1.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(650, 160);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Кабинет";
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(12, 410);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(75, 23);
            this.settingsButton.TabIndex = 21;
            this.settingsButton.Text = "Настройки";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 445);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.create);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.department);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.city);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.birthday);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lastTranslit);
            this.Controls.Add(this.firstTranslit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.last);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.first);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox first;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox last;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox firstTranslit;
        private System.Windows.Forms.TextBox lastTranslit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker birthday;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox city;
        private System.Windows.Forms.ComboBox department;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button create;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button settingsButton;
    }
}

