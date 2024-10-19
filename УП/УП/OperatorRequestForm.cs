using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace УП
{
    public partial class OperatorRequestForm : Form
    {
        private string connectionString = "Server=DESKTOP-17763KV\\SOFYA;Database=rybalchenko;Trusted_Connection=True;";
        private int clientId;
        private string clientName;
        private string role;

        public OperatorRequestForm(int clientId, string clientName, string role)
        {
            InitializeComponent();
            this.clientId = clientId;
            this.clientName = clientName;
            this.role = role;
            labelUserName.Text = $"Оператор: {clientName}";
            this.comboBoxFilterTechType.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterTechType_SelectedIndexChanged);
        }

        private void OperatorRequestForm_Load(object sender, EventArgs e)
        {
            LoadTechTypes();
            LoadStatuses();
            LoadRequests();
            UpdateCountLabel();
        }

        private void LoadRequests(string filter = "")
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"
        SELECT r.RequestID, r.StartDate, r.CompletionDate, tt.TypeName AS HomeTechType, r.HomeTechModel, 
               r.ProblemDescription, rs.StatusName AS RequestStatus, 
               u.FIO AS MasterName, c.FIO AS ClientName, c.Phone AS ClientPhone,
               (SELECT STRING_AGG(com.Message, '; ') 
                FROM Comments com 
                WHERE com.RequestID = r.RequestID) AS Comments,
               (SELECT STRING_AGG(part.PartName, '; ') 
                FROM RepairParts part 
                WHERE part.RequestID = r.RequestID) AS Parts
        FROM Requests r
        JOIN TechTypes tt ON r.HomeTechTypeID = tt.TypeID
        JOIN RequestStatuses rs ON r.RequestStatusID = rs.StatusID
        LEFT JOIN Users u ON r.MasterID = u.UserID
        JOIN Users c ON r.ClientID = c.UserID
        WHERE (c.FIO LIKE '%' + @Filter + '%' 
            OR tt.TypeName LIKE '%' + @Filter + '%' 
            OR r.HomeTechModel LIKE '%' + @Filter + '%' 
            OR r.ProblemDescription LIKE '%' + @Filter + '%')", connection);

                command.Parameters.AddWithValue("@Filter", filter);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Добавляем вычисляемый столбец для приоритета
                dataTable.Columns.Add("Priority", typeof(string));
                foreach (DataRow row in dataTable.Rows)
                {
                    row["Priority"] = DeterminePriority(row);
                }

                dataGridViewRequests.DataSource = dataTable;

                // Устанавливаем заголовки столбцов на русском языке
                dataGridViewRequests.Columns["RequestID"].HeaderText = "ID заявки";
                dataGridViewRequests.Columns["StartDate"].HeaderText = "Дата начала";
                dataGridViewRequests.Columns["CompletionDate"].HeaderText = "Дата окончания";
                dataGridViewRequests.Columns["HomeTechType"].HeaderText = "Тип техники";
                dataGridViewRequests.Columns["HomeTechModel"].HeaderText = "Модель техники";
                dataGridViewRequests.Columns["ProblemDescription"].HeaderText = "Описание проблемы";
                dataGridViewRequests.Columns["RequestStatus"].HeaderText = "Статус заявки";
                dataGridViewRequests.Columns["MasterName"].HeaderText = "Имя мастера";
                dataGridViewRequests.Columns["ClientName"].HeaderText = "Имя клиента";
                dataGridViewRequests.Columns["ClientPhone"].HeaderText = "Номер телефона";
                dataGridViewRequests.Columns["Priority"].HeaderText = "Приоритет";
                dataGridViewRequests.Columns["Comments"].HeaderText = "Комментарии";
                dataGridViewRequests.Columns["Parts"].HeaderText = "Запчасти"; // Новый столбец для запчастей

                UpdateCountLabel();
            }
        }

        private string DeterminePriority(DataRow row)
        {
            // Получаем данные заявки из строки
            DateTime startDate = (DateTime)row["StartDate"];
            string status = row["RequestStatus"].ToString();

            // Устанавливаем приоритет на основе времени и статуса заявки
            if (status == "Новая заявка")
            {
                return "Высокий"; // Новый запрос - высокий приоритет
            }
            else if (status == "В процессе ремонта")
            {
                return (DateTime.Now - startDate).TotalDays > 3 ? "Высокий" : "Средний"; // Зависит от времени с момента создания
            }
            else if (status == "Готова к выдаче")
            {
                return "Низкий"; // Готова к выдаче - низкий приоритет
            }
            return "Низкий"; // Все остальные заявки
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
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Requests", connection);
                return (int)command.ExecuteScalar();
            }
        }

        private void buttonAddRequest_Click(object sender, EventArgs e)
        {
            var addEditForm = new AddEditOperatorRequestForm(clientId); // передаем clientId
            addEditForm.FormClosed += (s, args) => LoadRequests();
            addEditForm.Show();
        }

        private void buttonEditRequest_Click(object sender, EventArgs e)
        {
            if (dataGridViewRequests.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewRequests.SelectedRows[0];

                // Проверяем, что в выбранной строке есть значение в столбце "RequestID"
                if (selectedRow.Cells["RequestID"].Value != null && selectedRow.Cells["RequestID"].Value != DBNull.Value)
                {
                    int requestId = (int)selectedRow.Cells["RequestID"].Value;

                    var addEditForm = new AddEditOperatorRequestForm(clientId, requestId);
                    addEditForm.FormClosed += (s, args) => LoadRequests();
                    addEditForm.Show();
                }
                else
                {
                    MessageBox.Show("Выберите корректную заявку для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            private void buttonDeleteRequest_Click(object sender, EventArgs e)
        {
            if (dataGridViewRequests.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewRequests.SelectedRows[0];
                int requestId = (int)selectedRow.Cells["RequestID"].Value;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Requests WHERE RequestID = @RequestID", connection);
                    command.Parameters.AddWithValue("@RequestID", requestId);
                    command.ExecuteNonQuery();
                }
                LoadRequests();
            }
            else
            {
                MessageBox.Show("Выберите заявку для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxFilterTechType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void comboBoxFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            string filter = textBoxSearch.Text;
            var selectedStatusItem = comboBoxFilterStatus.SelectedItem;
            int? statusId = selectedStatusItem != null && ((dynamic)selectedStatusItem).Value != null ? (int?)((dynamic)selectedStatusItem).Value : null;

            var selectedTechTypeItem = comboBoxFilterTechType.SelectedItem;
            int? techTypeId = selectedTechTypeItem != null && ((dynamic)selectedTechTypeItem).Value != null ? (int?)((dynamic)selectedTechTypeItem).Value : null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"
            SELECT r.RequestID, r.StartDate, r.CompletionDate, tt.TypeName AS HomeTechType, r.HomeTechModel, 
                   r.ProblemDescription, rs.StatusName AS RequestStatus, 
                   u.FIO AS MasterName, c.FIO AS ClientName, c.Phone AS ClientPhone,
                   (SELECT STRING_AGG(com.Message, '; ') 
                    FROM Comments com 
                    WHERE com.RequestID = r.RequestID) AS Comments,
                   (SELECT STRING_AGG(part.PartName, '; ') 
                    FROM RepairParts part 
                    WHERE part.RequestID = r.RequestID) AS Parts
            FROM Requests r
            JOIN TechTypes tt ON r.HomeTechTypeID = tt.TypeID
            JOIN RequestStatuses rs ON r.RequestStatusID = rs.StatusID
            LEFT JOIN Users u ON r.MasterID = u.UserID
            JOIN Users c ON r.ClientID = c.UserID
            WHERE (@Filter = '' OR c.FIO LIKE '%' + @Filter + '%' 
                OR tt.TypeName LIKE '%' + @Filter + '%' 
                OR r.HomeTechModel LIKE '%' + @Filter + '%' 
                OR r.ProblemDescription LIKE '%' + @Filter + '%')
            AND (@StatusId IS NULL OR r.RequestStatusID = @StatusId)
            AND (@TechTypeId IS NULL OR r.HomeTechTypeID = @TechTypeId)", connection);

                command.Parameters.AddWithValue("@Filter", filter);
                command.Parameters.AddWithValue("@StatusId", (object)statusId ?? DBNull.Value);
                command.Parameters.AddWithValue("@TechTypeId", (object)techTypeId ?? DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataTable.Columns.Add("Priority", typeof(string));
                foreach (DataRow row in dataTable.Rows)
                {
                    row["Priority"] = DeterminePriority(row);
                }

                dataGridViewRequests.DataSource = dataTable;
                UpdateCountLabel();
            }
        }

        private void LoadTechTypes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM TechTypes", connection);
                SqlDataReader reader = command.ExecuteReader();

                // Добавляем вариант "Вся техника" с значением `null`
                comboBoxFilterTechType.Items.Add(new { Text = "Вся техника", Value = (int?)null });

                // Загружаем остальные типы техники
                while (reader.Read())
                {
                    comboBoxFilterTechType.Items.Add(new { Text = reader["TypeName"].ToString(), Value = (int)reader["TypeID"] });
                }
                comboBoxFilterTechType.DisplayMember = "Text";
                comboBoxFilterTechType.ValueMember = "Value";

                // Устанавливаем "Вся техника" как выбранный элемент по умолчанию
                comboBoxFilterTechType.SelectedIndex = 0;
            }
        }

        private void LoadStatuses()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM RequestStatuses", connection);
                SqlDataReader reader = command.ExecuteReader();

                comboBoxFilterStatus.Items.Add(new { Text = "Все", Value = (int?)null });
                while (reader.Read())
                {
                    comboBoxFilterStatus.Items.Add(new { Text = reader["StatusName"].ToString(), Value = (int)reader["StatusID"] });
                }
                comboBoxFilterStatus.DisplayMember = "Text";
                comboBoxFilterStatus.ValueMember = "Value";
                comboBoxFilterStatus.SelectedIndex = 0;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void labelSearch_Click(object sender, EventArgs e)
        {

        }
    }
}