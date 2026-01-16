using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;
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

public partial class TableListForm : Form
{
    private readonly ITableService _tableService;
    private readonly IReservationService _reservationService;
    private readonly IServiceProvider _serviceProvider;
    public TableListForm(ITableService tableService, IReservationService reservationService, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _tableService = tableService;
        _reservationService = reservationService;
        _serviceProvider = serviceProvider;
    }

    private async void TableListForm_Load(object sender, EventArgs e)
    {
        InitFilters();
        await LoadTablesAsync();
    }
    private void InitFilters()
    {
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;

        cmbStatus.Items.Clear();
        cmbStatus.Items.Add(new ComboItem<TableStatus?>(null, "Hepsi"));
        cmbStatus.Items.Add(new ComboItem<TableStatus?>(TableStatus.Empty, "Boş"));
        cmbStatus.Items.Add(new ComboItem<TableStatus?>(TableStatus.Reserved, "Rezerve"));
        cmbStatus.Items.Add(new ComboItem<TableStatus?>(TableStatus.Full, "Dolu"));

        cmbStatus.SelectedIndex = 0;
    }
    private async Task LoadTablesAsync()
    {
        var status = (cmbStatus.SelectedItem as ComboItem<TableStatus?>)?.Value;

        var result = await _tableService.GetTableCardsAsync(statusFilter: status, reservedThresholdMinutes: 30);

        if (!result.Success)
        {
            MessageBox.Show(result.Message);
            return;
        }

        RenderTableButtons(result.Data);
    }

    private void RenderTableButtons(List<TableCardDto> tables)
    {
        flpTables.SuspendLayout();
        flpTables.Controls.Clear();

        foreach (var t in tables)
        {
            flpTables.Controls.Add(CreateTableButton(t));
        }

        flpTables.ResumeLayout();
    }

    private Button CreateTableButton(TableCardDto table)
    {
        var btn = new Button
        {
            Width = 220,
            Height = 80,
            Margin = new Padding(10),
            Text = table.Name,
            Tag = table,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            ForeColor = Color.White
        };
        btn.FlatAppearance.BorderSize = 0;

        btn.BackColor = table.Status switch
        {
            TableStatus.Empty => Color.SeaGreen,
            TableStatus.Reserved => Color.DarkOrange,
            TableStatus.Full => Color.IndianRed,
            _ => Color.Gray
        };

        btn.Click += TableButton_Click;
        return btn;
    }
    private TableCardDto? _selected;

    private void TableButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        if (btn.Tag is not TableCardDto table) return;

        _selected = table;
        HighlightSelected(btn);
        ShowDetail(table);
    }

    private void HighlightSelected(Button selected)
    {
        foreach (Control c in flpTables.Controls)
            if (c is Button b) b.FlatAppearance.BorderSize = 0;

        selected.FlatAppearance.BorderSize = 3;
        selected.FlatAppearance.BorderColor = Color.Black;
    }

    private void ShowDetail(TableCardDto t)
    {
        lblTable.Text = t.Name;
        lblGuestCount.Text = t.Seats.ToString();
        lblStatus.Text = t.Status switch
        {
            TableStatus.Empty => "Boş",
            TableStatus.Reserved => "Rezerve",
            TableStatus.Full => "Dolu",
            _ => "-"
        };

        //lblNextReservation.Text = t.NextReservationStartAt is null
        //    ? "Sıradaki Rez.: Yok"
        //    : $"Sıradaki Rez.: {t.NextReservationStartAt:HH:mm}";
    }

    private async void btnFilter_Click(object sender, EventArgs e)
    {
        await LoadTablesAsync();
    }

    private async void btnTableClose_Click(object sender, EventArgs e)
    {
        if (_selected is null)
        {
            MessageBox.Show("Lütfen bir masa seçin.");
            return;
        }

        // Sadece Rezerve (sarı) masalar check-in yapılabilir
        if (_selected.Status != TableStatus.Reserved)
        {
            MessageBox.Show("Check-in sadece rezerve masalar için yapılabilir.");
            return;
        }

        var confirm = MessageBox.Show(
            $"{_selected.Name} için müşteri geldi. Masa açılsın mı?",
            "Check-In",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
            return;

        var result = await _reservationService.CheckInByTableAsync(_selected.TableId);

        if (!result.Success)
        {
            MessageBox.Show(result.Message);
            return;
        }

        // Masaları yeniden yükle → renkler ve durumlar güncellensin
        await LoadTablesAsync();

        _selected = flpTables.Controls
            .OfType<Button>()
            .Select(b => b.Tag as TableCardDto)
            .FirstOrDefault(t => t != null && t.TableId == _selected!.TableId);

        if (_selected != null)
            ShowDetail(_selected);

        MessageBox.Show("Masa açıldı, müşteri alındı.");
    }

    private void btnTableManagement_Click(object sender, EventArgs e)
    {
        TableManagementForm form = new TableManagementForm(_tableService);
        form.ShowDialog();
    }

    //private async void btnGetOrder_Click(object sender, EventArgs e)
    //{
    //    if (_selected is null)
    //    {
    //        MessageBox.Show("Lütfen bir masa seçin.");
    //        return;
    //    }

    //    var res = await _reservationService.GetOrCreateActiveOrderIdByTableAsync(_selected.TableId);
    //    if (!res.Success)
    //    {
    //        MessageBox.Show(res.Message);
    //        return;
    //    }

    //    // Her açılışta yeni scope + yeni form instance
    //    using var scope = _serviceProvider.CreateScope();
    //    var orderForm = scope.ServiceProvider.GetRequiredService<OrderForm>();

    //    await orderForm.LoadOrderByIdAndBindUIAsync(res.Data);

    //    orderForm.ShowDialog(this);

    //}

}
