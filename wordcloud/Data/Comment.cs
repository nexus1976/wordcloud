namespace wordcloud.Data
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public string CommentParsed { get; set; } = string.Empty;
        public DateTime CommentDate { get; set; }
    }
}
