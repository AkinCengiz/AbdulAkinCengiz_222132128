using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Dtos.Payment;
using AbdulAkinCengiz_222132128.WinFormUI.Util;
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

namespace AbdulAkinCengiz_222132128.WinFormUI;

public partial class OrderPaymentForm : Form
{
    private readonly IOrderService _orderService;
    private readonly IReservationService _reservationService;
    private readonly IServiceProvider _serviceProvider;

    private int SelectedReservationId = 0;
    private int _orderNumber = 0;

    private OrderPaymentResponseDto? _ctx;
    public OrderPaymentForm(IOrderService orderService, IReservationService reservationService, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _orderService = orderService;
        _reservationService = reservationService;
        _serviceProvider = serviceProvider;
    }

    private async void OrderPaymentForm_Load(object sender, EventArgs e)
    {
        
    }

    private async Task GetReservationAsync()
    {
        var result = await _reservationService.GetDetailedCompletedReservationsByOrderAsync(_orderNumber);

        if (!result.Success || result.Data == null)
        {
            SelectedReservationId = 0;
            MessageBox.Show(result.Message ?? "Kayıt bulunamadı.");
            return;
        }

        SelectedReservationId = result.Data.Id;
    }

    private async void btnGetInfo_Click(object sender, EventArgs e)
    {
        if (_orderNumber <= 0)
        {
            MessageBox.Show("Önce geçerli bir Order No girip bilgileri getirmelisiniz.");
            return;
        }

        await GetReservationAsync();
        var paymentForm = _serviceProvider.GetRequiredService<PaymentForm>();

        // DİKKAT: SetOrder metodunuz rezervasyonId mi bekliyor?
        paymentForm.SetOrder(SelectedReservationId);

        paymentForm.ShowDialog(this);
        this.Close();
    }

    private void nudOrderNumber_ValueChanged(object sender, EventArgs e)
    {
        _orderNumber = (int)nudOrderNumber.Value;
    }
}
