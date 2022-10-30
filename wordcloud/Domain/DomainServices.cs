﻿using Microsoft.EntityFrameworkCore;
using System.Text;
using wordcloud.Data;

namespace wordcloud.Domain
{
    public sealed class DomainServices : IDomainServices
    {
        private readonly HashSet<string> _articles = new()
        {
            "a", "at", "to", "the", "it", "or", "and", "an", "of", "by", "on", "for", "this", "that",
            "is", "in", "then", "until", "from", "so", "he", "him", "her", "she", "they", "them",
            "no", "not", "his", "hers", "their", "theirs", "here", "there", "into", "onto", "unto"

        };

        private readonly HashSet<char> _punctuations = new()
        {
            ',', '.', '!', '?', ':', ';', '"', '\'', '(', ')', '{', '}', '/', '<', '>', '\\', '@', '#', '$', '%', '^', '&', '*', '_', '=', '+', '|', '[', ']', '-'
        };

        public string ParseComment(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                return string.Empty;

            // todo: optimize this
            var arr = comment.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new();
            foreach (var item in arr)
            {
                if (!_articles.Contains(item.ToLower()))
                {
                    sb.Append(item);
                    sb.Append(' ');
                }
            }
            return RemovePunctuation(sb);
        }
        private string RemovePunctuation(StringBuilder sb)
        {
            foreach (var item in _punctuations)
            {
                sb.Replace(item, ' ');
            }
            return sb.ToString().Trim();
        }

        public async Task<IDictionary<string, WordCount>> GetWordCountsAsync(string parsedComment, ICommandContext commandContext, CancellationToken cancellationToken)
        {
            Dictionary<string, WordCount> wordCounts = new();
            if (!string.IsNullOrWhiteSpace(parsedComment))
            {
                var words = parsedComment.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    if (wordCounts.ContainsKey(word))
                        wordCounts[word].Count++;
                    else
                    {
                        var wordCount = await commandContext.WordCounts.FirstOrDefaultAsync(d => d.Word == word, cancellationToken);
                        if (wordCount != null)
                        {
                            wordCount.Count++;
                        }
                        else
                        {
                            wordCount = new WordCount()
                            {
                                Word = word,
                                Count = 1,
                                Id = Guid.NewGuid()
                            };
                            commandContext.WordCounts.Add(wordCount);
                        }
                        wordCounts.Add(word, wordCount);
                    }
                }
            }
            return wordCounts;
        }
    }
}
