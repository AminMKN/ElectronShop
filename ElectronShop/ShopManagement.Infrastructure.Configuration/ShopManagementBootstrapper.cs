using ShopManagement.Application;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.AmazingAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Domain.ProductPictureAgg;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductSubCategoryAgg;
using ShopManagement.Application.Contracts.Amazing;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Infrastructure.EFCore.Repository;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductSubCategory;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Application.Contracts.ShopCart;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddSingleton<ICartService, CartService>();

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderApplication, OrderApplication>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductApplication, ProductApplication>();

            services.AddTransient<IAmazingRepository, AmazingRepository>();
            services.AddTransient<IAmazingApplication, AmazingApplication>();

            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

            services.AddTransient<IProductSubCategoryRepository, ProductSubCategoryRepository>();
            services.AddTransient<IProductSubCategoryApplication, ProductSubCategoryApplication>();

            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
