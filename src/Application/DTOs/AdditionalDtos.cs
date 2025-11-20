namespace Application.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateCategoryDto
{
    public string Name { get; set; } = string.Empty;
}

public class TagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateTagDto
{
    public string Name { get; set; } = string.Empty;
}
