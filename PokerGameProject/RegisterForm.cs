using Microsoft.Extensions.DependencyInjection;

namespace PokerGameProject
{
    public partial class RegisterForm : Form
    {
        private readonly AuthContainer authContainer;
        private readonly IServiceProvider serProvider;

        public RegisterForm(AuthContainer authContainer, IServiceProvider serProvider)
        {
            InitializeComponent();
            this.authContainer = authContainer;
            this.serProvider = serProvider;
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            authContainer.SetAuthenticated("test");

        }

        private void linkLabelToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var loginForm = serProvider.GetRequiredService<LoginForm>();
            loginForm.Show();
            this.Hide();
        }
    }
}
