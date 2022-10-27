namespace wordcloud.Models
{
    public sealed class WordCountModel
    {
        public Guid Id { get; set; }
        public string? Word { get; set; }
        public long Count { get; set; }
    }
}
