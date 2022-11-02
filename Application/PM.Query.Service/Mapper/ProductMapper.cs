using AutoMapper;
using PM.Domain.Product.Model;
using PM.Query.Service.Model.Product;

namespace PM.Query.Service.Mapper;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<ProductSearch, ProductSearchModel>();
    }
}