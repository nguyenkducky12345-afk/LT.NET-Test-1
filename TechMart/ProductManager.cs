using System.ComponentModel;

namespace TechMart;

public class ProductManager
{
    public BindingList<Product> Products { get; } = new();

    public BindingSource BindingSource { get; } = new();

    public ProductManager()
    {
        BindingSource.DataSource = Products;
    }

    public void Add(Product product)
    {
        Products.Add(product);
    }

    public void Remove(Product product)
    {
        Products.Remove(product);
    }
}