using Microsoft.Extensions.DependencyInjection;
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
    public partial class LoginForm : Form
    {
        private readonly AuthContainer authContainer;
        private readonly IServiceProvider serProvider;

        public LoginForm(AuthContainer authContainer, IServiceProvider serProvider)
        {
            InitializeComponent();
            this.authContainer = authContainer;
            this.serProvider = serProvider;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            authContainer.SetAuthenticated("test");



        }

        private void linkLabelToRegistration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var regForm = serProvider.GetRequiredService<RegisterForm>();
            regForm.Show();
            this.Hide();
            
        }
    }
}
