using api.Dtos.Comment;
using api.Models;

namespace api.Mappers;

public static class CommentMappers
{
    public static CommentResponseDto ToCommentDto(this Comment comment)
    {
        return new CommentResponseDto
        {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            CreatedOn = comment.CreatedOn,
            StockId = comment.StockId
        };
    }

    public static Comment ToCommentFromCreate(this CreateCommentRequestDto comment, int stockId)
    {
        return new Comment
        {
            Title = comment.Title,
            Content = comment.Content,
            StockId = stockId
        };
    }
}