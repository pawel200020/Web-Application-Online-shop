using AppAbstract.Store;
using AppAbstract.Store.Denpendencies;
using AppCommonTools.Linq;
using AppCore.BusinessEntities;
using Data.Abstract;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Product = AppCore.BusinessEntities.Product;

namespace AppCore.Store;

public class OrdersManager : IOrdersManager
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IProductsRepository _productsRepository;
    public OrdersManager(IOrdersRepository ordersRepository, IProductsRepository productsRepository)
    {
        _ordersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
        _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
    }
    public async Task<(IOrder[] orders, int quanitity)> GetAll(PaginationModel paginationModel)
    {
        var queryable = _ordersRepository.GetOrdersAsQueryable();
        var orders = await queryable.OrderBy(x => x.Name).Paginate(paginationModel).ToArrayAsync();
        return (orders,  await queryable.CountAsync());
    }

    public async Task<IOrder> GetById(int id)
    {
        var order = await _ordersRepository.GetById(id);
        if (order == null)
            throw new InvalidOperationException("trying access order with id which not exists in database");
        return order;
    }

    private async Task RemoveOrderedProducts(int id, int quantity)
    {
        var product = await _productsRepository.GetById(id) as Product ?? throw new InvalidOperationException("product does not exists");
        product.Quantity -= quantity;

        if (product.Quantity <= 0)
            product.IsAvalible = false;

        await _productsRepository.Save(product);
    }

    private async Task VerifyEnoughResources(IEnumerable<IOrdersProducts> products)
    {
        foreach (var product in products)
        {
            var resource = await _productsRepository.GetById(product.ProductId) as Product;
            if (resource is null)
                throw new InvalidOperationException("Product with derived id does not exists in database");
            if (!resource.IsAvalible)
                throw new InvalidOperationException("Selected product is currently unavailable");
            if (resource.Quantity < product.Quantity)
                throw new InvalidOperationException($"there is not enough resource {resource.Name}");
        }
    }
    private async Task<double> CountOrderValue(IEnumerable<IOrdersProducts> products)
    {
        double price = 0;
        foreach (var product in products)
        {
            var founded = await _productsRepository.GetById(product.ProductId);
            if (founded != null)
                price += product.Quantity * founded.Price;
        }
        return price;
    }

    public async Task<int> AddOrder(Order order)
    {
        await VerifyEnoughResources(order.OrdersProducts);
        foreach (var orderedProduct in order.OrdersProducts)
            await RemoveOrderedProducts(orderedProduct.ProductId, orderedProduct.Quantity);
        
        order.Value = await CountOrderValue(order.OrdersProducts);
        await _ordersRepository.AddOrder(order);
        return order.Id;
    }

    public async Task Delete(int id)
        => await _ordersRepository.Delete(id);
}