namespace AbdulAkinCengiz_222132128.WinFormUI;

public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();
    }

    private void LoginForm_Load(object sender, EventArgs e)
    {
        
    }
    private void btnClose_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
    private void btnLogin_Click(object sender, EventArgs e)
    {
        MainForm mainForm = new MainForm();
        mainForm.Show();
        this.Hide();
    }
}
