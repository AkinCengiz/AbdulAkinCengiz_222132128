namespace AbdulAkinCengiz_222132128.WinFormUI
{
    partial class MainForm
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
            btnTableList = new Button();
            btnCreateOrder = new Button();
            btnGetPaid = new Button();
            btnProductManagement = new Button();
            gbxReport = new GroupBox();
            panel2 = new Panel();
            lblOpenTable = new Label();
            label4 = new Label();
            panel3 = new Panel();
            lblActiveOrders = new Label();
            label3 = new Label();
            panel1 = new Panel();
            lblReservationCount = new Label();
            label2 = new Label();
            gbxReservations = new GroupBox();
            panel4 = new Panel();
            panel5 = new Panel();
            dgwTodayReservations = new DataGridView();
            label5 = new Label();
            gbxReport.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            gbxReservations.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgwTodayReservations).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(155, 57);
            label1.TabIndex = 0;
            label1.Text = "Ana Panel";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnTableList
            // 
            btnTableList.BackColor = Color.Blue;
            btnTableList.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnTableList.ForeColor = Color.White;
            btnTableList.Location = new Point(571, 12);
            btnTableList.Name = "btnTableList";
            btnTableList.Size = new Size(126, 57);
            btnTableList.TabIndex = 1;
            btnTableList.Text = "Masa Listesi";
            btnTableList.UseVisualStyleBackColor = false;
            btnTableList.Click += btnTableList_Click;
            // 
            // btnCreateOrder
            // 
            btnCreateOrder.BackColor = Color.Blue;
            btnCreateOrder.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCreateOrder.ForeColor = Color.White;
            btnCreateOrder.Location = new Point(703, 12);
            btnCreateOrder.Name = "btnCreateOrder";
            btnCreateOrder.Size = new Size(126, 57);
            btnCreateOrder.TabIndex = 2;
            btnCreateOrder.Text = "Sipariş Oluştur";
            btnCreateOrder.UseVisualStyleBackColor = false;
            btnCreateOrder.Click += btnCreateOrder_Click;
            // 
            // btnGetPaid
            // 
            btnGetPaid.BackColor = Color.Blue;
            btnGetPaid.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnGetPaid.ForeColor = Color.White;
            btnGetPaid.Location = new Point(835, 12);
            btnGetPaid.Name = "btnGetPaid";
            btnGetPaid.Size = new Size(126, 57);
            btnGetPaid.TabIndex = 3;
            btnGetPaid.Text = "Ödeme Al";
            btnGetPaid.UseVisualStyleBackColor = false;
            // 
            // btnProductManagement
            // 
            btnProductManagement.BackColor = Color.Blue;
            btnProductManagement.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnProductManagement.ForeColor = Color.White;
            btnProductManagement.Location = new Point(967, 12);
            btnProductManagement.Name = "btnProductManagement";
            btnProductManagement.Size = new Size(126, 57);
            btnProductManagement.TabIndex = 4;
            btnProductManagement.Text = "Ürün Yönetimi";
            btnProductManagement.UseVisualStyleBackColor = false;
            // 
            // gbxReport
            // 
            gbxReport.Controls.Add(panel2);
            gbxReport.Controls.Add(panel3);
            gbxReport.Controls.Add(panel1);
            gbxReport.Location = new Point(12, 72);
            gbxReport.Name = "gbxReport";
            gbxReport.Size = new Size(1081, 100);
            gbxReport.TabIndex = 5;
            gbxReport.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.LimeGreen;
            panel2.Controls.Add(lblOpenTable);
            panel2.Controls.Add(label4);
            panel2.Location = new Point(364, 22);
            panel2.Name = "panel2";
            panel2.Size = new Size(352, 72);
            panel2.TabIndex = 1;
            // 
            // lblOpenTable
            // 
            lblOpenTable.AutoSize = true;
            lblOpenTable.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblOpenTable.ForeColor = Color.White;
            lblOpenTable.Location = new Point(290, 14);
            lblOpenTable.Name = "lblOpenTable";
            lblOpenTable.Size = new Size(38, 45);
            lblOpenTable.TabIndex = 3;
            lblOpenTable.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label4.ForeColor = Color.White;
            label4.Location = new Point(25, 24);
            label4.Name = "label4";
            label4.Size = new Size(95, 21);
            label4.TabIndex = 2;
            label4.Text = "Açık Masa :";
            // 
            // panel3
            // 
            panel3.BackColor = Color.Orange;
            panel3.Controls.Add(lblActiveOrders);
            panel3.Controls.Add(label3);
            panel3.Location = new Point(723, 22);
            panel3.Name = "panel3";
            panel3.Size = new Size(352, 72);
            panel3.TabIndex = 1;
            // 
            // lblActiveOrders
            // 
            lblActiveOrders.AutoSize = true;
            lblActiveOrders.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblActiveOrders.ForeColor = Color.White;
            lblActiveOrders.Location = new Point(290, 14);
            lblActiveOrders.Name = "lblActiveOrders";
            lblActiveOrders.Size = new Size(38, 45);
            lblActiveOrders.TabIndex = 3;
            lblActiveOrders.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label3.ForeColor = Color.White;
            label3.Location = new Point(25, 24);
            label3.Name = "label3";
            label3.Size = new Size(130, 21);
            label3.TabIndex = 2;
            label3.Text = "Aktif Siparişler :";
            // 
            // panel1
            // 
            panel1.BackColor = Color.RoyalBlue;
            panel1.Controls.Add(lblReservationCount);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(6, 22);
            panel1.Name = "panel1";
            panel1.Size = new Size(352, 72);
            panel1.TabIndex = 0;
            // 
            // lblReservationCount
            // 
            lblReservationCount.AutoSize = true;
            lblReservationCount.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblReservationCount.ForeColor = Color.White;
            lblReservationCount.Location = new Point(286, 14);
            lblReservationCount.Name = "lblReservationCount";
            lblReservationCount.Size = new Size(38, 45);
            lblReservationCount.TabIndex = 1;
            lblReservationCount.Text = "0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label2.ForeColor = Color.White;
            label2.Location = new Point(21, 24);
            label2.Name = "label2";
            label2.Size = new Size(188, 21);
            label2.TabIndex = 0;
            label2.Text = "Bugünkü Rezervasyon :";
            // 
            // gbxReservations
            // 
            gbxReservations.Controls.Add(panel4);
            gbxReservations.Location = new Point(12, 190);
            gbxReservations.Name = "gbxReservations";
            gbxReservations.Size = new Size(1081, 468);
            gbxReservations.TabIndex = 6;
            gbxReservations.TabStop = false;
            gbxReservations.Text = "Rezervasyonlar";
            // 
            // panel4
            // 
            panel4.Controls.Add(panel5);
            panel4.Controls.Add(label5);
            panel4.Location = new Point(3, 19);
            panel4.Name = "panel4";
            panel4.Size = new Size(1072, 440);
            panel4.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.Controls.Add(dgwTodayReservations);
            panel5.Location = new Point(9, 52);
            panel5.Name = "panel5";
            panel5.Size = new Size(1063, 385);
            panel5.TabIndex = 1;
            // 
            // dgwTodayReservations
            // 
            dgwTodayReservations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgwTodayReservations.Dock = DockStyle.Fill;
            dgwTodayReservations.Location = new Point(0, 0);
            dgwTodayReservations.Name = "dgwTodayReservations";
            dgwTodayReservations.Size = new Size(1063, 385);
            dgwTodayReservations.TabIndex = 0;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label5.Location = new Point(3, 10);
            label5.Name = "label5";
            label5.Size = new Size(200, 21);
            label5.TabIndex = 0;
            label5.Text = "Bugünkü Rezervasyonlar";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(1102, 668);
            Controls.Add(gbxReservations);
            Controls.Add(gbxReport);
            Controls.Add(btnProductManagement);
            Controls.Add(btnGetPaid);
            Controls.Add(btnCreateOrder);
            Controls.Add(btnTableList);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rezervasyon Yönetim Sistemi";
            Load += MainForm_Load;
            gbxReport.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            gbxReservations.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgwTodayReservations).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Button btnTableList;
        private Button btnCreateOrder;
        private Button btnGetPaid;
        private Button btnProductManagement;
        private GroupBox gbxReport;
        private Panel panel2;
        private Panel panel3;
        private Panel panel1;
        private Label lblReservationCount;
        private Label label2;
        private Label lblOpenTable;
        private Label label4;
        private Label lblActiveOrders;
        private Label label3;
        private GroupBox gbxReservations;
        private Panel panel4;
        private Label label5;
        private Panel panel5;
        private DataGridView dgwTodayReservations;
    }
}