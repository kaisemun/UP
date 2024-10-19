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
    public partial class EditRequestMasterForm : Form
    {
        private string connectionString = "Server=DESKTOP-17763KV\\SOFYA;Database=rybalchenko;Trusted_Connection=True;";
        private int requestId;
        private int masterId; // Поле для хранения ID мастера

        public EditRequestMasterForm(int requestId, int masterId, string masterName)
        {
            InitializeComponent();
            this.requestId = requestId;
            this.masterId = masterId; // Устанавливаем ID мастера
            LoadRequestDetails();
        }

        private void LoadRequestDetails()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"
                    SELECT CompletionDate,
                           (SELECT STRING_AGG(com.Message, '; ') FROM Comments com WHERE com.RequestID = r.RequestID) AS Comments,
                           (SELECT STRING_AGG(part.PartName, '; ') FROM RepairParts part WHERE part.RequestID = r.RequestID) AS Parts
                    FROM Requests r
                    WHERE RequestID = @RequestID", connection);
                command.Parameters.AddWithValue("@RequestID", requestId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dateTimePickerCompletionDate.Value = reader["CompletionDate"] != DBNull.Value
                            ? (DateTime)reader["CompletionDate"]
                            : DateTime.Now;

                        // Загрузка комментариев и запчастей
                        textBoxComments.Text = reader["Comments"].ToString();
                        textBoxParts.Text = reader["Parts"].ToString();
                    }
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"
                    UPDATE Requests 
                    SET CompletionDate = @CompletionDate
                    WHERE RequestID = @RequestID", connection);

                command.Parameters.AddWithValue("@CompletionDate", dateTimePickerCompletionDate.Value);
                command.Parameters.AddWithValue("@RequestID", requestId);

                command.ExecuteNonQuery();

                // Обновление комментариев и запчастей
                UpdateCommentsAndParts();

                MessageBox.Show("Данные успешно обновлены.");
                this.Close();
            }
        }

        private void UpdateCommentsAndParts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Удаляем существующие комментарии и запчасти
                SqlCommand deleteComments = new SqlCommand("DELETE FROM Comments WHERE RequestID = @RequestID", connection);
                deleteComments.Parameters.AddWithValue("@RequestID", requestId);
                deleteComments.ExecuteNonQuery();

                SqlCommand deleteParts = new SqlCommand("DELETE FROM RepairParts WHERE RequestID = @RequestID", connection);
                deleteParts.Parameters.AddWithValue("@RequestID", requestId);
                deleteParts.ExecuteNonQuery();

                // Добавляем новые комментарии
                string[] comments = textBoxComments.Text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var comment in comments)
                {
                    SqlCommand insertComment = new SqlCommand("INSERT INTO Comments (Message, MasterID, RequestID) VALUES (@Message, @MasterID, @RequestID)", connection);
                    insertComment.Parameters.AddWithValue("@Message", comment.Trim());
                    insertComment.Parameters.AddWithValue("@MasterID", masterId); // Указываем ID мастера
                    insertComment.Parameters.AddWithValue("@RequestID", requestId);
                    insertComment.ExecuteNonQuery();
                }

                // Добавляем новые запчасти
                string[] parts = textBoxParts.Text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in parts)
                {
                    SqlCommand insertPart = new SqlCommand("INSERT INTO RepairParts (PartName, RequestID) VALUES (@PartName, @RequestID)", connection);
                    insertPart.Parameters.AddWithValue("@PartName", part.Trim());
                    insertPart.Parameters.AddWithValue("@RequestID", requestId);
                    insertPart.ExecuteNonQuery();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelComments_Click(object sender, EventArgs e)
        {

        }
    }
}