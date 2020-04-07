using System;
using Microsoft.EntityFrameworkCore;
using Smilebox.Data.Contracts.Models;
using Smilebox.Data.EntityFramework.ModelConfigurations;

namespace Smilebox.Data.EntityFramework
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DbCommentConfiguration());
            modelBuilder.ApplyConfiguration(new DbPostConfiguration());

            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbPost>()
                .HasData(
                    new DbPost
                    {
                        Id = 1, Title = "Test post #1 without comments", Text = "I'm a test post without comments",
                        PostDate = new DateTime(2020, 04, 07, 0, 1, 0)
                    },
                    new DbPost
                    {
                        Id = 2, Title = "Test post #2 without comments", Text = "I'm a test post without comments",
                        PostDate = new DateTime(2020, 04, 07, 0, 10, 0)
                    },
                    new DbPost
                    {
                        Id = 3, Title = "Test post #3 without comments", Text = "I'm a test post without comments",
                        PostDate = new DateTime(2020, 04, 07, 0, 20, 0)
                    },
                    new DbPost
                    {
                        Id = 4, Title = "Test post #1 with 1 comment", Text = "I'm a test post with comments",
                        PostDate = new DateTime(2020, 04, 07, 0, 5, 0)
                    },
                    new DbPost
                    {
                        Id = 5, Title = "Test post #2 with 2 comments", Text = "I'm a test post with comments",
                        PostDate = new DateTime(2020, 04, 07, 0, 15, 0)
                    },
                    new DbPost
                    {
                        Id = 6, Title = "Test post #3 with 3 comments", Text = "I'm a test post with comments",
                        PostDate = new DateTime(2020, 04, 07, 0, 25, 0)
                    }
                );

            modelBuilder.Entity<DbComment>()
                .HasData(
                    new DbComment
                    {
                        Id = 1, PostId = 4, Text = "I'm a test comment to post id #4",

                        CommentDate = new DateTime(2020, 04, 07, 0, 5, 30)
                    },
                    new DbComment
                    {
                        Id = 2, PostId = 5, Text = "I'm a test comment to post id #5",
                        CommentDate = new DateTime(2020, 04, 07, 0, 15, 30)
                    },
                    new DbComment
                    {
                        Id = 3, PostId = 5, Text = "I'm a test comment to post id #5",
                        CommentDate = new DateTime(2020, 04, 07, 0, 15, 55)
                    },
                    new DbComment
                    {
                        Id = 4, PostId = 6, Text = "I'm a test comment to post id #6",
                        CommentDate = new DateTime(2020, 04, 07, 0, 25, 15)
                    },
                    new DbComment
                    {
                        Id = 5, PostId = 6, Text = "I'm a test comment to post id #6",
                        CommentDate = new DateTime(2020, 04, 07, 0, 25, 30)
                    },
                    new DbComment
                    {
                        Id = 6, PostId = 6, Text = "I'm a test comment to post id #6",
                        CommentDate = new DateTime(2020, 04, 07, 0, 25, 45)
                    }
                );
        }
    }
}