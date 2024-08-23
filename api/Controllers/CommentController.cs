using api.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/comment")]
[ApiController]

public class CommentController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public CommentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var comments = _context.Comments.ToList();

        if (comments.Count == 0)
        {
            return NotFound();
        }

        return Ok(comments);
    }
    
}