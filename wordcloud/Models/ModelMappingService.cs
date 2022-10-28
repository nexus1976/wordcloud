using wordcloud.Data;

namespace wordcloud.Models
{
    public sealed class ModelMappingService : IModelMappingService
    {
        public CommentModel? MapCommentEntityToCommentModel(Comment? entity)
        {
            if (entity == null)
                return null;
            var model = new CommentModel()
            {
                Comment = entity.CommentText,
                CommentDate = entity.CommentDate,
                CommentParsed = entity.CommentParsed,
                Id = entity.Id
            };
            return model;
        }
        public IEnumerable<CommentModel>? MapCommentEntitiesToCommentModels(IEnumerable<Comment>? entities)
        {
            if (entities == null)
                return null;
            var modelList = new List<CommentModel>();
            foreach (var entity in entities)
            {
                var model = MapCommentEntityToCommentModel(entity);
                if (model != null)
                    modelList.Add(model);
            }
            return modelList;
        }
        public Comment? MapCommentModelToCommentEntity(CommentModel? model)
        {
            if (model == null)
                return null;
            var entity = new Comment()
            {
                Id = model.Id ?? Guid.Empty,
                CommentDate = model.CommentDate ?? DateTime.UtcNow,
                CommentParsed = model.CommentParsed ?? String.Empty,
                CommentText = model.Comment ?? String.Empty
            };
            return entity;
        }

        public WordCountModel? MapWordCountEntityToWordCountModel(WordCount? entity)
        {
            if (entity == null)
                return null;
            var model = new WordCountModel()
            {
                Word = entity.Word,
                Count = entity.Count
            };
            return model;
        }
        public IEnumerable<WordCountModel>? MapWordCountEntitiessToWordCountModels(IEnumerable<WordCount?> entities)
        {
            if (entities == null)
                return null;
            var modelList = new List<WordCountModel>();
            foreach (var entity in entities)
            {
                var model = MapWordCountEntityToWordCountModel(entity);
                if (model != null)
                    modelList.Add(model);
            }
            return modelList;
        }
    }
}
