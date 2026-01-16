namespace AbdulAkinCengiz_222132128.WinFormUI;

partial class TableListForm
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
        flpTables = new FlowLayoutPanel();
        groupBox1 = new GroupBox();
        btnFilter = new Button();
        cmbCapacity = new ComboBox();
        cmbStatus = new ComboBox();
        label3 = new Label();
        label2 = new Label();
        panel2 = new Panel();
        lblStatus = new Label();
        label7 = new Label();
        btnTableClose = new Button();
        btnTableManagement = new Button();
        lblOrderNumber = new Label();
        label6 = new Label();
        lblGuestCount = new Label();
        label4 = new Label();
        lblTable = new Label();
        panel1.SuspendLayout();
        groupBox1.SuspendLayout();
        panel2.SuspendLayout();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
        label1.ForeColor = Color.Black;
        label1.Location = new Point(12, 9);
        label1.Name = "label1";
        label1.Size = new Size(155, 57);
        label1.TabIndex = 1;
        label1.Text = "Masa Listesi";
        label1.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // panel1
        // 
        panel1.Controls.Add(flpTables);
        panel1.Controls.Add(groupBox1);
        panel1.Location = new Point(12, 69);
        panel1.Name = "panel1";
        panel1.Size = new Size(643, 566);
        panel1.TabIndex = 2;
        // 
        // flpTables
        // 
        flpTables.Location = new Point(12, 94);
        flpTables.Name = "flpTables";
        flpTables.Size = new Size(621, 459);
        flpTables.TabIndex = 2;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(btnFilter);
        groupBox1.Controls.Add(cmbCapacity);
        groupBox1.Controls.Add(cmbStatus);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Location = new Point(12, 13);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(621, 75);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Filtrele";
        // 
        // btnFilter
        // 
        btnFilter.BackColor = Color.DeepSkyBlue;
        btnFilter.Font = new Font("Microsoft Sans Serif", 12F);
        btnFilter.ForeColor = Color.White;
        btnFilter.Location = new Point(532, 23);
        btnFilter.Name = "btnFilter";
        btnFilter.Size = new Size(75, 35);
        btnFilter.TabIndex = 4;
        btnFilter.Text = "Filtrele";
        btnFilter.UseVisualStyleBackColor = false;
        btnFilter.Click += btnFilter_Click;
        // 
        // cmbCapacity
        // 
        cmbCapacity.Font = new Font("Microsoft Sans Serif", 12F);
        cmbCapacity.FormattingEnabled = true;
        cmbCapacity.Location = new Point(376, 26);
        cmbCapacity.Name = "cmbCapacity";
        cmbCapacity.Size = new Size(150, 28);
        cmbCapacity.TabIndex = 3;
        // 
        // cmbStatus
        // 
        cmbStatus.Font = new Font("Microsoft Sans Serif", 12F);
        cmbStatus.FormattingEnabled = true;
        cmbStatus.Location = new Point(89, 29);
        cmbStatus.Name = "cmbStatus";
        cmbStatus.Size = new Size(150, 28);
        cmbStatus.TabIndex = 2;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Font = new Font("Microsoft Sans Serif", 12F);
        label3.Location = new Point(280, 30);
        label3.Name = "label3";
        label3.Size = new Size(90, 20);
        label3.TabIndex = 1;
        label3.Text = "Kişi Sayısı  :";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        label2.Location = new Point(17, 31);
        label2.Name = "label2";
        label2.Size = new Size(66, 21);
        label2.TabIndex = 0;
        label2.Text = "Durum :";
        // 
        // panel2
        // 
        panel2.Controls.Add(lblStatus);
        panel2.Controls.Add(label7);
        panel2.Controls.Add(btnTableClose);
        panel2.Controls.Add(btnTableManagement);
        panel2.Controls.Add(lblOrderNumber);
        panel2.Controls.Add(label6);
        panel2.Controls.Add(lblGuestCount);
        panel2.Controls.Add(label4);
        panel2.Controls.Add(lblTable);
        panel2.Location = new Point(661, 69);
        panel2.Name = "panel2";
        panel2.Size = new Size(231, 566);
        panel2.TabIndex = 3;
        // 
        // lblStatus
        // 
        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        lblStatus.Location = new Point(119, 164);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(19, 21);
        lblStatus.TabIndex = 10;
        lblStatus.Text = "0";
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        label7.Location = new Point(13, 164);
        label7.Name = "label7";
        label7.Size = new Size(66, 21);
        label7.TabIndex = 9;
        label7.Text = "Durum :";
        // 
        // btnTableClose
        // 
        btnTableClose.BackColor = Color.Red;
        btnTableClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
        btnTableClose.ForeColor = Color.White;
        btnTableClose.Location = new Point(13, 199);
        btnTableClose.Name = "btnTableClose";
        btnTableClose.Size = new Size(198, 50);
        btnTableClose.TabIndex = 8;
        btnTableClose.Text = "Müşteri Geldi";
        btnTableClose.UseVisualStyleBackColor = false;
        btnTableClose.Click += btnTableClose_Click;
        // 
        // btnTableManagement
        // 
        btnTableManagement.BackColor = Color.RoyalBlue;
        btnTableManagement.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
        btnTableManagement.ForeColor = Color.White;
        btnTableManagement.Location = new Point(13, 255);
        btnTableManagement.Name = "btnTableManagement";
        btnTableManagement.Size = new Size(198, 50);
        btnTableManagement.TabIndex = 7;
        btnTableManagement.Text = "Masa Yönetimi";
        btnTableManagement.UseVisualStyleBackColor = false;
        btnTableManagement.Click += btnTableManagement_Click;
        // 
        // lblOrderNumber
        // 
        lblOrderNumber.AutoSize = true;
        lblOrderNumber.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        lblOrderNumber.Location = new Point(119, 126);
        lblOrderNumber.Name = "lblOrderNumber";
        lblOrderNumber.Size = new Size(19, 21);
        lblOrderNumber.TabIndex = 6;
        lblOrderNumber.Text = "0";
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        label6.Location = new Point(13, 126);
        label6.Name = "label6";
        label6.Size = new Size(89, 21);
        label6.TabIndex = 5;
        label6.Text = "Sipariş No :";
        // 
        // lblGuestCount
        // 
        lblGuestCount.AutoSize = true;
        lblGuestCount.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        lblGuestCount.Location = new Point(119, 94);
        lblGuestCount.Name = "lblGuestCount";
        lblGuestCount.Size = new Size(19, 21);
        lblGuestCount.TabIndex = 4;
        lblGuestCount.Text = "0";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        label4.Location = new Point(13, 94);
        label4.Name = "label4";
        label4.Size = new Size(85, 21);
        label4.TabIndex = 3;
        label4.Text = "Kişi Sayısı :";
        // 
        // lblTable
        // 
        lblTable.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
        lblTable.ForeColor = Color.Black;
        lblTable.Location = new Point(13, 21);
        lblTable.Name = "lblTable";
        lblTable.Size = new Size(155, 67);
        lblTable.TabIndex = 2;
        lblTable.Text = "Masa";
        lblTable.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // TableListForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Silver;
        ClientSize = new Size(901, 647);
        Controls.Add(panel2);
        Controls.Add(panel1);
        Controls.Add(label1);
        Name = "TableListForm";
        Text = "TableListForm";
        Load += TableListForm_Load;
        panel1.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        panel2.ResumeLayout(false);
        panel2.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Label label1;
    private Panel panel1;
    private GroupBox groupBox1;
    private Label label2;
    private Button btnFilter;
    private ComboBox cmbCapacity;
    private ComboBox cmbStatus;
    private Label label3;
    private GroupBox gbxTableList;
    private Panel panel2;
    private Label lblGuestCount;
    private Label label4;
    private Label lblTable;
    private Button btnTableClose;
    private Button btnTableManagement;
    private Label lblOrderNumber;
    private Label label6;
    private FlowLayoutPanel flpTables;
    private Label lblStatus;
    private Label label7;
}