using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Dtos.Auth;
using AbdulAkinCengiz_222132128.WinFormUI.Util;
using Microsoft.Extensions.DependencyInjection;

namespace AbdulAkinCengiz_222132128.WinFormUI;

public partial class LoginForm : Form
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAuthService _authService;
    private readonly ISessionContext _session;
    public LoginForm(IServiceProvider serviceProvider, IAuthService authService, ISessionContext session)
    {
        _serviceProvider = serviceProvider;
        _authService = authService;
        _session = session;
        InitializeComponent();
    }

    private async void LoginForm_Load(object sender, EventArgs e)
    {
        var saved = RememberMeStore.Load();
        if (saved == null) return;

        var user = await _authService.RememberMeValidateAsync(saved.Value.UserId, saved.Value.Token);
        if (user == null)
        {
            RememberMeStore.Clear();
            return;
        }

        _session.SetUser(user);

        var mainForm = _serviceProvider.GetRequiredService<MainForm>();
        mainForm.FormClosed += (_, __) => this.Close();
        mainForm.Show();

        this.Hide();
    }
    private void btnClose_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
    private async void btnLogin_Click(object sender, EventArgs e)
    {
        btnLogin.Enabled = false;

        try
        {
            var dto = new LoginRequestDto
            {
                UserNameOrEmail = txtUserName.Text,
                Password = txtPassword.Text
            };

            var result = await _authService.LoginAsync(dto);

            if (!result.Success)
            {
                MessageBox.Show(result.Message);
                return;
            }

            _session.SetUser(result.User!);

            if (cbxRememberMe.Checked)
            {
                var issued = await _authService.RememberMeIssueTokenAsync(result.User!);
                if (issued.Success)
                {
                    RememberMeStore.Save(result.User!.Id, issued.Message); // Message içine token dönmüþtük
                }
            }
            else
            {
                RememberMeStore.Clear();
                await _authService.RememberMeRevokeAsync(result.User!);
            }

            var mainForm = _serviceProvider.GetRequiredService<MainForm>();
            mainForm.FormClosed += (_, __) => this.Close(); // Main kapanýnca login de kapansýn
            mainForm.Show();

            this.Hide();
        }
        finally
        {
            btnLogin.Enabled = true;
        }
    }

    private void cbxShowPassword_CheckedChanged(object sender, EventArgs e)
    {
        txtPassword.UseSystemPasswordChar = !cbxShowPassword.Checked;
    }
}
