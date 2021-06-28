using AutoMapper;
using IHunger.Domain.Models;
using IHunger.WebAPI.ViewModels.CategoryProduct;
using IHunger.WebAPI.ViewModels.CategoryRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHunger.WebAPI.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CategoryProduct, CategoryProductViewModel>();
            CreateMap<CategoryProductCreatedViewModel, CategoryProduct>();
            CreateMap<CategoryProductViewModel, CategoryProduct>();

            CreateMap<CategoryRestaurant, CategoryRestaurantViewModel>();
            CreateMap<CategoryRestaurantCreatedViewModel, CategoryRestaurant>();
            CreateMap<CategoryRestaurantViewModel, CategoryRestaurant>();
        }
    }
}
