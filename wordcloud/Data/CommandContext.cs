using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Npgsql;

namespace wordcloud.Data
{
    public sealed class CommandContext : DbContext, ICommandContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CommandContext() { }
        public CommandContext(DbContextOptions options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public DbSet<Comment> Comments { get; set; }
        public DbSet<WordCount> WordCounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseNpgsql();
                dbContextOptionsBuilder.EnableDetailedErrors(true);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comments");
                entity.HasKey("Id");
                entity.Property(e => e.Id).IsRequired().ValueGeneratedNever().HasColumnName("id");
                entity.Property(e => e.CommentText).IsRequired().HasColumnName("commenttext");
                entity.Property(e => e.CommentParsed).IsRequired().HasColumnName("commentparsed");
                entity.Property(e => e.CommentDate).IsRequired().HasColumnName("commentdate");
            });

            modelBuilder.Entity<WordCount>(entity =>
            {
                entity.ToTable("WordCounts");
                entity.HasKey("Id");
                entity.Property(e => e.Id).IsRequired().ValueGeneratedNever().HasColumnName("id");
                entity.Property(e => e.Word).IsRequired().HasMaxLength(1024).HasColumnName("word");
                entity.Property(e => e.Count).IsRequired().HasColumnName("wordcount");
            });
        }
    }
}
