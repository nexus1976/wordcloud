namespace wordcloud.Data
{
    public class WordCount
    {
        public Guid Id { get; set; }
        public string Word { get; set; } = string.Empty;
        public long Count { get; set; }
    }
}
