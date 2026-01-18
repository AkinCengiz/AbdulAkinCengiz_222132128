namespace AbdulAkinCengiz_222132128.WinFormUI;

partial class OrderPaymentForm
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
        nudOrderNumber = new NumericUpDown();
        btnGetInfo = new Button();
        ((System.ComponentModel.ISupportInitialize)nudOrderNumber).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(13, 9);
        label1.Margin = new Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new Size(89, 21);
        label1.TabIndex = 0;
        label1.Text = "Sipariş No :";
        // 
        // nudOrderNumber
        // 
        nudOrderNumber.Location = new Point(124, 7);
        nudOrderNumber.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
        nudOrderNumber.Name = "nudOrderNumber";
        nudOrderNumber.Size = new Size(120, 29);
        nudOrderNumber.TabIndex = 1;
        nudOrderNumber.TextAlign = HorizontalAlignment.Right;
        nudOrderNumber.ValueChanged += nudOrderNumber_ValueChanged;
        // 
        // btnGetInfo
        // 
        btnGetInfo.BackColor = Color.RoyalBlue;
        btnGetInfo.ForeColor = Color.White;
        btnGetInfo.Location = new Point(13, 42);
        btnGetInfo.Name = "btnGetInfo";
        btnGetInfo.Size = new Size(231, 40);
        btnGetInfo.TabIndex = 2;
        btnGetInfo.Text = "Ödeme Bilgilerini Göster";
        btnGetInfo.UseVisualStyleBackColor = false;
        btnGetInfo.Click += btnGetInfo_Click;
        // 
        // OrderPaymentForm
        // 
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(261, 94);
        Controls.Add(btnGetInfo);
        Controls.Add(nudOrderNumber);
        Controls.Add(label1);
        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        Margin = new Padding(4);
        Name = "OrderPaymentForm";
        Text = "Sipariş Görüntüleme";
        Load += OrderPaymentForm_Load;
        ((System.ComponentModel.ISupportInitialize)nudOrderNumber).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private NumericUpDown nudOrderNumber;
    private Button btnGetInfo;
}