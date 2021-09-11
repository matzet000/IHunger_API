using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Repository;
using IHunger.Domain.Interfaces.Services;
using IHunger.Domain.Models;
using IHunger.Domain.Notifications;
using IHunger.Infra.Data;
using IHunger.Infra.Data.Context;
using IHunger.Infra.Data.Repository;
using IHunger.Service;
using IHunger.WebAPI.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace IHunger.WebAPI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<DataIdentityDbContext>();

           
            services.AddScoped<INotifier, Notifier>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
            
            services.AddScoped<RoleManager<IdentityRole<Guid>>>();
            services.AddScoped<UserManager<User>>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #region Repository
            services.AddScoped<IAddressRestaurantRepository, AddressRestaurantRepository>();
            services.AddScoped<IAddressUserRepository, AddressUserRepository>();
            services.AddScoped<ICategoryProductRepository, CategoryProductRepository>();
            services.AddScoped<ICategoryRestaurantRepository, CategoryRestaurantRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IProfileUserRepository, ProfileUserRepository>();

            #endregion

            #region Service
            services.AddScoped<IAddressRestaurantService, AddressRestaurantService>();
            services.AddScoped<IAddressUserService, AddressUserService>();
            services.AddScoped<ICategoryProductService, CategoryProductService>();
            services.AddScoped<ICategoryRestaurantService, CategoryRestaurantService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderStatusService, OrderStatusService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            #endregion

            return services;
        }
    }
}
