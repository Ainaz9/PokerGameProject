﻿using Microsoft.Extensions.DependencyInjection;
using PokerGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerGameProject
{
    public partial class App : Form
    {
        private readonly AuthContainer authContainer;
        private readonly IServiceProvider serviceProvider;

        public App(AuthContainer authContainer, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.authContainer = authContainer;
            this.serviceProvider = serviceProvider;
            authContainer.Authenticated += OnAuthenticated;

        }

        private void App_Load(object sender, EventArgs e)
        {
            /*var mainMenu1 = serviceProvider.GetRequiredService<MainMenuForm>();
            mainMenu1.Show();
            return;*/
            if (authContainer.IsAuthenticated == false)
            {
                var loginForm = serviceProvider.GetRequiredService<LoginForm>();
                loginForm.Show();
                this.Close();
               

            }
            else
            {
                var mainMenu = serviceProvider.GetRequiredService<MainMenuForm>();
                mainMenu.Show();
                this.Close();


            }
        }
        private void OnAuthenticated()
        {
            var loginForm = serviceProvider.GetRequiredService<LoginForm>();
            loginForm.Close();
            var regForm = serviceProvider.GetRequiredService<RegisterForm>();
            regForm.Close();
            var mainMenu = serviceProvider.GetRequiredService<MainMenuForm>();
            mainMenu.Show();
            this.Close();
        }


    }
}