using AutoMapper;
using PM.Domain.Category.Model;
using PM.Query.Service.Model.Category;

namespace PM.Query.Service.Mapper;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<Category, CategoryModel>();
    }
}