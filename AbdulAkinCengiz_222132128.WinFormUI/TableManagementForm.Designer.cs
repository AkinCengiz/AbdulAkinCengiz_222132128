namespace AbdulAkinCengiz_222132128.WinFormUI;

partial class TableManagementForm
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
        gbxTableList = new GroupBox();
        lstTableList = new ListBox();
        gbxTableInfos = new GroupBox();
        dgvTableInfos = new DataGridView();
        groupBox1 = new GroupBox();
        groupBox3 = new GroupBox();
        btnDelete = new Button();
        cbxIsDeleted = new CheckBox();
        cbxIsActive = new CheckBox();
        txtId = new TextBox();
        label6 = new Label();
        btnUpdate = new Button();
        nudUpdateGuestCount = new NumericUpDown();
        txtUpdateName = new TextBox();
        label2 = new Label();
        label5 = new Label();
        groupBox2 = new GroupBox();
        btnAdd = new Button();
        nudAddGuestCount = new NumericUpDown();
        txtAddName = new TextBox();
        label3 = new Label();
        label4 = new Label();
        gbxTableList.SuspendLayout();
        gbxTableInfos.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTableInfos).BeginInit();
        groupBox1.SuspendLayout();
        groupBox3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudUpdateGuestCount).BeginInit();
        groupBox2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudAddGuestCount).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
        label1.ForeColor = Color.Black;
        label1.Location = new Point(12, 9);
        label1.Name = "label1";
        label1.Size = new Size(189, 57);
        label1.TabIndex = 1;
        label1.Text = "Masa Yönetimi";
        label1.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // gbxTableList
        // 
        gbxTableList.Controls.Add(lstTableList);
        gbxTableList.Font = new Font("Segoe UI", 12F);
        gbxTableList.Location = new Point(12, 69);
        gbxTableList.Name = "gbxTableList";
        gbxTableList.Size = new Size(189, 591);
        gbxTableList.TabIndex = 2;
        gbxTableList.TabStop = false;
        gbxTableList.Text = "Masa Listesi";
        // 
        // lstTableList
        // 
        lstTableList.FormattingEnabled = true;
        lstTableList.Location = new Point(6, 22);
        lstTableList.Name = "lstTableList";
        lstTableList.Size = new Size(177, 550);
        lstTableList.TabIndex = 0;
        // 
        // gbxTableInfos
        // 
        gbxTableInfos.Controls.Add(dgvTableInfos);
        gbxTableInfos.Font = new Font("Segoe UI", 12F);
        gbxTableInfos.Location = new Point(207, 69);
        gbxTableInfos.Name = "gbxTableInfos";
        gbxTableInfos.Size = new Size(540, 591);
        gbxTableInfos.TabIndex = 3;
        gbxTableInfos.TabStop = false;
        gbxTableInfos.Text = "Masa Bilgileri";
        // 
        // dgvTableInfos
        // 
        dgvTableInfos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvTableInfos.Dock = DockStyle.Fill;
        dgvTableInfos.Location = new Point(3, 25);
        dgvTableInfos.Name = "dgvTableInfos";
        dgvTableInfos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvTableInfos.Size = new Size(534, 563);
        dgvTableInfos.TabIndex = 0;
        dgvTableInfos.CellDoubleClick += dgvTableInfos_CellDoubleClick;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(groupBox3);
        groupBox1.Controls.Add(groupBox2);
        groupBox1.Font = new Font("Segoe UI", 12F);
        groupBox1.Location = new Point(753, 69);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(261, 591);
        groupBox1.TabIndex = 4;
        groupBox1.TabStop = false;
        groupBox1.Text = "Masa İşlemleri";
        // 
        // groupBox3
        // 
        groupBox3.BackColor = Color.LightGreen;
        groupBox3.Controls.Add(btnDelete);
        groupBox3.Controls.Add(cbxIsDeleted);
        groupBox3.Controls.Add(cbxIsActive);
        groupBox3.Controls.Add(txtId);
        groupBox3.Controls.Add(label6);
        groupBox3.Controls.Add(btnUpdate);
        groupBox3.Controls.Add(nudUpdateGuestCount);
        groupBox3.Controls.Add(txtUpdateName);
        groupBox3.Controls.Add(label2);
        groupBox3.Controls.Add(label5);
        groupBox3.Location = new Point(6, 255);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(249, 330);
        groupBox3.TabIndex = 4;
        groupBox3.TabStop = false;
        groupBox3.Text = "Masa Güncelleme ve Silme";
        // 
        // btnDelete
        // 
        btnDelete.BackColor = Color.Red;
        btnDelete.ForeColor = Color.White;
        btnDelete.Location = new Point(6, 271);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(237, 53);
        btnDelete.TabIndex = 15;
        btnDelete.Text = "SİL";
        btnDelete.UseVisualStyleBackColor = false;
        btnDelete.Click += btnDelete_Click;
        // 
        // cbxIsDeleted
        // 
        cbxIsDeleted.AutoSize = true;
        cbxIsDeleted.Location = new Point(93, 175);
        cbxIsDeleted.Name = "cbxIsDeleted";
        cbxIsDeleted.Size = new Size(101, 25);
        cbxIsDeleted.TabIndex = 14;
        cbxIsDeleted.Text = "Silindi Mi?";
        cbxIsDeleted.UseVisualStyleBackColor = true;
        // 
        // cbxIsActive
        // 
        cbxIsActive.AutoSize = true;
        cbxIsActive.Location = new Point(93, 144);
        cbxIsActive.Name = "cbxIsActive";
        cbxIsActive.Size = new Size(90, 25);
        cbxIsActive.TabIndex = 13;
        cbxIsActive.Text = "Aktif Mi?";
        cbxIsActive.UseVisualStyleBackColor = true;
        // 
        // txtId
        // 
        txtId.Location = new Point(93, 37);
        txtId.Name = "txtId";
        txtId.ReadOnly = true;
        txtId.Size = new Size(150, 29);
        txtId.TabIndex = 12;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(6, 40);
        label6.Name = "label6";
        label6.Size = new Size(32, 21);
        label6.TabIndex = 11;
        label6.Text = "ID :";
        // 
        // btnUpdate
        // 
        btnUpdate.BackColor = Color.SeaGreen;
        btnUpdate.ForeColor = Color.White;
        btnUpdate.Location = new Point(6, 212);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(237, 53);
        btnUpdate.TabIndex = 10;
        btnUpdate.Text = "GÜNCELLE";
        btnUpdate.UseVisualStyleBackColor = false;
        btnUpdate.Click += btnUpdate_Click;
        // 
        // nudUpdateGuestCount
        // 
        nudUpdateGuestCount.Location = new Point(93, 109);
        nudUpdateGuestCount.Name = "nudUpdateGuestCount";
        nudUpdateGuestCount.Size = new Size(150, 29);
        nudUpdateGuestCount.TabIndex = 9;
        nudUpdateGuestCount.TextAlign = HorizontalAlignment.Center;
        // 
        // txtUpdateName
        // 
        txtUpdateName.Location = new Point(93, 72);
        txtUpdateName.Name = "txtUpdateName";
        txtUpdateName.Size = new Size(150, 29);
        txtUpdateName.TabIndex = 8;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(6, 75);
        label2.Name = "label2";
        label2.Size = new Size(81, 21);
        label2.TabIndex = 6;
        label2.Text = "Masa Adı :";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(6, 111);
        label5.Name = "label5";
        label5.Size = new Size(85, 21);
        label5.TabIndex = 7;
        label5.Text = "Kişi Sayısı :";
        // 
        // groupBox2
        // 
        groupBox2.BackColor = Color.CornflowerBlue;
        groupBox2.Controls.Add(btnAdd);
        groupBox2.Controls.Add(nudAddGuestCount);
        groupBox2.Controls.Add(txtAddName);
        groupBox2.Controls.Add(label3);
        groupBox2.Controls.Add(label4);
        groupBox2.Location = new Point(6, 28);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(249, 221);
        groupBox2.TabIndex = 3;
        groupBox2.TabStop = false;
        groupBox2.Text = "Masa Ekleme";
        // 
        // btnAdd
        // 
        btnAdd.BackColor = Color.RoyalBlue;
        btnAdd.ForeColor = Color.White;
        btnAdd.Location = new Point(6, 156);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(237, 59);
        btnAdd.TabIndex = 5;
        btnAdd.Text = "EKLE";
        btnAdd.UseVisualStyleBackColor = false;
        btnAdd.Click += btnAdd_Click;
        // 
        // nudAddGuestCount
        // 
        nudAddGuestCount.Location = new Point(93, 82);
        nudAddGuestCount.Name = "nudAddGuestCount";
        nudAddGuestCount.Size = new Size(150, 29);
        nudAddGuestCount.TabIndex = 4;
        nudAddGuestCount.TextAlign = HorizontalAlignment.Center;
        nudAddGuestCount.Value = new decimal(new int[] { 2, 0, 0, 0 });
        // 
        // txtAddName
        // 
        txtAddName.Location = new Point(93, 45);
        txtAddName.Name = "txtAddName";
        txtAddName.Size = new Size(150, 29);
        txtAddName.TabIndex = 3;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(6, 48);
        label3.Name = "label3";
        label3.Size = new Size(81, 21);
        label3.TabIndex = 1;
        label3.Text = "Masa Adı :";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(6, 84);
        label4.Name = "label4";
        label4.Size = new Size(85, 21);
        label4.TabIndex = 2;
        label4.Text = "Kişi Sayısı :";
        // 
        // TableManagementForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Silver;
        ClientSize = new Size(1026, 672);
        Controls.Add(groupBox1);
        Controls.Add(gbxTableInfos);
        Controls.Add(gbxTableList);
        Controls.Add(label1);
        Name = "TableManagementForm";
        Text = "Masa Yönetimi";
        Load += TableManagementForm_Load;
        gbxTableList.ResumeLayout(false);
        gbxTableInfos.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvTableInfos).EndInit();
        groupBox1.ResumeLayout(false);
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudUpdateGuestCount).EndInit();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudAddGuestCount).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Label label1;
    private GroupBox gbxTableList;
    private ListBox lstTableList;
    private GroupBox gbxTableInfos;
    private DataGridView dgvTableInfos;
    private GroupBox groupBox1;
    private GroupBox groupBox3;
    private GroupBox groupBox2;
    private Label label3;
    private Label label4;
    private CheckBox cbxIsDeleted;
    private CheckBox cbxIsActive;
    private TextBox txtId;
    private Label label6;
    private Button btnUpdate;
    private NumericUpDown nudUpdateGuestCount;
    private TextBox txtUpdateName;
    private Label label2;
    private Label label5;
    private Button btnAdd;
    private NumericUpDown nudAddGuestCount;
    private TextBox txtAddName;
    private Button btnDelete;
}