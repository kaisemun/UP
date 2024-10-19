namespace УП
{
    partial class OperatorRequestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperatorRequestForm));
            this.dataGridViewRequests = new System.Windows.Forms.DataGridView();
            this.labelCount = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.comboBoxFilterStatus = new System.Windows.Forms.ComboBox();
            this.comboBoxFilterTechType = new System.Windows.Forms.ComboBox();
            this.buttonAddRequest = new System.Windows.Forms.Button();
            this.buttonEditRequest = new System.Windows.Forms.Button();
            this.buttonDeleteRequest = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.labelSearch = new System.Windows.Forms.Label();
            this.labelFilterStatus = new System.Windows.Forms.Label();
            this.labelFilterTechType = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewRequests
            // 
            this.dataGridViewRequests.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataGridViewRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRequests.Location = new System.Drawing.Point(12, 127);
            this.dataGridViewRequests.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewRequests.Name = "dataGridViewRequests";
            this.dataGridViewRequests.RowHeadersWidth = 51;
            this.dataGridViewRequests.Size = new System.Drawing.Size(988, 518);
            this.dataGridViewRequests.TabIndex = 0;
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(1126, 629);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(114, 16);
            this.labelCount.TabIndex = 1;
            this.labelCount.Text = "Показано: 0 из 0";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(65, 93);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(200, 22);
            this.textBoxSearch.TabIndex = 2;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // comboBoxFilterStatus
            // 
            this.comboBoxFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterStatus.FormattingEnabled = true;
            this.comboBoxFilterStatus.Location = new System.Drawing.Point(428, 91);
            this.comboBoxFilterStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxFilterStatus.Name = "comboBoxFilterStatus";
            this.comboBoxFilterStatus.Size = new System.Drawing.Size(160, 24);
            this.comboBoxFilterStatus.TabIndex = 3;
            this.comboBoxFilterStatus.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterStatus_SelectedIndexChanged);
            // 
            // comboBoxFilterTechType
            // 
            this.comboBoxFilterTechType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterTechType.FormattingEnabled = true;
            this.comboBoxFilterTechType.Location = new System.Drawing.Point(840, 91);
            this.comboBoxFilterTechType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxFilterTechType.Name = "comboBoxFilterTechType";
            this.comboBoxFilterTechType.Size = new System.Drawing.Size(160, 24);
            this.comboBoxFilterTechType.TabIndex = 4;
            // 
            // buttonAddRequest
            // 
            this.buttonAddRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonAddRequest.Location = new System.Drawing.Point(1129, 289);
            this.buttonAddRequest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAddRequest.Name = "buttonAddRequest";
            this.buttonAddRequest.Size = new System.Drawing.Size(208, 43);
            this.buttonAddRequest.TabIndex = 5;
            this.buttonAddRequest.Text = "Добавить";
            this.buttonAddRequest.UseVisualStyleBackColor = false;
            this.buttonAddRequest.Click += new System.EventHandler(this.buttonAddRequest_Click);
            // 
            // buttonEditRequest
            // 
            this.buttonEditRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonEditRequest.Location = new System.Drawing.Point(1129, 363);
            this.buttonEditRequest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonEditRequest.Name = "buttonEditRequest";
            this.buttonEditRequest.Size = new System.Drawing.Size(208, 43);
            this.buttonEditRequest.TabIndex = 6;
            this.buttonEditRequest.Text = "Изменить";
            this.buttonEditRequest.UseVisualStyleBackColor = false;
            this.buttonEditRequest.Click += new System.EventHandler(this.buttonEditRequest_Click);
            // 
            // buttonDeleteRequest
            // 
            this.buttonDeleteRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonDeleteRequest.Location = new System.Drawing.Point(1129, 437);
            this.buttonDeleteRequest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDeleteRequest.Name = "buttonDeleteRequest";
            this.buttonDeleteRequest.Size = new System.Drawing.Size(208, 43);
            this.buttonDeleteRequest.TabIndex = 7;
            this.buttonDeleteRequest.Text = "Удалить";
            this.buttonDeleteRequest.UseVisualStyleBackColor = false;
            this.buttonDeleteRequest.Click += new System.EventHandler(this.buttonDeleteRequest_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonExit.Location = new System.Drawing.Point(12, 1);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(104, 41);
            this.buttonExit.TabIndex = 8;
            this.buttonExit.Text = "Выход";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(12, 94);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(47, 16);
            this.labelSearch.TabIndex = 9;
            this.labelSearch.Text = "Поиск";
            this.labelSearch.Click += new System.EventHandler(this.labelSearch_Click);
            // 
            // labelFilterStatus
            // 
            this.labelFilterStatus.AutoSize = true;
            this.labelFilterStatus.Location = new System.Drawing.Point(369, 96);
            this.labelFilterStatus.Name = "labelFilterStatus";
            this.labelFilterStatus.Size = new System.Drawing.Size(53, 16);
            this.labelFilterStatus.TabIndex = 10;
            this.labelFilterStatus.Text = "Статус";
            // 
            // labelFilterTechType
            // 
            this.labelFilterTechType.AutoSize = true;
            this.labelFilterTechType.Location = new System.Drawing.Point(773, 94);
            this.labelFilterTechType.Name = "labelFilterTechType";
            this.labelFilterTechType.Size = new System.Drawing.Size(61, 16);
            this.labelFilterTechType.TabIndex = 11;
            this.labelFilterTechType.Text = "Техника";
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(1056, 41);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(75, 16);
            this.labelUserName.TabIndex = 12;
            this.labelUserName.Text = "Оператор:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1370, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 55);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitle.Location = new System.Drawing.Point(653, 1);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(151, 46);
            this.labelTitle.TabIndex = 14;
            this.labelTitle.Text = "Заявки";
            // 
            // OperatorRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1468, 724);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.labelFilterTechType);
            this.Controls.Add(this.labelFilterStatus);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonDeleteRequest);
            this.Controls.Add(this.buttonEditRequest);
            this.Controls.Add(this.buttonAddRequest);
            this.Controls.Add(this.comboBoxFilterTechType);
            this.Controls.Add(this.comboBoxFilterStatus);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.dataGridViewRequests);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "OperatorRequestForm";
            this.Text = "Управление заявками";
            this.Load += new System.EventHandler(this.OperatorRequestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridView dataGridViewRequests;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.ComboBox comboBoxFilterStatus;
        private System.Windows.Forms.ComboBox comboBoxFilterTechType;
        private System.Windows.Forms.Button buttonAddRequest;
        private System.Windows.Forms.Button buttonEditRequest;
        private System.Windows.Forms.Button buttonDeleteRequest;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.Label labelFilterStatus;
        private System.Windows.Forms.Label labelFilterTechType;
        private System.Windows.Forms.Label labelUserName; // Объявление нового элемента
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelTitle;
    }
}
