namespace AbdulAkinCengiz_222132128.WinFormUI;

partial class ReservationForm
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
        label1 = new Label();
        panel1 = new Panel();
        panel2 = new Panel();
        dgvReservations = new DataGridView();
        groupBox1 = new GroupBox();
        label2 = new Label();
        panel3 = new Panel();
        groupBox3 = new GroupBox();
        btnSearchTables = new Button();
        cmbTables = new ComboBox();
        label4 = new Label();
        label6 = new Label();
        dtpEndDate = new DateTimePicker();
        label7 = new Label();
        label12 = new Label();
        dtpStartDate = new DateTimePicker();
        nudGuestCount = new NumericUpDown();
        groupBox2 = new GroupBox();
        btnGetList = new Button();
        btnCreateReservation = new Button();
        txtPhone = new TextBox();
        txtEmail = new TextBox();
        txtLastName = new TextBox();
        txtFirstName = new TextBox();
        label5 = new Label();
        label11 = new Label();
        label9 = new Label();
        label10 = new Label();
        label3 = new Label();
        panel1.SuspendLayout();
        panel2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvReservations).BeginInit();
        groupBox1.SuspendLayout();
        panel3.SuspendLayout();
        groupBox3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudGuestCount).BeginInit();
        groupBox2.SuspendLayout();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
        label1.ForeColor = Color.Black;
        label1.Location = new Point(12, 9);
        label1.Name = "label1";
        label1.Size = new Size(277, 57);
        label1.TabIndex = 2;
        label1.Text = "Rezervasyon Yönetimi";
        label1.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // panel1
        // 
        panel1.Controls.Add(panel2);
        panel1.Controls.Add(groupBox1);
        panel1.Location = new Point(12, 58);
        panel1.Name = "panel1";
        panel1.Size = new Size(643, 558);
        panel1.TabIndex = 3;
        // 
        // panel2
        // 
        panel2.Controls.Add(dgvReservations);
        panel2.Location = new Point(8, 94);
        panel2.Name = "panel2";
        panel2.Size = new Size(621, 447);
        panel2.TabIndex = 4;
        // 
        // dgvReservations
        // 
        dgvReservations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvReservations.Dock = DockStyle.Fill;
        dgvReservations.Location = new Point(0, 0);
        dgvReservations.Name = "dgvReservations";
        dgvReservations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvReservations.Size = new Size(621, 447);
        dgvReservations.TabIndex = 0;
        dgvReservations.CellDoubleClick += dgvReservations_CellDoubleClick;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(label2);
        groupBox1.Location = new Point(8, 13);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(621, 75);
        groupBox1.TabIndex = 3;
        groupBox1.TabStop = false;
        // 
        // label2
        // 
        label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
        label2.ForeColor = Color.Black;
        label2.Location = new Point(6, 18);
        label2.Name = "label2";
        label2.Size = new Size(277, 45);
        label2.TabIndex = 3;
        label2.Text = "Rezervasyon Listesi";
        label2.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // panel3
        // 
        panel3.Controls.Add(groupBox3);
        panel3.Controls.Add(groupBox2);
        panel3.Controls.Add(label3);
        panel3.Location = new Point(661, 58);
        panel3.Name = "panel3";
        panel3.Size = new Size(361, 558);
        panel3.TabIndex = 4;
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(btnSearchTables);
        groupBox3.Controls.Add(cmbTables);
        groupBox3.Controls.Add(label4);
        groupBox3.Controls.Add(label6);
        groupBox3.Controls.Add(dtpEndDate);
        groupBox3.Controls.Add(label7);
        groupBox3.Controls.Add(label12);
        groupBox3.Controls.Add(dtpStartDate);
        groupBox3.Controls.Add(nudGuestCount);
        groupBox3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        groupBox3.Location = new Point(14, 61);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(333, 212);
        groupBox3.TabIndex = 19;
        groupBox3.TabStop = false;
        groupBox3.Text = "Rezervasyon Yap";
        // 
        // btnSearchTables
        // 
        btnSearchTables.BackColor = Color.DodgerBlue;
        btnSearchTables.ForeColor = Color.White;
        btnSearchTables.Location = new Point(6, 127);
        btnSearchTables.Name = "btnSearchTables";
        btnSearchTables.Size = new Size(314, 39);
        btnSearchTables.TabIndex = 21;
        btnSearchTables.Text = "Uygun Masaları Getir";
        btnSearchTables.UseVisualStyleBackColor = false;
        btnSearchTables.Click += btnSearchTables_Click;
        // 
        // cmbTables
        // 
        cmbTables.FormattingEnabled = true;
        cmbTables.Location = new Point(163, 172);
        cmbTables.Name = "cmbTables";
        cmbTables.Size = new Size(157, 29);
        cmbTables.TabIndex = 20;
        cmbTables.SelectedIndexChanged += cmbTables_SelectedIndexChanged;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Font = new Font("Segoe UI", 12F);
        label4.Location = new Point(5, 175);
        label4.Name = "label4";
        label4.Size = new Size(126, 21);
        label4.TabIndex = 19;
        label4.Text = "Uygun Masalar : ";
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Font = new Font("Segoe UI", 12F);
        label6.Location = new Point(5, 28);
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
        dtpEndDate.Location = new Point(163, 57);
        dtpEndDate.Name = "dtpEndDate";
        dtpEndDate.ShowUpDown = true;
        dtpEndDate.Size = new Size(157, 29);
        dtpEndDate.TabIndex = 18;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Font = new Font("Segoe UI", 12F);
        label7.Location = new Point(5, 94);
        label7.Name = "label7";
        label7.Size = new Size(89, 21);
        label7.TabIndex = 9;
        label7.Text = "Kişi Sayısı : ";
        // 
        // label12
        // 
        label12.AutoSize = true;
        label12.Font = new Font("Segoe UI", 12F);
        label12.Location = new Point(5, 63);
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
        dtpStartDate.Location = new Point(163, 22);
        dtpStartDate.Name = "dtpStartDate";
        dtpStartDate.ShowUpDown = true;
        dtpStartDate.Size = new Size(157, 29);
        dtpStartDate.TabIndex = 15;
        // 
        // nudGuestCount
        // 
        nudGuestCount.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        nudGuestCount.Location = new Point(163, 92);
        nudGuestCount.Name = "nudGuestCount";
        nudGuestCount.Size = new Size(157, 29);
        nudGuestCount.TabIndex = 16;
        nudGuestCount.TextAlign = HorizontalAlignment.Center;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(btnGetList);
        groupBox2.Controls.Add(btnCreateReservation);
        groupBox2.Controls.Add(txtPhone);
        groupBox2.Controls.Add(txtEmail);
        groupBox2.Controls.Add(txtLastName);
        groupBox2.Controls.Add(txtFirstName);
        groupBox2.Controls.Add(label5);
        groupBox2.Controls.Add(label11);
        groupBox2.Controls.Add(label9);
        groupBox2.Controls.Add(label10);
        groupBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        groupBox2.Location = new Point(14, 279);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(333, 262);
        groupBox2.TabIndex = 14;
        groupBox2.TabStop = false;
        groupBox2.Text = "Müşteri Bilgileri";
        // 
        // btnGetList
        // 
        btnGetList.BackColor = Color.DodgerBlue;
        btnGetList.ForeColor = Color.White;
        btnGetList.Location = new Point(6, 210);
        btnGetList.Name = "btnGetList";
        btnGetList.Size = new Size(314, 39);
        btnGetList.TabIndex = 20;
        btnGetList.Text = "LİSTELE";
        btnGetList.UseVisualStyleBackColor = false;
        // 
        // btnCreateReservation
        // 
        btnCreateReservation.BackColor = Color.DodgerBlue;
        btnCreateReservation.ForeColor = Color.White;
        btnCreateReservation.Location = new Point(6, 165);
        btnCreateReservation.Name = "btnCreateReservation";
        btnCreateReservation.Size = new Size(314, 39);
        btnCreateReservation.TabIndex = 20;
        btnCreateReservation.Text = "Rezervasyon Yap";
        btnCreateReservation.UseVisualStyleBackColor = false;
        btnCreateReservation.Click += btnCreateReservation_Click;
        // 
        // txtPhone
        // 
        txtPhone.Location = new Point(163, 130);
        txtPhone.Name = "txtPhone";
        txtPhone.Size = new Size(157, 29);
        txtPhone.TabIndex = 17;
        // 
        // txtEmail
        // 
        txtEmail.Location = new Point(163, 95);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(157, 29);
        txtEmail.TabIndex = 16;
        // 
        // txtLastName
        // 
        txtLastName.Location = new Point(163, 60);
        txtLastName.Name = "txtLastName";
        txtLastName.Size = new Size(157, 29);
        txtLastName.TabIndex = 15;
        // 
        // txtFirstName
        // 
        txtFirstName.Location = new Point(163, 25);
        txtFirstName.Name = "txtFirstName";
        txtFirstName.Size = new Size(157, 29);
        txtFirstName.TabIndex = 14;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Font = new Font("Segoe UI", 12F);
        label5.Location = new Point(6, 28);
        label5.Name = "label5";
        label5.Size = new Size(101, 21);
        label5.TabIndex = 7;
        label5.Text = "Müşteri Adı : ";
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Font = new Font("Segoe UI", 12F);
        label11.Location = new Point(6, 133);
        label11.Name = "label11";
        label11.Size = new Size(70, 21);
        label11.TabIndex = 13;
        label11.Text = "Telefon : ";
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Font = new Font("Segoe UI", 12F);
        label9.Location = new Point(5, 63);
        label9.Name = "label9";
        label9.Size = new Size(125, 21);
        label9.TabIndex = 11;
        label9.Text = "Müşteri Soyadı : ";
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Font = new Font("Segoe UI", 12F);
        label10.Location = new Point(6, 98);
        label10.Name = "label10";
        label10.Size = new Size(59, 21);
        label10.TabIndex = 12;
        label10.Text = "Email : ";
        // 
        // label3
        // 
        label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
        label3.ForeColor = Color.Black;
        label3.Location = new Point(3, 13);
        label3.Name = "label3";
        label3.Size = new Size(277, 45);
        label3.TabIndex = 4;
        label3.Text = "Rezervasyon İşlemleri";
        label3.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // ReservationForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Silver;
        ClientSize = new Size(1036, 631);
        Controls.Add(panel3);
        Controls.Add(panel1);
        Controls.Add(label1);
        Name = "ReservationForm";
        Text = "Rezervasyon Yönetimi";
        Load += ReservationForm_Load;
        panel1.ResumeLayout(false);
        panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvReservations).EndInit();
        groupBox1.ResumeLayout(false);
        panel3.ResumeLayout(false);
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudGuestCount).EndInit();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Label label1;
    private Panel panel1;
    private GroupBox groupBox1;
    private Label label2;
    private Panel panel2;
    private DataGridView dgvReservations;
    private Panel panel3;
    private Label label7;
    private Label label6;
    private Label label5;
    private Label label3;
    private DateTimePicker dtpStartDate;
    private GroupBox groupBox2;
    private Label label11;
    private Label label9;
    private Label label10;
    private NumericUpDown nudGuestCount;
    private DateTimePicker dtpEndDate;
    private Label label12;
    private GroupBox groupBox3;
    private Button btnCreateReservation;
    private TextBox txtPhone;
    private TextBox txtEmail;
    private TextBox txtLastName;
    private TextBox txtFirstName;
    private ComboBox cmbTables;
    private Label label4;
    private Button btnGetList;
    private Button btnSearchTables;
}