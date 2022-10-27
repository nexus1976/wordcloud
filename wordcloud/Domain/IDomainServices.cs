using wordcloud.Data;

namespace wordcloud.Domain
{
    public interface IDomainServices
    {
        string ParseComment(string comment);
        Task<IDictionary<string, WordCount>> GetWordCountsAsync(string parsedComment, ICommandContext commandContext, CancellationToken cancellationToken);
    }
}