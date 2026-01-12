using AbdulAkinCengiz_222132128.Business.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace AbdulAkinCengiz_222132128.WinFormUI
{
    public partial class MainForm : Form
    {
        private readonly IReservationService _reservationService;
        private readonly IDashboardService _dashboardService;
        private readonly IServiceProvider _serviceProvider;
        private bool _loadingDashboard = false;
        public MainForm(IReservationService reservationService, IDashboardService dashboardService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _reservationService = reservationService;
            _dashboardService = dashboardService;
            _serviceProvider = serviceProvider;
            ConfigureGrid();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadDashboardAsync();
            await GetReservation();
        }

        private void ConfigureGrid()
        {
            dgwTodayReservations.AutoGenerateColumns = false;
            dgwTodayReservations.AllowUserToAddRows = false;
            dgwTodayReservations.AllowUserToDeleteRows = false;
            dgwTodayReservations.ReadOnly = true;
            dgwTodayReservations.MultiSelect = false;
            dgwTodayReservations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgwTodayReservations.RowHeadersVisible = false;

            dgwTodayReservations.Columns.Clear();

            // Rezervasyon No
            dgwTodayReservations.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Masa",
                DataPropertyName = "TableName",
                Width = 60
            });

            // Müşteri
            dgwTodayReservations.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Müşteri",
                DataPropertyName = "CustomerFullName", // birazdan ekleyeceğiz
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Saat
            dgwTodayReservations.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Saat",
                DataPropertyName = "StartAt",
                Width = 90,
                DefaultCellStyle = { Format = "HH:mm" }
            });

            // Kişi sayısı
            dgwTodayReservations.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Kişi",
                DataPropertyName = "GuestCount",
                Width = 60
            });

            // Durum
            dgwTodayReservations.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "Onay",
                DataPropertyName = "IsConfirm",
                Width = 60
            });
        }

        private async Task GetReservation()
        {
            //var data = _reservationService.GetTodayReservationAsync();
            //dgwTodayReservations.DataSource = data.Result.Data;
            try
            {
                var result = await _reservationService.GetTodayReservationAsync();

                if (!result.Success)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                //dgwTodayReservations.AutoGenerateColumns = false; // kolonları siz yönetiyorsanız
                var list = result.Data.ToList();
                dgwTodayReservations.DataSource = list;
                //lblReservationCount.Text = list.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadDashboardAsync()
        {
            if (_loadingDashboard) return;
            _loadingDashboard = true;

            try
            {
                var result = await _dashboardService.GetSummaryAsync();
                if (!result.Success)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                lblActiveOrders.Text = result.Data.ActiveOrderCount.ToString();
                lblOpenTable.Text = result.Data.OpenTableCount.ToString();
                lblReservationCount.Text = result.Data.TodayReservationCount.ToString();
            }
            finally
            {
                _loadingDashboard = false;
            }
        }

        private void btnTableList_Click(object sender, EventArgs e)
        {
            TableListForm tableListForm = _serviceProvider.GetRequiredService<TableListForm>();
            tableListForm.ShowDialog();
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            OrderForm orderForm = _serviceProvider.GetRequiredService<OrderForm>();
            orderForm.ShowDialog();
        }
    }
}
