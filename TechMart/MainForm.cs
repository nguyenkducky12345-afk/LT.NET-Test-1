using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace TechMart;

public partial class MainForm : Form
{
    private readonly ProductManager productManager = new();

    private TableLayoutPanel mainLayout = null!;
    private Panel inputPanel = null!;
    private Panel dataPanel = null!;
    private DataGridView dgvProducts = null!;

    private TextBox txtProductId = null!;
    private TextBox txtProductName = null!;
    private TextBox txtUnitPrice = null!;
    private TextBox txtQuantity = null!;
    private ComboBox cboCategory = null!;

    private PictureBox picAvatar = null!;
    private Button btnChooseImage = null!;

    private Button btnAdd = null!;
    private Button btnUpdate = null!;
    private Button btnDelete = null!;

    private TextBox txtSearch = null!;

    private ErrorProvider errorProvider = null!;

    private ToolStripStatusLabel lblTotalProducts = null!;

    public MainForm()
    {
        InitializeComponent();

        SetupForm();
        CreateMainLayout();

        productManager.Add(new Product
{
    ProductId = "SP001",
    ProductName = "Laptop Dell",
    Category = "Laptop",
    UnitPrice = 25000000,
    Quantity = 5
});

productManager.Add(new Product
{
    ProductId = "SP002",
    ProductName = "iPhone",
    Category = "Điện thoại",
    UnitPrice = 20000000,
    Quantity = 10
});

UpdateProductCount();

    }

    private void SetupForm()
    {
        Text = "TechMart Product Manager";

        StartPosition = FormStartPosition.CenterScreen;

        MinimumSize = new Size(900, 600);

        Size = new Size(1200, 700);
    }

    private void CreateMainLayout()
    {

        mainLayout = new TableLayoutPanel();

        MenuStrip menuStrip = new MenuStrip();

ToolStripMenuItem fileMenu =
    new ToolStripMenuItem("File");

ToolStripMenuItem exportMenu =
    new ToolStripMenuItem("Export CSV");

exportMenu.ShortcutKeys =
    Keys.Control | Keys.E;

exportMenu.Click += ExportMenu_Click;

ToolStripMenuItem exitMenu =
    new ToolStripMenuItem("Exit");

exitMenu.ShortcutKeys =
    Keys.Control | Keys.X;

exitMenu.Click += ExitMenu_Click;

fileMenu.DropDownItems.Add(exportMenu);
fileMenu.DropDownItems.Add(exitMenu);

menuStrip.Items.Add(fileMenu);

MainMenuStrip = menuStrip;

Controls.Add(menuStrip);

        mainLayout.Dock = DockStyle.Fill;

        mainLayout.ColumnCount = 2;
        mainLayout.RowCount = 1;

        mainLayout.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 35F));

        mainLayout.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 65F));

        Controls.Add(mainLayout);

        StatusStrip statusStrip = new StatusStrip();

lblTotalProducts = new ToolStripStatusLabel();

statusStrip.Items.Add(lblTotalProducts);

Controls.Add(statusStrip);

UpdateProductCount();

        CreateInputPanel();
        CreateDataPanel();

        mainLayout.Controls.Add(inputPanel, 0, 0);
        mainLayout.Controls.Add(dataPanel, 1, 0);
    }

    private void ExitMenu_Click(
    object? sender,
    EventArgs e)
{
    Close();
}

private void UpdateProductCount()
{
    lblTotalProducts.Text =
        $"Tổng số sản phẩm: {productManager.Products.Count}";
}
private void ExportMenu_Click(
    object? sender,
    EventArgs e)
{
    using SaveFileDialog dialog = new SaveFileDialog();

    dialog.Title = "Xuất danh sách sản phẩm";
    dialog.Filter = "CSV Files|*.csv";
    dialog.FileName = "products.csv";

    if (dialog.ShowDialog() != DialogResult.OK)
    {
        return;
    }

    using StreamWriter writer =
        new StreamWriter(
            dialog.FileName,
            false,
            System.Text.Encoding.UTF8);

    writer.WriteLine(
        "Mã SP,Tên sản phẩm,Danh mục,Đơn giá,Số lượng");

    foreach (Product product in productManager.Products)
    {
        writer.WriteLine(
            $"\"{product.ProductId}\"," +
            $"\"{product.ProductName}\"," +
            $"\"{product.Category}\"," +
            $"{product.UnitPrice}," +
            $"{product.Quantity}");
    }

    MessageBox.Show(
        "Xuất CSV thành công!",
        "Thông báo",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information);
}

    private void CreateInputPanel()
    {
        inputPanel = new Panel();

        inputPanel.Dock = DockStyle.Fill;
        inputPanel.Padding = new Padding(15);

        Label title = new Label();

        title.Text = "THÔNG TIN SẢN PHẨM";
        title.Dock = DockStyle.Top;
        title.Height = 40;
        title.Font = new Font(
            "Segoe UI",
            14,
            FontStyle.Bold
        );

        inputPanel.Controls.Add(title);

        // Panel chứa các ô nhập
        TableLayoutPanel formLayout = new TableLayoutPanel();

        formLayout.Dock = DockStyle.Fill;

        formLayout.ColumnCount = 2;

        formLayout.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 35F));

        formLayout.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 65F));

        formLayout.RowCount = 9;

        for (int i = 0; i < 9; i++)
        {
            formLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 40F));
        }

        // Mã sản phẩm
        AddLabel(formLayout, "Mã SP:", 0);

        txtProductId = new TextBox();
        txtProductId.Dock = DockStyle.Fill;

        formLayout.Controls.Add(txtProductId, 1, 0);

        // Tên sản phẩm
        AddLabel(formLayout, "Tên SP:", 1);

        txtProductName = new TextBox();
        txtProductName.Dock = DockStyle.Fill;

        formLayout.Controls.Add(txtProductName, 1, 1);

        // Đơn giá
        AddLabel(formLayout, "Đơn giá:", 2);

        txtUnitPrice = new TextBox();
        txtUnitPrice.Dock = DockStyle.Fill;

        formLayout.Controls.Add(txtUnitPrice, 1, 2);

        // Số lượng
        AddLabel(formLayout, "Số lượng:", 3);

        txtQuantity = new TextBox();
        txtQuantity.Dock = DockStyle.Fill;

        formLayout.Controls.Add(txtQuantity, 1, 3);

        // Danh mục
        AddLabel(formLayout, "Danh mục:", 4);

        cboCategory = new ComboBox();

        cboCategory.Dock = DockStyle.Fill;

        cboCategory.DropDownStyle =
            ComboBoxStyle.DropDownList;

        cboCategory.Items.AddRange(
            new object[]
            {
                "Điện thoại",
                "Laptop",
                "Phụ kiện"
            });

        if (cboCategory.Items.Count > 0)
            cboCategory.SelectedIndex = 0;

        formLayout.Controls.Add(cboCategory, 1, 4);

        // Ảnh
        AddLabel(formLayout, "Ảnh:", 5);

        picAvatar = new PictureBox();

        picAvatar.Size = new Size(120, 120);

        picAvatar.SizeMode = PictureBoxSizeMode.Zoom;

        picAvatar.BorderStyle =
            BorderStyle.FixedSingle;

        formLayout.Controls.Add(picAvatar, 1, 5);

        // Nút chọn ảnh
        btnChooseImage = new Button();

        btnChooseImage.Text = "Chọn ảnh";

        btnChooseImage.AutoSize = true;

        btnChooseImage.Click += BtnChooseImage_Click;

        formLayout.Controls.Add(btnChooseImage, 1, 6);

        // Các nút chức năng
        FlowLayoutPanel buttonPanel =
            new FlowLayoutPanel();

        buttonPanel.Dock = DockStyle.Fill;

        btnAdd = new Button();
        btnAdd.Text = "Thêm";
        btnAdd.AutoSize = true;

        btnAdd.Click += BtnAdd_Click;

        btnUpdate = new Button();
        btnUpdate.Text = "Cập nhật";
        btnUpdate.AutoSize = true;

        btnUpdate.Click += BtnUpdate_Click;

        btnDelete = new Button();
        btnDelete.Text = "Xóa";
        btnDelete.AutoSize = true;

        btnDelete.Click += BtnDelete_Click;

        buttonPanel.Controls.Add(btnAdd);
        buttonPanel.Controls.Add(btnUpdate);
        buttonPanel.Controls.Add(btnDelete);

        formLayout.Controls.Add(
            buttonPanel,
            1,
            7);

        // Thêm formLayout vào panel
        inputPanel.Controls.Add(formLayout);

        // Đưa title lên trên form
        title.BringToFront();

        errorProvider = new ErrorProvider();
    }

    private void BtnDelete_Click(
    object? sender,
    EventArgs e)
{
    if (dgvProducts.CurrentRow == null)
    {
        MessageBox.Show(
            "Vui lòng chọn sản phẩm cần xóa.",
            "Thông báo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
    }

    Product? product =
        dgvProducts.CurrentRow.DataBoundItem as Product;

    if (product == null)
    {
        return;
    }

    DialogResult result = MessageBox.Show(
        $"Bạn có chắc muốn xóa sản phẩm \"{product.ProductName}\"?",
        "Xác nhận xóa",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

    if (result == DialogResult.Yes)
    {
        productManager.Remove(product);

        UpdateProductCount();

        MessageBox.Show(
            "Xóa sản phẩm thành công!",
            "Thông báo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        ClearProductInput();
    }
}

private void BtnUpdate_Click(
    object? sender,
    EventArgs e)
{
    if (!ValidateProductInput())
    {
        return;
    }

    if (dgvProducts.CurrentRow == null)
    {
        MessageBox.Show(
            "Vui lòng chọn sản phẩm cần cập nhật.",
            "Thông báo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
    }

    Product? product =
        dgvProducts.CurrentRow.DataBoundItem as Product;

    if (product == null)
    {
        return;
    }

    product.ProductId = txtProductId.Text.Trim();
    product.ProductName = txtProductName.Text.Trim();
    product.Category = cboCategory.Text;
    product.UnitPrice = decimal.Parse(txtUnitPrice.Text);
    product.Quantity = int.Parse(txtQuantity.Text);
    product.AvatarPath =
        picAvatar.Tag?.ToString() ?? string.Empty;

    productManager.BindingSource.ResetBindings(false);

    MessageBox.Show(
        "Cập nhật sản phẩm thành công!",
        "Thông báo",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information);
}

private bool ValidateProductInput()
{
    errorProvider.Clear();

    bool isValid = true;

    if (string.IsNullOrWhiteSpace(txtProductId.Text))
    {
        errorProvider.SetError(
            txtProductId,
            "Vui lòng nhập mã sản phẩm.");
        isValid = false;
    }

    if (string.IsNullOrWhiteSpace(txtProductName.Text))
    {
        errorProvider.SetError(
            txtProductName,
            "Vui lòng nhập tên sản phẩm.");
        isValid = false;
    }

    if (!decimal.TryParse(txtUnitPrice.Text, out decimal price) ||
        price <= 0)
    {
        errorProvider.SetError(
            txtUnitPrice,
            "Đơn giá phải là số lớn hơn 0.");
        isValid = false;
    }

    if (!int.TryParse(txtQuantity.Text, out int quantity) ||
        quantity < 0)
    {
        errorProvider.SetError(
            txtQuantity,
            "Số lượng phải là số nguyên từ 0 trở lên.");
        isValid = false;
    }

    if (string.IsNullOrWhiteSpace(cboCategory.Text))
    {
        errorProvider.SetError(
            cboCategory,
            "Vui lòng chọn danh mục.");
        isValid = false;
    }

    return isValid;
}

private void ClearProductInput()
{
    txtProductId.Clear();
    txtProductName.Clear();
    txtUnitPrice.Clear();
    txtQuantity.Clear();

    cboCategory.SelectedIndex = 0;

    picAvatar.Image = null;
    picAvatar.Tag = null;

    errorProvider.Clear();
}
    private void AddLabel(
        TableLayoutPanel layout,
        string text,
        int row)
    {
        Label label = new Label();

        label.Text = text;

        label.Dock = DockStyle.Fill;

        label.TextAlign =
            ContentAlignment.MiddleLeft;

        layout.Controls.Add(label, 0, row);
    }

    private void BtnChooseImage_Click(
    object? sender,
    EventArgs e)
{
    using OpenFileDialog dialog = new OpenFileDialog();

    dialog.Title = "Chọn ảnh sản phẩm";
    dialog.Filter =
        "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

    if (dialog.ShowDialog() == DialogResult.OK)
    {
        picAvatar.Image = Image.FromFile(dialog.FileName);
        picAvatar.Tag = dialog.FileName;
    }
}
    private void CreateDataPanel()
    {
        dataPanel = new Panel();

        dataPanel.Dock = DockStyle.Fill;

        dataPanel.Padding = new Padding(15);

        // Thanh tìm kiếm
        txtSearch = new TextBox();

        txtSearch.Dock = DockStyle.Top;

        txtSearch.Height = 35;

        txtSearch.PlaceholderText =
            "Tìm kiếm theo tên sản phẩm...";

            txtSearch.TextChanged += TxtSearch_TextChanged;

        dataPanel.Controls.Add(txtSearch);

        // DataGridView
        dgvProducts = new DataGridView();

        dgvProducts.Dock = DockStyle.Fill;

        dgvProducts.AutoGenerateColumns = false;

        dgvProducts.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;

        dgvProducts.MultiSelect = false;

        dgvProducts.AllowUserToAddRows = false;

        dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;

        dgvProducts.Columns.Add(
    CreateTextColumn(
        "Mã SP",
        "ProductId"));

dgvProducts.Columns.Add(
    CreateTextColumn(
        "Tên sản phẩm",
        "ProductName"));

dgvProducts.Columns.Add(
    CreateTextColumn(
        "Danh mục",
        "Category"));

dgvProducts.Columns.Add(
    CreatePriceColumn());

dgvProducts.Columns.Add(
    CreateTextColumn(
        "Số lượng",
        "Quantity"));
        
        dgvProducts.DataSource = productManager.BindingSource;
        
        dataPanel.Controls.Add(dgvProducts);
    }

    private void DgvProducts_SelectionChanged(
    object? sender,
    EventArgs e)
{
    if (dgvProducts.CurrentRow == null)
    {
        return;
    }

    Product? product =
        dgvProducts.CurrentRow.DataBoundItem as Product;

    if (product == null)
    {
        return;
    }

    txtProductId.Text = product.ProductId;
    txtProductName.Text = product.ProductName;
    cboCategory.Text = product.Category;
    txtUnitPrice.Text = product.UnitPrice.ToString();
    txtQuantity.Text = product.Quantity.ToString();

    if (!string.IsNullOrWhiteSpace(product.AvatarPath) &&
        File.Exists(product.AvatarPath))
    {
        picAvatar.Image = Image.FromFile(product.AvatarPath);
        picAvatar.Tag = product.AvatarPath;
    }
    else
    {
        picAvatar.Image = null;
        picAvatar.Tag = null;
    }
}

private DataGridViewTextBoxColumn CreateTextColumn(
    string header,
    string propertyName)
{
    return new DataGridViewTextBoxColumn
    {
        HeaderText = header,
        DataPropertyName = propertyName,
        AutoSizeMode =
            DataGridViewAutoSizeColumnMode.Fill
    };
}

private DataGridViewTextBoxColumn CreatePriceColumn()
{
    return new DataGridViewTextBoxColumn
    {
        HeaderText = "Đơn giá",
        DataPropertyName = "UnitPrice",
        DefaultCellStyle = new DataGridViewCellStyle
        {
            Format = "N0"
        },
        AutoSizeMode =
            DataGridViewAutoSizeColumnMode.Fill
    };
}

private void BtnAdd_Click(object? sender, EventArgs e)
{
    if (!ValidateProductInput())
    {
        return;
    }

    Product product = new Product
    {
        ProductId = txtProductId.Text.Trim(),
        ProductName = txtProductName.Text.Trim(),
        Category = cboCategory.Text,
        UnitPrice = decimal.Parse(txtUnitPrice.Text),
        Quantity = int.Parse(txtQuantity.Text),
        AvatarPath = picAvatar.Tag?.ToString() ?? string.Empty
    };

    productManager.Add(product);

    UpdateProductCount();

    MessageBox.Show(
        "Thêm sản phẩm thành công!",
        "Thông báo",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information);

    ClearProductInput();
}

private void TxtSearch_TextChanged(
    object? sender,
    EventArgs e)
{
    string keyword = txtSearch.Text.Trim();

    if (string.IsNullOrWhiteSpace(keyword))
    {
        productManager.BindingSource.DataSource =
            productManager.Products;

        return;
    }

    var filteredProducts =
        productManager.Products
            .Where(p =>
                p.ProductName.Contains(
                    keyword,
                    StringComparison.OrdinalIgnoreCase))
            .ToList();

    productManager.BindingSource.DataSource =
        filteredProducts;
}

}