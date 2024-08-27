using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;
    
    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.FindAsync(id);
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    // public async Task<Comment?> UpdateAsync(int id, Comment comment)
    // {
    //     var commentToUpdate = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
    //
    //     if (commentToUpdate == null)
    //     {
    //         return null;
    //     }
    //     
    //     commentToUpdate.Title = comment.Title;
    //     commentToUpdate.Content = comment.Content;
    //     commentToUpdate.CreatedOn = comment.CreatedOn;
    //     
    //     
    // }
}