using AutoMapper;
using IHunger.Domain.Models;
using IHunger.WebAPI.ViewModels.Address;
using IHunger.WebAPI.ViewModels.CategoryProduct;
using IHunger.WebAPI.ViewModels.CategoryRestaurant;
using IHunger.WebAPI.ViewModels.Comment;
using IHunger.WebAPI.ViewModels.Coupon;
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

            CreateMap<Restaurant, RestaurantViewModel>()
                .ForMember(dest => dest.AddressRestaurant, opt => opt.MapFrom(src => src.AddressRestaurant))
                .ForMember(dest => dest.CategoryRestaurant, opt => opt.MapFrom(src => src.CategoryRestaurant))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ReverseMap();

            CreateMap<RestaurantCreatedViewModel, Restaurant>();

            CreateMap<RestaurantViewModel, Restaurant>()
                .ForMember(dest => dest.AddressRestaurant, opt => opt.MapFrom(src => src.AddressRestaurant))
                .ForMember(dest => dest.CategoryRestaurant, opt => opt.MapFrom(src => src.CategoryRestaurant));

            CreateMap<RestaurantCreatedViewModel, Restaurant>()
                .ForMember(dest => dest.AddressRestaurant, opt => opt.MapFrom(src => src.Address));

            CreateMap<Comment, CommentViewModel>()
                .ReverseMap();
            
            CreateMap<CommentCreatedViewModel, Comment>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Starts, opt => opt.MapFrom(src => src.Starts));

            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.CategoryProduct, opt => opt.MapFrom(src => src.CategoryProduct))
                .ForMember(dest => dest.Restaurant, opt => opt.MapFrom(src => src.Restaurant))
                .ReverseMap();

            CreateMap<ProductCreatedViewModel, Product>()
                .ForMember(dest => dest.IdRestaurant, opt => opt.MapFrom(src => src.IdRestaurant))
                .ForMember(dest => dest.IdCategoryProduct, opt => opt.MapFrom(src => src.IdCategoryProduct));

            CreateMap<Coupon, CouponViewModel>()
                .ReverseMap();

            CreateMap<Coupon, CouponCreatedViewModel>()
                .ReverseMap();

            CreateMap<CouponCreatedViewModel, Coupon>()
                .ReverseMap();

            CreateMap<CouponViewModel, Coupon>().
                ForMember(x => x.Orders, opt => opt.Ignore());

        }
    }
}
