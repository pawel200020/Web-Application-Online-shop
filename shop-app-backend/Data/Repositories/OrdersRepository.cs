using AppAbstract.Store;
using AppAbstract.Store.Denpendecies;
using Data.Abstract;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

internal class OrdersRepository(ApplicationDbContext context) : IOrdersRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    
    public async Task<IOrder?> GetById(int id) =>
        await _context.Orders
            .Include(x => x.OrdersProducts).ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

    public IQueryable<IOrder> GetOrdersAsQueryable()
    {
        return _context.Orders.AsQueryable();
    }

    private async Task RemoveOrderedProducts(int id, int quantity)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id)
                      ?? throw new InvalidOperationException("product does not exists");
        product.Quantity -= quantity;

        if (product.Quantity <= 0)
            product.IsAvalible = false;

        await _context.SaveChangesAsync();
    }

    private async Task VerifyEnoughResources(IEnumerable<IOrdersProducts> products)
    {
        foreach (var product in products)
        {
            var resource = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == product.ProductId);
            if (resource is null)
                throw new InvalidOperationException("Product with derived id does not exists in database");
            if (!resource.IsAvalible)
                throw new InvalidOperationException("Selected product is currently unavailable");
            if (resource.Quantity < product.Quantity)
                throw new InvalidOperationException($"there is not enough resource {resource.Name}");

            await RemoveOrderedProducts(product.ProductId, product.Quantity);
        }
    }
    private async Task<double> CountOrderValue(IEnumerable<IOrdersProducts> products)
    {
        double price = 0;
        foreach (var product in products)
        {
            var founded = await _context.Products
                .Include(x => x.ProductsCategories).ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == product.ProductId);
            if (founded != null)
            {
                price += product.Quantity * founded.Price;
            }
        }
        return price;
    }

    public async Task<int> AddOrder(IOrder order)
    {
        var dbOrder = order as Order;
        if (dbOrder is null) throw new InvalidProgramException();
        await VerifyEnoughResources(dbOrder.OrdersProducts);
        dbOrder.Value = await CountOrderValue(order.OrdersProducts);
        _context.Add(order);
        await _context.SaveChangesAsync();
        return order.Id;
    }

    public async Task Delete(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        if (order == null)
            throw new InvalidOperationException("Product with derived id does not exists in database");
        _context.Remove(order);
        await _context.SaveChangesAsync();
    }
}