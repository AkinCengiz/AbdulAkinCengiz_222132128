using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;
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

namespace AbdulAkinCengiz_222132128.WinFormUI;

public partial class PaymentForm : Form
{
    private readonly IOrderService _orderService;
    private readonly IReservationService _reservationService;
    private ReservationDetailResponseDto reservation;
    private int _reservationID;
    private List<OrderItemGridResponseDto> _gridItems = new();

    public void SetOrder(int reservationId)
    {
        _reservationID = reservationId;
    }
    public PaymentForm(IOrderService orderService, IReservationService reservationService)
    {
        InitializeComponent();
        ConfigureOrderItemsGrid();
        _orderService = orderService;
        _reservationService = reservationService;
    }

    private async void PaymentForm_Load(object sender, EventArgs e)
    {
        await LoadOrderAsync();
    }

    private async Task LoadOrderAsync()
    {
        var result = await _reservationService.GetDetailByIdAsync(_reservationID);
        if (!result.Success)
        {
            MessageBox.Show(result.Message);
            Close();
            return;
        }

        reservation = result.Data;

        if (reservation?.Order == null)
        {
            MessageBox.Show("Bu rezervasyona ait sipariş bulunamadı.");
            Close();
            return;
        }

        var order = reservation.Order;

        // Üst bilgi
        lblOrderNumber.Text = $"#{order.Id}";
        lblCustomer.Text = $"{reservation.Customer.FirstName} {reservation.Customer.LastName}";
        lblGeneralTotal.Text = order.Total.ToString("N2");
        lblTable.Text = reservation.Table.Name;
        if (reservation.Order.IsPaid)
        {
            btnPay.Enabled = false;
            btnPay.Text = "Ödeme Tamamlandı";
        }
        else
        {
            btnPay.Enabled = true;
            btnPay.Text = "Ödeme Al";
        }


        _gridItems.Clear();

        var orderItems = order.OrderItems.Select(oi => new OrderItemGridResponseDto
        {
            ProductName = oi.Product.Name,
            Quantity = oi.Quantity,
            Price = oi.UnitPrice,
            LineTotal = oi.Quantity * oi.UnitPrice
        });

        decimal total = orderItems.Sum(oi => oi.LineTotal);
        lblTotal.Text = total.ToString("N2");
        lblKdv.Text = (total * 0.08m).ToString("N2");

        _gridItems = orderItems.ToList();
        dgvOrderDetails.DataSource = _gridItems;
    }

    private void ConfigureOrderItemsGrid()
    {
        dgvOrderDetails.AutoGenerateColumns = false;
        dgvOrderDetails.Columns.Clear();

        dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Ürün",
            DataPropertyName = "ProductName",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Adet",
            DataPropertyName = "Quantity",
            Width = 70
        });

        dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Birim Fiyat",
            DataPropertyName = "Price",
            Width = 90,
            DefaultCellStyle = { Format = "N2" }
        });

        dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Toplam",
            DataPropertyName = "LineTotal",
            Width = 90,
            DefaultCellStyle = { Format = "N2" }
        });

        dgvOrderDetails.ReadOnly = true;
        dgvOrderDetails.AllowUserToAddRows = false;
        dgvOrderDetails.RowHeadersVisible = false;

        // DataSource sadece burada, bir kez bağlanır
        dgvOrderDetails.DataSource = _gridItems;
    }

    private async void btnPay_Click(object sender, EventArgs e)
    {

        btnPay.Enabled = false;

        try
        {
            if (reservation.Order.Id <= 0 || _reservationID <= 0)
            {
                MessageBox.Show("Sipariş/Rezervasyon bilgisi bulunamadı.");
                return;
            }

            var confirm = MessageBox.Show(
                "Ödeme alınacaktır. Devam edilsin mi?",
                "Ödeme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            // 1) Ödeme
            var payResult = await _orderService.PayAsync(reservation.Order.Id);
            if (!payResult.Success)
            {
                MessageBox.Show(payResult.Message);
                return;
            }

            MessageBox.Show("Ödeme tamamlandı.");

            // 2) Ödeme tamamlandıktan sonra fiş önizleme aç
            using (var previewForm = new ReceiptPreviewForm(_reservationService, _reservationID))
            {
                previewForm.ShowDialog(this);
            }

            // 3) İstersen ödeme sonrası PaymentForm kapansın
            //this.Close();
        }
        finally
        {
            btnPay.Enabled = true;
        }
    }

    private async void btnPayPrint_Click(object sender, EventArgs e)
    {
        var res = await _reservationService.GetDetailByIdAsync(_reservationID);
        if (!res.Success)
        {
            MessageBox.Show(res.Message);
            return;
        }

        var reservation = res.Data;
        if (reservation?.Order == null)
        {
            MessageBox.Show("Bu rezervasyon için sipariş bulunamadı.");
            return;
        }

        var order = reservation.Order;

        // Kalemler
        var rows = (order.OrderItems ?? new List<OrderItemResponseDto>())
            .Select(oi => new ReceiptItemRow
            {
                ProductName = oi.Product?.Name ?? "",
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                LineTotal = oi.UnitPrice * oi.Quantity
            })
            .ToList();

        // Total hesap (ister DB'deki order.Total, ister yeniden hesap)
        const decimal VatRate = 0.08m;
        var subTotal = rows.Sum(x => x.LineTotal);
        var vat = Math.Round(subTotal * VatRate, 2, MidpointRounding.AwayFromZero);
        var grand = subTotal + vat;

        using var sfd = new SaveFileDialog
        {
            Filter = "PDF (*.pdf)|*.pdf",
            FileName = $"Fis_{order.Id}_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
        };

        if (sfd.ShowDialog() != DialogResult.OK)
            return;

        ReceiptPdfService.CreateReceiptPdf(
            filePath: sfd.FileName,
            receiptNo: order.Id.ToString(),
            tableName: reservation.Table?.Name ?? "",
            customerFullName: $"{reservation.Customer?.FirstName} {reservation.Customer?.LastName}".Trim(),
            createdAt: DateTime.Now,
            subTotal: subTotal,
            vatAmount: vat,
            grandTotal: grand,
            items: rows
        );

        ReceiptPdfService.OpenPdf(sfd.FileName);
    }

    private void btnReceiptPreview_Click(object sender, EventArgs e)
    {
        // Sizde rezervasyon üzerinden ödeme akışı var:
        if (_reservationID <= 0)
        {
            MessageBox.Show("Rezervasyon bulunamadı.");
            return;
        }

        var previewForm = new ReceiptPreviewForm(_reservationService, _reservationID);
        previewForm.ShowDialog();
    }
}
