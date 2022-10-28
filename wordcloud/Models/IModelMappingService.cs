using wordcloud.Data;

namespace wordcloud.Models
{
    public interface IModelMappingService
    {
        CommentModel? MapCommentEntityToCommentModel(Comment? entity);
        IEnumerable<CommentModel>? MapCommentEntitiesToCommentModels(IEnumerable<Comment>? entities);
        Comment? MapCommentModelToCommentEntity(CommentModel? model);
        WordCountModel? MapWordCountEntityToWordCountModel(WordCount? entity);
        IEnumerable<WordCountModel>? MapWordCountEntitiessToWordCountModels(IEnumerable<WordCount?> entities);
    }
}