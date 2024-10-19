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

namespace УП
{
    public partial class HistoryLoginForm : Form
    {
        public HistoryLoginForm()
        {
            InitializeComponent();
            LoadLoginHistory();
        }

        private void LoadLoginHistory()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Server=DESKTOP-17763KV\\SOFYA;Database=rybalchenko;Trusted_Connection=True;"))
                {
                    conn.Open();
                    // Сортируем по AttemptTime (дате) в порядке убывания
                    string query = "SELECT Login, AttemptTime, CASE WHEN IsSuccessful = 1 THEN 'Успешно' ELSE 'Ошибка' END AS Status " +
                                   "FROM LoginHistory ORDER BY AttemptTime DESC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Очистка предыдущих данных перед заполнением
                    dataGridView1.DataSource = null; // Сначала очищаем источник данных
                    dataGridView1.Rows.Clear(); // Очищаем строки
                    dataGridView1.DataSource = dataTable; // Устанавливаем новый источник данных
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки истории входа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string filter = filterTextBox.Text;
            try
            {
                using (SqlConnection conn = new SqlConnection("Server=DESKTOP-17763KV\\SOFYA;Database=rybalchenko;Trusted_Connection=True;"))
                {
                    conn.Open();
                    // Сортируем по AttemptTime (дате) в порядке убывания
                    string query = "SELECT Login, AttemptTime, CASE WHEN IsSuccessful = 1 THEN 'Успешно' ELSE 'Ошибка' END AS Status " +
                                   "FROM LoginHistory WHERE Login LIKE @filter ORDER BY AttemptTime DESC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@filter", "%" + filter + "%");
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Очистка предыдущих данных перед заполнением
                    dataGridView1.DataSource = null; // Сначала очищаем источник данных
                    dataGridView1.Rows.Clear(); // Очищаем строки
                    dataGridView1.DataSource = dataTable; // Устанавливаем новый источник данных
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска по логину: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}