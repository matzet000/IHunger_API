using AutoMapper;
using IHunger.Domain.Models;
using IHunger.WebAPI.ViewModels.Address;
using IHunger.WebAPI.ViewModels.CategoryProduct;
using IHunger.WebAPI.ViewModels.CategoryRestaurant;
using IHunger.WebAPI.ViewModels.Restaurant;
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

            CreateMap<AddressUser, AddressUserViewModel>();
            CreateMap<AddressUserCreatedViewModel, AddressUser>();
            CreateMap<AddressUserViewModel, AddressUser>();

            CreateMap<AddressRestaurant, AddressRestaurantViewModel>();
            CreateMap<AddressRestaurantCreatedViewModel, AddressRestaurant>();
            CreateMap<AddressRestaurantViewModel, AddressRestaurant>();

            CreateMap<Restaurant, RestaurantViewModel>();
            CreateMap<RestaurantCreatedViewModel, Restaurant>();
            CreateMap<RestaurantViewModel, Restaurant>().ForMember(dest => dest.AddressRestaurant, opt => opt.MapFrom(src => src.AddressRestaurant));
        }
    }
}
