using Application.DTOs;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryDto> CreateAsync(CreateCategoryDto request);
    Task<bool> DeleteAsync(int id);
}

public interface ITagService
{
    Task<IEnumerable<TagDto>> GetAllAsync();
    Task<TagDto> CreateAsync(CreateTagDto request);
    Task<bool> DeleteAsync(int id);
}
