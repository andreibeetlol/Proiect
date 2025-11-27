using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetAll()
    {
        return Ok(await _tagService.GetAllAsync());
    }

    [HttpPost]
    public async Task<ActionResult<TagDto>> Create(CreateTagDto request)
    {
        var tag = await _tagService.CreateAsync(request);
        return Ok(tag);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _tagService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
