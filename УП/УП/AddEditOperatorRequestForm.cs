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
using System.Windows.Forms.VisualStyles;

namespace УП
{
    public partial class AddEditOperatorRequestForm : Form
    {
        private string connectionString = "Server=DESKTOP-17763KV\\SOFYA;Database=rybalchenko;Trusted_Connection=True;";
        private int? requestId;
        private int clientId;

        public AddEditOperatorRequestForm(int clientId, int? requestId = null)
        {
            InitializeComponent();
            this.clientId = clientId;
            this.requestId = requestId;
            LoadTechTypes();
            LoadStatuses();
            LoadMasters();
            LoadClients(); // Загрузка списка клиентов

            if (requestId.HasValue)
            {
                LoadRequestData(requestId.Value);
                this.Text = "Редактирование заявки";
            }
            else
            {
                this.Text = "Добавление заявки";
            }
        }

        private void LoadTechTypes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM TechTypes", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable techTypes = new DataTable();
                adapter.Fill(techTypes);

                comboBoxTechType.DisplayMember = "TypeName";
                comboBoxTechType.ValueMember = "TypeID";
                comboBoxTechType.DataSource = techTypes;
            }
        }

        private void LoadStatuses()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM RequestStatuses", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable statuses = new DataTable();
                adapter.Fill(statuses);

                comboBoxStatus.DisplayMember = "StatusName";
                comboBoxStatus.ValueMember = "StatusID";
                comboBoxStatus.DataSource = statuses;
            }
        }

        private void LoadMasters()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT UserID, FIO FROM Users WHERE UserType = 'Мастер'", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable masters = new DataTable();
                adapter.Fill(masters);

                comboBoxMaster.DisplayMember = "FIO";
                comboBoxMaster.ValueMember = "UserID";
                comboBoxMaster.DataSource = masters;
                comboBoxMaster.SelectedIndex = -1; // Default to no selection
            }
        }

        private void LoadClients()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT UserID, FIO, Phone FROM Users WHERE UserType = 'Заказчик'", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable clients = new DataTable();
                adapter.Fill(clients);

                comboBoxClient.DisplayMember = "FIO";
                comboBoxClient.ValueMember = "UserID";
                comboBoxClient.DataSource = clients;
                comboBoxClient.SelectedIndex = -1;
            }
        }

        private void LoadRequestData(int requestId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"
                    SELECT r.HomeTechTypeID, r.HomeTechModel, r.ProblemDescription, 
                           r.RequestStatusID, r.MasterID, r.StartDate,
                           c.UserID AS ClientID, c.Phone AS ClientPhone
                    FROM Requests r
                    JOIN Users c ON r.ClientID = c.UserID
                    WHERE r.RequestID = @RequestID", connection);
                command.Parameters.AddWithValue("@RequestID", requestId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        comboBoxTechType.SelectedValue = reader["HomeTechTypeID"];
                        textBoxModel.Text = reader["HomeTechModel"].ToString();
                        textBoxDescription.Text = reader["ProblemDescription"].ToString();
                        comboBoxStatus.SelectedValue = reader["RequestStatusID"];
                        comboBoxMaster.SelectedValue = reader["MasterID"];
                        dateTimePickerStartDate.Value = (DateTime)reader["StartDate"];
                        comboBoxClient.SelectedValue = reader["ClientID"];
                        textBoxClientPhone.Text = reader["ClientPhone"].ToString();
                    }
                }
            }
        }

        private void comboBoxClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxClient.SelectedValue is int selectedClientId)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT Phone FROM Users WHERE UserID = @ClientID", connection);
                    command.Parameters.AddWithValue("@ClientID", selectedClientId);
                    textBoxClientPhone.Text = (string)command.ExecuteScalar();
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;

                    if (requestId.HasValue) // Редактирование существующей заявки
                    {
                        command = new SqlCommand(@"
                            UPDATE Requests 
                            SET HomeTechTypeID = @TechTypeID, 
                                HomeTechModel = @Model, 
                                ProblemDescription = @Description, 
                                RequestStatusID = @StatusID, 
                                MasterID = @MasterID,
                                StartDate = @StartDate,
                                ClientID = @ClientID
                            WHERE RequestID = @RequestID", connection);
                        command.Parameters.AddWithValue("@RequestID", requestId.Value);
                    }
                    else // Добавление новой заявки
                    {
                        command = new SqlCommand(@"
                            INSERT INTO Requests (StartDate, HomeTechTypeID, HomeTechModel, 
                                                  ProblemDescription, RequestStatusID, MasterID, ClientID)
                            VALUES (@StartDate, @TechTypeID, @Model, @Description, @StatusID, @MasterID, @ClientID)", connection);
                    }

                    command.Parameters.AddWithValue("@StartDate", dateTimePickerStartDate.Value);
                    command.Parameters.AddWithValue("@TechTypeID", comboBoxTechType.SelectedValue);
                    command.Parameters.AddWithValue("@Model", textBoxModel.Text);
                    command.Parameters.AddWithValue("@Description", textBoxDescription.Text);
                    command.Parameters.AddWithValue("@StatusID", comboBoxStatus.SelectedValue);
                    command.Parameters.AddWithValue("@MasterID", comboBoxMaster.SelectedValue ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ClientID", comboBoxClient.SelectedValue);

                    command.ExecuteNonQuery();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private bool ValidateForm()
        {
            if (comboBoxTechType.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(textBoxModel.Text) ||
                string.IsNullOrWhiteSpace(textBoxDescription.Text) ||
                comboBoxStatus.SelectedIndex == -1 ||
                comboBoxClient.SelectedIndex == -1)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}