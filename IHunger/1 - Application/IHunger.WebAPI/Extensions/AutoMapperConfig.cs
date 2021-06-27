using AutoMapper;
using IHunger.Domain.Models;
using IHunger.WebAPI.ViewModels.CategoryProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHunger.WebAPI.Extensions
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CategoryProduct, CategoryProductViewModel>();
            CreateMap<CategoryProductViewModel, CategoryProduct>()
                .ForMember(x => x.CreatedAt, y => y.Ignore())
                .ForMember(x => x.UpdatedAt, y => y.Ignore())
                .ForMember(x => x.Id, y => y.Ignore());
            CreateMap<CategoryProductCreatedViewModel, CategoryProduct>();

        }
    }
}
