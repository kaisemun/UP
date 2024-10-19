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
    public partial class ManagerForm : Form
    {
        private string connectionString = "Server=DESKTOP-17763KV\\SOFYA;Database=rybalchenko;Trusted_Connection=True;";
        private int selectedRequestId;

        public ManagerForm()
        {
            InitializeComponent();
            LoadRequests();
            LoadTechnicians();

            // Подключаем обработчик события CellClick для dataGridViewRequests
            dataGridViewRequests.CellClick += dataGridViewRequests_CellClick;
            buttonExit.Click += buttonExit_Click;
        }

        private void LoadRequests()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT RequestID, HomeTechModel FROM Requests", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridViewRequests.DataSource = dataTable;
            }
        }

        private void LoadTechnicians()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT UserID, FIO FROM Users WHERE UserType = 'Мастер'", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable techTable = new DataTable();
                adapter.Fill(techTable);
                comboBoxTechnicians.DisplayMember = "FIO";
                comboBoxTechnicians.ValueMember = "UserID";
                comboBoxTechnicians.DataSource = techTable;
            }
        }

        private void buttonAssignTechnician_Click(object sender, EventArgs e)
        {
            if (selectedRequestId > 0 && comboBoxTechnicians.SelectedValue != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Requests SET MasterID = @MasterID WHERE RequestID = @RequestID", connection);
                    command.Parameters.AddWithValue("@MasterID", comboBoxTechnicians.SelectedValue);
                    command.Parameters.AddWithValue("@RequestID", selectedRequestId);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Техник успешно назначен.");
                        LoadRequests(); // Обновляем список заявок
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при назначении техника.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заявку и техника.");
            }
        }


        private void buttonExtendDeadline_Click(object sender, EventArgs e)
        {
            if (selectedRequestId > 0)
            {
                DateTime newDeadline = dateTimePickerDeadline.Value;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Requests SET CompletionDate = @CompletionDate WHERE RequestID = @RequestID", connection);
                    command.Parameters.AddWithValue("@CompletionDate", newDeadline);
                    command.Parameters.AddWithValue("@RequestID", selectedRequestId);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Срок выполнения заявки продлён.");
                        LoadRequests(); // Обновляем список заявок
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при продлении срока выполнения.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заявку.");
            }
        }

        private void dataGridViewRequests_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Проверяем, что выбрана строка, а не заголовок
            {
                selectedRequestId = Convert.ToInt32(dataGridViewRequests.Rows[e.RowIndex].Cells["RequestID"].Value);
            }
            else
            {
                selectedRequestId = 0; // Если ничего не выбрано, сбрасываем ID
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