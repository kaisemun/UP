using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace УП
{
    public partial class RequestFormMaster : Form
    {
        private string connectionString = "Server=DESKTOP-17763KV\\SOFYA;Database=rybalchenko;Trusted_Connection=True;";
        private int masterId;
        private string masterName;

        public RequestFormMaster(int masterId, string masterName, string role)
        {
            InitializeComponent();
            this.masterId = masterId;
            this.masterName = masterName;
            labelMasterName.Text = $"Мастер: {masterName}";
            LoadRequests();
        }

        private void RequestFormMaster_Load(object sender, EventArgs e)
        {
            LoadRequests();
            UpdateCountLabel();
        }

        private void LoadRequests(string filter = "")
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"
                SELECT 
                    r.RequestID, 
                    r.StartDate, 
                    r.CompletionDate, 
                    tt.TypeName AS HomeTechType, 
                    r.HomeTechModel, 
                    r.ProblemDescription, 
                    rs.StatusName AS RequestStatus, 
                    c.FIO AS ClientName, 
                    c.Phone AS ClientPhone,
                    (SELECT STRING_AGG(com.Message, '; ') 
                     FROM Comments com 
                     WHERE com.RequestID = r.RequestID) AS Comments,
                    (SELECT STRING_AGG(part.PartName, '; ') 
                     FROM RepairParts part 
                     WHERE part.RequestID = r.RequestID) AS Parts
                FROM Requests r
                JOIN TechTypes tt ON r.HomeTechTypeID = tt.TypeID
                JOIN RequestStatuses rs ON r.RequestStatusID = rs.StatusID
                JOIN Users c ON r.ClientID = c.UserID
                WHERE r.MasterID = @MasterID", connection);

                command.Parameters.AddWithValue("@MasterID", this.masterId);

                if (!string.IsNullOrEmpty(filter))
                {
                    command.CommandText += " AND (c.FIO LIKE '%' + @Filter + '%' OR tt.TypeName LIKE '%' + @Filter + '%' OR r.HomeTechModel LIKE '%' + @Filter + '%' OR r.ProblemDescription LIKE '%' + @Filter + '%')";
                    command.Parameters.AddWithValue("@Filter", filter);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataTable.Columns.Add("Priority", typeof(string));
                foreach (DataRow row in dataTable.Rows)
                {
                    row["Priority"] = DeterminePriority(row);
                }

                dataGridViewRequests.DataSource = dataTable;

                SetColumnHeaders();
                UpdateAverageRepairTime(dataTable);
                UpdateCountLabel();
            }
        }

        private void SetColumnHeaders()
        {
            dataGridViewRequests.Columns["RequestID"].HeaderText = "ID заявки";
            dataGridViewRequests.Columns["StartDate"].HeaderText = "Дата начала";
            dataGridViewRequests.Columns["CompletionDate"].HeaderText = "Дата окончания";
            dataGridViewRequests.Columns["HomeTechType"].HeaderText = "Тип техники";
            dataGridViewRequests.Columns["HomeTechModel"].HeaderText = "Модель техники";
            dataGridViewRequests.Columns["ProblemDescription"].HeaderText = "Описание проблемы";
            dataGridViewRequests.Columns["RequestStatus"].HeaderText = "Статус заявки";
            dataGridViewRequests.Columns["ClientName"].HeaderText = "Имя клиента";
            dataGridViewRequests.Columns["ClientPhone"].HeaderText = "Номер телефона";
            dataGridViewRequests.Columns["Comments"].HeaderText = "Комментарии";
            dataGridViewRequests.Columns["Parts"].HeaderText = "Запчасти";
            dataGridViewRequests.Columns["Priority"].HeaderText = "Приоритет";
        }

        private void UpdateAverageRepairTime(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                var totalDays = dataTable.AsEnumerable()
                    .Where(row => !row.IsNull("CompletionDate"))
                    .Sum(row => ((DateTime)row["CompletionDate"] - (DateTime)row["StartDate"]).TotalDays);
                
                var completedRequestsCount = dataTable.AsEnumerable()
                    .Count(row => !row.IsNull("CompletionDate"));

                if (completedRequestsCount > 0)
                {
                    var averageDays = totalDays / completedRequestsCount;
                    labelAverageRepairTime.Text = $"Среднее время ремонта: {averageDays:F2} дней"; // Форматирование до 2 знаков после запятой
                }
                else
                {
                    labelAverageRepairTime.Text = "Среднее время ремонта: 0 дней";
                }
            }
            else
            {
                labelAverageRepairTime.Text = "Среднее время ремонта: 0 дней";
            }
        }

        private string DeterminePriority(DataRow row)
        {
            DateTime startDate = (DateTime)row["StartDate"];
            string status = row["RequestStatus"].ToString();

            if (status == "Новая заявка")
            {
                return "Высокий";
            }
            else if (status == "В процессе ремонта")
            {
                return (DateTime.Now - startDate).TotalDays > 3 ? "Высокий" : "Средний";
            }
            else if (status == "Готова к выдаче")
            {
                return "Низкий";
            }
            return "Низкий";
        }

        private void UpdateCountLabel()
        {
            int displayedRecords = dataGridViewRequests.Rows.Cast<DataGridViewRow>()
                .Count(row => !row.IsNewRow);
            int totalRecords = GetTotalRequestsCount();
            labelCount.Text = $"Показано: {displayedRecords} из {totalRecords}";
        }

        private int GetTotalRequestsCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Requests WHERE MasterID = @MasterId", connection);
                command.Parameters.AddWithValue("@MasterId", this.masterId);
                return (int)command.ExecuteScalar();
            }
        }

        private void buttonEditRequest_Click(object sender, EventArgs e)
        {
            if (dataGridViewRequests.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewRequests.SelectedRows[0];
                int requestId = (int)selectedRow.Cells["RequestID"].Value;

                var editForm = new EditRequestMasterForm(requestId, masterId, masterName);
                editForm.FormClosed += (s, args) => LoadRequests();
                editForm.Show();
            }
            else
            {
                MessageBox.Show("Выберите заявку для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRequests(textBoxSearch.Text);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void buttonGenerateReport_Click(object sender, EventArgs e)
        {
            if (dataGridViewRequests.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewRequests.SelectedRows[0];
                int requestId = (int)selectedRow.Cells["RequestID"].Value;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(@"
                        SELECT 
                            r.RequestID, r.StartDate, r.CompletionDate, 
                            tt.TypeName AS HomeTechType, r.HomeTechModel, 
                            r.ProblemDescription, rs.StatusName AS RequestStatus, 
                            c.FIO AS ClientName, c.Phone AS ClientPhone, 
                            (SELECT STRING_AGG(com.Message, '; ') FROM Comments com WHERE com.RequestID = r.RequestID) AS Comments,
                            (SELECT STRING_AGG(part.PartName, '; ') FROM RepairParts part WHERE part.RequestID = r.RequestID) AS Parts
                        FROM Requests r
                        JOIN TechTypes tt ON r.HomeTechTypeID = tt.TypeID
                        JOIN RequestStatuses rs ON r.RequestStatusID = rs.StatusID
                        JOIN Users c ON r.ClientID = c.UserID
                        WHERE r.RequestID = @RequestID", connection);

                    command.Parameters.AddWithValue("@RequestID", requestId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var reportContent = new StringBuilder();
                            reportContent.AppendLine($"Отчет о заявке #{reader["RequestID"]}");
                            reportContent.AppendLine($"Дата начала: {reader["StartDate"]}");
                            reportContent.AppendLine($"Дата окончания: {reader["CompletionDate"]}");
                            reportContent.AppendLine($"Тип техники: {reader["HomeTechType"]}");
                            reportContent.AppendLine($"Модель техники: {reader["HomeTechModel"]}");
                            reportContent.AppendLine($"Описание проблемы: {reader["ProblemDescription"]}");
                            reportContent.AppendLine($"Статус заявки: {reader["RequestStatus"]}");
                            reportContent.AppendLine($"Клиент: {reader["ClientName"]}");
                            reportContent.AppendLine($"Телефон клиента: {reader["ClientPhone"]}");
                            reportContent.AppendLine($"Комментарии: {reader["Comments"]}");
                            reportContent.AppendLine($"Запчасти: {reader["Parts"]}");

                            SaveReportToFile(reportContent.ToString());
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для создания отчета.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void SaveReportToFile(string reportContent)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files|*.txt";
                saveFileDialog.Title = "Сохранить отчет";
                saveFileDialog.FileName = "Отчет_заявки.txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, reportContent, Encoding.UTF8);
                    MessageBox.Show("Отчет успешно сохранен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}