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
using System.Windows.Forms.VisualStyles;

namespace УП
{
    public partial class AddEditRequestForm : Form
    {
        private string connectionString = "Server=DESKTOP-17763KV\\SOFYA;Database=rybalchenko;Trusted_Connection=True;";
        private int? requestId;
        private string currentClientFIO;

        public AddEditRequestForm(int? requestId, string clientFIO)
        {
            InitializeComponent();
            this.requestId = requestId;
            this.currentClientFIO = clientFIO;  // Передаем ФИО владельца аккаунта
        }

        private void AddEditRequestForm_Load(object sender, EventArgs e)
        {
            LoadTechTypes();
            LoadRequestStatuses();

            if (requestId.HasValue)
            {
                LoadRequestData(requestId.Value);
            }
            else
            {
                // Устанавливаем текущее ФИО владельца аккаунта
                textBoxFIO.Text = currentClientFIO;
            }

            textBoxFIO.ReadOnly = true; // Блокируем поле ФИО от редактирования
        }

        private void LoadRequestData(int requestId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(@"
        SELECT r.HomeTechTypeID, r.HomeTechModel, r.ProblemDescription, 
               u.FIO AS ClientName, u.Phone AS ClientPhone, r.RequestStatusID
        FROM Requests r
        JOIN Users u ON r.ClientID = u.UserID
        WHERE r.RequestID = @RequestID", connection);
                command.Parameters.AddWithValue("@RequestID", requestId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Устанавливаем значения полей для редактируемой заявки
                        textBoxModel.Text = reader["HomeTechModel"].ToString();
                        textBoxDescription.Text = reader["ProblemDescription"].ToString();
                        textBoxFIO.Text = reader["ClientName"].ToString();
                        textBoxPhone.Text = reader["ClientPhone"].ToString();
                        comboBoxStatus.SelectedValue = reader["RequestStatusID"];

                        // Устанавливаем тип оборудования
                        int selectedTypeID = (int)reader["HomeTechTypeID"];
                        comboBoxType.SelectedIndex = comboBoxType.Items.Cast<dynamic>().ToList()
                            .FindIndex(item => item.Value == selectedTypeID);
                    }
                }
            }
        }


        private void LoadRequestStatuses()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                comboBoxStatus.Items.Clear();

                if (requestId.HasValue)
                {
                    // Получаем текущий статус при редактировании
                    using (SqlCommand statusCommand = new SqlCommand(@"
                SELECT rs.StatusID, rs.StatusName
                FROM RequestStatuses rs
                JOIN Requests r ON r.RequestStatusID = rs.StatusID
                WHERE r.RequestID = @RequestID", connection))
                    {
                        statusCommand.Parameters.AddWithValue("@RequestID", requestId.Value);

                        using (SqlDataReader statusReader = statusCommand.ExecuteReader())
                        {
                            if (statusReader.Read())
                            {
                                comboBoxStatus.Items.Add(new
                                {
                                    Text = statusReader["StatusName"].ToString(),
                                    Value = (int)statusReader["StatusID"]
                                });
                                comboBoxStatus.SelectedIndex = 0;
                            }
                        }
                    }
                    comboBoxStatus.Enabled = false; // Блокируем изменение статуса
                }
                else
                {
                    // Добавляем статус "Новая заявка" для новых записей
                    using (SqlCommand command = new SqlCommand("SELECT * FROM RequestStatuses WHERE StatusName = 'Новая заявка'", connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBoxStatus.Items.Add(new
                            {
                                Text = reader["StatusName"].ToString(),
                                Value = (int)reader["StatusID"]
                            });
                        }
                        comboBoxStatus.SelectedIndex = 0;
                    }
                }

                comboBoxStatus.DisplayMember = "Text";
                comboBoxStatus.ValueMember = "Value";
            }
        }

        private void LoadTechTypes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM TechTypes", connection);
                SqlDataReader reader = command.ExecuteReader();
                comboBoxType.Items.Clear();

                while (reader.Read())
                {
                    comboBoxType.Items.Add(new
                    {
                        Text = reader["TypeName"].ToString(),
                        Value = reader["TypeID"]
                    });
                }

                comboBoxType.DisplayMember = "Text";
                comboBoxType.ValueMember = "Value";
            }
        }

      

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;

                    // Проверяем, что ФИО совпадает с владельцем аккаунта
                    if (!textBoxFIO.Text.Equals(currentClientFIO, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("ФИО не совпадает с ФИО владельца аккаунта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (requestId.HasValue)
                    {
                        // Обновление существующей заявки
                        command = new SqlCommand(@"
                        UPDATE Requests 
                        SET HomeTechTypeID = @HomeTechTypeID, HomeTechModel = @HomeTechModel, 
                            ProblemDescription = @ProblemDescription, 
                            RequestStatusID = @RequestStatusID
                        WHERE RequestID = @RequestID", connection);
                        command.Parameters.AddWithValue("@RequestID", requestId.Value);
                    }
                    else
                    {
                        // Создание новой заявки
                        command = new SqlCommand(@"
                        INSERT INTO Requests (HomeTechTypeID, HomeTechModel, ProblemDescription, ClientID, RequestStatusID, StartDate)
                        VALUES (@HomeTechTypeID, @HomeTechModel, @ProblemDescription, @ClientID, @RequestStatusID, @StartDate)", connection);
                        command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                        command.Parameters.AddWithValue("@ClientID", GetClientId());
                    }

                    // Добавляем параметры
                    command.Parameters.AddWithValue("@HomeTechTypeID", ((dynamic)comboBoxType.SelectedItem).Value);
                    command.Parameters.AddWithValue("@HomeTechModel", textBoxModel.Text);
                    command.Parameters.AddWithValue("@ProblemDescription", textBoxDescription.Text);
                    command.Parameters.AddWithValue("@RequestStatusID", ((dynamic)comboBoxStatus.SelectedItem).Value);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Заявка успешно сохранена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private bool ValidateInputs()
        {
            // Проверка на заполненность всех полей
            if (comboBoxType.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип оборудования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxModel.Text))
            {
                MessageBox.Show("Введите модель оборудования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxDescription.Text))
            {
                MessageBox.Show("Введите описание проблемы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxFIO.Text))
            {
                MessageBox.Show("Введите ФИО клиента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxPhone.Text) || !IsPhoneNumberValid(textBoxPhone.Text))
            {
                MessageBox.Show("Введите корректный номер телефона.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (comboBoxStatus.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус заявки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool IsPhoneNumberValid(string phone)
        {
            // Минимальная длина номера
            return phone.Length >= 7;
        }

        private int GetClientId()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Получаем ID клиента по текущему ФИО и телефону
                SqlCommand command = new SqlCommand("SELECT UserID FROM Users WHERE FIO = @FIO AND Phone = @Phone", connection);
                command.Parameters.AddWithValue("@FIO", currentClientFIO);
                command.Parameters.AddWithValue("@Phone", textBoxPhone.Text.Trim());

                var result = command.ExecuteScalar();
                if (result != null)
                {
                    return (int)result;
                }
                else
                {
                    throw new Exception("Не удалось найти клиента с указанным ФИО и номером телефона.");
                }
            }
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}