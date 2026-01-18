namespace AbdulAkinCengiz_222132128.WinFormUI;

partial class PaymentForm
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
        groupBox1 = new GroupBox();
        lblCustomer = new Label();
        label5 = new Label();
        lblOrderNumber = new Label();
        lblTable = new Label();
        label3 = new Label();
        label2 = new Label();
        panel1 = new Panel();
        groupBox3 = new GroupBox();
        btnPayPrint = new Button();
        btnPay = new Button();
        label11 = new Label();
        lblKdv = new Label();
        label13 = new Label();
        label8 = new Label();
        lblGeneralTotal = new Label();
        label10 = new Label();
        label7 = new Label();
        lblTotal = new Label();
        label4 = new Label();
        groupBox2 = new GroupBox();
        dgvOrderDetails = new DataGridView();
        btnReceiptPreview = new Button();
        groupBox1.SuspendLayout();
        panel1.SuspendLayout();
        groupBox3.SuspendLayout();
        groupBox2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvOrderDetails).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
        label1.ForeColor = Color.Black;
        label1.Location = new Point(12, 9);
        label1.Name = "label1";
        label1.Size = new Size(155, 57);
        label1.TabIndex = 5;
        label1.Text = "Ödeme Ekranı";
        label1.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(lblCustomer);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(lblOrderNumber);
        groupBox1.Controls.Add(lblTable);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        groupBox1.Location = new Point(12, 69);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(776, 75);
        groupBox1.TabIndex = 6;
        groupBox1.TabStop = false;
        groupBox1.Text = "Rezervasyon ve Sipariş Bilgileri";
        // 
        // lblCustomer
        // 
        lblCustomer.AutoSize = true;
        lblCustomer.Font = new Font("Microsoft Sans Serif", 12F);
        lblCustomer.Location = new Point(554, 32);
        lblCustomer.Name = "lblCustomer";
        lblCustomer.Size = new Size(0, 20);
        lblCustomer.TabIndex = 8;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Font = new Font("Microsoft Sans Serif", 12F);
        label5.Location = new Point(479, 32);
        label5.Name = "label5";
        label5.Size = new Size(69, 20);
        label5.TabIndex = 7;
        label5.Text = "Müşteri :";
        // 
        // lblOrderNumber
        // 
        lblOrderNumber.AutoSize = true;
        lblOrderNumber.Font = new Font("Microsoft Sans Serif", 12F);
        lblOrderNumber.Location = new Point(308, 32);
        lblOrderNumber.Name = "lblOrderNumber";
        lblOrderNumber.Size = new Size(18, 20);
        lblOrderNumber.TabIndex = 6;
        lblOrderNumber.Text = "#";
        // 
        // lblTable
        // 
        lblTable.AutoSize = true;
        lblTable.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        lblTable.Location = new Point(77, 31);
        lblTable.Name = "lblTable";
        lblTable.Size = new Size(0, 21);
        lblTable.TabIndex = 5;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Font = new Font("Microsoft Sans Serif", 12F);
        label3.Location = new Point(213, 32);
        label3.Name = "label3";
        label3.Size = new Size(89, 20);
        label3.TabIndex = 1;
        label3.Text = "Sipariş No :";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        label2.Location = new Point(17, 31);
        label2.Name = "label2";
        label2.Size = new Size(54, 21);
        label2.TabIndex = 0;
        label2.Text = "Masa :";
        // 
        // panel1
        // 
        panel1.Controls.Add(groupBox3);
        panel1.Controls.Add(groupBox2);
        panel1.Location = new Point(12, 150);
        panel1.Name = "panel1";
        panel1.Size = new Size(776, 288);
        panel1.TabIndex = 7;
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(btnReceiptPreview);
        groupBox3.Controls.Add(btnPayPrint);
        groupBox3.Controls.Add(btnPay);
        groupBox3.Controls.Add(label11);
        groupBox3.Controls.Add(lblKdv);
        groupBox3.Controls.Add(label13);
        groupBox3.Controls.Add(label8);
        groupBox3.Controls.Add(lblGeneralTotal);
        groupBox3.Controls.Add(label10);
        groupBox3.Controls.Add(label7);
        groupBox3.Controls.Add(lblTotal);
        groupBox3.Controls.Add(label4);
        groupBox3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        groupBox3.Location = new Point(479, 3);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(294, 279);
        groupBox3.TabIndex = 1;
        groupBox3.TabStop = false;
        groupBox3.Text = "Sipariş Ödeme Bilgileri";
        // 
        // btnPayPrint
        // 
        btnPayPrint.BackColor = Color.ForestGreen;
        btnPayPrint.ForeColor = Color.White;
        btnPayPrint.Location = new Point(10, 236);
        btnPayPrint.Name = "btnPayPrint";
        btnPayPrint.Size = new Size(278, 37);
        btnPayPrint.TabIndex = 10;
        btnPayPrint.Text = "Fiş Yazdır";
        btnPayPrint.UseVisualStyleBackColor = false;
        btnPayPrint.Click += btnPayPrint_Click;
        // 
        // btnPay
        // 
        btnPay.BackColor = Color.ForestGreen;
        btnPay.ForeColor = Color.White;
        btnPay.Location = new Point(10, 150);
        btnPay.Name = "btnPay";
        btnPay.Size = new Size(278, 37);
        btnPay.TabIndex = 9;
        btnPay.Text = "Ödeme Al";
        btnPay.UseVisualStyleBackColor = false;
        btnPay.Click += btnPay_Click;
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Location = new Point(258, 74);
        label11.Name = "label11";
        label11.Size = new Size(26, 21);
        label11.TabIndex = 8;
        label11.Text = "TL";
        // 
        // lblKdv
        // 
        lblKdv.Location = new Point(143, 74);
        lblKdv.Name = "lblKdv";
        lblKdv.Size = new Size(109, 21);
        lblKdv.TabIndex = 7;
        lblKdv.Text = "0,00";
        lblKdv.TextAlign = ContentAlignment.TopRight;
        // 
        // label13
        // 
        label13.AutoSize = true;
        label13.Location = new Point(6, 74);
        label13.Name = "label13";
        label13.Size = new Size(91, 21);
        label13.TabIndex = 6;
        label13.Text = "KDV ( %8 ) :";
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        label8.Location = new Point(258, 109);
        label8.Name = "label8";
        label8.Size = new Size(27, 21);
        label8.TabIndex = 5;
        label8.Text = "TL";
        // 
        // lblGeneralTotal
        // 
        lblGeneralTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblGeneralTotal.Location = new Point(143, 109);
        lblGeneralTotal.Name = "lblGeneralTotal";
        lblGeneralTotal.Size = new Size(109, 21);
        lblGeneralTotal.TabIndex = 4;
        lblGeneralTotal.Text = "0,00";
        lblGeneralTotal.TextAlign = ContentAlignment.TopRight;
        // 
        // label10
        // 
        label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        label10.Location = new Point(6, 109);
        label10.Name = "label10";
        label10.Size = new Size(131, 21);
        label10.TabIndex = 3;
        label10.Text = "Genel Toplam :";
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new Point(258, 38);
        label7.Name = "label7";
        label7.Size = new Size(26, 21);
        label7.TabIndex = 2;
        label7.Text = "TL";
        // 
        // lblTotal
        // 
        lblTotal.Location = new Point(143, 38);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(109, 21);
        lblTotal.TabIndex = 1;
        lblTotal.Text = "0,00";
        lblTotal.TextAlign = ContentAlignment.TopRight;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(6, 38);
        label4.Name = "label4";
        label4.Size = new Size(95, 21);
        label4.TabIndex = 0;
        label4.Text = "Ara Toplam :";
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(dgvOrderDetails);
        groupBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        groupBox2.Location = new Point(3, 3);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(470, 282);
        groupBox2.TabIndex = 0;
        groupBox2.TabStop = false;
        groupBox2.Text = "Sipariş Detayları";
        // 
        // dgvOrderDetails
        // 
        dgvOrderDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvOrderDetails.Dock = DockStyle.Fill;
        dgvOrderDetails.Location = new Point(3, 25);
        dgvOrderDetails.Name = "dgvOrderDetails";
        dgvOrderDetails.Size = new Size(464, 254);
        dgvOrderDetails.TabIndex = 0;
        // 
        // btnReceiptPreview
        // 
        btnReceiptPreview.BackColor = Color.ForestGreen;
        btnReceiptPreview.ForeColor = Color.White;
        btnReceiptPreview.Location = new Point(10, 193);
        btnReceiptPreview.Name = "btnReceiptPreview";
        btnReceiptPreview.Size = new Size(278, 37);
        btnReceiptPreview.TabIndex = 11;
        btnReceiptPreview.Text = "Önizleme";
        btnReceiptPreview.UseVisualStyleBackColor = false;
        btnReceiptPreview.Click += btnReceiptPreview_Click;
        // 
        // PaymentForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Silver;
        ClientSize = new Size(800, 450);
        Controls.Add(panel1);
        Controls.Add(groupBox1);
        Controls.Add(label1);
        Name = "PaymentForm";
        Text = "Ödeme Formu";
        Load += PaymentForm_Load;
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        panel1.ResumeLayout(false);
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        groupBox2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvOrderDetails).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Label label1;
    private GroupBox groupBox1;
    private Label lblTable;
    private Label label3;
    private Label label2;
    private Label lblOrderNumber;
    private Label lblCustomer;
    private Label label5;
    private Panel panel1;
    private GroupBox groupBox3;
    private Label label4;
    private GroupBox groupBox2;
    private DataGridView dgvOrderDetails;
    private Button btnPay;
    private Label label11;
    private Label lblKdv;
    private Label label13;
    private Label label8;
    private Label lblGeneralTotal;
    private Label label10;
    private Label label7;
    private Label lblTotal;
    private Button btnPayPrint;
    private Button btnReceiptPreview;
}