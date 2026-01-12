namespace AbdulAkinCengiz_222132128.WinFormUI;

partial class OrderForm
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
        btnGoPay = new Button();
        btnSaveOrder = new Button();
        lblCustomer = new Label();
        label4 = new Label();
        lblTable = new Label();
        panel2 = new Panel();
        lblGeneralTotal = new Label();
        label10 = new Label();
        lblKdv = new Label();
        label8 = new Label();
        lblTotal = new Label();
        label5 = new Label();
        panel4 = new Panel();
        dgvOrderItems = new DataGridView();
        lblReservation = new Label();
        btnFilter = new Button();
        cmbReservation = new ComboBox();
        cmbTable = new ComboBox();
        label3 = new Label();
        panel1 = new Panel();
        flpTables = new FlowLayoutPanel();
        flpCategories = new FlowLayoutPanel();
        panel3 = new Panel();
        btnRemove = new Button();
        btnAdd = new Button();
        nudQuantity = new NumericUpDown();
        dgvProducts = new DataGridView();
        groupBox1 = new GroupBox();
        label2 = new Label();
        label1 = new Label();
        panel2.SuspendLayout();
        panel4.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvOrderItems).BeginInit();
        panel1.SuspendLayout();
        flpTables.SuspendLayout();
        panel3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
        groupBox1.SuspendLayout();
        SuspendLayout();
        // 
        // btnGoPay
        // 
        btnGoPay.BackColor = Color.Green;
        btnGoPay.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
        btnGoPay.ForeColor = Color.White;
        btnGoPay.Location = new Point(13, 505);
        btnGoPay.Name = "btnGoPay";
        btnGoPay.Size = new Size(324, 50);
        btnGoPay.TabIndex = 8;
        btnGoPay.Text = "Ödeme Ekranına Geç";
        btnGoPay.UseVisualStyleBackColor = false;
        // 
        // btnSaveOrder
        // 
        btnSaveOrder.BackColor = Color.RoyalBlue;
        btnSaveOrder.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
        btnSaveOrder.ForeColor = Color.White;
        btnSaveOrder.Location = new Point(13, 449);
        btnSaveOrder.Name = "btnSaveOrder";
        btnSaveOrder.Size = new Size(324, 50);
        btnSaveOrder.TabIndex = 7;
        btnSaveOrder.Text = "Siparişi Kaydet";
        btnSaveOrder.UseVisualStyleBackColor = false;
        btnSaveOrder.Click += btnSaveOrder_Click;
        // 
        // lblCustomer
        // 
        lblCustomer.AutoSize = true;
        lblCustomer.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        lblCustomer.Location = new Point(119, 85);
        lblCustomer.Name = "lblCustomer";
        lblCustomer.Size = new Size(19, 21);
        lblCustomer.TabIndex = 4;
        lblCustomer.Text = "0";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        label4.Location = new Point(13, 85);
        label4.Name = "label4";
        label4.Size = new Size(70, 21);
        label4.TabIndex = 3;
        label4.Text = "Müşteri :";
        // 
        // lblTable
        // 
        lblTable.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
        lblTable.ForeColor = Color.Black;
        lblTable.Location = new Point(13, 13);
        lblTable.Name = "lblTable";
        lblTable.Size = new Size(324, 30);
        lblTable.TabIndex = 2;
        lblTable.Text = "Masa";
        lblTable.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // panel2
        // 
        panel2.Controls.Add(lblGeneralTotal);
        panel2.Controls.Add(label10);
        panel2.Controls.Add(lblKdv);
        panel2.Controls.Add(label8);
        panel2.Controls.Add(lblTotal);
        panel2.Controls.Add(label5);
        panel2.Controls.Add(panel4);
        panel2.Controls.Add(lblReservation);
        panel2.Controls.Add(btnGoPay);
        panel2.Controls.Add(btnSaveOrder);
        panel2.Controls.Add(lblCustomer);
        panel2.Controls.Add(label4);
        panel2.Controls.Add(lblTable);
        panel2.Location = new Point(660, 66);
        panel2.Name = "panel2";
        panel2.Size = new Size(352, 566);
        panel2.TabIndex = 6;
        // 
        // lblGeneralTotal
        // 
        lblGeneralTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblGeneralTotal.Location = new Point(186, 391);
        lblGeneralTotal.Name = "lblGeneralTotal";
        lblGeneralTotal.Size = new Size(151, 21);
        lblGeneralTotal.TabIndex = 18;
        lblGeneralTotal.Text = "0";
        lblGeneralTotal.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        label10.Location = new Point(13, 391);
        label10.Name = "label10";
        label10.Size = new Size(123, 21);
        label10.TabIndex = 17;
        label10.Text = "Genel Toplam :";
        // 
        // lblKdv
        // 
        lblKdv.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblKdv.Location = new Point(186, 358);
        lblKdv.Name = "lblKdv";
        lblKdv.Size = new Size(151, 21);
        lblKdv.TabIndex = 16;
        lblKdv.Text = "0";
        lblKdv.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        label8.Location = new Point(13, 358);
        label8.Name = "label8";
        label8.Size = new Size(51, 21);
        label8.TabIndex = 15;
        label8.Text = "KDV :";
        // 
        // lblTotal
        // 
        lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTotal.Location = new Point(186, 325);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(151, 21);
        lblTotal.TabIndex = 14;
        lblTotal.Text = "0";
        lblTotal.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        label5.Location = new Point(13, 325);
        label5.Name = "label5";
        label5.Size = new Size(105, 21);
        label5.TabIndex = 13;
        label5.Text = "Ara Toplam :";
        // 
        // panel4
        // 
        panel4.Controls.Add(dgvOrderItems);
        panel4.Location = new Point(13, 118);
        panel4.Name = "panel4";
        panel4.Size = new Size(324, 182);
        panel4.TabIndex = 12;
        // 
        // dgvOrderItems
        // 
        dgvOrderItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvOrderItems.Dock = DockStyle.Fill;
        dgvOrderItems.Location = new Point(0, 0);
        dgvOrderItems.Name = "dgvOrderItems";
        dgvOrderItems.Size = new Size(324, 182);
        dgvOrderItems.TabIndex = 0;
        // 
        // lblReservation
        // 
        lblReservation.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
        lblReservation.ForeColor = Color.Black;
        lblReservation.Location = new Point(13, 45);
        lblReservation.Name = "lblReservation";
        lblReservation.Size = new Size(324, 30);
        lblReservation.TabIndex = 11;
        lblReservation.Text = "Rezervasyon";
        lblReservation.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // btnFilter
        // 
        btnFilter.BackColor = Color.DeepSkyBlue;
        btnFilter.Font = new Font("Microsoft Sans Serif", 12F);
        btnFilter.ForeColor = Color.White;
        btnFilter.Location = new Point(532, 25);
        btnFilter.Name = "btnFilter";
        btnFilter.Size = new Size(75, 35);
        btnFilter.TabIndex = 4;
        btnFilter.Text = "Filtrele";
        btnFilter.UseVisualStyleBackColor = false;
        btnFilter.Click += btnFilter_Click;
        // 
        // cmbReservation
        // 
        cmbReservation.Font = new Font("Microsoft Sans Serif", 12F);
        cmbReservation.FormattingEnabled = true;
        cmbReservation.Location = new Point(376, 29);
        cmbReservation.Name = "cmbReservation";
        cmbReservation.Size = new Size(150, 28);
        cmbReservation.TabIndex = 3;
        // 
        // cmbTable
        // 
        cmbTable.Font = new Font("Microsoft Sans Serif", 12F);
        cmbTable.FormattingEnabled = true;
        cmbTable.Location = new Point(89, 29);
        cmbTable.Name = "cmbTable";
        cmbTable.Size = new Size(150, 28);
        cmbTable.TabIndex = 2;
        cmbTable.SelectedIndexChanged += cmbTable_SelectedIndexChanged;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Font = new Font("Microsoft Sans Serif", 12F);
        label3.Location = new Point(257, 32);
        label3.Name = "label3";
        label3.Size = new Size(113, 20);
        label3.TabIndex = 1;
        label3.Text = "Rezervasyon  :";
        // 
        // panel1
        // 
        panel1.Controls.Add(flpTables);
        panel1.Controls.Add(groupBox1);
        panel1.Location = new Point(11, 66);
        panel1.Name = "panel1";
        panel1.Size = new Size(643, 566);
        panel1.TabIndex = 5;
        // 
        // flpTables
        // 
        flpTables.Controls.Add(flpCategories);
        flpTables.Controls.Add(panel3);
        flpTables.Location = new Point(12, 94);
        flpTables.Name = "flpTables";
        flpTables.Size = new Size(621, 467);
        flpTables.TabIndex = 2;
        // 
        // flpCategories
        // 
        flpCategories.Location = new Point(3, 3);
        flpCategories.Name = "flpCategories";
        flpCategories.Size = new Size(170, 456);
        flpCategories.TabIndex = 0;
        // 
        // panel3
        // 
        panel3.Controls.Add(btnRemove);
        panel3.Controls.Add(btnAdd);
        panel3.Controls.Add(nudQuantity);
        panel3.Controls.Add(dgvProducts);
        panel3.Location = new Point(179, 3);
        panel3.Name = "panel3";
        panel3.Size = new Size(428, 456);
        panel3.TabIndex = 1;
        // 
        // btnRemove
        // 
        btnRemove.BackColor = Color.Red;
        btnRemove.Font = new Font("Segoe UI", 12F);
        btnRemove.ForeColor = Color.White;
        btnRemove.Location = new Point(3, 332);
        btnRemove.Name = "btnRemove";
        btnRemove.Size = new Size(75, 30);
        btnRemove.TabIndex = 3;
        btnRemove.Text = "SİL";
        btnRemove.UseVisualStyleBackColor = false;
        // 
        // btnAdd
        // 
        btnAdd.BackColor = Color.Green;
        btnAdd.Font = new Font("Segoe UI", 12F);
        btnAdd.ForeColor = Color.White;
        btnAdd.Location = new Point(147, 332);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(75, 30);
        btnAdd.TabIndex = 2;
        btnAdd.Text = "EKLE";
        btnAdd.UseVisualStyleBackColor = false;
        btnAdd.Click += btnAdd_Click;
        // 
        // nudQuantity
        // 
        nudQuantity.Font = new Font("Segoe UI", 12F);
        nudQuantity.Location = new Point(84, 332);
        nudQuantity.Name = "nudQuantity";
        nudQuantity.Size = new Size(57, 29);
        nudQuantity.TabIndex = 1;
        nudQuantity.TextAlign = HorizontalAlignment.Right;
        nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // dgvProducts
        // 
        dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvProducts.Dock = DockStyle.Top;
        dgvProducts.Location = new Point(0, 0);
        dgvProducts.Name = "dgvProducts";
        dgvProducts.Size = new Size(428, 326);
        dgvProducts.TabIndex = 0;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(btnFilter);
        groupBox1.Controls.Add(cmbReservation);
        groupBox1.Controls.Add(cmbTable);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Location = new Point(12, 13);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(621, 75);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Filtrele";
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
        // label1
        // 
        label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
        label1.ForeColor = Color.Black;
        label1.Location = new Point(11, 6);
        label1.Name = "label1";
        label1.Size = new Size(155, 57);
        label1.TabIndex = 4;
        label1.Text = "Sipariş Ekranı";
        label1.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // OrderForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1024, 639);
        Controls.Add(panel2);
        Controls.Add(panel1);
        Controls.Add(label1);
        Name = "OrderForm";
        Text = "OrderForm";
        Load += OrderForm_Load;
        panel2.ResumeLayout(false);
        panel2.PerformLayout();
        panel4.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvOrderItems).EndInit();
        panel1.ResumeLayout(false);
        flpTables.ResumeLayout(false);
        panel3.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ResumeLayout(false);
    }

    #endregion
    private Button btnGoPay;
    private Button btnSaveOrder;
    private Label lblCustomer;
    private Label label4;
    private Label lblTable;
    private Panel panel2;
    private Button btnFilter;
    private ComboBox cmbReservation;
    private ComboBox cmbTable;
    private Label label3;
    private Panel panel1;
    private FlowLayoutPanel flpTables;
    private GroupBox groupBox1;
    private Label label2;
    private Label label1;
    private FlowLayoutPanel flpCategories;
    private Panel panel3;
    private DataGridView dgvProducts;
    private NumericUpDown nudQuantity;
    private Button btnRemove;
    private Button btnAdd;
    private Label lblReservation;
    private Panel panel4;
    private Label lblGeneralTotal;
    private Label label10;
    private Label lblKdv;
    private Label label8;
    private Label lblTotal;
    private Label label5;
    private DataGridView dgvOrderItems;
}