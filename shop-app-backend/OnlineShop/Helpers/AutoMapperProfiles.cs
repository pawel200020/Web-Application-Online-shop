﻿using AutoMapper;
using Data.Entities;
using ShopCore;
using ViewModels.Accounts;
using ViewModels.Pagination;
using ViewModels.Rating;
using ViewModels.Shop.Categories;
using ViewModels.Shop.Orders;
using ViewModels.Shop.Products;

namespace ShopPortal.Helpers
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CategoryViewModel, Category>().ReverseMap();
            CreateMap<CategoryCreationViewModel, Category>();

            CreateMap<PaginationModel, PaginationViewModel>().ReverseMap();
            CreateMap<UserCredentialsViewModel, UserCredentials>().ReverseMap();
            CreateMap<AuthenticationResponseViewModel,AuthenticationResponse>().ReverseMap();
            CreateMap<ProductsOrdersViewModel, ProductsOrders>().ReverseMap();
            CreateMap<FilterProducts,FilterProductsViewModel>()
                .ForMember(x => x.PaginationViewModel, y=> y.MapFrom(z=>z.PaginationModel));
            CreateMap<FilterProductsViewModel, FilterProducts>()
                .ForMember(x => x.PaginationModel, y => y.MapFrom(z => z.PaginationViewModel));
            CreateMap<ProductPutGet,ProductPutGetViewModel>().ReverseMap();
            CreateMap<RatingViewModel, Rating>().
                ForMember(x => x.Rate, y => y.MapFrom(z => z.Rating));

            CreateMap<OrderViewModel, Order>().ReverseMap();
            CreateMap<OrderCreationViewModel, Order>()
                .ForMember(x => x.OrdersProducts, options => options.MapFrom(MapOrderProducts));

            CreateMap<ProductViewModel, Product>().ReverseMap();
            CreateMap<ProductCreationViewModel, Product>()
                .ForMember(x => x.Picture, options => options.Ignore())
                .ForMember(x=>x.PictureFile, options=>options.MapFrom(z=>z.Picture))
                .ForMember(x => x.ProductsCategories, options => options.MapFrom(MapProductCategories));

            CreateMap<Product, ProductViewModel>()
                .ForMember(x => x.Category, options => options.MapFrom(MapProductCategories));

            CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.OrdersProducts, options => options.MapFrom(MapOrderProductsOrder));
        }

        private List<ProductsCategories> MapProductCategories(ProductCreationViewModel productCreationViewModel, Product product)
        {
            var result = new List<ProductsCategories>();
            if (productCreationViewModel.CategoriesIds == null)
            {
                return result;
            }

            foreach (var id in productCreationViewModel.CategoriesIds)
            {
                result.Add(new ProductsCategories() {CategoryId = id});
            }

            return result;
        }

        private List<OrdersProducts> MapOrderProducts(OrderCreationViewModel orderCreationViewModel, Order order)
        {
            var result = new List<OrdersProducts>();
            if (orderCreationViewModel.OrdersProducts == null)
            {
                return result;
            }

            foreach (var product in orderCreationViewModel.OrdersProducts)
            {
                result.Add(new OrdersProducts() {ProductId = product.Id, Quantity = product.Quantity});
            }

            return result;
        }

        private List<CategoryViewModel> MapProductCategories(Product product, ProductViewModel productViewModel)
        {
            var res  = new List<CategoryViewModel>();
            if (product.ProductsCategories != null)
            {
                foreach (var cat in product.ProductsCategories)
                {
                    res.Add(new CategoryViewModel(){Id=cat.CategoryId,Name = cat.Category.Name});
                }
            }

            return res;
        }

        private List<ProductsOrdersViewModel> MapOrderProductsOrder(Order order, OrderViewModel orderViewModel)
        {
            var res = new List<ProductsOrdersViewModel>();
            if (order.OrdersProducts != null)
            {
                foreach (var product in order.OrdersProducts)
                {
                    res.Add(new ProductsOrdersViewModel(){Id = product.ProductId, Name = product.Product.Name, Picture = product.Product.Picture, Quantity = product.Quantity});
                }
            }

            return res;
        }
    }
}
