namespace УП
{
    partial class EditRequestMasterForm
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
            this.labelCompletionDate = new System.Windows.Forms.Label();
            this.dateTimePickerCompletionDate = new System.Windows.Forms.DateTimePicker();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelComments = new System.Windows.Forms.Label();
            this.textBoxComments = new System.Windows.Forms.TextBox();
            this.labelParts = new System.Windows.Forms.Label();
            this.textBoxParts = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelCompletionDate
            // 
            this.labelCompletionDate.AutoSize = true;
            this.labelCompletionDate.Location = new System.Drawing.Point(12, 47);
            this.labelCompletionDate.Name = "labelCompletionDate";
            this.labelCompletionDate.Size = new System.Drawing.Size(174, 16);
            this.labelCompletionDate.TabIndex = 1;
            this.labelCompletionDate.Text = "Дата окончания ремонта:";
            // 
            // dateTimePickerCompletionDate
            // 
            this.dateTimePickerCompletionDate.Location = new System.Drawing.Point(230, 47);
            this.dateTimePickerCompletionDate.Name = "dateTimePickerCompletionDate";
            this.dateTimePickerCompletionDate.Size = new System.Drawing.Size(300, 22);
            this.dateTimePickerCompletionDate.TabIndex = 2;
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonSave.Location = new System.Drawing.Point(230, 232);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(182, 45);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonCancel.Location = new System.Drawing.Point(418, 232);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 45);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelComments
            // 
            this.labelComments.AutoSize = true;
            this.labelComments.Location = new System.Drawing.Point(87, 78);
            this.labelComments.Name = "labelComments";
            this.labelComments.Size = new System.Drawing.Size(99, 16);
            this.labelComments.TabIndex = 5;
            this.labelComments.Text = "Комментарии:";
            this.labelComments.Click += new System.EventHandler(this.labelComments_Click);
            // 
            // textBoxComments
            // 
            this.textBoxComments.Location = new System.Drawing.Point(230, 75);
            this.textBoxComments.Multiline = true;
            this.textBoxComments.Name = "textBoxComments";
            this.textBoxComments.Size = new System.Drawing.Size(300, 66);
            this.textBoxComments.TabIndex = 6;
            // 
            // labelParts
            // 
            this.labelParts.AutoSize = true;
            this.labelParts.Location = new System.Drawing.Point(113, 150);
            this.labelParts.Name = "labelParts";
            this.labelParts.Size = new System.Drawing.Size(73, 16);
            this.labelParts.TabIndex = 7;
            this.labelParts.Text = "Запчасти:";
            // 
            // textBoxParts
            // 
            this.textBoxParts.Location = new System.Drawing.Point(230, 147);
            this.textBoxParts.Multiline = true;
            this.textBoxParts.Name = "textBoxParts";
            this.textBoxParts.Size = new System.Drawing.Size(300, 66);
            this.textBoxParts.TabIndex = 8;
            // 
            // EditRequestMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(597, 321);
            this.Controls.Add(this.textBoxParts);
            this.Controls.Add(this.labelParts);
            this.Controls.Add(this.textBoxComments);
            this.Controls.Add(this.labelComments);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.dateTimePickerCompletionDate);
            this.Controls.Add(this.labelCompletionDate);
            this.Name = "EditRequestMasterForm";
            this.Text = "Редактирование заявки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label labelCompletionDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerCompletionDate;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelComments;
        private System.Windows.Forms.TextBox textBoxComments;
        private System.Windows.Forms.Label labelParts;
        private System.Windows.Forms.TextBox textBoxParts;
    }
}
