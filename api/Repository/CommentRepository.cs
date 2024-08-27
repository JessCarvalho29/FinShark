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