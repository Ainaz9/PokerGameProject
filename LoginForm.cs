using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PokerGame.Models;
using PokerGameRSF;
using PokerGameRSF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerGame

{
    public partial class LoginForm : Form
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly MyDbContext _context;

        private Logger _logger;

        public LoginForm(MyDbContext context, Logger logger)
        {
            InitializeComponent();
            _serviceProvider = Program.ServiceProvider;

            _context = context;
            _logger = logger;
            textBoxLoginLog.Cue = "Введите логин";
            textBoxPasswordLog.Cue = "Введите пароль";
        }


        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string login = textBoxLoginLog.Text.Trim();
            string password = textBoxPasswordLog.Text.Trim();

            if (string.IsNullOrEmpty(login))
            {
                textBoxLoginLog.Text = "";
                MessageBox.Show("Логин не может быть пустым");
                _logger.Log($"Произошла неудачная попытка авторизоваться. Некорректный логин - {login}");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                textBoxPasswordLog.Text = "";
                MessageBox.Show("Пароль не может быть пустым");
                _logger.Log($"Произошла неудачная попытка авторизоваться. Некорректный пароль у пользователя - {login}");
                return;
            }

            User user = new User();

            user.Login = login;
            user.Password = Encoding.UTF8.GetBytes(password);

            User? foundedUser = _context.Users.FirstOrDefault(user => user.Login == login);

            if (foundedUser == null)
            {
                MessageBox.Show("Такого пользователя не существует");
                _logger.Log($"Произошла неудачная попытка авторизоваться. Пользователь с логином {login} не существует");
                return;
            }

            if (foundedUser.Password != user.Password)
            {
                MessageBox.Show("Введён некорректный пароль");
                _logger.Log($"Произошла неудачная попытка авторизоваться. Пользователь с логином {login} ввёл неверный пароль");
                return;
            }

            Form mainForm = _serviceProvider.GetRequiredService<MainMenuForm>();
            mainForm.Show();
            this.Close();

            _logger.Log($"Успешная попытка авторизоваться. Пользователь '{user.Id} {user.Email} {user.Login}' авторизован");
        }

        private void linkLabelToRegistration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registrationForm = _serviceProvider.GetRequiredService<RegisterForm>();

            registrationForm.Show();
            this.Hide();
        }
    }

}
