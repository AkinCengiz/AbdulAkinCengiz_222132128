namespace AbdulAkinCengiz_222132128.WinFormUI;

partial class ReservationActionForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        panel3 = new Panel();
        groupBox4 = new GroupBox();
        btnCheckIn = new Button();
        btnUpdate = new Button();
        btnDelete = new Button();
        btnConfirm = new Button();
        groupBox3 = new GroupBox();
        btnGetTables = new Button();
        cmbTables = new ComboBox();
        label1 = new Label();
        label6 = new Label();
        dtpEndDate = new DateTimePicker();
        label7 = new Label();
        label12 = new Label();
        dtpStartDate = new DateTimePicker();
        nudGuestCount = new NumericUpDown();
        groupBox2 = new GroupBox();
        txtPhone = new TextBox();
        txtEmail = new TextBox();
        txtLastName = new TextBox();
        txtFirstName = new TextBox();
        label5 = new Label();
        label11 = new Label();
        label9 = new Label();
        label10 = new Label();
        panel3.SuspendLayout();
        groupBox4.SuspendLayout();
        groupBox3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudGuestCount).BeginInit();
        groupBox2.SuspendLayout();
        SuspendLayout();
        // 
        // panel3
        // 
        panel3.Controls.Add(groupBox4);
        panel3.Controls.Add(groupBox3);
        panel3.Controls.Add(groupBox2);
        panel3.Location = new Point(12, 12);
        panel3.Name = "panel3";
        panel3.Size = new Size(703, 372);
        panel3.TabIndex = 5;
        // 
        // groupBox4
        // 
        groupBox4.Controls.Add(btnCheckIn);
        groupBox4.Controls.Add(btnUpdate);
        groupBox4.Controls.Add(btnDelete);
        groupBox4.Controls.Add(btnConfirm);
        groupBox4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        groupBox4.Location = new Point(14, 244);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new Size(672, 115);
        groupBox4.TabIndex = 20;
        groupBox4.TabStop = false;
        groupBox4.Text = "İşlemler";
        // 
        // btnCheckIn
        // 
        btnCheckIn.BackColor = Color.LightSeaGreen;
        btnCheckIn.ForeColor = Color.White;
        btnCheckIn.Location = new Point(171, 28);
        btnCheckIn.Name = "btnCheckIn";
        btnCheckIn.Size = new Size(149, 81);
        btnCheckIn.TabIndex = 23;
        btnCheckIn.Text = "CHECK IN";
        btnCheckIn.UseVisualStyleBackColor = false;
        btnCheckIn.Click += btnCheckIn_Click;
        // 
        // btnUpdate
        // 
        btnUpdate.BackColor = Color.Green;
        btnUpdate.ForeColor = Color.White;
        btnUpdate.Location = new Point(510, 28);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(149, 81);
        btnUpdate.TabIndex = 22;
        btnUpdate.Text = "GÜNCELLE";
        btnUpdate.UseVisualStyleBackColor = false;
        btnUpdate.Click += btnUpdate_Click;
        // 
        // btnDelete
        // 
        btnDelete.BackColor = Color.Red;
        btnDelete.ForeColor = Color.White;
        btnDelete.Location = new Point(339, 28);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(149, 81);
        btnDelete.TabIndex = 21;
        btnDelete.Text = "SİL";
        btnDelete.UseVisualStyleBackColor = false;
        btnDelete.Click += btnDelete_Click;
        // 
        // btnConfirm
        // 
        btnConfirm.BackColor = Color.DodgerBlue;
        btnConfirm.ForeColor = Color.White;
        btnConfirm.Location = new Point(6, 28);
        btnConfirm.Name = "btnConfirm";
        btnConfirm.Size = new Size(149, 81);
        btnConfirm.TabIndex = 20;
        btnConfirm.Text = "ONAYLA";
        btnConfirm.UseVisualStyleBackColor = false;
        btnConfirm.Click += btnConfirm_Click;
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(btnGetTables);
        groupBox3.Controls.Add(cmbTables);
        groupBox3.Controls.Add(label1);
        groupBox3.Controls.Add(label6);
        groupBox3.Controls.Add(dtpEndDate);
        groupBox3.Controls.Add(label7);
        groupBox3.Controls.Add(label12);
        groupBox3.Controls.Add(dtpStartDate);
        groupBox3.Controls.Add(nudGuestCount);
        groupBox3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        groupBox3.Location = new Point(14, 15);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(333, 223);
        groupBox3.TabIndex = 19;
        groupBox3.TabStop = false;
        groupBox3.Text = "Rezervasyon Bilgileri";
        // 
        // btnGetTables
        // 
        btnGetTables.BackColor = Color.OrangeRed;
        btnGetTables.ForeColor = Color.White;
        btnGetTables.Location = new Point(163, 133);
        btnGetTables.Name = "btnGetTables";
        btnGetTables.Size = new Size(157, 49);
        btnGetTables.TabIndex = 21;
        btnGetTables.Text = "Masaları Getir";
        btnGetTables.UseVisualStyleBackColor = false;
        btnGetTables.Click += btnGetTables_Click;
        // 
        // cmbTables
        // 
        cmbTables.Enabled = false;
        cmbTables.FormattingEnabled = true;
        cmbTables.Location = new Point(163, 188);
        cmbTables.Name = "cmbTables";
        cmbTables.Size = new Size(157, 29);
        cmbTables.TabIndex = 20;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 12F);
        label1.Location = new Point(5, 191);
        label1.Name = "label1";
        label1.Size = new Size(58, 21);
        label1.TabIndex = 19;
        label1.Text = "Masa : ";
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Font = new Font("Segoe UI", 12F);
        label6.Location = new Point(5, 34);
        label6.Name = "label6";
        label6.Size = new Size(127, 21);
        label6.TabIndex = 8;
        label6.Text = "Başlangıç Tarihi : ";
        // 
        // dtpEndDate
        // 
        dtpEndDate.CustomFormat = "dd.MM.yyyy HH:mm";
        dtpEndDate.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        dtpEndDate.Format = DateTimePickerFormat.Custom;
        dtpEndDate.Location = new Point(163, 63);
        dtpEndDate.Name = "dtpEndDate";
        dtpEndDate.ShowUpDown = true;
        dtpEndDate.Size = new Size(157, 29);
        dtpEndDate.TabIndex = 18;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Font = new Font("Segoe UI", 12F);
        label7.Location = new Point(5, 100);
        label7.Name = "label7";
        label7.Size = new Size(89, 21);
        label7.TabIndex = 9;
        label7.Text = "Kişi Sayısı : ";
        // 
        // label12
        // 
        label12.AutoSize = true;
        label12.Font = new Font("Segoe UI", 12F);
        label12.Location = new Point(5, 69);
        label12.Name = "label12";
        label12.Size = new Size(91, 21);
        label12.TabIndex = 17;
        label12.Text = "Bitiş Tarihi : ";
        // 
        // dtpStartDate
        // 
        dtpStartDate.CustomFormat = "dd.MM.yyyy HH:mm";
        dtpStartDate.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        dtpStartDate.Format = DateTimePickerFormat.Custom;
        dtpStartDate.Location = new Point(163, 28);
        dtpStartDate.Name = "dtpStartDate";
        dtpStartDate.ShowUpDown = true;
        dtpStartDate.Size = new Size(157, 29);
        dtpStartDate.TabIndex = 15;
        // 
        // nudGuestCount
        // 
        nudGuestCount.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        nudGuestCount.Location = new Point(163, 98);
        nudGuestCount.Name = "nudGuestCount";
        nudGuestCount.Size = new Size(157, 29);
        nudGuestCount.TabIndex = 16;
        nudGuestCount.TextAlign = HorizontalAlignment.Center;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(txtPhone);
        groupBox2.Controls.Add(txtEmail);
        groupBox2.Controls.Add(txtLastName);
        groupBox2.Controls.Add(txtFirstName);
        groupBox2.Controls.Add(label5);
        groupBox2.Controls.Add(label11);
        groupBox2.Controls.Add(label9);
        groupBox2.Controls.Add(label10);
        groupBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        groupBox2.Location = new Point(353, 15);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(333, 223);
        groupBox2.TabIndex = 14;
        groupBox2.TabStop = false;
        groupBox2.Text = "Müşteri Bilgileri";
        // 
        // txtPhone
        // 
        txtPhone.Location = new Point(163, 136);
        txtPhone.Name = "txtPhone";
        txtPhone.ReadOnly = true;
        txtPhone.Size = new Size(157, 29);
        txtPhone.TabIndex = 17;
        // 
        // txtEmail
        // 
        txtEmail.Location = new Point(163, 101);
        txtEmail.Name = "txtEmail";
        txtEmail.ReadOnly = true;
        txtEmail.Size = new Size(157, 29);
        txtEmail.TabIndex = 16;
        // 
        // txtLastName
        // 
        txtLastName.Location = new Point(163, 66);
        txtLastName.Name = "txtLastName";
        txtLastName.ReadOnly = true;
        txtLastName.Size = new Size(157, 29);
        txtLastName.TabIndex = 15;
        // 
        // txtFirstName
        // 
        txtFirstName.Location = new Point(163, 31);
        txtFirstName.Name = "txtFirstName";
        txtFirstName.ReadOnly = true;
        txtFirstName.Size = new Size(157, 29);
        txtFirstName.TabIndex = 14;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Font = new Font("Segoe UI", 12F);
        label5.Location = new Point(6, 34);
        label5.Name = "label5";
        label5.Size = new Size(101, 21);
        label5.TabIndex = 7;
        label5.Text = "Müşteri Adı : ";
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Font = new Font("Segoe UI", 12F);
        label11.Location = new Point(6, 139);
        label11.Name = "label11";
        label11.Size = new Size(70, 21);
        label11.TabIndex = 13;
        label11.Text = "Telefon : ";
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Font = new Font("Segoe UI", 12F);
        label9.Location = new Point(5, 69);
        label9.Name = "label9";
        label9.Size = new Size(125, 21);
        label9.TabIndex = 11;
        label9.Text = "Müşteri Soyadı : ";
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Font = new Font("Segoe UI", 12F);
        label10.Location = new Point(6, 104);
        label10.Name = "label10";
        label10.Size = new Size(59, 21);
        label10.TabIndex = 12;
        label10.Text = "Email : ";
        // 
        // ReservationActionForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Silver;
        ClientSize = new Size(726, 396);
        Controls.Add(panel3);
        Name = "ReservationActionForm";
        Text = "Rezervasyon İşlemleri";
        Load += ReservationActionForm_Load;
        panel3.ResumeLayout(false);
        groupBox4.ResumeLayout(false);
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudGuestCount).EndInit();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel panel3;
    private GroupBox groupBox4;
    private Button btnDelete;
    private Button btnConfirm;
    private GroupBox groupBox3;
    private Label label6;
    private DateTimePicker dtpEndDate;
    private Label label7;
    private Label label12;
    private DateTimePicker dtpStartDate;
    private NumericUpDown nudGuestCount;
    private GroupBox groupBox2;
    private TextBox txtPhone;
    private TextBox txtEmail;
    private TextBox txtLastName;
    private TextBox txtFirstName;
    private Label label5;
    private Label label11;
    private Label label9;
    private Label label10;
    private Button btnUpdate;
    private Button btnCheckIn;
    private ComboBox cmbTables;
    private Label label1;
    private Button btnGetTables;
}