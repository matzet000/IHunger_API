using AutoMapper;
using IHunger.Domain.Models;
using IHunger.WebAPI.ViewModels.Address;
using IHunger.WebAPI.ViewModels.CategoryProduct;
using IHunger.WebAPI.ViewModels.CategoryRestaurant;
using IHunger.WebAPI.ViewModels.Comment;
using IHunger.WebAPI.ViewModels.Product;
using IHunger.WebAPI.ViewModels.Restaurant;

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
            CreateMap<RestaurantViewModel, Restaurant>()
                .ForMember(dest => dest.AddressRestaurant, opt => opt.MapFrom(src => src.AddressRestaurant));

            CreateMap<Comment, CommentViewModel>();
            CreateMap<CommentCreatedViewModel, Comment>()
                .ForMember(dest => dest.IdRestaurant, opt => opt.MapFrom(src => src.IdRestaurant));
            CreateMap<CommentViewModel, Comment>();

            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductCreatedViewModel, Product>()
                .ForMember(dest => dest.IdRestaurant, opt => opt.MapFrom(src => src.IdRestaurant))
                .ForMember(dest => dest.IdCategoryProduct, opt => opt.MapFrom(src => src.IdCategoryProduct));
            CreateMap<ProductViewModel, Product>()
                .ForMember(dest => dest.CategoryProduct, opt => opt.MapFrom(src => src.CategoryProduct));
        }
    }
}
