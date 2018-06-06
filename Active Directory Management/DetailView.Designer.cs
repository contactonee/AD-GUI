namespace Active_Directory_Management
{
    partial class DetailView
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
			this.create = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.subdepartmentLabel = new System.Windows.Forms.Label();
			this.subdepartmentCombo = new System.Windows.Forms.ComboBox();
			this.posCombo = new System.Windows.Forms.ComboBox();
			this.internalLabel = new System.Windows.Forms.Label();
			this.divLabel = new System.Windows.Forms.Label();
			this.positionLabel = new System.Windows.Forms.Label();
			this.telCombo = new System.Windows.Forms.ComboBox();
			this.roomCombo = new System.Windows.Forms.ComboBox();
			this.divCombo = new System.Windows.Forms.ComboBox();
			this.roomLabel = new System.Windows.Forms.Label();
			this.departmentLabel = new System.Windows.Forms.Label();
			this.cityCombo = new System.Windows.Forms.ComboBox();
			this.departmentCombo = new System.Windows.Forms.ComboBox();
			this.cityLabel = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.mobileTextBox = new System.Windows.Forms.MaskedTextBox();
			this.nameTranslitTextBox = new System.Windows.Forms.TextBox();
			this.adressLabel = new System.Windows.Forms.Label();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.adressTextBox = new System.Windows.Forms.TextBox();
			this.nameLabel = new System.Windows.Forms.Label();
			this.mobileLabel = new System.Windows.Forms.Label();
			this.surnameTextBox = new System.Windows.Forms.TextBox();
			this.surnameLabel = new System.Windows.Forms.Label();
			this.middleNameLabel = new System.Windows.Forms.Label();
			this.surnameTranslitTextBox = new System.Windows.Forms.TextBox();
			this.middleNameTextBox = new System.Windows.Forms.TextBox();
			this.nameTranslitLabel = new System.Windows.Forms.Label();
			this.surnameTranslitLabel = new System.Windows.Forms.Label();
			this.birthdayLabel = new System.Windows.Forms.Label();
			this.birthdayDatePicker = new System.Windows.Forms.DateTimePicker();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.cdCheck = new System.Windows.Forms.CheckBox();
			this.usbDiskCheck = new System.Windows.Forms.CheckBox();
			this.usbDeviceCheck = new System.Windows.Forms.CheckBox();
			this.cloudCheck = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.expirationDatePicker = new System.Windows.Forms.DateTimePicker();
			this.unlimitedRadio = new System.Windows.Forms.RadioButton();
			this.limitedRadio = new System.Windows.Forms.RadioButton();
			this.internetLabel = new System.Windows.Forms.Label();
			this.internetCombo = new System.Windows.Forms.ComboBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// create
			// 
			this.create.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.create.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.create.Location = new System.Drawing.Point(327, 391);
			this.create.Name = "create";
			this.create.Size = new System.Drawing.Size(131, 42);
			this.create.TabIndex = 18;
			this.create.Text = "Создать пользователя";
			this.create.UseVisualStyleBackColor = true;
			this.create.Click += new System.EventHandler(this.CreateButton);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(760, 373);
			this.tabControl1.TabIndex = 22;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(752, 347);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Информация о сотруднике";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.subdepartmentLabel);
			this.groupBox2.Controls.Add(this.subdepartmentCombo);
			this.groupBox2.Controls.Add(this.posCombo);
			this.groupBox2.Controls.Add(this.internalLabel);
			this.groupBox2.Controls.Add(this.divLabel);
			this.groupBox2.Controls.Add(this.positionLabel);
			this.groupBox2.Controls.Add(this.telCombo);
			this.groupBox2.Controls.Add(this.roomCombo);
			this.groupBox2.Controls.Add(this.divCombo);
			this.groupBox2.Controls.Add(this.roomLabel);
			this.groupBox2.Controls.Add(this.departmentLabel);
			this.groupBox2.Controls.Add(this.cityCombo);
			this.groupBox2.Controls.Add(this.departmentCombo);
			this.groupBox2.Controls.Add(this.cityLabel);
			this.groupBox2.Location = new System.Drawing.Point(6, 170);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(740, 171);
			this.groupBox2.TabIndex = 45;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Информация о сотруднике";
			// 
			// subdepartmentLabel
			// 
			this.subdepartmentLabel.AutoSize = true;
			this.subdepartmentLabel.Location = new System.Drawing.Point(28, 76);
			this.subdepartmentLabel.Name = "subdepartmentLabel";
			this.subdepartmentLabel.Size = new System.Drawing.Size(91, 13);
			this.subdepartmentLabel.TabIndex = 47;
			this.subdepartmentLabel.Text = "Субдепартамент";
			// 
			// subdepartmentCombo
			// 
			this.subdepartmentCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.subdepartmentCombo.FormattingEnabled = true;
			this.subdepartmentCombo.Location = new System.Drawing.Point(125, 73);
			this.subdepartmentCombo.Name = "subdepartmentCombo";
			this.subdepartmentCombo.Size = new System.Drawing.Size(289, 21);
			this.subdepartmentCombo.Sorted = true;
			this.subdepartmentCombo.TabIndex = 46;
			this.subdepartmentCombo.SelectedIndexChanged += new System.EventHandler(this.SubdepartmentCombo_SelectedIndexChanged);
			// 
			// posCombo
			// 
			this.posCombo.FormattingEnabled = true;
			this.posCombo.Location = new System.Drawing.Point(125, 127);
			this.posCombo.Name = "posCombo";
			this.posCombo.Size = new System.Drawing.Size(289, 21);
			this.posCombo.Sorted = true;
			this.posCombo.TabIndex = 45;
			// 
			// internalLabel
			// 
			this.internalLabel.AutoSize = true;
			this.internalLabel.Location = new System.Drawing.Point(420, 49);
			this.internalLabel.Name = "internalLabel";
			this.internalLabel.Size = new System.Drawing.Size(101, 13);
			this.internalLabel.TabIndex = 44;
			this.internalLabel.Text = "Внутренний номер";
			// 
			// divLabel
			// 
			this.divLabel.AutoSize = true;
			this.divLabel.Location = new System.Drawing.Point(81, 103);
			this.divLabel.Name = "divLabel";
			this.divLabel.Size = new System.Drawing.Size(38, 13);
			this.divLabel.TabIndex = 43;
			this.divLabel.Text = "Отдел";
			// 
			// positionLabel
			// 
			this.positionLabel.AutoSize = true;
			this.positionLabel.Location = new System.Drawing.Point(54, 130);
			this.positionLabel.Name = "positionLabel";
			this.positionLabel.Size = new System.Drawing.Size(65, 13);
			this.positionLabel.TabIndex = 42;
			this.positionLabel.Text = "Должность";
			// 
			// telCombo
			// 
			this.telCombo.FormattingEnabled = true;
			this.telCombo.Location = new System.Drawing.Point(527, 46);
			this.telCombo.Name = "telCombo";
			this.telCombo.Size = new System.Drawing.Size(123, 21);
			this.telCombo.Sorted = true;
			this.telCombo.TabIndex = 40;
			// 
			// roomCombo
			// 
			this.roomCombo.FormattingEnabled = true;
			this.roomCombo.Location = new System.Drawing.Point(527, 19);
			this.roomCombo.Name = "roomCombo";
			this.roomCombo.Size = new System.Drawing.Size(123, 21);
			this.roomCombo.Sorted = true;
			this.roomCombo.TabIndex = 39;
			this.roomCombo.TextChanged += new System.EventHandler(this.RoomCombo_TextChanged);
			// 
			// divCombo
			// 
			this.divCombo.FormattingEnabled = true;
			this.divCombo.Location = new System.Drawing.Point(125, 100);
			this.divCombo.Name = "divCombo";
			this.divCombo.Size = new System.Drawing.Size(289, 21);
			this.divCombo.Sorted = true;
			this.divCombo.TabIndex = 38;
			this.divCombo.SelectedIndexChanged += new System.EventHandler(this.DivCombo_SelectedIndexChanged);
			// 
			// roomLabel
			// 
			this.roomLabel.AutoSize = true;
			this.roomLabel.Location = new System.Drawing.Point(472, 22);
			this.roomLabel.Name = "roomLabel";
			this.roomLabel.Size = new System.Drawing.Size(49, 13);
			this.roomLabel.TabIndex = 37;
			this.roomLabel.Text = "Кабинет";
			// 
			// departmentLabel
			// 
			this.departmentLabel.AutoSize = true;
			this.departmentLabel.Enabled = false;
			this.departmentLabel.Location = new System.Drawing.Point(43, 49);
			this.departmentLabel.Name = "departmentLabel";
			this.departmentLabel.Size = new System.Drawing.Size(76, 13);
			this.departmentLabel.TabIndex = 34;
			this.departmentLabel.Text = "Департамент";
			// 
			// cityCombo
			// 
			this.cityCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cityCombo.FormattingEnabled = true;
			this.cityCombo.Location = new System.Drawing.Point(125, 19);
			this.cityCombo.Name = "cityCombo";
			this.cityCombo.Size = new System.Drawing.Size(289, 21);
			this.cityCombo.TabIndex = 33;
			this.cityCombo.SelectedIndexChanged += new System.EventHandler(this.CityCombo_SelectedIndexChanged);
			// 
			// departmentCombo
			// 
			this.departmentCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.departmentCombo.Enabled = false;
			this.departmentCombo.FormattingEnabled = true;
			this.departmentCombo.Location = new System.Drawing.Point(125, 46);
			this.departmentCombo.Name = "departmentCombo";
			this.departmentCombo.Size = new System.Drawing.Size(289, 21);
			this.departmentCombo.Sorted = true;
			this.departmentCombo.TabIndex = 35;
			this.departmentCombo.SelectedIndexChanged += new System.EventHandler(this.DepartmentCombo_SelectedIndexChanged);
			// 
			// cityLabel
			// 
			this.cityLabel.AutoSize = true;
			this.cityLabel.Location = new System.Drawing.Point(71, 22);
			this.cityLabel.Name = "cityLabel";
			this.cityLabel.Size = new System.Drawing.Size(48, 13);
			this.cityLabel.TabIndex = 32;
			this.cityLabel.Text = "Филиал";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.mobileTextBox);
			this.groupBox1.Controls.Add(this.nameTranslitTextBox);
			this.groupBox1.Controls.Add(this.adressLabel);
			this.groupBox1.Controls.Add(this.nameTextBox);
			this.groupBox1.Controls.Add(this.adressTextBox);
			this.groupBox1.Controls.Add(this.nameLabel);
			this.groupBox1.Controls.Add(this.mobileLabel);
			this.groupBox1.Controls.Add(this.surnameTextBox);
			this.groupBox1.Controls.Add(this.surnameLabel);
			this.groupBox1.Controls.Add(this.middleNameLabel);
			this.groupBox1.Controls.Add(this.surnameTranslitTextBox);
			this.groupBox1.Controls.Add(this.middleNameTextBox);
			this.groupBox1.Controls.Add(this.nameTranslitLabel);
			this.groupBox1.Controls.Add(this.surnameTranslitLabel);
			this.groupBox1.Controls.Add(this.birthdayLabel);
			this.groupBox1.Controls.Add(this.birthdayDatePicker);
			this.groupBox1.Location = new System.Drawing.Point(6, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(740, 158);
			this.groupBox1.TabIndex = 44;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Личные данные";
			// 
			// mobileTextBox
			// 
			this.mobileTextBox.Location = new System.Drawing.Point(125, 97);
			this.mobileTextBox.Mask = "+7 (000) 000-0000";
			this.mobileTextBox.Name = "mobileTextBox";
			this.mobileTextBox.Size = new System.Drawing.Size(197, 20);
			this.mobileTextBox.TabIndex = 44;
			// 
			// nameTranslitTextBox
			// 
			this.nameTranslitTextBox.Location = new System.Drawing.Point(453, 19);
			this.nameTranslitTextBox.Name = "nameTranslitTextBox";
			this.nameTranslitTextBox.Size = new System.Drawing.Size(197, 20);
			this.nameTranslitTextBox.TabIndex = 26;
			this.nameTranslitTextBox.TabStop = false;
			this.nameTranslitTextBox.TextChanged += new System.EventHandler(this.nameTranslitTextBox_TextChanged);
			// 
			// adressLabel
			// 
			this.adressLabel.AutoSize = true;
			this.adressLabel.Enabled = false;
			this.adressLabel.Location = new System.Drawing.Point(24, 126);
			this.adressLabel.Name = "adressLabel";
			this.adressLabel.Size = new System.Drawing.Size(95, 13);
			this.adressLabel.TabIndex = 43;
			this.adressLabel.Text = "Домашний адрес";
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(125, 19);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(197, 20);
			this.nameTextBox.TabIndex = 22;
			this.nameTextBox.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);
			// 
			// adressTextBox
			// 
			this.adressTextBox.Enabled = false;
			this.adressTextBox.Location = new System.Drawing.Point(125, 123);
			this.adressTextBox.Name = "adressTextBox";
			this.adressTextBox.Size = new System.Drawing.Size(197, 20);
			this.adressTextBox.TabIndex = 42;
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(90, 22);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(29, 13);
			this.nameLabel.TabIndex = 23;
			this.nameLabel.Text = "Имя";
			// 
			// mobileLabel
			// 
			this.mobileLabel.AutoSize = true;
			this.mobileLabel.Location = new System.Drawing.Point(0, 100);
			this.mobileLabel.Name = "mobileLabel";
			this.mobileLabel.Size = new System.Drawing.Size(119, 13);
			this.mobileLabel.TabIndex = 41;
			this.mobileLabel.Text = "Номер моб. телефона";
			// 
			// surnameTextBox
			// 
			this.surnameTextBox.Location = new System.Drawing.Point(125, 45);
			this.surnameTextBox.Name = "surnameTextBox";
			this.surnameTextBox.Size = new System.Drawing.Size(197, 20);
			this.surnameTextBox.TabIndex = 24;
			this.surnameTextBox.TextChanged += new System.EventHandler(this.SurnameTextBox_TextChanged);
			// 
			// surnameLabel
			// 
			this.surnameLabel.AutoSize = true;
			this.surnameLabel.Location = new System.Drawing.Point(63, 48);
			this.surnameLabel.Name = "surnameLabel";
			this.surnameLabel.Size = new System.Drawing.Size(56, 13);
			this.surnameLabel.TabIndex = 25;
			this.surnameLabel.Text = "Фамилия";
			// 
			// middleNameLabel
			// 
			this.middleNameLabel.AutoSize = true;
			this.middleNameLabel.Location = new System.Drawing.Point(65, 74);
			this.middleNameLabel.Name = "middleNameLabel";
			this.middleNameLabel.Size = new System.Drawing.Size(54, 13);
			this.middleNameLabel.TabIndex = 39;
			this.middleNameLabel.Text = "Отчество";
			// 
			// surnameTranslitTextBox
			// 
			this.surnameTranslitTextBox.Location = new System.Drawing.Point(453, 45);
			this.surnameTranslitTextBox.Name = "surnameTranslitTextBox";
			this.surnameTranslitTextBox.Size = new System.Drawing.Size(197, 20);
			this.surnameTranslitTextBox.TabIndex = 27;
			this.surnameTranslitTextBox.TabStop = false;
			this.surnameTranslitTextBox.TextChanged += new System.EventHandler(this.surnameTranslitTextBox_TextChanged);
			// 
			// middleNameTextBox
			// 
			this.middleNameTextBox.Location = new System.Drawing.Point(125, 71);
			this.middleNameTextBox.Name = "middleNameTextBox";
			this.middleNameTextBox.Size = new System.Drawing.Size(197, 20);
			this.middleNameTextBox.TabIndex = 38;
			// 
			// nameTranslitLabel
			// 
			this.nameTranslitLabel.AutoSize = true;
			this.nameTranslitLabel.Location = new System.Drawing.Point(369, 22);
			this.nameTranslitLabel.Name = "nameTranslitLabel";
			this.nameTranslitLabel.Size = new System.Drawing.Size(78, 13);
			this.nameTranslitLabel.TabIndex = 28;
			this.nameTranslitLabel.Text = "Имя транслит";
			// 
			// surnameTranslitLabel
			// 
			this.surnameTranslitLabel.AutoSize = true;
			this.surnameTranslitLabel.Location = new System.Drawing.Point(342, 48);
			this.surnameTranslitLabel.Name = "surnameTranslitLabel";
			this.surnameTranslitLabel.Size = new System.Drawing.Size(105, 13);
			this.surnameTranslitLabel.TabIndex = 29;
			this.surnameTranslitLabel.Text = "Фамилия транслит";
			// 
			// birthdayLabel
			// 
			this.birthdayLabel.AutoSize = true;
			this.birthdayLabel.Location = new System.Drawing.Point(361, 74);
			this.birthdayLabel.Name = "birthdayLabel";
			this.birthdayLabel.Size = new System.Drawing.Size(86, 13);
			this.birthdayLabel.TabIndex = 30;
			this.birthdayLabel.Text = "Дата рождения";
			// 
			// birthdayDatePicker
			// 
			this.birthdayDatePicker.CustomFormat = " ";
			this.birthdayDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.birthdayDatePicker.Location = new System.Drawing.Point(453, 71);
			this.birthdayDatePicker.MaxDate = new System.DateTime(2018, 5, 29, 0, 0, 0, 0);
			this.birthdayDatePicker.MinDate = new System.DateTime(1850, 1, 1, 0, 0, 0, 0);
			this.birthdayDatePicker.Name = "birthdayDatePicker";
			this.birthdayDatePicker.Size = new System.Drawing.Size(94, 20);
			this.birthdayDatePicker.TabIndex = 31;
			this.birthdayDatePicker.Value = new System.DateTime(1975, 6, 12, 0, 0, 0, 0);
			this.birthdayDatePicker.ValueChanged += new System.EventHandler(this.BirthdayDatePicker_ValueChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.cdCheck);
			this.tabPage2.Controls.Add(this.usbDiskCheck);
			this.tabPage2.Controls.Add(this.usbDeviceCheck);
			this.tabPage2.Controls.Add(this.cloudCheck);
			this.tabPage2.Controls.Add(this.groupBox3);
			this.tabPage2.Controls.Add(this.internetLabel);
			this.tabPage2.Controls.Add(this.internetCombo);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(752, 347);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Опции";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// cdCheck
			// 
			this.cdCheck.Appearance = System.Windows.Forms.Appearance.Button;
			this.cdCheck.Location = new System.Drawing.Point(26, 21);
			this.cdCheck.Name = "cdCheck";
			this.cdCheck.Size = new System.Drawing.Size(168, 28);
			this.cdCheck.TabIndex = 10;
			this.cdCheck.Text = "Доступ к CD/DVD";
			this.cdCheck.UseVisualStyleBackColor = true;
			// 
			// usbDiskCheck
			// 
			this.usbDiskCheck.Appearance = System.Windows.Forms.Appearance.Button;
			this.usbDiskCheck.Location = new System.Drawing.Point(26, 55);
			this.usbDiskCheck.Name = "usbDiskCheck";
			this.usbDiskCheck.Size = new System.Drawing.Size(168, 28);
			this.usbDiskCheck.TabIndex = 11;
			this.usbDiskCheck.Text = "Доступ к USB дискам";
			this.usbDiskCheck.UseVisualStyleBackColor = true;
			// 
			// usbDeviceCheck
			// 
			this.usbDeviceCheck.Appearance = System.Windows.Forms.Appearance.Button;
			this.usbDeviceCheck.Location = new System.Drawing.Point(26, 89);
			this.usbDeviceCheck.Name = "usbDeviceCheck";
			this.usbDeviceCheck.Size = new System.Drawing.Size(168, 28);
			this.usbDeviceCheck.TabIndex = 12;
			this.usbDeviceCheck.Text = "Доступ к USB устройствам";
			this.usbDeviceCheck.UseVisualStyleBackColor = true;
			// 
			// cloudCheck
			// 
			this.cloudCheck.Appearance = System.Windows.Forms.Appearance.Button;
			this.cloudCheck.Enabled = false;
			this.cloudCheck.Location = new System.Drawing.Point(26, 123);
			this.cloudCheck.Name = "cloudCheck";
			this.cloudCheck.Size = new System.Drawing.Size(168, 28);
			this.cloudCheck.TabIndex = 13;
			this.cloudCheck.Text = "Личная папка (Диск K:\\)";
			this.cloudCheck.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.expirationDatePicker);
			this.groupBox3.Controls.Add(this.unlimitedRadio);
			this.groupBox3.Controls.Add(this.limitedRadio);
			this.groupBox3.Location = new System.Drawing.Point(395, 21);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(200, 100);
			this.groupBox3.TabIndex = 8;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Срок действия учетной записи";
			// 
			// expirationDatePicker
			// 
			this.expirationDatePicker.Enabled = false;
			this.expirationDatePicker.Location = new System.Drawing.Point(6, 65);
			this.expirationDatePicker.Name = "expirationDatePicker";
			this.expirationDatePicker.Size = new System.Drawing.Size(137, 20);
			this.expirationDatePicker.TabIndex = 9;
			// 
			// unlimitedRadio
			// 
			this.unlimitedRadio.AutoSize = true;
			this.unlimitedRadio.Location = new System.Drawing.Point(6, 19);
			this.unlimitedRadio.Name = "unlimitedRadio";
			this.unlimitedRadio.Size = new System.Drawing.Size(111, 17);
			this.unlimitedRadio.TabIndex = 6;
			this.unlimitedRadio.TabStop = true;
			this.unlimitedRadio.Text = "Неограниченный";
			this.unlimitedRadio.UseVisualStyleBackColor = true;
			this.unlimitedRadio.CheckedChanged += new System.EventHandler(this.UnlimitedRadio_CheckedChanged);
			// 
			// limitedRadio
			// 
			this.limitedRadio.AutoSize = true;
			this.limitedRadio.Location = new System.Drawing.Point(6, 42);
			this.limitedRadio.Name = "limitedRadio";
			this.limitedRadio.Size = new System.Drawing.Size(76, 17);
			this.limitedRadio.TabIndex = 7;
			this.limitedRadio.TabStop = true;
			this.limitedRadio.Text = "Истекает:";
			this.limitedRadio.UseVisualStyleBackColor = true;
			this.limitedRadio.CheckedChanged += new System.EventHandler(this.LimitedRadio_CheckedChanged);
			// 
			// internetLabel
			// 
			this.internetLabel.AutoSize = true;
			this.internetLabel.Location = new System.Drawing.Point(23, 192);
			this.internetLabel.Name = "internetLabel";
			this.internetLabel.Size = new System.Drawing.Size(104, 13);
			this.internetLabel.TabIndex = 5;
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
			this.internetCombo.Location = new System.Drawing.Point(133, 189);
			this.internetCombo.Name = "internetCombo";
			this.internetCombo.Size = new System.Drawing.Size(121, 21);
			this.internetCombo.TabIndex = 4;
			// 
			// DetailView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 445);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.create);
			this.Name = "DetailView";
			this.Text = "Детали учетной записи";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button create;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label roomLabel;
        private System.Windows.Forms.ComboBox departmentCombo;
        private System.Windows.Forms.Label departmentLabel;
        private System.Windows.Forms.ComboBox cityCombo;
        private System.Windows.Forms.Label cityLabel;
        private System.Windows.Forms.DateTimePicker birthdayDatePicker;
        private System.Windows.Forms.Label birthdayLabel;
        private System.Windows.Forms.Label surnameTranslitLabel;
        private System.Windows.Forms.Label nameTranslitLabel;
        private System.Windows.Forms.TextBox surnameTranslitTextBox;
        private System.Windows.Forms.TextBox nameTranslitTextBox;
        private System.Windows.Forms.Label surnameLabel;
        private System.Windows.Forms.TextBox surnameTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label middleNameLabel;
        private System.Windows.Forms.TextBox middleNameTextBox;
        private System.Windows.Forms.Label mobileLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label adressLabel;
        private System.Windows.Forms.TextBox adressTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label divLabel;
        private System.Windows.Forms.Label positionLabel;
        private System.Windows.Forms.ComboBox telCombo;
        private System.Windows.Forms.ComboBox roomCombo;
        private System.Windows.Forms.ComboBox divCombo;
        private System.Windows.Forms.Label internalLabel;
        private System.Windows.Forms.RadioButton limitedRadio;
        private System.Windows.Forms.RadioButton unlimitedRadio;
        private System.Windows.Forms.Label internetLabel;
        private System.Windows.Forms.ComboBox internetCombo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker expirationDatePicker;
        private System.Windows.Forms.MaskedTextBox mobileTextBox;
        private System.Windows.Forms.ComboBox posCombo;
        private System.Windows.Forms.Label subdepartmentLabel;
        private System.Windows.Forms.ComboBox subdepartmentCombo;
        private System.Windows.Forms.CheckBox cdCheck;
        private System.Windows.Forms.CheckBox usbDiskCheck;
        private System.Windows.Forms.CheckBox usbDeviceCheck;
        private System.Windows.Forms.CheckBox cloudCheck;
    }
}

