using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Linq;

namespace УП
{
    partial class RequestForm
    {
        private System.ComponentModel.IContainer components = null;

        // Элементы управления
        private System.Windows.Forms.DataGridView dataGridViewRequests;
        private System.Windows.Forms.Button buttonAddRequest;
        private System.Windows.Forms.Button buttonEditRequest;
        private System.Windows.Forms.Button buttonDeleteRequest;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.ComboBox comboBoxFilterStatus;
        private System.Windows.Forms.Label labelFilterStatus;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label labelFIO;
        private System.Windows.Forms.Label labelRole;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonGenerateQRCode;
        private System.Windows.Forms.PictureBox pictureBoxQRCode;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestForm));
            this.dataGridViewRequests = new System.Windows.Forms.DataGridView();
            this.buttonAddRequest = new System.Windows.Forms.Button();
            this.buttonEditRequest = new System.Windows.Forms.Button();
            this.buttonDeleteRequest = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.comboBoxFilterStatus = new System.Windows.Forms.ComboBox();
            this.labelFilterStatus = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.labelFIO = new System.Windows.Forms.Label();
            this.labelRole = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonGenerateQRCode = new System.Windows.Forms.Button();
            this.pictureBoxQRCode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewRequests
            // 
            this.dataGridViewRequests.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataGridViewRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRequests.Location = new System.Drawing.Point(10, 155);
            this.dataGridViewRequests.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewRequests.Name = "dataGridViewRequests";
            this.dataGridViewRequests.RowHeadersWidth = 51;
            this.dataGridViewRequests.Size = new System.Drawing.Size(1115, 406);
            this.dataGridViewRequests.TabIndex = 0;
            this.dataGridViewRequests.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRequests_CellDoubleClick);
            // 
            // buttonAddRequest
            // 
            this.buttonAddRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonAddRequest.Location = new System.Drawing.Point(1175, 126);
            this.buttonAddRequest.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddRequest.Name = "buttonAddRequest";
            this.buttonAddRequest.Size = new System.Drawing.Size(213, 41);
            this.buttonAddRequest.TabIndex = 1;
            this.buttonAddRequest.Text = "Добавить";
            this.buttonAddRequest.UseVisualStyleBackColor = false;
            this.buttonAddRequest.Click += new System.EventHandler(this.buttonAddRequest_Click);
            // 
            // buttonEditRequest
            // 
            this.buttonEditRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonEditRequest.Location = new System.Drawing.Point(1175, 175);
            this.buttonEditRequest.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEditRequest.Name = "buttonEditRequest";
            this.buttonEditRequest.Size = new System.Drawing.Size(213, 41);
            this.buttonEditRequest.TabIndex = 2;
            this.buttonEditRequest.Text = "Редактировать";
            this.buttonEditRequest.UseVisualStyleBackColor = false;
            this.buttonEditRequest.Click += new System.EventHandler(this.buttonEditRequest_Click);
            // 
            // buttonDeleteRequest
            // 
            this.buttonDeleteRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonDeleteRequest.Location = new System.Drawing.Point(1175, 224);
            this.buttonDeleteRequest.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDeleteRequest.Name = "buttonDeleteRequest";
            this.buttonDeleteRequest.Size = new System.Drawing.Size(213, 41);
            this.buttonDeleteRequest.TabIndex = 3;
            this.buttonDeleteRequest.Text = "Удалить";
            this.buttonDeleteRequest.UseVisualStyleBackColor = false;
            this.buttonDeleteRequest.Click += new System.EventHandler(this.buttonDeleteRequest_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(10, 123);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(377, 22);
            this.textBoxSearch.TabIndex = 4;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(7, 103);
            this.labelSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(50, 16);
            this.labelSearch.TabIndex = 5;
            this.labelSearch.Text = "Поиск:";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(1011, 565);
            this.labelCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(114, 16);
            this.labelCount.TabIndex = 6;
            this.labelCount.Text = "Показано: 0 из 0";
            // 
            // comboBoxFilterStatus
            // 
            this.comboBoxFilterStatus.FormattingEnabled = true;
            this.comboBoxFilterStatus.Location = new System.Drawing.Point(965, 126);
            this.comboBoxFilterStatus.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxFilterStatus.Name = "comboBoxFilterStatus";
            this.comboBoxFilterStatus.Size = new System.Drawing.Size(160, 24);
            this.comboBoxFilterStatus.TabIndex = 7;
            this.comboBoxFilterStatus.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterStatus_SelectedIndexChanged);
            // 
            // labelFilterStatus
            // 
            this.labelFilterStatus.AutoSize = true;
            this.labelFilterStatus.Location = new System.Drawing.Point(901, 129);
            this.labelFilterStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFilterStatus.Name = "labelFilterStatus";
            this.labelFilterStatus.Size = new System.Drawing.Size(56, 16);
            this.labelFilterStatus.TabIndex = 8;
            this.labelFilterStatus.Text = "Статус:";
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonExit.Location = new System.Drawing.Point(10, 10);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(100, 40);
            this.buttonExit.TabIndex = 9;
            this.buttonExit.Text = "Выход";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // labelFIO
            // 
            this.labelFIO.AutoSize = true;
            this.labelFIO.Location = new System.Drawing.Point(1087, 22);
            this.labelFIO.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFIO.Name = "labelFIO";
            this.labelFIO.Size = new System.Drawing.Size(161, 16);
            this.labelFIO.TabIndex = 10;
            this.labelFIO.Text = "Фамилия Имя Отчество";
            // 
            // labelRole
            // 
            this.labelRole.AutoSize = true;
            this.labelRole.Location = new System.Drawing.Point(1172, 50);
            this.labelRole.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRole.Name = "labelRole";
            this.labelRole.Size = new System.Drawing.Size(39, 16);
            this.labelRole.TabIndex = 11;
            this.labelRole.Text = "Роль";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1312, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 55);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitle.Location = new System.Drawing.Point(538, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(151, 46);
            this.labelTitle.TabIndex = 13;
            this.labelTitle.Text = "Заявки";
            // 
            // buttonGenerateQRCode
            // 
            this.buttonGenerateQRCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonGenerateQRCode.Location = new System.Drawing.Point(1175, 500);
            this.buttonGenerateQRCode.Margin = new System.Windows.Forms.Padding(4);
            this.buttonGenerateQRCode.Name = "buttonGenerateQRCode";
            this.buttonGenerateQRCode.Size = new System.Drawing.Size(213, 61);
            this.buttonGenerateQRCode.TabIndex = 14;
            this.buttonGenerateQRCode.Text = "Сгенерировать QR-код для отзывов";
            this.buttonGenerateQRCode.UseVisualStyleBackColor = false;
            this.buttonGenerateQRCode.Click += new System.EventHandler(this.buttonGenerateQRCode_Click);
            // 
            // pictureBoxQRCode
            // 
            this.pictureBoxQRCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxQRCode.Location = new System.Drawing.Point(1175, 279);
            this.pictureBoxQRCode.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxQRCode.Name = "pictureBoxQRCode";
            this.pictureBoxQRCode.Size = new System.Drawing.Size(213, 213);
            this.pictureBoxQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxQRCode.TabIndex = 15;
            this.pictureBoxQRCode.TabStop = false;
            // 
            // RequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1423, 655);
            this.Controls.Add(this.labelRole);
            this.Controls.Add(this.labelFIO);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.labelFilterStatus);
            this.Controls.Add(this.comboBoxFilterStatus);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonDeleteRequest);
            this.Controls.Add(this.buttonEditRequest);
            this.Controls.Add(this.buttonAddRequest);
            this.Controls.Add(this.dataGridViewRequests);
            this.Controls.Add(this.buttonGenerateQRCode);
            this.Controls.Add(this.pictureBoxQRCode);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RequestForm";
            this.Text = "Заявки на ремонт";
            this.Load += new System.EventHandler(this.RequestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}