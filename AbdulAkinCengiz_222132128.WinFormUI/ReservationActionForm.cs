using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;
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

public partial class ReservationActionForm : Form
{
    private readonly IReservationService _reservationService;
    private readonly int _reservationId;
    private int _tableId;
    private List<TableResponseDto> _availableTables = new();
    private TableResponseDto? _selectedTable;

    public ReservationActionForm(
        int reservationId,
        IReservationService reservationService)
    {
        _reservationId = reservationId;
        _reservationService = reservationService;
        InitializeComponent();
    }

    private async void ReservationActionForm_Load(object sender, EventArgs e)
    {
        await LoadReservation();
        cmbTables.Enabled = false;
    }
    private async Task LoadReservation()
    {
        var result = await _reservationService.GetDetailByIdAsync(_reservationId);

        if (!result.Success)
        {
            MessageBox.Show(result.Message);
            Close();
            return;
        }

        var r = result.Data;

        _tableId = r.Table.Id;
        cmbTables.DataSource = new List<TableResponseDto> { r.Table };
        cmbTables.DisplayMember = "Name";
        cmbTables.ValueMember = "Id";
        //cmbTables.SelectedValue = _tableId;

        // ---- FORM ALANLARINA DOLDUR ----
        txtFirstName.Text = r.Customer.FirstName;
        txtLastName.Text = r.Customer.LastName;
        txtPhone.Text = r.Customer.Phone;
        txtEmail.Text = r.Customer.Email;


        dtpStartDate.Value = r.StartAt;
        dtpEndDate.Value = r.EndAt;
        nudGuestCount.Value = r.GuestCount;
        cmbTables.SelectedValue = _tableId;
    }

    private async void btnConfirm_Click(object sender, EventArgs e)
    {
        var reservation = await _reservationService.GetByIdAsync(_reservationId);
        reservation.Data.IsConfirm = true;
        ReservationUpdateRequestDto dto = new()
        {
            Id = reservation.Data.Id,
            CustomerId = reservation.Data.Customer.Id,
            TableId = _tableId,
            StartAt = reservation.Data.StartAt,
            EndAt = reservation.Data.EndAt,
            GuestCount = reservation.Data.GuestCount,
            IsConfirm = true
        };
        await _reservationService.UpdateAsync(dto);
    }

    private async void btnCheckIn_Click(object sender, EventArgs e)
    {
        try
        {
            if (_tableId <= 0)
            {
                MessageBox.Show("Masa bilgisi bulunamadı. Formu kapatıp tekrar açın.");
                return;
            }

            var result = await _reservationService.CheckInByTableAsync(_tableId);

            if (!result.Success)
            {
                MessageBox.Show(result.Message);
                return;
            }

            MessageBox.Show(result.Message);

            // Güncel bilgileri tekrar yükle
            await LoadReservation();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async void btnDelete_Click(object sender, EventArgs e)
    {
        var result = await _reservationService.RemoveAsync(_reservationId);
        MessageBox.Show(result.Message);
    }

    private async void btnUpdate_Click(object sender, EventArgs e)
    {
        var reservation = await _reservationService.GetByIdAsync(_reservationId);
        
        ReservationUpdateRequestDto dto = new()
        {
            Id = _reservationId,
            CustomerId = reservation.Data.Customer.Id,
            StartAt = dtpStartDate.Value,
            EndAt = dtpEndDate.Value,
            GuestCount = (byte)nudGuestCount.Value,
            TableId = (int)cmbTables.SelectedValue,
            IsConfirm = false
        };
        await _reservationService.UnCheckInByTableAsync(_tableId);
        var result = await _reservationService.UpdateAsync(dto);
        MessageBox.Show(result.Message);
    }

    private async Task SearchAvailableTables()
    {
        try
        {
            var startAt = dtpStartDate.Value;
            var endAt = dtpEndDate.Value;
            var guestCount = (byte)nudGuestCount.Value;

            if (endAt <= startAt)
            {
                MessageBox.Show("Bitiş saati, başlangıç saatinden büyük olmalıdır.");
                return;
            }


            var searchDto = new ReservationSearchTableDto
            {
                StartAt = startAt,
                EndAt = endAt,
                GuestCount = guestCount
            };

            var result = await _reservationService.GetAvailableTablesAsync(startAt, endAt, guestCount);

            if (!result.Success)
            {
                MessageBox.Show(result.Message);
                return;
            }

            _availableTables = result.Data?.ToList() ?? new List<TableResponseDto>();

            cmbTables.DataSource = _availableTables;
            cmbTables.DisplayMember = "Name";
            cmbTables.ValueMember = "Id";

            _selectedTable = null;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void btnGetTables_Click(object sender, EventArgs e)
    {
        cmbTables.Enabled = true;
        SearchAvailableTables();
    }
}
