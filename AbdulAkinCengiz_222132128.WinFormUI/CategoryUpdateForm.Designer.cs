namespace AbdulAkinCengiz_222132128.WinFormUI;

partial class CategoryUpdateForm
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
        txtId = new TextBox();
        txtName = new TextBox();
        label2 = new Label();
        cbxIsActive = new CheckBox();
        cbxDeleted = new CheckBox();
        btnUpdate = new Button();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(21, 36);
        label1.Name = "label1";
        label1.Size = new Size(32, 21);
        label1.TabIndex = 0;
        label1.Text = "ID :";
        // 
        // txtId
        // 
        txtId.Enabled = false;
        txtId.Location = new Point(122, 33);
        txtId.Name = "txtId";
        txtId.Size = new Size(168, 29);
        txtId.TabIndex = 1;
        // 
        // txtName
        // 
        txtName.Location = new Point(122, 68);
        txtName.Name = "txtName";
        txtName.Size = new Size(168, 29);
        txtName.TabIndex = 3;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(21, 71);
        label2.Name = "label2";
        label2.Size = new Size(75, 21);
        label2.TabIndex = 2;
        label2.Text = "Kategori :";
        // 
        // cbxIsActive
        // 
        cbxIsActive.AutoSize = true;
        cbxIsActive.Location = new Point(122, 103);
        cbxIsActive.Name = "cbxIsActive";
        cbxIsActive.Size = new Size(90, 25);
        cbxIsActive.TabIndex = 4;
        cbxIsActive.Text = "Aktif Mi?";
        cbxIsActive.UseVisualStyleBackColor = true;
        // 
        // cbxDeleted
        // 
        cbxDeleted.AutoSize = true;
        cbxDeleted.Location = new Point(122, 134);
        cbxDeleted.Name = "cbxDeleted";
        cbxDeleted.Size = new Size(101, 25);
        cbxDeleted.TabIndex = 5;
        cbxDeleted.Text = "Silindi Mi?";
        cbxDeleted.UseVisualStyleBackColor = true;
        // 
        // btnUpdate
        // 
        btnUpdate.BackColor = Color.Green;
        btnUpdate.ForeColor = Color.White;
        btnUpdate.Location = new Point(122, 165);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(168, 43);
        btnUpdate.TabIndex = 6;
        btnUpdate.Text = "Güncelle";
        btnUpdate.UseVisualStyleBackColor = false;
        btnUpdate.Click += btnUpdate_Click;
        // 
        // CategoryUpdateForm
        // 
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Silver;
        ClientSize = new Size(310, 220);
        Controls.Add(btnUpdate);
        Controls.Add(cbxDeleted);
        Controls.Add(cbxIsActive);
        Controls.Add(txtName);
        Controls.Add(label2);
        Controls.Add(txtId);
        Controls.Add(label1);
        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        Margin = new Padding(4);
        Name = "CategoryUpdateForm";
        Text = "Kategori Güncelle";
        Load += CategoryUpdateForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private TextBox txtId;
    private TextBox txtName;
    private Label label2;
    private CheckBox cbxIsActive;
    private CheckBox cbxDeleted;
    private Button btnUpdate;
}