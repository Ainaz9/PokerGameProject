﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokerGameRSF.Models;
using PokerGameProject;
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
using PokerGameRSF.DataAccess;

namespace PokerGame

{
    public partial class LoginForm : Form
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly MyDbContext _context;

        private ILogger<LoginForm> _logger;

        public LoginForm(MyDbContext context, ILogger<LoginForm> logger)
        {
            InitializeComponent();
            _serviceProvider = Program.ServiceProvider;

            _context = context;
            _logger = logger;
            textBoxLoginLog.Cue = "Введите логин";
            textBoxPasswordLog.Cue = "Введите пароль";
        }

        private void loginButtonPicture_Click(object sender, EventArgs e)
        {
            string login = textBoxLoginLog.Text.Trim();
            string password = textBoxPasswordLog.Text.Trim();

            if (string.IsNullOrEmpty(login))
            {
                textBoxLoginLog.Text = "";
                MessageBox.Show("Логин не может быть пустым");
                _logger.LogError($"Произошла неудачная попытка авторизоваться. Некорректный логин - {login}");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                textBoxPasswordLog.Text = "";
                MessageBox.Show("Пароль не может быть пустым");
                _logger.LogError($"Произошла неудачная попытка авторизоваться. Некорректный пароль у пользователя - {login}");
                return;
            }
           
            
            User user = new User();

            user.Username = login;
            user.Password = password;

            User? foundedUser = _context.Users.FirstOrDefault(user => user.Username == login);

            if (foundedUser == null)
            {
                MessageBox.Show("Такого пользователя не существует");
                _logger.LogError($"Произошла неудачная попытка авторизоваться. Пользователь с логином {login} не существует");
                return;
            }

            if (foundedUser.Password != user.Password)
            {
                MessageBox.Show("Введён некорректный пароль");
                _logger.LogError($"Произошла неудачная попытка авторизоваться. Пользователь с логином {login} ввёл неверный пароль");
                return;
            }
            var authContainer = _serviceProvider.GetRequiredService<AuthContainer>();
            authContainer.SetAuthenticated(foundedUser.Id.ToString());
            _logger.LogInformation($"Успешная попытка авторизоваться. Пользователь '{user.Id} {user.Email} {user.Username}' авторизован");
            return;

           
        }

        private void linkLabelToRegistration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registrationForm = _serviceProvider.GetRequiredService<RegisterForm>();

            registrationForm.Show();
            this.Hide();
        }

        
    }

}
