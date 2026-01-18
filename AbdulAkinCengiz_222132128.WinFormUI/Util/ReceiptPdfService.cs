using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.WinFormUI.Util;

public static class ReceiptPdfService
{
    public static void CreateReceiptPdf(
        string filePath,
        string receiptNo,
        string tableName,
        string customerFullName,
        DateTime createdAt,
        decimal subTotal,
        decimal vatAmount,
        decimal grandTotal,
        IEnumerable<ReceiptItemRow> items)
    {
        var doc = new PdfDocument();
        doc.Info.Title = "Fis";

        var page = doc.AddPage();
        page.Width = XUnit.FromMillimeter(80);     // POS fiş genişliği (80mm)
        page.Height = XUnit.FromMillimeter(250);   // gerekirse uzatılır

        var gfx = XGraphics.FromPdfPage(page);

        var fontTitle = new XFont("Arial", 12, XFontStyle.Bold);
        var fontBold = new XFont("Arial", 9, XFontStyle.Bold);
        var font = new XFont("Arial", 9, XFontStyle.Regular);

        double y = 12;
        double left = 8;
        double right = page.Width - 8;

        // Başlık
        gfx.DrawString("RESTAURANT FİŞİ", fontTitle, XBrushes.Black, new XRect(0, y, page.Width, 20), XStringFormats.TopCenter);
        y += 18;

        gfx.DrawLine(XPens.Black, left, y, right, y);
        y += 6;

        // Üst bilgiler
        gfx.DrawString($"Fiş No: {receiptNo}", font, XBrushes.Black, left, y); y += 12;
        gfx.DrawString($"Tarih : {createdAt:dd.MM.yyyy HH:mm}", font, XBrushes.Black, left, y); y += 12;
        gfx.DrawString($"Masa  : {tableName}", font, XBrushes.Black, left, y); y += 12;
        gfx.DrawString($"Müşteri: {customerFullName}", font, XBrushes.Black, left, y); y += 14;

        gfx.DrawLine(XPens.Black, left, y, right, y);
        y += 6;

        // Tablo başlığı
        gfx.DrawString("Ürün", fontBold, XBrushes.Black, left, y);
        gfx.DrawString("Adet", fontBold, XBrushes.Black, right - 110, y);
        gfx.DrawString("Tutar", fontBold, XBrushes.Black, right - 40, y, XStringFormats.TopRight);
        y += 12;

        gfx.DrawLine(XPens.Black, left, y, right, y);
        y += 6;

        // Kalemler
        foreach (var it in items)
        {
            // Ürün adı (uzunsa kırp)
            var name = it.ProductName ?? "";
            if (name.Length > 22) name = name.Substring(0, 22) + "...";

            gfx.DrawString(name, font, XBrushes.Black, left, y);
            gfx.DrawString(it.Quantity.ToString(), font, XBrushes.Black, right - 110, y);
            gfx.DrawString(it.LineTotal.ToString("N2"), font, XBrushes.Black, right - 8, y, XStringFormats.TopRight);
            y += 12;
        }

        y += 4;
        gfx.DrawLine(XPens.Black, left, y, right, y);
        y += 8;

        // Totaller
        gfx.DrawString("Ara Toplam:", fontBold, XBrushes.Black, left, y);
        gfx.DrawString(subTotal.ToString("N2"), fontBold, XBrushes.Black, right - 8, y, XStringFormats.TopRight);
        y += 12;

        gfx.DrawString("KDV:", fontBold, XBrushes.Black, left, y);
        gfx.DrawString(vatAmount.ToString("N2"), fontBold, XBrushes.Black, right - 8, y, XStringFormats.TopRight);
        y += 12;

        gfx.DrawLine(XPens.Black, left, y, right, y);
        y += 8;

        gfx.DrawString("GENEL TOPLAM:", fontTitle, XBrushes.Black, left, y);
        gfx.DrawString(grandTotal.ToString("N2"), fontTitle, XBrushes.Black, right - 8, y, XStringFormats.TopRight);
        y += 18;

        gfx.DrawLine(XPens.Black, left, y, right, y);
        y += 12;

        gfx.DrawString("Teşekkür ederiz.", font, XBrushes.Black, new XRect(0, y, page.Width, 20), XStringFormats.TopCenter);

        doc.Save(filePath);
        doc.Close();
    }

    public static void OpenPdf(string filePath)
    {
        // Varsayılan PDF görüntüleyicide aç (kullanıcı yazdırır)
        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
    }
}
