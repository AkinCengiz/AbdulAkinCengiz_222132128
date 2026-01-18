using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Category;
using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using AbdulAkinCengiz_222132128.WinFormUI.Util;
using Core.Utilities.Results;
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

public partial class OrderForm : Form
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly IOrderItemService _orderItemService;
    private readonly IOrderService _orderService;
    private readonly IReservationService _reservationService;
    private readonly ITableService _tableService;
    private readonly IServiceProvider _serviceProvider;

    private int? _selectedCategoryId = null;
    private readonly BindingList<OrderItemRowDto> _orderItems = new();
    private int _currentOrderId;
    private const decimal VatRate = 0.08m; // %8 örnek, sizde kaçsa değiştirin
    private int? SelectedTableId =>
        (cmbTable.SelectedItem as ComboItem<int?>)?.Value;
    private bool _isLoadingCombos;
    private readonly SemaphoreSlim _reservationsLoadLock = new(1, 1);
    private int? _lastLoadedTableId = null;
    private int? SelectedReservationId =>
        (cmbReservation.SelectedItem as ComboItem<int?>)?.Value;
    public OrderForm(ICategoryService categoryService, IProductService productService, IOrderItemService orderItemService, IOrderService orderService, IReservationService reservationService, ITableService tableService, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _categoryService = categoryService;
        _productService = productService;
        _orderItemService = orderItemService;
        _orderService = orderService;
        _reservationService = reservationService;
        _tableService = tableService;
        _serviceProvider = serviceProvider;
        ConfigureGrid();
        ConfigureOrderItemsGrid();

        cmbTable.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbReservation.DropDownStyle = ComboBoxStyle.DropDownList;

        cmbTable.SelectedIndexChanged += cmbTable_SelectedIndexChanged;
    }

    private async void OrderForm_Load(object sender, EventArgs e)
    {
        ConfigureCategoryPanel();
        await LoadCategoriesAsync();
        await LoadProductsAsync(null);

        _isLoadingCombos = true;
        try
        {
            await LoadTablesToComboAsync();

            // 1) Eğer TableListForm’dan geldiysek ilgili masayı seç
            if (_openedFromTableList && _initialTableId.HasValue)
            {
                var ds = cmbTable.DataSource as List<ComboItem<int?>>;
                var idx = ds?.FindIndex(x => x.Value == _initialTableId.Value) ?? -1;
                cmbTable.SelectedIndex = idx >= 0 ? idx : 0;

                // rezervasyonları yükle (event'e güvenmeyelim)
                await LoadReservationsToComboAsync(_initialTableId.Value);
            }
            else
            {
                cmbTable.SelectedIndex = 0;
                await LoadReservationsToComboAsync(null);
            }
        }
        finally
        {
            _isLoadingCombos = false;
        }

        // 2) OrderId varsa order kalemlerini yükle
        if (_openedFromTableList && _initialOrderId.HasValue && _initialOrderId.Value > 0)
        {
            await LoadOrderAsync(_initialOrderId.Value);
        }
    }
    private int? _initialTableId;
    private int? _initialOrderId;
    private bool _openedFromTableList;

    public void SetContextFromTableList(int tableId, int orderId)
    {
        _openedFromTableList = true;
        _initialTableId = tableId;
        _initialOrderId = orderId;
    }
    private void ConfigureCategoryPanel()
    {
        flpCategories.Controls.Clear();
        flpCategories.AutoScroll = true;
        flpCategories.FlowDirection = FlowDirection.TopDown;
        flpCategories.WrapContents = false;
        flpCategories.Padding = new Padding(5);
    }

    private async Task LoadCategoriesAsync()
    {
        var result = await _categoryService.GetAllAsync(); // sizdeki metoda göre uyarlayın
        if (!result.Success)
        {
            MessageBox.Show(result.Message);
            return;
        }

        RenderCategoryButtons(result.Data.ToList());
    }

    private void RenderCategoryButtons(List<CategoryResponseDto> categories)
    {
        flpCategories.SuspendLayout();
        flpCategories.Controls.Clear();

        // "Hepsi" butonu
        flpCategories.Controls.Add(CreateCategoryButton(null, "Hepsi"));

        foreach (var c in categories)
            flpCategories.Controls.Add(CreateCategoryButton(c.Id, c.Name));

        flpCategories.ResumeLayout();
    }

    private Button CreateCategoryButton(int? categoryId, string text)
    {
        var btn = new Button
        {
            Height = 45,
            Width = flpCategories.ClientSize.Width - 25,  // scrollbar payı
            Text = text,
            Tag = categoryId,                             // null = hepsi
            TextAlign = ContentAlignment.MiddleLeft,
            FlatStyle = FlatStyle.Flat,

            UseVisualStyleBackColor = false               // renkler için kritik
        };

        btn.FlatAppearance.BorderSize = 0;
        btn.BackColor = Color.Gainsboro;
        btn.ForeColor = Color.Black;

        btn.Click += CategoryButton_Click;

        return btn;
    }

    private async void CategoryButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn) return;

        _selectedCategoryId = (int?)btn.Tag;

        HighlightSelectedCategory(btn);

        await LoadProductsAsync(_selectedCategoryId);
    }

    private void HighlightSelectedCategory(Button selected)
    {
        foreach (Control c in flpCategories.Controls)
        {
            if (c is Button b)
            {
                b.BackColor = Color.Gainsboro;
                b.ForeColor = Color.Black;
                b.Font = new Font(b.Font, FontStyle.Regular);
            }
        }

        selected.BackColor = Color.SteelBlue;
        selected.ForeColor = Color.White;
        selected.Font = new Font(selected.Font, FontStyle.Bold);
    }

    private async Task LoadProductsAsync(int? categoryId)
    {
        IDataResult<List<ProductResponseDto>> result;
        if (categoryId is null)
        {
            result = await _productService.GetAllForOrderAsync();
        }
        else
        {
            result = await _productService.GetProductByCategoryIdAsync(categoryId.Value);
        }

        if (!result.Success)
        {
            MessageBox.Show(result.Message);
            return;
        }

        dgvProducts.AutoGenerateColumns = false;
        dgvProducts.DataSource = result.Data.ToList();
    }

    private void flpCategories_SizeChanged(object sender, EventArgs e)
    {
        foreach (Control c in flpCategories.Controls)
        {
            if (c is Button b)
                b.Width = flpCategories.ClientSize.Width - 25;
        }
    }
    private void ConfigureGrid()
    {
        dgvProducts.AutoGenerateColumns = false;
        dgvProducts.AllowUserToAddRows = false;
        dgvProducts.AllowUserToDeleteRows = false;
        dgvProducts.ReadOnly = true;
        dgvProducts.MultiSelect = false;
        dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvProducts.RowHeadersVisible = false;

        dgvProducts.Columns.Clear();

        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Id",
            DataPropertyName = "Id",
            Visible = false
        });
        // Ürün Adı
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Ürün Adı",
            DataPropertyName = "Name",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        // Fiyat
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Fiyatı",
            DataPropertyName = "Price", // birazdan ekleyeceğiz
            Width = 90
        });

        // Stock
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Stok",
            DataPropertyName = "Stock",
            Width = 90
        });

    }
    private void ConfigureOrderItemsGrid()
    {
        dgvOrderItems.AutoGenerateColumns = false;
        dgvOrderItems.DataSource = _orderItems;

        dgvOrderItems.Columns.Clear();
        dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ürün", DataPropertyName = "ProductName", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Adet", DataPropertyName = "Quantity", Width = 70 });
        dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Birim Fiyat", DataPropertyName = "UnitPrice", Width = 90 });
        dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Toplam", DataPropertyName = "Total", Width = 90 });

        dgvOrderItems.ReadOnly = true;
        dgvOrderItems.AllowUserToAddRows = false;
        dgvOrderItems.RowHeadersVisible = false;
    }

    private async void btnAdd_Click(object sender, EventArgs e)
    {
        //if (_currentOrderId <= 0)
        //{
        //    MessageBox.Show("Önce bir masa/sipariş seçmelisiniz.");
        //    return;
        //}

        //if (dgvProducts.CurrentRow?.DataBoundItem is not ProductResponseDto product)
        //{
        //    MessageBox.Show("Lütfen bir ürün seçin.");
        //    return;
        //}

        //var qty = (byte)nudQuantity.Value;
        //if (qty <= 0)
        //{
        //    MessageBox.Show("Adet 0 olamaz.");
        //    return;
        //}

        //// 1) UI’da sepete ekle (varsa adet artır)
        //var existing = _orderItems.FirstOrDefault(x => x.ProductId == product.Id);
        //if (existing is null)
        //    _orderItems.Add(new OrderItemRowDto { ProductId = product.Id, ProductName = product.Name, Quantity = qty, UnitPrice = product.Price });
        //else
        //    existing.Quantity += qty;

        //// BindingList -> değişiklik görünmesi için
        //dgvOrderItems.Refresh();

        //// 2) DB’ye yaz (Business üzerinden)
        //// Burada kendi servisinizin adını kullanın: IOrderItemService veya IOrderService
        //var result = await _orderItemService.AddOrIncreaseAsync(_currentOrderId, product.Id, qty);

        //if (!result.Success)
        //{
        //    MessageBox.Show(result.Message);
        //    // İsterseniz başarısızsa UI değişikliğini geri alabilirsiniz.
        //    return;
        //}

        //nudQuantity.Value = 1;

        //RecalcTotals(); // ara toplam, kdv, genel toplam label’ları
        if (_currentOrderId <= 0)
        {
            MessageBox.Show("Önce bir masa/sipariş seçmelisiniz.");
            return;
        }

        if (dgvProducts.CurrentRow?.DataBoundItem is not ProductResponseDto product)
        {
            MessageBox.Show("Lütfen bir ürün seçin.");
            return;
        }

        var qty = (byte)nudQuantity.Value;
        if (qty <= 0)
        {
            MessageBox.Show("Adet 0 olamaz.");
            return;
        }

        // UI sepet: varsa artır, yoksa ekle
        var existing = _orderItems.FirstOrDefault(x => x.ProductId == product.Id);
        if (existing is null)
            _orderItems.Add(new OrderItemRowDto
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = qty,
                UnitPrice = product.Price
            });
        else
            existing.Quantity += qty;

        dgvOrderItems.Refresh();

        nudQuantity.Value = 1;
        RecalcTotals();
    }
    private void RecalcTotals()
    {
        // _orderItems: BindingList<OrderItemRowDto>
        var subTotal = _orderItems.Sum(x => x.UnitPrice * x.Quantity);
        var vat = Math.Round(subTotal * VatRate, 2, MidpointRounding.AwayFromZero);
        var grandTotal = subTotal + vat;

        lblTotal.Text = subTotal.ToString("N2") + " TL";
        lblKdv.Text = vat.ToString("N2") + " TL";
        lblGeneralTotal.Text = grandTotal.ToString("N2") + " TL";
    }
    public async Task LoadOrderAsync(int orderId)
    {
        _currentOrderId = orderId;
        await LoadOrderItemsAsync();
    }
    private async Task LoadOrderItemsAsync()
    {
        if (_currentOrderId <= 0) return;

        var result = await _orderService.GetByIdAsync(_currentOrderId);
        if (!result.Success)
        {
            MessageBox.Show(result.Message);
            return;
        }

        _orderItems.Clear();

        foreach (var item in result.Data.OrderItems)
        {
            _orderItems.Add(new OrderItemRowDto
            {
                ProductId = item.ProductId,
                ProductName = item.Product.Name,   // DTO’da olmalı
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            });
        }

        RecalcTotals();
    }

    private async Task LoadTablesToComboAsync()
    {
        var result = await _tableService.GetAllAsync();
        if (!result.Success)
        {
            MessageBox.Show(result.Message);
            return;
        }

        var items = new List<ComboItem<int?>>
        {
            new ComboItem<int?>(null, "Seçiniz")
        };

        foreach (var t in result.Data)
            items.Add(new ComboItem<int?>(t.Id, t.Name));

        cmbTable.DataSource = null;   // <-- önemli
        cmbTable.Items.Clear();       // <-- önemli
        cmbTable.DataSource = items;
        cmbTable.SelectedIndex = 0;
    }
    private async void cmbTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_isLoadingCombos) return;

        var tableId = SelectedTableId;
        if (tableId is null)
        {
            _lastLoadedTableId = null;
            await LoadReservationsToComboAsync(null);
            return;
        }

        if (_lastLoadedTableId == tableId) return;

        await _reservationsLoadLock.WaitAsync();
        try
        {
            tableId = SelectedTableId;
            if (tableId is null) return;

            if (_lastLoadedTableId == tableId) return;
            _lastLoadedTableId = tableId;

            await LoadReservationsToComboAsync(tableId);
        }
        finally
        {
            _reservationsLoadLock.Release();
        }
    }
    private async Task LoadReservationsToComboAsync(int? tableId)
    {
        var items = new List<ComboItem<int?>>
        {
            new ComboItem<int?>(null, "Seçiniz")
        };

        if (tableId is not null)
        {
            var result = await _reservationService.GetReservationsByTableIdAsync(tableId.Value);

            if (!result.Success)
            {
                MessageBox.Show(result.Message);
            }
            else
            {
                foreach (var r in result.Data)
                {
                    var text = $"{r.StartAt:HH:mm}-{r.EndAt:HH:mm} ({r.GuestCount} kişi)";
                    items.Add(new ComboItem<int?>(r.Id, text));
                }
            }
        }

        cmbReservation.DataSource = null;  // <-- önemli
        cmbReservation.Items.Clear();      // <-- önemli
        cmbReservation.DataSource = items;
        cmbReservation.SelectedIndex = 0;
    }

    private async void btnFilter_Click(object sender, EventArgs e)
    {
        if (SelectedTableId is null || SelectedReservationId is null)
        {
            MessageBox.Show("Lütfen Masa ve Rezervasyon seçiniz.");
            return;
        }

        // 1) Bu rezervasyon için aktif order var mı? Yoksa oluştur
        var orderIdResult = await _orderService.GetOrCreateActiveOrderByReservationAsync(SelectedReservationId.Value);
        if (!orderIdResult.Success)
        {
            MessageBox.Show(orderIdResult.Message);
            return;
        }

        // 2) current order id set
        _currentOrderId = orderIdResult.Data;

        // 3) sağ panel bilgilerini doldur (müşteri/masa/rezervasyon)
        var resDetail = await _reservationService.GetDetailByIdAsync(SelectedReservationId.Value);
        if (resDetail.Success)
        {
            lblTable.Text = $"Masa: {resDetail.Data.Table.Name}";
            lblReservation.Text = $"Rezervasyon No: #{resDetail.Data.Id}";
            lblCustomer.Text = $"{resDetail.Data.Customer.FirstName} {resDetail.Data.Customer.LastName}";
        }

        // 4) order items grid doldur
        await LoadOrderAsync(_currentOrderId);
    }

    private async void btnSaveOrder_Click(object sender, EventArgs e)
    {
        if (_currentOrderId <= 0)
        {
            MessageBox.Show("Önce masa/rezervasyon seçip sipariş açınız.");
            return;
        }

        if (_orderItems.Count == 0)
        {
            MessageBox.Show("Sipariş kalemi yok.");
            return;
        }

        var dtoList = _orderItems.Select(x => new OrderItemCreateRequestDto
        {
            OrderId = _currentOrderId,
            ProductId = x.ProductId,
            Quantity = x.Quantity,
            UnitPrice = x.UnitPrice
        }).ToList();

        // 🔴 BU SATIR
        var result = await _orderService.SaveItemsAsync(_currentOrderId, dtoList);

        if (!result.Success)
        {
            MessageBox.Show(result.Message);
            return;
        }

        // 🔴 TOTAL ARTIK BACKEND'DEN GELİYOR
        lblTotal.Text = $"{result.Data:N2} ₺";

        MessageBox.Show("Sipariş kaydedildi.");
        this.Close();   // kapatmak istemiyorsanız kaldırabilirsiniz
    }
    public async Task LoadOrderByIdAndBindUIAsync(int orderId)
    {
        // 1) Order context getir
        var ctxRes = await _orderService.GetContextAsync(orderId);
        if (!ctxRes.Success)
        {
            MessageBox.Show(ctxRes.Message);
            return;
        }

        var ctx = ctxRes.Data;
        if (ctx.TableId is null || ctx.ReservationId is null)
        {
            MessageBox.Show("Sipariş rezervasyona bağlı değil.");
            return;
        }
        _isLoadingCombos = true;
        try
        {
            // 2) Masa listesini yükle
            await LoadTablesToComboAsync();

            // 3) Masayı otomatik seç
            var tables = cmbTable.DataSource as List<ComboItem<int?>>;
            var tableIndex = tables?.FindIndex(x => x.Value == ctx.TableId) ?? -1;
            cmbTable.SelectedIndex = tableIndex >= 0 ? tableIndex : 0;

            // 4) Seçilen masanın rezervasyonlarını yükle (event’e güvenmeyelim)
            await LoadReservationsToComboAsync(ctx.TableId);

            // 5) Rezervasyonu otomatik seç
            var reservations = cmbReservation.DataSource as List<ComboItem<int?>>;
            var resIndex = reservations?.FindIndex(x => x.Value == ctx.ReservationId) ?? -1;
            cmbReservation.SelectedIndex = resIndex >= 0 ? resIndex : 0;

            // 6) Sağ panel info
            lblTable.Text = $"Masa: {ctx.TableName}";
            lblReservation.Text = $"Rezervasyon: {ctx.StartAt:HH:mm}-{ctx.EndAt:HH:mm}  (# {ctx.ReservationId})";
            lblCustomer.Text = ctx.CustomerFullName;

            // 7) OrderId set + kalemleri yükle
            _currentOrderId = ctx.OrderId;
            await LoadOrderItemsAsync();
        }
        finally
        {
            _isLoadingCombos = false;
        }
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
        if (_currentOrderId <= 0)
        {
            MessageBox.Show("Önce bir masa/sipariş seçmelisiniz.");
            return;
        }

        if (dgvOrderItems.CurrentRow?.DataBoundItem is not OrderItemRowDto row)
        {
            MessageBox.Show("Lütfen sepetten bir ürün seçin.");
            return;
        }

        // Kaç adet düşülecek? (aynı nudQuantity’yi kullanmak istiyorsanız)
        var dec = (byte)nudQuantity.Value;
        if (dec <= 0)
        {
            MessageBox.Show("Adet 0 olamaz.");
            return;
        }

        // Sepetteki item'ı bul (DataBoundItem zaten referans olabilir ama garantiye alalım)
        var existing = _orderItems.FirstOrDefault(x => x.ProductId == row.ProductId);
        if (existing is null)
        {
            MessageBox.Show("Seçilen ürün sepette bulunamadı.");
            return;
        }

        // Adet düş
        if (dec >= existing.Quantity)
        {
            // 0 veya altına düşüyorsa tamamen kaldır
            _orderItems.Remove(existing);
        }
        else
        {
            existing.Quantity -= dec;
        }

        dgvOrderItems.Refresh();
        nudQuantity.Value = 1;
        RecalcTotals();
    }

    private void btnGoPay_Click(object sender, EventArgs e)
    {
        if (SelectedReservationId <= 0 || SelectedReservationId == null)
        {
            MessageBox.Show("Önce bir rezervasyon seçmelisiniz.");
            var orderPaymentForm = _serviceProvider.GetRequiredService<OrderPaymentForm>();
            orderPaymentForm.ShowDialog();
        }
        else
        {
            var paymentForm = _serviceProvider.GetRequiredService<PaymentForm>();
            paymentForm.SetOrder(SelectedReservationId.Value);
            paymentForm.ShowDialog();
        }

            
    }
}
