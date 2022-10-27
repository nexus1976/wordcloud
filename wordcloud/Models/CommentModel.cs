namespace wordcloud.Models
{
    public sealed class CommentModel
    {
        public Guid? Id { get; set; }
        public string? Comment { get; set; }
        public string? CommentParsed { get; set; }
        public DateTime? CommentDate { get; set; }
    }
}
