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

public partial class CategoryCreateForm : Form
{
    private readonly ICategoryService _categoryService;
    public CategoryCreateForm(ICategoryService categoryService)
    {
        _categoryService = categoryService;
        InitializeComponent();
    }

    private void CategoryCreateForm_Load(object sender, EventArgs e)
    {

    }

    private async void btnCreate_Click(object sender, EventArgs e)
    {
        CategoryCreateRequestDto dto = new()
        {
            Name = txtCategory.Text
        };
        var result = await _categoryService.AddAsync(dto);
        MessageBox.Show(result.Message);

        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}
