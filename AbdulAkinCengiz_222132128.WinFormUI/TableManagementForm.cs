using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;

namespace AbdulAkinCengiz_222132128.WinFormUI;

public partial class TableManagementForm : Form
{
    private readonly ITableService _tableService;
    private int _tableId = 0;
    public TableManagementForm(ITableService tableService)
    {
        _tableService = tableService;
        InitializeComponent();
        ConfigureGrid();
    }

    private void TableManagementForm_Load(object sender, EventArgs e)
    {
        GetTables();
    }

    public async Task GetTables()
    {
        lstTableList.Items.Clear();
        var tables = await _tableService.GetAllAsync();
        foreach (var table in tables.Data)
        {
            lstTableList.Items.Add(table.Name);
        }

        dgvTableInfos.DataSource = tables.Data;


    }

    private async Task ConfigureGrid()
    {
        dgvTableInfos.AutoGenerateColumns = false;
        dgvTableInfos.AllowUserToAddRows = false;
        dgvTableInfos.AllowUserToDeleteRows = false;
        dgvTableInfos.ReadOnly = true;
        dgvTableInfos.MultiSelect = false;
        dgvTableInfos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvTableInfos.RowHeadersVisible = false;

        dgvTableInfos.Columns.Clear();

        // ID
        dgvTableInfos.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Id",
            HeaderText = "ID",
            DataPropertyName = "Id",
            Width = 100
        });

        // Müşteri
        dgvTableInfos.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Masa Adı",
            DataPropertyName = "Name", 
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        // Kişi sayısı
        dgvTableInfos.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Kişi Sayısı",
            DataPropertyName = "Seats",
            Width = 100
        });

    }

    private async void btnAdd_Click(object sender, EventArgs e)
    {
        TableCreateRequestDto dto = new TableCreateRequestDto()
        {
            Name = txtAddName.Text,
            Seats = (byte)nudAddGuestCount.Value
        };
        var result = await _tableService.AddAsync(dto);
        txtAddName.Clear();
        nudAddGuestCount.Value = 2;
        MessageBox.Show(result.Message);
        GetTables();
    }


    private async void dgvTableInfos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
                _tableId = (int)dgvTableInfos.Rows[e.RowIndex].Cells["Id"].Value;
        var entity = await _tableService.GetDetailByIdAsync(_tableId);
        txtId.Text = _tableId.ToString();
        txtUpdateName.Text = entity.Data.Name;
        nudUpdateGuestCount.Value = entity.Data.Seats;
        cbxIsActive.Checked = entity.Data.IsActive;
        cbxIsDeleted.Checked = entity.Data.IsDeleted;
    }

    private async void btnDelete_Click(object sender, EventArgs e)
    {
        var result = await _tableService.DeleteAsync(_tableId);
        MessageBox.Show(result.Message);
        ClearControl();
        GetTables();
    }

    private async void btnUpdate_Click(object sender, EventArgs e)
    {
        var updateTable = new TableUpdateRequestDto()
        {
            Id = _tableId,
            Name = txtUpdateName.Text,
            Seats = (byte)nudUpdateGuestCount.Value,
            IsActive = cbxIsActive.Checked,
            IsDeleted = cbxIsDeleted.Checked
        };
        var result = await _tableService.UpdateAsync(updateTable);
        MessageBox.Show(result.Message);
        ClearControl();
        GetTables();
    }

    private void ClearControl()
    {
        txtUpdateName.Clear();
        nudAddGuestCount.Value = 2;
        cbxIsActive.Checked = false;
        cbxIsDeleted.Checked = false;
        nudUpdateGuestCount.Value = 2;
        txtId.Clear();
        txtAddName.Clear();
    }
}
