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
using System.Timers;


namespace УП
{
    public partial class LoginForm : Form
    {
        private int loginAttempts = 0;  // Счетчик попыток входа
        private bool isCaptchaEnabled = false;  // Флаг активации капчи
        private System.Timers.Timer blockTimer;  // Таймер блокировки
        private int blockDuration = 180000;  // Длительность блокировки (3 минуты)
        private string captchaCode = "";  // Хранение капчи
        private bool isBlockedWithTimer = false;  // Флаг блокировки после таймера
        private int postTimerAttempts = 0;  // Счетчик попыток после снятия таймера

        public LoginForm()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            // Проверка блокировки
            if (isBlockedWithTimer)
            {
                postTimerAttempts++;
                if (postTimerAttempts > 1)
                {
                    MessageBox.Show("Слишком много неудачных попыток. Перезапустите приложение для продолжения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close(); // Закрываем приложение для перезапуска
                    return;
                }
                return;
            }

            // Проверка капчи, если она включена
            if (isCaptchaEnabled && !ValidateCaptcha())
            {
                MessageBox.Show("Неверная капча. Повторите попытку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GenerateCaptcha(); // Генерируем новую капчу
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection("Server=DESKTOP-17763KV\\SOFYA;Database=rybalchenko;Trusted_Connection=True;"))
                {
                    conn.Open();

                    string query = "SELECT UserType, UserID FROM Users WHERE Login = @login";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@login", login);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Логин существует, проверяем пароль
                        string userType = reader["UserType"].ToString();
                        int clientId = Convert.ToInt32(reader["UserID"]);

                        reader.Close(); // Закрываем ридер перед выполнением нового запроса

                        string passwordQuery = "SELECT Password FROM Users WHERE Login = @login";
                        SqlCommand passwordCmd = new SqlCommand(passwordQuery, conn);
                        passwordCmd.Parameters.AddWithValue("@login", login);
                        string storedPassword = (string)passwordCmd.ExecuteScalar();

                        if (password == storedPassword)
                        {
                            // Успешная авторизация
                            MessageBox.Show("Авторизация успешна!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            OpenRoleForm(userType, clientId); // Передаем роль и clientId
                            LogLoginAttempt(login, true); // Логирование успешной попытки
                            loginAttempts = 0; // Сбрасываем попытки после успешного входа
                            isCaptchaEnabled = false; // Отключаем капчу после успешного входа
                            pictureBoxCaptcha.Visible = false; // Скрываем капчу
                            textBoxCaptcha.Visible = false; // Скрываем поле капчи
                        }
                        else
                        {
                            // Неверный пароль
                            loginAttempts++;
                            MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LogLoginAttempt(login, false);

                            // Включаем капчу после первой попытки
                            if (loginAttempts == 1)
                            {
                                EnableCaptcha();
                            }
                            else if (loginAttempts == 2)
                            {
                                StartBlockTimer(); // Блокируем пользователя после второй неудачной попытки
                            }
                        }
                    }
                    else
                    {
                        // Логин не существует
                        loginAttempts++;
                        MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LogLoginAttempt(login, false);

                        // Включаем капчу при первой неудачной попытке
                        if (loginAttempts == 1)
                        {
                            EnableCaptcha();
                        }
                        else if (loginAttempts == 2)
                        {
                            StartBlockTimer(); // Блокируем пользователя после второй неудачной попытки
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnableCaptcha()
        {
            isCaptchaEnabled = true;
            MessageBox.Show("Необходимо ввести капчу после первой неудачной попытки.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            GenerateCaptcha();  // Генерация капчи
            pictureBoxCaptcha.Visible = true; // Делаем pictureBox видимым
            textBoxCaptcha.Visible = true; // Делаем textBox видимым

            // Перерисовываем элементы интерфейса
            pictureBoxCaptcha.Invalidate();
            textBoxCaptcha.Invalidate();
        }

        private void LogLoginAttempt(string login, bool isSuccess)
        {
            try
            {
                // Запись попытки входа в таблицу LoginHistory
                using (SqlConnection conn = new SqlConnection("Server=DESKTOP-17763KV\\SOFYA;Database=rybalchenko;Trusted_Connection=True;"))
                {
                    conn.Open();

                    string query = "INSERT INTO LoginHistory (Login, AttemptTime, Status, IsSuccessful) VALUES (@login, @attemptTime, @status, @isSuccessful)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@attemptTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@status", isSuccess ? "Успешно" : "Ошибка");
                    cmd.Parameters.AddWithValue("@isSuccessful", isSuccess); // Здесь мы добавляем значение isSuccess

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка записи истории входа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void GenerateCaptcha()
        {
            // Генерация простой капчи из случайных символов
            captchaCode = GenerateRandomCaptcha();
            pictureBoxCaptcha.Image = GenerateCaptchaImage(captchaCode);
        }

        private string GenerateRandomCaptcha()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private Image GenerateCaptchaImage(string captcha)
        {
            // Генерация изображения с капчей (реализовать простое изображение с шумом)
            Bitmap bitmap = new Bitmap(100, 40);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                Random random = new Random();

                // Шум
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(bitmap.Width);
                    int y = random.Next(bitmap.Height);
                    bitmap.SetPixel(x, y, Color.Gray);
                }

                // Настройка шрифта
                Font font = new Font("Arial", 18, FontStyle.Bold | FontStyle.Italic);

                // Рисование капчи с небольшой случайной амплитудой и наклоном
                for (int i = 0; i < captcha.Length; i++)
                {
                    float angle = random.Next(-30, 30); // Случайный угол наклона
                    float x = 10 + (i * 20) + random.Next(-5, 5); // Смещение по x для каждого символа
                    float y = random.Next(5, 10); // Случайное смещение по y

                    // Рисование символа
                    g.TranslateTransform(x, y);
                    g.RotateTransform(angle);
                    g.DrawString(captcha[i].ToString(), font, Brushes.Black, 0, 0);
                    g.ResetTransform(); // Сброс трансформации для следующего символа
                }
            }

            return bitmap;
        }

        private bool ValidateCaptcha()
        {
            return textBoxCaptcha.Text.Equals(captchaCode, StringComparison.OrdinalIgnoreCase);
        }

        private void StartBlockTimer()
        {
            MessageBox.Show("Ваша учетная запись заблокирована на 3 минуты.", "Блокировка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            blockTimer = new System.Timers.Timer(blockDuration);
            blockTimer.Elapsed += UnblockLogin;
            blockTimer.AutoReset = false;
            blockTimer.Start();

            // Блокируем ввод
            txtLogin.Enabled = false;
            txtPassword.Enabled = false;
            btnLogin.Enabled = false;
        }

        private void UnblockLogin(object sender, ElapsedEventArgs e)
        {
            // Разблокировка входа после таймера
            Invoke((Action)(() =>
            {
                MessageBox.Show("Блокировка снята. Вы можете попробовать снова.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLogin.Enabled = true;
                txtPassword.Enabled = true;
                btnLogin.Enabled = true;
                loginAttempts = 0;  // Сброс счетчика попыток
                isBlockedWithTimer = true;  // Теперь следим за попытками после таймера
            }));
        }

        private void OpenRoleForm(string role, int clientId)
        {
            string clientName = GetClientName(clientId); // Получаем имя клиента из базы данных

            switch (role)
            {
                case "Заказчик":
                    new RequestForm(clientId, clientName, role).Show(); // Передаем clientId, clientName и роль
                    break;
                case "Оператор":
                    new OperatorRequestForm(clientId, clientName, role).Show(); // Добавляем вызов формы для оператора
                    break;
                case "Мастер":
                    new RequestFormMaster(clientId, clientName, role).Show(); // Если есть форма для мастера, также передаем параметры
                    break;
                case "Менеджер": // Добавляем обработку для менеджера
                    new ManagerForm().Show(); // Открываем форму для менеджера
                    break;
                default:
                    MessageBox.Show("Неизвестная роль пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            this.Hide(); // Скрываем текущую форму входа после открытия нужной формы
        }

        // Метод для получения имени клиента по его ID
        private string GetClientName(int clientId)
        {
            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-17763KV\\SOFYA;Database=rybalchenko;Trusted_Connection=True;"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT FIO FROM Users WHERE UserID = @ClientId", connection);
                command.Parameters.AddWithValue("@ClientId", clientId);

                return (string)command.ExecuteScalar();
            }
        }
        private void BtnHistory_Click(object sender, EventArgs e)
        {
            // Переход на форму истории входов
            new HistoryLoginForm().Show();
        }

        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            // Показ/скрытие пароля
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
            btnTogglePassword.Text = txtPassword.UseSystemPasswordChar ? "👁" : "👁‍";
        }


        private void LoginForm_Load(object sender, EventArgs e)
        {
            pictureBoxCaptcha.Visible = false; // Скрываем капчу изначально
            textBoxCaptcha.Visible = false; // Скрываем поле для ввода капчи
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}