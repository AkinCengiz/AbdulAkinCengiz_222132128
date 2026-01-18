namespace AbdulAkinCengiz_222132128.WinFormUI;

partial class ProductAndCategoryManagementForm
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
        btnDelete = new Button();
        btnUpdate = new Button();
        btnCreate = new Button();
        groupBox1 = new GroupBox();
        dgvCategories = new DataGridView();
        label2 = new Label();
        panel2 = new Panel();
        groupBox4 = new GroupBox();
        dgvProducts = new DataGridView();
        groupBox3 = new GroupBox();
        cmbSorting = new ComboBox();
        label6 = new Label();
        cmbCategories = new ComboBox();
        label5 = new Label();
        label4 = new Label();
        panel3 = new Panel();
        groupBox2 = new GroupBox();
        btnProductDelete = new Button();
        gbxUpdateAndDelete = new GroupBox();
        btnCancel = new Button();
        txtId = new TextBox();
        cbxIsDeleted = new CheckBox();
        label11 = new Label();
        cbxIsActive = new CheckBox();
        btnProductUpdate = new Button();
        cmbCategory = new ComboBox();
        btnProductCreate = new Button();
        nudStock = new NumericUpDown();
        nudPrice = new NumericUpDown();
        label10 = new Label();
        label9 = new Label();
        label8 = new Label();
        txtProductName = new TextBox();
        label7 = new Label();
        label3 = new Label();
        panel1.SuspendLayout();
        groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
        panel2.SuspendLayout();
        groupBox4.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
        groupBox3.SuspendLayout();
        panel3.SuspendLayout();
        groupBox2.SuspendLayout();
        gbxUpdateAndDelete.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudStock).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudPrice).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
        label1.ForeColor = Color.Black;
        label1.Location = new Point(12, 9);
        label1.Name = "label1";
        label1.Size = new Size(305, 57);
        label1.TabIndex = 3;
        label1.Text = "Kategori ve Ürün Yönetimi";
        label1.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // panel1
        // 
        panel1.Controls.Add(btnDelete);
        panel1.Controls.Add(btnUpdate);
        panel1.Controls.Add(btnCreate);
        panel1.Controls.Add(groupBox1);
        panel1.Controls.Add(label2);
        panel1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        panel1.Location = new Point(12, 69);
        panel1.Name = "panel1";
        panel1.Size = new Size(251, 541);
        panel1.TabIndex = 4;
        // 
        // btnDelete
        // 
        btnDelete.BackColor = Color.Red;
        btnDelete.Enabled = false;
        btnDelete.ForeColor = Color.White;
        btnDelete.Location = new Point(6, 500);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(239, 32);
        btnDelete.TabIndex = 9;
        btnDelete.Text = "Sil";
        btnDelete.UseVisualStyleBackColor = false;
        btnDelete.Click += btnDelete_Click;
        // 
        // btnUpdate
        // 
        btnUpdate.BackColor = Color.SeaGreen;
        btnUpdate.ForeColor = Color.White;
        btnUpdate.Location = new Point(6, 462);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(239, 32);
        btnUpdate.TabIndex = 8;
        btnUpdate.Text = "Güncelle";
        btnUpdate.UseVisualStyleBackColor = false;
        btnUpdate.Click += btnUpdate_Click;
        // 
        // btnCreate
        // 
        btnCreate.BackColor = Color.DodgerBlue;
        btnCreate.ForeColor = Color.White;
        btnCreate.Location = new Point(6, 424);
        btnCreate.Name = "btnCreate";
        btnCreate.Size = new Size(239, 32);
        btnCreate.TabIndex = 7;
        btnCreate.Text = "Ekle";
        btnCreate.UseVisualStyleBackColor = false;
        btnCreate.Click += btnCreate_Click;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(dgvCategories);
        groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        groupBox1.Location = new Point(3, 37);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(245, 381);
        groupBox1.TabIndex = 6;
        groupBox1.TabStop = false;
        groupBox1.Text = "Kategoriler";
        // 
        // dgvCategories
        // 
        dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvCategories.Dock = DockStyle.Fill;
        dgvCategories.Location = new Point(3, 25);
        dgvCategories.Name = "dgvCategories";
        dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCategories.Size = new Size(239, 353);
        dgvCategories.TabIndex = 6;
        dgvCategories.CellDoubleClick += dgvCategories_CellDoubleClick;
        // 
        // label2
        // 
        label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
        label2.ForeColor = Color.Black;
        label2.Location = new Point(6, 8);
        label2.Name = "label2";
        label2.Size = new Size(223, 26);
        label2.TabIndex = 5;
        label2.Text = "Kategori Yönetimi";
        label2.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // panel2
        // 
        panel2.Controls.Add(groupBox4);
        panel2.Controls.Add(groupBox3);
        panel2.Controls.Add(label4);
        panel2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        panel2.Location = new Point(269, 69);
        panel2.Name = "panel2";
        panel2.Size = new Size(487, 541);
        panel2.TabIndex = 5;
        // 
        // groupBox4
        // 
        groupBox4.Controls.Add(dgvProducts);
        groupBox4.Location = new Point(3, 108);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new Size(481, 424);
        groupBox4.TabIndex = 8;
        groupBox4.TabStop = false;
        groupBox4.Text = "Ürün Listesi";
        // 
        // dgvProducts
        // 
        dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvProducts.Dock = DockStyle.Fill;
        dgvProducts.Location = new Point(3, 25);
        dgvProducts.Name = "dgvProducts";
        dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvProducts.Size = new Size(475, 396);
        dgvProducts.TabIndex = 0;
        dgvProducts.CellDoubleClick += dgvProducts_CellDoubleClick;
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(cmbSorting);
        groupBox3.Controls.Add(label6);
        groupBox3.Controls.Add(cmbCategories);
        groupBox3.Controls.Add(label5);
        groupBox3.Location = new Point(6, 37);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(475, 65);
        groupBox3.TabIndex = 7;
        groupBox3.TabStop = false;
        groupBox3.Text = "Filtre Paneli";
        // 
        // cmbSorting
        // 
        cmbSorting.FormattingEnabled = true;
        cmbSorting.Location = new Point(299, 22);
        cmbSorting.Name = "cmbSorting";
        cmbSorting.Size = new Size(170, 29);
        cmbSorting.TabIndex = 3;
        cmbSorting.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(243, 25);
        label6.Name = "label6";
        label6.Size = new Size(56, 21);
        label6.TabIndex = 2;
        label6.Text = "Sırala :";
        // 
        // cmbCategories
        // 
        cmbCategories.FormattingEnabled = true;
        cmbCategories.Location = new Point(87, 22);
        cmbCategories.Name = "cmbCategories";
        cmbCategories.Size = new Size(150, 29);
        cmbCategories.TabIndex = 1;
        cmbCategories.SelectedIndexChanged += cmbCategories_SelectedIndexChanged;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(6, 25);
        label5.Name = "label5";
        label5.Size = new Size(75, 21);
        label5.TabIndex = 0;
        label5.Text = "Kategori :";
        // 
        // label4
        // 
        label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
        label4.ForeColor = Color.Black;
        label4.Location = new Point(6, 8);
        label4.Name = "label4";
        label4.Size = new Size(223, 26);
        label4.TabIndex = 6;
        label4.Text = "Ürün Bilgileri";
        label4.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // panel3
        // 
        panel3.Controls.Add(groupBox2);
        panel3.Controls.Add(label3);
        panel3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        panel3.Location = new Point(762, 69);
        panel3.Name = "panel3";
        panel3.Size = new Size(251, 541);
        panel3.TabIndex = 6;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(btnProductDelete);
        groupBox2.Controls.Add(gbxUpdateAndDelete);
        groupBox2.Controls.Add(btnProductUpdate);
        groupBox2.Controls.Add(cmbCategory);
        groupBox2.Controls.Add(btnProductCreate);
        groupBox2.Controls.Add(nudStock);
        groupBox2.Controls.Add(nudPrice);
        groupBox2.Controls.Add(label10);
        groupBox2.Controls.Add(label9);
        groupBox2.Controls.Add(label8);
        groupBox2.Controls.Add(txtProductName);
        groupBox2.Controls.Add(label7);
        groupBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        groupBox2.Location = new Point(3, 37);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(245, 495);
        groupBox2.TabIndex = 6;
        groupBox2.TabStop = false;
        groupBox2.Text = "Ürün";
        // 
        // btnProductDelete
        // 
        btnProductDelete.BackColor = Color.Red;
        btnProductDelete.Enabled = false;
        btnProductDelete.ForeColor = Color.White;
        btnProductDelete.Location = new Point(0, 460);
        btnProductDelete.Name = "btnProductDelete";
        btnProductDelete.Size = new Size(245, 32);
        btnProductDelete.TabIndex = 9;
        btnProductDelete.Text = "Sil";
        btnProductDelete.UseVisualStyleBackColor = false;
        btnProductDelete.Click += btnProductDelete_Click;
        // 
        // gbxUpdateAndDelete
        // 
        gbxUpdateAndDelete.Controls.Add(btnCancel);
        gbxUpdateAndDelete.Controls.Add(txtId);
        gbxUpdateAndDelete.Controls.Add(cbxIsDeleted);
        gbxUpdateAndDelete.Controls.Add(label11);
        gbxUpdateAndDelete.Controls.Add(cbxIsActive);
        gbxUpdateAndDelete.Location = new Point(6, 219);
        gbxUpdateAndDelete.Name = "gbxUpdateAndDelete";
        gbxUpdateAndDelete.Size = new Size(233, 194);
        gbxUpdateAndDelete.TabIndex = 12;
        gbxUpdateAndDelete.TabStop = false;
        gbxUpdateAndDelete.Text = "Güncelleme ve Silme Paneli";
        gbxUpdateAndDelete.Visible = false;
        // 
        // btnCancel
        // 
        btnCancel.BackColor = Color.DodgerBlue;
        btnCancel.ForeColor = Color.White;
        btnCancel.Location = new Point(12, 125);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(221, 32);
        btnCancel.TabIndex = 12;
        btnCancel.Text = "İptal";
        btnCancel.UseVisualStyleBackColor = false;
        btnCancel.Click += btnCancel_Click;
        // 
        // txtId
        // 
        txtId.Enabled = false;
        txtId.Location = new Point(79, 28);
        txtId.Name = "txtId";
        txtId.Size = new Size(148, 29);
        txtId.TabIndex = 9;
        // 
        // cbxIsDeleted
        // 
        cbxIsDeleted.AutoSize = true;
        cbxIsDeleted.Location = new Point(79, 94);
        cbxIsDeleted.Name = "cbxIsDeleted";
        cbxIsDeleted.Size = new Size(101, 25);
        cbxIsDeleted.TabIndex = 11;
        cbxIsDeleted.Text = "Silindi Mi?";
        cbxIsDeleted.UseVisualStyleBackColor = true;
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Location = new Point(6, 31);
        label11.Name = "label11";
        label11.Size = new Size(32, 21);
        label11.TabIndex = 8;
        label11.Text = "ID :";
        // 
        // cbxIsActive
        // 
        cbxIsActive.AutoSize = true;
        cbxIsActive.Location = new Point(79, 63);
        cbxIsActive.Name = "cbxIsActive";
        cbxIsActive.Size = new Size(90, 25);
        cbxIsActive.TabIndex = 10;
        cbxIsActive.Text = "Aktif Mi?";
        cbxIsActive.UseVisualStyleBackColor = true;
        // 
        // btnProductUpdate
        // 
        btnProductUpdate.BackColor = Color.SeaGreen;
        btnProductUpdate.Enabled = false;
        btnProductUpdate.ForeColor = Color.White;
        btnProductUpdate.Location = new Point(3, 419);
        btnProductUpdate.Name = "btnProductUpdate";
        btnProductUpdate.Size = new Size(245, 32);
        btnProductUpdate.TabIndex = 8;
        btnProductUpdate.Text = "Güncelle";
        btnProductUpdate.UseVisualStyleBackColor = false;
        btnProductUpdate.Click += btnProductUpdate_Click;
        // 
        // cmbCategory
        // 
        cmbCategory.FormattingEnabled = true;
        cmbCategory.Location = new Point(91, 146);
        cmbCategory.Name = "cmbCategory";
        cmbCategory.Size = new Size(148, 29);
        cmbCategory.TabIndex = 7;
        // 
        // btnProductCreate
        // 
        btnProductCreate.BackColor = Color.DodgerBlue;
        btnProductCreate.ForeColor = Color.White;
        btnProductCreate.Location = new Point(0, 181);
        btnProductCreate.Name = "btnProductCreate";
        btnProductCreate.Size = new Size(245, 32);
        btnProductCreate.TabIndex = 7;
        btnProductCreate.Text = "Ekle";
        btnProductCreate.UseVisualStyleBackColor = false;
        btnProductCreate.Click += btnProductCreate_Click;
        // 
        // nudStock
        // 
        nudStock.Location = new Point(91, 111);
        nudStock.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        nudStock.Name = "nudStock";
        nudStock.Size = new Size(148, 29);
        nudStock.TabIndex = 6;
        nudStock.TextAlign = HorizontalAlignment.Center;
        // 
        // nudPrice
        // 
        nudPrice.DecimalPlaces = 2;
        nudPrice.Location = new Point(91, 76);
        nudPrice.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        nudPrice.Name = "nudPrice";
        nudPrice.Size = new Size(148, 29);
        nudPrice.TabIndex = 5;
        nudPrice.TextAlign = HorizontalAlignment.Right;
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Location = new Point(3, 149);
        label10.Name = "label10";
        label10.Size = new Size(75, 21);
        label10.TabIndex = 4;
        label10.Text = "Kategori :";
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Location = new Point(6, 78);
        label9.Name = "label9";
        label9.Size = new Size(54, 21);
        label9.TabIndex = 3;
        label9.Text = "Fiyatı :";
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Location = new Point(6, 113);
        label8.Name = "label8";
        label8.Size = new Size(47, 21);
        label8.TabIndex = 2;
        label8.Text = "Stok :";
        // 
        // txtProductName
        // 
        txtProductName.Location = new Point(91, 41);
        txtProductName.Name = "txtProductName";
        txtProductName.Size = new Size(148, 29);
        txtProductName.TabIndex = 1;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new Point(6, 44);
        label7.Name = "label7";
        label7.Size = new Size(79, 21);
        label7.TabIndex = 0;
        label7.Text = "Ürün Adı :";
        // 
        // label3
        // 
        label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
        label3.ForeColor = Color.Black;
        label3.Location = new Point(6, 8);
        label3.Name = "label3";
        label3.Size = new Size(223, 26);
        label3.TabIndex = 5;
        label3.Text = "Ürün Bilgileri";
        label3.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // ProductAndCategoryManagementForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Silver;
        ClientSize = new Size(1025, 622);
        Controls.Add(panel3);
        Controls.Add(panel2);
        Controls.Add(panel1);
        Controls.Add(label1);
        Name = "ProductAndCategoryManagementForm";
        Text = "Kategori ve Ürün Yönetimi";
        Load += ProductAndCategoryManagementForm_Load;
        panel1.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
        panel2.ResumeLayout(false);
        groupBox4.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        panel3.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        gbxUpdateAndDelete.ResumeLayout(false);
        gbxUpdateAndDelete.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudStock).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudPrice).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Label label1;
    private Panel panel1;
    private Label label2;
    private Button btnDelete;
    private Button btnUpdate;
    private Button btnCreate;
    private GroupBox groupBox1;
    private DataGridView dgvCategories;
    private Panel panel2;
    private Panel panel3;
    private Button btnProductDelete;
    private Button btnProductUpdate;
    private Button btnProductCreate;
    private GroupBox groupBox2;
    private Label label3;
    private GroupBox groupBox4;
    private GroupBox groupBox3;
    private ComboBox cmbSorting;
    private Label label6;
    private ComboBox cmbCategories;
    private Label label5;
    private Label label4;
    private DataGridView dgvProducts;
    private TextBox txtProductName;
    private Label label7;
    private Label label10;
    private Label label9;
    private Label label8;
    private NumericUpDown nudStock;
    private NumericUpDown nudPrice;
    private ComboBox cmbCategory;
    private CheckBox cbxIsDeleted;
    private CheckBox cbxIsActive;
    private TextBox txtId;
    private Label label11;
    private GroupBox gbxUpdateAndDelete;
    private Button btnCancel;
}