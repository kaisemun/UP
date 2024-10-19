namespace УП
{
    partial class RequestFormMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestFormMaster));
            this.dataGridViewRequests = new System.Windows.Forms.DataGridView();
            this.buttonEditRequest = new System.Windows.Forms.Button();
            this.buttonGenerateReport = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.labelMasterName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelAverageRepairTime = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewRequests
            // 
            this.dataGridViewRequests.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataGridViewRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRequests.Location = new System.Drawing.Point(29, 117);
            this.dataGridViewRequests.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewRequests.Name = "dataGridViewRequests";
            this.dataGridViewRequests.RowHeadersWidth = 51;
            this.dataGridViewRequests.Size = new System.Drawing.Size(971, 429);
            this.dataGridViewRequests.TabIndex = 0;
            // 
            // buttonEditRequest
            // 
            this.buttonEditRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonEditRequest.Location = new System.Drawing.Point(1052, 274);
            this.buttonEditRequest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonEditRequest.Name = "buttonEditRequest";
            this.buttonEditRequest.Size = new System.Drawing.Size(198, 50);
            this.buttonEditRequest.TabIndex = 1;
            this.buttonEditRequest.Text = "Редактировать";
            this.buttonEditRequest.UseVisualStyleBackColor = false;
            this.buttonEditRequest.Click += new System.EventHandler(this.buttonEditRequest_Click);
            // 
            // buttonGenerateReport
            // 
            this.buttonGenerateReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonGenerateReport.Location = new System.Drawing.Point(1052, 344);
            this.buttonGenerateReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGenerateReport.Name = "buttonGenerateReport";
            this.buttonGenerateReport.Size = new System.Drawing.Size(198, 49);
            this.buttonGenerateReport.TabIndex = 2;
            this.buttonGenerateReport.Text = "Генерация отчета";
            this.buttonGenerateReport.UseVisualStyleBackColor = false;
            this.buttonGenerateReport.Click += new System.EventHandler(this.buttonGenerateReport_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(97, 85);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(200, 22);
            this.textBoxSearch.TabIndex = 3;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(29, 87);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(47, 16);
            this.labelSearch.TabIndex = 4;
            this.labelSearch.Text = "Поиск";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(1082, 509);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(114, 16);
            this.labelCount.TabIndex = 5;
            this.labelCount.Text = "Показано: 0 из 0";
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonExit.Location = new System.Drawing.Point(24, 5);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(116, 44);
            this.buttonExit.TabIndex = 6;
            this.buttonExit.Text = "Выход";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // labelMasterName
            // 
            this.labelMasterName.AutoSize = true;
            this.labelMasterName.Location = new System.Drawing.Point(1022, 88);
            this.labelMasterName.Name = "labelMasterName";
            this.labelMasterName.Size = new System.Drawing.Size(59, 16);
            this.labelMasterName.TabIndex = 7;
            this.labelMasterName.Text = "Мастер:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1185, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 55);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // labelAverageRepairTime
            // 
            this.labelAverageRepairTime.AutoSize = true;
            this.labelAverageRepairTime.Location = new System.Drawing.Point(1022, 128);
            this.labelAverageRepairTime.Name = "labelAverageRepairTime";
            this.labelAverageRepairTime.Size = new System.Drawing.Size(179, 16);
            this.labelAverageRepairTime.TabIndex = 8;
            this.labelAverageRepairTime.Text = "Среднее время ремонта: 0";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitle.Location = new System.Drawing.Point(520, 14);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(151, 46);
            this.labelTitle.TabIndex = 14;
            this.labelTitle.Text = "Заявки";
            // 
            // RequestFormMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1309, 589);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelAverageRepairTime);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelMasterName);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonGenerateReport);
            this.Controls.Add(this.buttonEditRequest);
            this.Controls.Add(this.dataGridViewRequests);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "RequestFormMaster";
            this.Text = "Исполнение заявок мастером";
            this.Load += new System.EventHandler(this.RequestFormMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridView dataGridViewRequests;
        private System.Windows.Forms.Button buttonEditRequest;
        private System.Windows.Forms.Button buttonGenerateReport;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label labelMasterName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelAverageRepairTime; // Поле для среднего времени ремонта
        private System.Windows.Forms.Label labelTitle;
    }
}