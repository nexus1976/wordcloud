using Microsoft.EntityFrameworkCore;

namespace wordcloud.Data
{
    public interface ICommandContext
    {
        DbSet<Comment> Comments { get; set; }
        DbSet<WordCount> WordCounts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
