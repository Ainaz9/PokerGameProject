using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokerGame;
using PokerGame.Models;
using PokerGameRSF;
using PokerGameRSF.Models;
using PokerGameRSF.Properties;
using System.Text;

namespace PokerGame
{
    public partial class RegisterForm : Form
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly MyDbContext _context;
        private readonly ILogger<RegisterForm> _logger;

        public RegisterForm(MyDbContext context, ILogger<RegisterForm> logger)
        {
            InitializeComponent();
            _serviceProvider = Program.ServiceProvider;

            _context = context;
            _logger = logger;
            textBoxEmailReg.Cue = "Введите почту";
            textBoxLoginReg.Cue = "Введите логин";
            textBoxPasswordReg.Cue = "Введите пароль";
            textBoxRepeatPasswordReg.Cue = "Введите пароль повторно";
        }

        private void linkLabelToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var loginForm = _serviceProvider.GetRequiredService<RegisterForm>();
            loginForm.Show();
            this.Hide();
            _logger.LogInformation("Форма регистрации закрыта, открывается форма авторизации");
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string login = textBoxLoginReg.Text.Trim();
            string email = textBoxEmailReg.Text.Trim();
            string password = textBoxPasswordReg.Text.Trim();
            string repeatedPassword = textBoxRepeatPasswordReg.Text.Trim();

            if (string.IsNullOrEmpty(login))
            {
                textBoxLoginReg.Text = "";
                MessageBox.Show("Логин не может быть пустым");
                _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Некорректный логин - {login}");
                return;
            }

            if (string.IsNullOrEmpty(email))
            {
                textBoxEmailReg.Text = "";
                MessageBox.Show("Email не может быть пустым");
                _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Некорректный email - {email}");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                textBoxPasswordReg.Text = "";
                MessageBox.Show("Пароль не может быть пустым");
                _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Некорректный пароль - {password}");
                return;
            }

            if (string.IsNullOrEmpty(repeatedPassword))
            {
                textBoxRepeatPasswordReg.Text = "";
                MessageBox.Show("Повторение пароля не может быть пустым");
                _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Некорректный повтор пароля - {repeatedPassword}");
                return;
            }

            if (password != repeatedPassword)
            {
                textBoxPasswordReg.Text = "";
                textBoxRepeatPasswordReg.Text = "";

                _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Пароли не совпадают");
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            User user = new User();

            user.Login = login;
            user.Email = email;
            user.Password = Encoding.UTF8.GetBytes(password);

            User? foundedUser = _context.Users.FirstOrDefault(user => user.Email == email);

            if (foundedUser != null)
            {
                MessageBox.Show("Такой пользователь уже зарегистрирован");
                _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Такой пользователь уже есть");
                return;
            }

            _context.Users.Add(user);
            _logger.LogInformation($"Регистрация прошла успешно. Пользователь '{user.Id} {user.Email} {user.Login}' зарегистрирован");
        }

        /*private void pictureBoxTogglePassword_Click(object sender, EventArgs e)
        {
            textBoxPasswordReg.UseSystemPasswordChar = !textBoxPasswordReg.UseSystemPasswordChar;
            pictureBoxTogglePassword.BackgroundImage = textBoxPasswordReg.UseSystemPasswordChar ? Image.FromFile("") : Image.FromFile("");
        }

        private void pictureBoxRepeatedPassword_Click(object sender, EventArgs e)
        {
            textBoxRepeatPasswordReg.UseSystemPasswordChar = !textBoxRepeatPasswordReg.UseSystemPasswordChar;
            pictureBoxRepeatedPassword.BackgroundImage = textBoxRepeatPasswordReg.UseSystemPasswordChar ? Image.FromFile("") : Image.FromFile("");

        }*/

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBoxLoginReg_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }


}
