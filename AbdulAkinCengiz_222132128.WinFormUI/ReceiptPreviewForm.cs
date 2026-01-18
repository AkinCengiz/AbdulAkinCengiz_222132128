using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;
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

public partial class ReceiptPreviewForm : Form
{
    private readonly IReservationService _reservationService;
    private readonly int _reservationId;

    private string? _pdfPath;
    public ReceiptPreviewForm(IReservationService reservationService, int reservationId)
    {
        InitializeComponent();
        _reservationService = reservationService;
        _reservationId = reservationId;
        
    }

    private async void ReceiptPreviewForm_Load(object? sender, EventArgs e)
    {
        await BuildAndPreviewAsync();
    }

    private async Task BuildAndPreviewAsync()
    {
        var res = await _reservationService.GetDetailByIdAsync(_reservationId);
        if (!res.Success)
        {
            MessageBox.Show(res.Message);
            this.Close();
            return;
        }

        var reservation = res.Data;
        if (reservation?.Order == null)
        {
            MessageBox.Show("Bu rezervasyona ait sipariş bulunamadı.");
            this.Close();
            return;
        }

        var order = reservation.Order;

        // OrderItem -> fiş satırları (UnitPrice üzerinden)
        var items = (order.OrderItems ?? new List<OrderItemResponseDto>())
            .Select(oi => new ReceiptItemRow
            {
                ProductName = oi.Product?.Name ?? "",
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                LineTotal = oi.UnitPrice * oi.Quantity
            })
            .ToList();

        // Total (ister DB order.Total, ister yeniden hesap)
        const decimal VatRate = 0.08m;
        var subTotal = items.Sum(x => x.LineTotal);
        var vat = Math.Round(subTotal * VatRate, 2, MidpointRounding.AwayFromZero);
        var grand = subTotal + vat;

        // Temp pdf
        var tempDir = Path.Combine(Path.GetTempPath(), "OrderReceipts");
        Directory.CreateDirectory(tempDir);
        _pdfPath = Path.Combine(tempDir, $"Fis_{order.Id}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

        ReceiptPdfService.CreateReceiptPdf(
            filePath: _pdfPath,
            receiptNo: order.Id.ToString(),
            tableName: reservation.Table?.Name ?? "",
            customerFullName: $"{reservation.Customer?.FirstName} {reservation.Customer?.LastName}".Trim(),
            createdAt: DateTime.Now,
            subTotal: subTotal,
            vatAmount: vat,
            grandTotal: grand,
            items: items
        );

        // WebView2 preview
        await webView21.EnsureCoreWebView2Async();
        webView21.Source = new Uri(_pdfPath);
    }

    private async void btnPrint_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_pdfPath) || !File.Exists(_pdfPath))
        {
            MessageBox.Show("Önizleme PDF'i bulunamadı.");
            return;
        }

        await webView21.EnsureCoreWebView2Async();
        webView21.CoreWebView2.ShowPrintUI();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_pdfPath) || !File.Exists(_pdfPath))
        {
            MessageBox.Show("Önizleme PDF'i bulunamadı.");
            return;
        }

        using var sfd = new SaveFileDialog
        {
            Filter = "PDF (*.pdf)|*.pdf",
            FileName = Path.GetFileName(_pdfPath)
        };

        if (sfd.ShowDialog() != DialogResult.OK) return;

        File.Copy(_pdfPath, sfd.FileName, overwrite: true);
        MessageBox.Show("PDF kaydedildi.");
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}
