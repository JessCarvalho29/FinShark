using api.Data;
using api.Dtos.Comment;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
        // Adding the Include to show the appUser (because of deferred execution)
        return await _context.Comments.Include(a => a.AppUser).ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        // Adding the Include to show the appUser (because of deferred execution)
        return await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateCommentRequestDto)
    {
        var commentToUpdate = await _context.Comments.FindAsync(id);
    
        if (commentToUpdate == null)
        {
            return null;
        }
        
        commentToUpdate.Title = updateCommentRequestDto.Title;
        commentToUpdate.Content = updateCommentRequestDto.Content;
        
        await _context.SaveChangesAsync();
        return commentToUpdate;

    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var commentModel = await _context.Comments.FindAsync(id);

        if (commentModel == null)
        {
            return null;
        }

        _context.Comments.Remove(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }
}