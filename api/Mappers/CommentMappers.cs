using api.Dtos.Comment;
using api.Models;

namespace api.Mappers;

public static class CommentMappers
{
    public static CommentResponseDto ToCommentResponseDto(this Comment comment)
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

    public static Comment ToCommentFromCreateCommentRequestDto(this CreateCommentRequestDto createCommentRequestDto, int stockId)
    {
        return new Comment
        {
            Title = createCommentRequestDto.Title,
            Content = createCommentRequestDto.Content,
            StockId = stockId
        };
    }
    
}