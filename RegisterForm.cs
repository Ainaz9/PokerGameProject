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
        private void buttonRegisterPicture_Click_1(object sender, EventArgs e)
        {
            string login = textBoxLoginReg.Text;
            string email = textBoxEmailReg.Text;
            string password = textBoxPasswordReg.Text;
            string repeatedPassword = textBoxRepeatPasswordReg.Text;
            const string validLoginSymbols = "qwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            const string validEmailSymbols = "qwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM@.";
            const string validPasswordSymbols = "qwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM";

            if (login.Length < 4)
            {
                MessageBox.Show("В логине должно быть не менее 4 символов");
                _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Логин меньше 4 символов");
                return;
            }
            foreach (var s in login)
            {
                if (validLoginSymbols.Contains(s) == false)
                {

                    MessageBox.Show($"В логине разрешены только такие символы: {validLoginSymbols}");
                    _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Логин содержит запрещенные символы");
                    return;
                }
            }


            foreach (var s in email)
            {
                if (validEmailSymbols.Contains(s) == false)
                {

                    MessageBox.Show($"В почте разрешены только такие символы: {validEmailSymbols}");
                    _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Почта содержит запрещенные символы");
                    return;
                }
            }
            if (email.Contains("@") == false)
            {
                MessageBox.Show("В почте должен обязательно содержаться символ @");
                _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Почта не содержит обязательный символ @");
                return;
            }
            if (email.Count(s => s == '@') == 1)
            {
                MessageBox.Show("В почте должен быть 1 символ @");
                _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Почта не может содержать более 1 символа @");
                return;
            }
            if (email.IndexOf("@") == 0)
            {
                MessageBox.Show("В почте символ @ не должен стоять в начале");
                _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Почта не может содержать символ @ в начале почты");
                return;
            }
            if (email.Count(s => s == '.') == 1)
            {
                MessageBox.Show("В почте должен быть 1 символ точки.");
                _logger.LogError($"Произошла неудачная попытка зарегистрироваться. В почте должен быть 1 символ точки.");
                return;
            }
            if (email.IndexOf(".") - email.IndexOf("@") < 0)
            {
                MessageBox.Show("Некорректно введена почта");
                _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Некорректно введена почта.");
                return;

            }
            string[] splitted = email.Split("@.");
            if (splitted.Length != 3)
            {
                MessageBox.Show("Некорректно введена почта");
                _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Некорректно введена почта.");
                return;
            }
            foreach (var item in splitted)
            {
                if (item.Length < 1)
                {
                    MessageBox.Show("Некорректно введена почта");
                    _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Некорректно введена почта.");
                    return;
                }
            }

            if (password.Length < 8)
            {
                MessageBox.Show("В пароле должно быть не менее 8 символов");
                _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Пароль меньше 8 символов");
                return;
            }
            foreach (var s in password)
            {
                if (validPasswordSymbols.Contains(s) == false)
                {

                    MessageBox.Show($"В пароле разрешены только такие символы: {validPasswordSymbols}");
                    _logger.LogError($"Произошла неудачная попытка зарегистрироваться. Пароль содержит запрещенные символы");
                    return;
                }
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
            if (textBoxPasswordReg.UseSystemPasswordChar == false)
            {
                textBoxPasswordReg.UseSystemPasswordChar = true;
                passwordLockPicture.Image = PokerGameRSF.Properties.Resources.замок_для_пароля;

            }
            else
            {
                textBoxPasswordReg.UseSystemPasswordChar = false;
                passwordLockPicture.Image = PokerGameRSF.Properties.Resources.eye_fo_pin;
                
            }
        }

        private void repeatedPasswordLockPicture_Click(object sender, EventArgs e)
        {

            if (textBoxPasswordReg.UseSystemPasswordChar == false)
            {
                textBoxRepeatPasswordReg.UseSystemPasswordChar = true;
                repeatedPasswordLockPicture.Image = PokerGameRSF.Properties.Resources.замок_для_пароля;
            }
            else
            {
                textBoxRepeatPasswordReg.UseSystemPasswordChar = false;
                repeatedPasswordLockPicture.Image = PokerGameRSF.Properties.Resources.eye_fo_pin;
            }
        }

        
    }


}
