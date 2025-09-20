using AutoMapper;
using ProductMicroService.DTOS;
using ProductMicroService.Models;

namespace ProductMicroService.Helper
{
    public class AutomapperProfile:Profile
    {

        public AutomapperProfile()
        {
            CreateMap<AddUpdateProductDto, Product>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName.Trim().ToUpper())).
                ForMember(dest => dest.ProductCompany, opt => opt.MapFrom(src => src.ProductCompany.ToUpper().Trim()));
            CreateMap<AddUpdateCategoryDto, Category>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName.Trim().ToUpper()))
                ;


            CreateMap<Product, ProductSendDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<Category, CategoryProductSendDto>();
        }
    }
}
