using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QRCoder;

namespace УП
{
    public partial class RequestForm : Form
    {
        private string connectionString = "Server=DESKTOP-17763KV\\SOFYA;Database=rybalchenko;Trusted_Connection=True;";
        private int clientId;
        private string clientName;
        private string clientRole;

        public RequestForm(int clientId, string clientName, string clientRole)
        {
            InitializeComponent();
            this.clientId = clientId;
            this.clientName = clientName;
            this.clientRole = clientRole;
            labelFIO.Text = clientName;
            labelRole.Text = clientRole;
        }

        private void RequestForm_Load(object sender, EventArgs e)
        {
            LoadStatuses();
            LoadRequests();
            UpdateCountLabel();

            dataGridViewRequests.SelectionChanged += (s, args) => UpdateButtonState();

            // Вызываем метод для инициализации состояния кнопок
            UpdateButtonState();
            // Проверка и блокировка кнопок редактирования и удаления
            foreach (DataGridViewRow row in dataGridViewRequests.Rows)
            {
                string requestStatus = row.Cells["RequestStatus"].Value.ToString();
                if (requestStatus == "В процессе ремонта" || requestStatus == "Готова к выдаче")
                {
                    buttonEditRequest.Enabled = false;
                    buttonDeleteRequest.Enabled = false;
                    break;
                }
            }
        }


        private void LoadRequests(string filter = "")
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"
                SELECT r.RequestID, r.StartDate, tt.TypeName AS HomeTechType, r.HomeTechModel, 
                       r.ProblemDescription, rs.StatusName AS RequestStatus, 
                       u.FIO AS ClientName, u.Phone AS ClientPhone
                FROM Requests r
                JOIN TechTypes tt ON r.HomeTechTypeID = tt.TypeID
                JOIN RequestStatuses rs ON r.RequestStatusID = rs.StatusID
                JOIN Users u ON r.ClientID = u.UserID
                WHERE r.ClientID = @ClientId
                AND (u.FIO LIKE '%' + @Filter + '%' 
                    OR tt.TypeName LIKE '%' + @Filter + '%' 
                    OR r.HomeTechModel LIKE '%' + @Filter + '%' 
                    OR r.ProblemDescription LIKE '%' + @Filter + '%')", connection);

                command.Parameters.AddWithValue("@ClientId", this.clientId);
                command.Parameters.AddWithValue("@Filter", filter);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridViewRequests.DataSource = dataTable;

                // Переводим заголовки столбцов на русский
                dataGridViewRequests.Columns["RequestID"].HeaderText = "ID заявки";
                dataGridViewRequests.Columns["StartDate"].HeaderText = "Дата начала";
                dataGridViewRequests.Columns["HomeTechType"].HeaderText = "Тип техники";
                dataGridViewRequests.Columns["HomeTechModel"].HeaderText = "Модель техники";
                dataGridViewRequests.Columns["ProblemDescription"].HeaderText = "Описание проблемы";
                dataGridViewRequests.Columns["RequestStatus"].HeaderText = "Статус заявки";
                dataGridViewRequests.Columns["ClientName"].HeaderText = "Имя клиента";
                dataGridViewRequests.Columns["ClientPhone"].HeaderText = "Номер телефона";

                UpdateCountLabel();
            }
        }

        private void UpdateCountLabel()
        {
            // Считаем только строки, которые содержат данные, исключая последнюю пустую строку
            int displayedRecords = dataGridViewRequests.Rows.Cast<DataGridViewRow>()
                                        .Count(row => !row.IsNewRow);
            int totalRecords = GetTotalRequestsCount();

            // Обновляем текст метки с актуальными значениями
            labelCount.Text = $"Показано: {displayedRecords} из {totalRecords}";
        }

        private int GetTotalRequestsCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Requests WHERE ClientID = @ClientId", connection);
                command.Parameters.AddWithValue("@ClientId", this.clientId);
                return (int)command.ExecuteScalar();
            }
        }

        private void buttonAddRequest_Click(object sender, EventArgs e)
        {
            var addEditForm = new AddEditRequestForm(null, clientName);
            addEditForm.FormClosed += (s, args) => LoadRequests();
            addEditForm.Show();
        }

        private void buttonEditRequest_Click(object sender, EventArgs e)
        {
            if (dataGridViewRequests.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewRequests.SelectedRows[0];
                int requestId = (int)selectedRow.Cells["RequestID"].Value;
                string requestStatus = selectedRow.Cells["RequestStatus"].Value.ToString();

                if (requestStatus != "Новая заявка")
                {
                    MessageBox.Show("Редактирование доступно только для заявок со статусом 'Новая заявка'.",
                                    "Доступ ограничен", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var addEditForm = new AddEditRequestForm(requestId, clientName);
                addEditForm.FormClosed += (s, args) => LoadRequests();
                addEditForm.Show();
            }
            else
            {
                MessageBox.Show("Выберите заявку для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonDeleteRequest_Click(object sender, EventArgs e)
        {
            if (dataGridViewRequests.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewRequests.SelectedRows[0];
                int requestId = (int)selectedRow.Cells["RequestID"].Value;
                string requestStatus = selectedRow.Cells["RequestStatus"].Value.ToString();

                if (requestStatus != "Новая заявка")
                {
                    MessageBox.Show("Удаление доступно только для заявок со статусом 'Новая заявка'.",
                                    "Доступ ограничен", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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
                MessageBox.Show("Выберите заявку для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridViewRequests_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int requestId = (int)dataGridViewRequests.Rows[e.RowIndex].Cells["RequestID"].Value;

                AddEditRequestForm editForm = new AddEditRequestForm(requestId, clientName);
                editForm.ShowDialog();
                LoadRequests();
            }
        }

        private void UpdateButtonState()
        {
            if (dataGridViewRequests.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewRequests.SelectedRows[0];
                string requestStatus = selectedRow.Cells["RequestStatus"].Value.ToString();

                // Разблокировка кнопок только для статуса "новая заявка"
                buttonEditRequest.Enabled = requestStatus == "Новая заявка";
                buttonDeleteRequest.Enabled = requestStatus == "Новая заявка";
            }
            else
            {
                // Блокируем кнопки, если нет выбранной строки
                buttonEditRequest.Enabled = false;
                buttonDeleteRequest.Enabled = false;
            }
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
            var selectedItem = comboBoxFilterStatus.SelectedItem;
            int? statusId = selectedItem != null && ((dynamic)selectedItem).Value != null ? (int?)((dynamic)selectedItem).Value : null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(@"
                SELECT r.RequestID, r.StartDate, tt.TypeName AS HomeTechType, r.HomeTechModel, 
                       r.ProblemDescription, rs.StatusName AS RequestStatus, 
                       u.FIO AS ClientName
                FROM Requests r
                JOIN TechTypes tt ON r.HomeTechTypeID = tt.TypeID
                JOIN RequestStatuses rs ON r.RequestStatusID = rs.StatusID
                JOIN Users u ON r.ClientID = u.UserID
                WHERE r.ClientID = @ClientId
                AND (@Filter = '' OR u.FIO LIKE '%' + @Filter + '%' 
                    OR tt.TypeName LIKE '%' + @Filter + '%' 
                    OR r.HomeTechModel LIKE '%' + @Filter + '%' 
                    OR r.ProblemDescription LIKE '%' + @Filter + '%')
                AND (@StatusId IS NULL OR r.RequestStatusID = @StatusId)", connection);

                command.Parameters.AddWithValue("@ClientId", this.clientId);
                command.Parameters.AddWithValue("@Filter", filter);
                command.Parameters.AddWithValue("@StatusId", (object)statusId ?? DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridViewRequests.DataSource = dataTable;

                UpdateCountLabel();
            }
        }

        private void LoadStatuses()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM RequestStatuses", connection);
                SqlDataReader reader = command.ExecuteReader();

                comboBoxFilterStatus.Items.Add(new { Text = "Все записи", Value = (int?)null });

                while (reader.Read())
                {
                    comboBoxFilterStatus.Items.Add(new { Text = reader["StatusName"].ToString(), Value = (int)reader["StatusID"] });
                }
                comboBoxFilterStatus.DisplayMember = "Text";
                comboBoxFilterStatus.ValueMember = "Value";

                comboBoxFilterStatus.SelectedIndex = 0;
            }
        }

        private void buttonGenerateQRCode_Click(object sender, EventArgs e)
        {
            try
            {
                // Ссылка на Google Forms для обратной связи
                string url = "https://docs.google.com/forms/d/e/1FAIpQLScgxjOu26hJL2RKJ5LakJUDedn-1USndtQsTw0UMBfKz2SIeQ/viewform?usp=sf_link";

                // Генерация QR-кода для указанной ссылки
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                // Отображение QR-кода в PictureBox
                pictureBoxQRCode.Image = qrCode.GetGraphic(20);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при генерации QR-кода: " + ex.Message);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}