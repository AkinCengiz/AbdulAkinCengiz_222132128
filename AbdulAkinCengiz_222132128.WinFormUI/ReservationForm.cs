using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AbdulAkinCengiz_222132128.Entity.Dtos.Customer;
using AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;

namespace AbdulAkinCengiz_222132128.WinFormUI;

public partial class ReservationForm : Form
{
    private readonly IReservationService _reservationService;
    private readonly ITableService _tableService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    private List<TableResponseDto> _availableTables = new();
    private TableResponseDto? _selectedTable;

    public ReservationForm(IReservationService reservationService, ITableService tableService, ICategoryService categoryService, IMapper mapper)
    {
        _reservationService = reservationService;
        _tableService = tableService;
        _categoryService = categoryService;
        _mapper = mapper;
        InitializeComponent();
        ConfigureGrid();
    }
    private async void ReservationForm_Load(object sender, EventArgs e)
    {
        await FormLoad();
        await GetReservation();
    }
    private async Task ConfigureGrid()
    {
        dgvReservations.AutoGenerateColumns = false;
        dgvReservations.AllowUserToAddRows = false;
        dgvReservations.AllowUserToDeleteRows = false;
        dgvReservations.ReadOnly = true;
        dgvReservations.MultiSelect = false;
        dgvReservations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvReservations.RowHeadersVisible = false;

        dgvReservations.Columns.Clear();

        // Masa
        dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Masa",
            DataPropertyName = "TableName",
            Width = 60
        });

        // Müşteri
        dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Müşteri",
            DataPropertyName = "CustomerFullName", // birazdan ekleyeceğiz
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        // Saat
        dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Saat",
            DataPropertyName = "StartAt",
            Width = 90,
            DefaultCellStyle = { Format = "HH:mm" }
        });

        // Kişi sayısı
        dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Kişi",
            DataPropertyName = "GuestCount",
            Width = 60
        });

        // Durum
        dgvReservations.Columns.Add(new DataGridViewCheckBoxColumn
        {
            HeaderText = "Onay",
            DataPropertyName = "IsConfirm",
            Width = 60
        });
    }

    private async Task GetReservation()
    {
        //var data = _reservationService.GetTodayReservationAsync();
        //dgvReservations.DataSource = data.Result.Data;
        try
        {
            var result = await _reservationService.GetTodayReservationAsync();

            if (!result.Success)
            {
                MessageBox.Show(result.Message);
                return;
            }

            //dgvReservations.AutoGenerateColumns = false; // kolonları siz yönetiyorsanız
            var list = result.Data.ToList();
            dgvReservations.DataSource = list;
            //lblReservationCount.Text = list.Count.ToString();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private async Task GetByDateReservation(DateTime date)
    {
        //var data = _reservationService.GetTodayReservationAsync();
        //dgvReservations.DataSource = data.Result.Data;
        try
        {
            var result = await _reservationService.GetReservationsByDateAsync(date);

            if (!result.Success)
            {
                MessageBox.Show(result.Message);
                return;
            }

            //dgvReservations.AutoGenerateColumns = false; // kolonları siz yönetiyorsanız
            var list = result.Data.ToList();
            dgvReservations.DataSource = list;
            //lblReservationCount.Text = list.Count.ToString();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async Task<ReservationFormResponseDto> FormLoadData()
    {
        var model = new ReservationFormResponseDto()
        {
            Search = new ReservationSearchTableDto()
            {
                StartAt = DateTime.Now.AddHours(1),
                EndAt = DateTime.Now.AddHours(2),
                GuestCount = 2,
                AvailableTables = new List<TableResponseDto>()
            },
            Create = new ReservationCreateWithCustomerRequestDto()
            {
                Customer = new CustomerCreateRequestDto()
            }
        };
        return model;
    }

    private async Task FormLoad()
    {
        var model = await FormLoadData();
        dtpStartDate.Value = model.Search.StartAt;
        dtpEndDate.Value = model.Search.EndAt;
        nudGuestCount.Value = model.Search.GuestCount;
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

            // MVC'deki arama DTO'nuz
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

    private async void btnSearchTables_Click(object sender, EventArgs e)
    {
        await SearchAvailableTables();
    }



    private async Task CreateReservation()
    {
        if (_selectedTable == null)
        {
            MessageBox.Show("Lütfen uygun masalardan birini seçiniz.");
            return;
        }

        var startAt = dtpStartDate.Value;
        var endAt = dtpEndDate.Value;

        if (endAt <= startAt)
        {
            MessageBox.Show("Bitiş saati, başlangıç saatinden büyük olmalıdır.");
            return;
        }

        // Basit form doğrulama
        if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
        {
            MessageBox.Show("Müşteri ad ve soyad zorunludur.");
            return;
        }

        try
        {
            var dto = new ReservationCreateWithCustomerRequestDto()
            {
                TableId = _selectedTable.Id,
                StartAt = startAt,
                EndAt = endAt,
                GuestCount = (byte)nudGuestCount.Value,

                Customer = new CustomerCreateRequestDto
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim()
                }
            };

            var result = await _reservationService.CreateWithCustomerAsync(dto);

            if (!result.Success)
            {
                MessageBox.Show(result.Message);
                return;
            }

            MessageBox.Show("Rezervasyon oluşturuldu.");

            // Rezervasyonları yenile
            await GetReservation();
            

            // Uygun masa listesini yenile
            await SearchAvailableTables();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async void btnCreateReservation_Click(object sender, EventArgs e)
    {
        await CreateReservation();
        //this.DialogResult = DialogResult.OK; 
        //this.Close();
    }

    private async void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbTables.SelectedValue is int id)
        {
            await GetTable(id);
        }
    }

    private async Task GetTable(int id)
    {
        var table = await _tableService.GetByIdAsync(id);
        _selectedTable = new TableResponseDto()
        {
            Id = table.Data.Id,
            Name = table.Data.Name,
            Seats = table.Data.Seats
        };
    }

    private void dgvReservations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        if (dgvReservations.Rows[e.RowIndex].DataBoundItem is not ReservationResponseDto r)
            return;

        OpenReservationActionForm(r.Id);
    }

    private void OpenReservationActionForm(int reservationId)
    {
        using var form = new ReservationActionForm(reservationId, _reservationService);
        form.ShowDialog();

        _ = GetReservation();
    }

    private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
    {
        DateTime dateInfo = dateTimePicker1.Value;
        _ = GetByDateReservation(dateInfo);
    }

    private void btnGetList_Click(object sender, EventArgs e)
    {
        dateTimePicker1.Value = DateTime.Now;
    }
}
