namespace AbdulAkinCengiz_222132128.WinFormUI;

partial class LoginForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
        pictureBox1 = new PictureBox();
        gbxGiris = new GroupBox();
        btnLogin = new Button();
        cbxShowPassword = new CheckBox();
        textBox2 = new TextBox();
        txtUserName = new TextBox();
        label2 = new Label();
        label1 = new Label();
        btnClose = new Button();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        gbxGiris.SuspendLayout();
        SuspendLayout();
        // 
        // pictureBox1
        // 
        resources.ApplyResources(pictureBox1, "pictureBox1");
        pictureBox1.Name = "pictureBox1";
        pictureBox1.TabStop = false;
        // 
        // gbxGiris
        // 
        gbxGiris.Controls.Add(btnLogin);
        gbxGiris.Controls.Add(cbxShowPassword);
        gbxGiris.Controls.Add(textBox2);
        gbxGiris.Controls.Add(txtUserName);
        gbxGiris.Controls.Add(label2);
        gbxGiris.Controls.Add(label1);
        resources.ApplyResources(gbxGiris, "gbxGiris");
        gbxGiris.Name = "gbxGiris";
        gbxGiris.TabStop = false;
        // 
        // btnLogin
        // 
        btnLogin.BackColor = Color.DodgerBlue;
        btnLogin.ForeColor = Color.White;
        resources.ApplyResources(btnLogin, "btnLogin");
        btnLogin.Name = "btnLogin";
        btnLogin.UseVisualStyleBackColor = false;
        btnLogin.Click += btnLogin_Click;
        // 
        // cbxShowPassword
        // 
        resources.ApplyResources(cbxShowPassword, "cbxShowPassword");
        cbxShowPassword.Name = "cbxShowPassword";
        cbxShowPassword.UseVisualStyleBackColor = true;
        // 
        // textBox2
        // 
        resources.ApplyResources(textBox2, "textBox2");
        textBox2.Name = "textBox2";
        // 
        // txtUserName
        // 
        resources.ApplyResources(txtUserName, "txtUserName");
        txtUserName.Name = "txtUserName";
        // 
        // label2
        // 
        resources.ApplyResources(label2, "label2");
        label2.Name = "label2";
        // 
        // label1
        // 
        resources.ApplyResources(label1, "label1");
        label1.Name = "label1";
        // 
        // btnClose
        // 
        btnClose.BackColor = Color.Red;
        btnClose.ForeColor = Color.White;
        resources.ApplyResources(btnClose, "btnClose");
        btnClose.Name = "btnClose";
        btnClose.UseVisualStyleBackColor = false;
        btnClose.Click += btnClose_Click;
        // 
        // LoginForm
        // 
        resources.ApplyResources(this, "$this");
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Silver;
        Controls.Add(btnClose);
        Controls.Add(gbxGiris);
        Controls.Add(pictureBox1);
        FormBorderStyle = FormBorderStyle.None;
        Name = "LoginForm";
        Load += LoginForm_Load;
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        gbxGiris.ResumeLayout(false);
        gbxGiris.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private PictureBox pictureBox1;
    private GroupBox gbxGiris;
    private TextBox textBox2;
    private TextBox txtUserName;
    private Label label2;
    private Label label1;
    private Button btnLogin;
    private CheckBox cbxShowPassword;
    private Button btnClose;
}
