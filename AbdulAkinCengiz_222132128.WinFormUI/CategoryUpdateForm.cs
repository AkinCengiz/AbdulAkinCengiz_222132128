using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Dtos.Category;

namespace AbdulAkinCengiz_222132128.WinFormUI;

public partial class CategoryUpdateForm : Form
{
    private readonly ICategoryService _categoryService;
    public int UpdatedCategoryId;
    public CategoryUpdateForm(ICategoryService categoryService)
    {
        _categoryService = categoryService;
        InitializeComponent();
    }

    private async void btnUpdate_Click(object sender, EventArgs e)
    {
        CategoryUpdateRequestDto dto = new()
        {
            Id = UpdatedCategoryId,
            Name = txtName.Text,
            IsActive = cbxIsActive.Checked,
            IsDeleted = cbxDeleted.Checked
        };
        var result = await _categoryService.UpdateAsync(dto);
        MessageBox.Show(result.Message);
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private async void CategoryUpdateForm_Load(object sender, EventArgs e)
    {
        await GetCategory();
    }

    private async Task GetCategory()
    {
        var result = await _categoryService.GetDetailByIdAsync(UpdatedCategoryId);
        if (result.Success)
        {
            txtId.Text = result.Data.Id.ToString();
            txtName.Text = result.Data.Name;
            cbxIsActive.Checked = result.Data.IsActive;
            cbxDeleted.Checked = result.Data.IsDeleted;
        }

    }
}
