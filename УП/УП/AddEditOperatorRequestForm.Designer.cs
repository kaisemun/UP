namespace УП
{
    partial class AddEditOperatorRequestForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.comboBoxTechType = new System.Windows.Forms.ComboBox();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.comboBoxMaster = new System.Windows.Forms.ComboBox();
            this.comboBoxClient = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxClientPhone = new System.Windows.Forms.TextBox();
            this.labelTechType = new System.Windows.Forms.Label();
            this.labelModel = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelMaster = new System.Windows.Forms.Label();
            this.labelStartDate = new System.Windows.Forms.Label();
            this.labelClientName = new System.Windows.Forms.Label();
            this.labelClientPhone = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxTechType
            // 
            this.comboBoxTechType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTechType.Location = new System.Drawing.Point(221, 20);
            this.comboBoxTechType.Name = "comboBoxTechType";
            this.comboBoxTechType.Size = new System.Drawing.Size(244, 24);
            this.comboBoxTechType.TabIndex = 8;
            // 
            // textBoxModel
            // 
            this.textBoxModel.Location = new System.Drawing.Point(221, 60);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(244, 22);
            this.textBoxModel.TabIndex = 9;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(221, 100);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(244, 22);
            this.textBoxDescription.TabIndex = 10;
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.Location = new System.Drawing.Point(221, 140);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(244, 24);
            this.comboBoxStatus.TabIndex = 11;
            // 
            // comboBoxMaster
            // 
            this.comboBoxMaster.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaster.Location = new System.Drawing.Point(221, 180);
            this.comboBoxMaster.Name = "comboBoxMaster";
            this.comboBoxMaster.Size = new System.Drawing.Size(244, 24);
            this.comboBoxMaster.TabIndex = 12;
            // 
            // comboBoxClient
            // 
            this.comboBoxClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClient.Location = new System.Drawing.Point(221, 260);
            this.comboBoxClient.Name = "comboBoxClient";
            this.comboBoxClient.Size = new System.Drawing.Size(244, 24);
            this.comboBoxClient.TabIndex = 14;
            this.comboBoxClient.SelectedIndexChanged += new System.EventHandler(this.comboBoxClient_SelectedIndexChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonSave.Location = new System.Drawing.Point(221, 348);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(148, 45);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonCancel.Location = new System.Drawing.Point(375, 348);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 45);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(221, 220);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(244, 22);
            this.dateTimePickerStartDate.TabIndex = 13;
            // 
            // textBoxClientPhone
            // 
            this.textBoxClientPhone.Location = new System.Drawing.Point(221, 300);
            this.textBoxClientPhone.Name = "textBoxClientPhone";
            this.textBoxClientPhone.ReadOnly = true;
            this.textBoxClientPhone.Size = new System.Drawing.Size(244, 22);
            this.textBoxClientPhone.TabIndex = 15;
            // 
            // labelTechType
            // 
            this.labelTechType.AutoSize = true;
            this.labelTechType.Location = new System.Drawing.Point(20, 23);
            this.labelTechType.Name = "labelTechType";
            this.labelTechType.Size = new System.Drawing.Size(87, 16);
            this.labelTechType.TabIndex = 0;
            this.labelTechType.Text = "Тип техники";
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Location = new System.Drawing.Point(20, 63);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(57, 16);
            this.labelModel.TabIndex = 1;
            this.labelModel.Text = "Модель";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(20, 103);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(72, 16);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "Описание";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(20, 143);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(53, 16);
            this.labelStatus.TabIndex = 3;
            this.labelStatus.Text = "Статус";
            // 
            // labelMaster
            // 
            this.labelMaster.AutoSize = true;
            this.labelMaster.Location = new System.Drawing.Point(20, 183);
            this.labelMaster.Name = "labelMaster";
            this.labelMaster.Size = new System.Drawing.Size(56, 16);
            this.labelMaster.TabIndex = 4;
            this.labelMaster.Text = "Мастер";
            // 
            // labelStartDate
            // 
            this.labelStartDate.AutoSize = true;
            this.labelStartDate.Location = new System.Drawing.Point(20, 223);
            this.labelStartDate.Name = "labelStartDate";
            this.labelStartDate.Size = new System.Drawing.Size(90, 16);
            this.labelStartDate.TabIndex = 5;
            this.labelStartDate.Text = "Дата начала";
            // 
            // labelClientName
            // 
            this.labelClientName.AutoSize = true;
            this.labelClientName.Location = new System.Drawing.Point(20, 263);
            this.labelClientName.Name = "labelClientName";
            this.labelClientName.Size = new System.Drawing.Size(90, 16);
            this.labelClientName.TabIndex = 6;
            this.labelClientName.Text = "Имя клиента";
            // 
            // labelClientPhone
            // 
            this.labelClientPhone.AutoSize = true;
            this.labelClientPhone.Location = new System.Drawing.Point(12, 306);
            this.labelClientPhone.Name = "labelClientPhone";
            this.labelClientPhone.Size = new System.Drawing.Size(124, 16);
            this.labelClientPhone.TabIndex = 7;
            this.labelClientPhone.Text = "Телефон клиента";
            // 
            // AddEditOperatorRequestForm
            // 
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(566, 410);
            this.Controls.Add(this.labelTechType);
            this.Controls.Add(this.labelModel);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelMaster);
            this.Controls.Add(this.labelStartDate);
            this.Controls.Add(this.labelClientName);
            this.Controls.Add(this.labelClientPhone);
            this.Controls.Add(this.comboBoxTechType);
            this.Controls.Add(this.textBoxModel);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.comboBoxMaster);
            this.Controls.Add(this.dateTimePickerStartDate);
            this.Controls.Add(this.comboBoxClient);
            this.Controls.Add(this.textBoxClientPhone);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Name = "AddEditOperatorRequestForm";
            this.Text = "Заявка";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox comboBoxTechType;
        private System.Windows.Forms.TextBox textBoxModel;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.ComboBox comboBoxMaster;
        private System.Windows.Forms.ComboBox comboBoxClient; // Новый ComboBox для выбора клиента
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.TextBox textBoxClientPhone;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelTechType;
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelMaster;
        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.Label labelClientName;
        private System.Windows.Forms.Label labelClientPhone;
    }
}