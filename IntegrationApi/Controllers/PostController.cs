using IntegrationBusiness.Services;
using IntegrationDtos;
using IntegrationDtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] PostRequest postRequest)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(new Response<List<string>>(false, "Validation failed") { Data = errors });
        }

        var response = await _postService.InsertNewPost(postRequest);

        if (!response.Success)
            return BadRequest(response);

        return Created($"api/posts/{response.Data.Id}", response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemovePost(long id)
    {
        var response = await _postService.removePost(id);
        return response.Success ? Ok(response) : NotFound(response);
    }
    
    
}