using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Category;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using Microsoft.Extensions.DependencyInjection;
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

public partial class ProductAndCategoryManagementForm : Form
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IServiceProvider _serviceProvider;
    private int _selectedCategoryId;
    private int _deletedCategoryId;
    private int _selectedProductId;
    public ProductAndCategoryManagementForm(IProductService productService, ICategoryService categoryService, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _productService = productService;
        _categoryService = categoryService;
        _serviceProvider = serviceProvider;
        ConfigureGrid();
    }

    private async void ProductAndCategoryManagementForm_Load(object sender, EventArgs e)
    {
        cmbSorting.Items.Add("Kategoriye göre");
        cmbSorting.Items.Add("İsme göre");
        cmbSorting.Items.Add("Fiyata göre");
        //cmbSorting.SelectedIndex = 0;
        await GetCategoryData();
        await GetProductData();
    }

    private async Task ConfigureGrid()
    {
        dgvCategories.AutoGenerateColumns = false;
        dgvCategories.AllowUserToAddRows = false;
        dgvCategories.AllowUserToDeleteRows = false;
        dgvCategories.ReadOnly = true;
        dgvCategories.MultiSelect = false;
        dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCategories.RowHeadersVisible = false;

        dgvCategories.Columns.Clear();

        // Masa
        dgvCategories.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Id",
            DataPropertyName = "Id",
            Visible = false,
        });

        // Müşteri
        dgvCategories.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Kategori Adı",
            Name = "Name",
            DataPropertyName = "Name",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgvProducts.AutoGenerateColumns = false;
        dgvProducts.AllowUserToAddRows = false;
        dgvProducts.AllowUserToDeleteRows = false;
        dgvProducts.ReadOnly = true;
        dgvProducts.MultiSelect = false;
        dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvProducts.RowHeadersVisible = false;

        dgvProducts.Columns.Clear();

        // ID
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Id",
            DataPropertyName = "Id",
            Visible = false,
        });

        // Ürün Adı
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Ürün Adı",
            Name = "Name",
            DataPropertyName = "Name",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Fiyatı",
            Name = "Price",
            DataPropertyName = "Price",
            Width = 60
        });
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Stok Adedi",
            Name = "Stock",
            DataPropertyName = "Stock",
            Width = 60
        });
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Kategori",
            Name = "CategoryName",
            DataPropertyName = "CategoryName",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
    }

    private async Task GetCategoryData()
    {
        try
        {
            var result = await _categoryService.GetAllAsync();

            if (!result.Success)
            {
                MessageBox.Show(result.Message);
                return;
            }
            dgvCategories.DataSource = result.Data;
            var list = result.Data.ToList();
            list.Insert(0, new CategoryResponseDto()
            {
                Id = 0,
                Name = "Tüm kategoriler"
            });

            cmbCategories.DataSource = list;
            cmbCategories.DisplayMember = "Name";
            cmbCategories.ValueMember = "Id";

            cmbCategory.DataSource = result.Data;
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Id";
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }
    private async Task GetProductData()
    {
        try
        {
            var result = await _productService.GetAllAsync();
            if (!result.Success)
            {
                MessageBox.Show(result.Message);
                return;
            }
            dgvProducts.DataSource = result.Data.Select(p => new ProductGridResponseDto()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.Category.Name
            }).ToList();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private async void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (cmbCategories.SelectedValue is int id)
        {
            _selectedCategoryId = (int)cmbCategories.SelectedValue;
        }

        if (_selectedCategoryId == 0)
        {
            await GetProductData();
            return;
        }

        var result = await _productService.GetProductByCategoryIdAsync(_selectedCategoryId);
        dgvProducts.DataSource = result.Data.Select(p => new ProductGridResponseDto() { Id = p.Id, Name = p.Name, Price = p.Price, Stock = p.Stock, CategoryName = p.Category.Name }).ToList();
    }

    private async void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (cmbSorting.SelectedIndex == 0)
        {
            var result = await _productService.GetProductsSortedByCategoryAsync();
            dgvProducts.DataSource = result.Data.Select(p => new ProductGridResponseDto()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.Category.Name
            }).ToList();
        }
        else if (cmbSorting.SelectedIndex == 1)
        {
            var result = await _productService.GetProductsSortedByNameAsync();
            dgvProducts.DataSource = result.Data.Select(p => new ProductGridResponseDto()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.Category.Name
            }).ToList();
        }
        else
        {
            var result = await _productService.GetProductsSortedByPriceAsync();
            dgvProducts.DataSource = result.Data.Select(p => new ProductGridResponseDto()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.Category.Name
            }).ToList();
        }
    }

    private void btnCreate_Click(object sender, EventArgs e)
    {
        using (var form = _serviceProvider.GetRequiredService<CategoryCreateForm>())
        {
            if (form.ShowDialog() == DialogResult.OK)
            {
                GetCategoryData();
            }
        }
    }

    private void dgvCategories_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        _deletedCategoryId = (int)dgvCategories.Rows[e.RowIndex].Cells["Id"].Value;
        btnDelete.Enabled = true;
    }

    private async void btnDelete_Click(object sender, EventArgs e)
    {
        var result = await _categoryService.RemoveAsync(_deletedCategoryId);
        MessageBox.Show(result.Message);
        _deletedCategoryId = 0;
        btnDelete.Enabled = false;
        GetCategoryData();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        int selectedCategoryId = (int)dgvCategories.CurrentRow.Cells["Id"].Value;
        using (var form = _serviceProvider.GetRequiredService<CategoryUpdateForm>())
        {
            form.UpdatedCategoryId = selectedCategoryId;
            if (form.ShowDialog() == DialogResult.OK)
            {
                GetCategoryData();
            }
        }
    }

    private async void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        _selectedProductId = (int)dgvProducts.Rows[e.RowIndex].Cells["Id"].Value;
        gbxUpdateAndDelete.Visible = true;
        btnProductCreate.Enabled = false;
        btnProductUpdate.Enabled = true;
        btnProductDelete.Enabled = true;
        await ProductPanelFill();
    }

    private async Task ProductPanelFill()
    {
        var data = await _productService.GetDetailByIdAsync(_selectedProductId);
        if (data.Success)
        {
            txtId.Text = _selectedProductId.ToString();
            txtProductName.Text = data.Data.Name;
            nudPrice.Value = data.Data.Price;
            nudStock.Value = data.Data.Stock;
            cmbCategory.SelectedValue = data.Data.Category.Id;
            cbxIsActive.Checked = data.Data.IsActive;
            cbxIsDeleted.Checked = data.Data.IsDeleted;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        InitForm();
    }

    private async void btnProductCreate_Click(object sender, EventArgs e)
    {
        ProductCreateRequestDto dto = new()
        {
            Name = txtProductName.Text,
            Price = nudPrice.Value,
            Stock = (int)nudStock.Value,
            CategoryId = (int)cmbCategory.SelectedValue
        };
        var result = await _productService.AddAsync(dto);
        MessageBox.Show(result.Message);
        InitForm();
    }

    private void InitForm()
    {
        this.Controls.Clear();
        InitializeComponent();
        ConfigureGrid();
        ProductAndCategoryManagementForm_Load(null, null);
    }

    private async void btnProductUpdate_Click(object sender, EventArgs e)
    {
        ProductUpdateRequestDto dto = new()
        {
            Id = _selectedProductId,
            Name = txtProductName.Text,
            Price = nudPrice.Value,
            Stock = (int)nudStock.Value,
            CategoryId = (int)cmbCategory.SelectedValue,
            IsActive = cbxIsActive.Checked,
            IsDeleted = cbxIsDeleted.Checked
        };
        var result = await _productService.UpdateAsync(dto);
        MessageBox.Show(result.Message);
        InitForm();
    }

    private async void btnProductDelete_Click(object sender, EventArgs e)
    {
        var result = await _productService.RemoveAsync(_selectedProductId);
        MessageBox.Show(result.Message);
        InitForm();
    }
}
