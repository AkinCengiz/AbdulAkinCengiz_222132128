namespace AbdulAkinCengiz_222132128.WinFormUI;

partial class CategoryCreateForm
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
        btnCreate = new Button();
        label1 = new Label();
        txtCategory = new TextBox();
        SuspendLayout();
        // 
        // btnCreate
        // 
        btnCreate.Location = new Point(210, 48);
        btnCreate.Margin = new Padding(4, 4, 4, 4);
        btnCreate.Name = "btnCreate";
        btnCreate.Size = new Size(96, 32);
        btnCreate.TabIndex = 0;
        btnCreate.Text = "Ekle";
        btnCreate.UseVisualStyleBackColor = true;
        btnCreate.Click += btnCreate_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(15, 15);
        label1.Margin = new Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new Size(102, 21);
        label1.TabIndex = 1;
        label1.Text = "Kategori Adı :";
        // 
        // txtCategory
        // 
        txtCategory.Location = new Point(124, 12);
        txtCategory.Name = "txtCategory";
        txtCategory.Size = new Size(182, 29);
        txtCategory.TabIndex = 2;
        // 
        // CategoryCreateForm
        // 
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(321, 90);
        Controls.Add(txtCategory);
        Controls.Add(label1);
        Controls.Add(btnCreate);
        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        Margin = new Padding(4, 4, 4, 4);
        Name = "CategoryCreateForm";
        Text = "Kategori Ekle";
        Load += CategoryCreateForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button btnCreate;
    private Label label1;
    private TextBox txtCategory;
}