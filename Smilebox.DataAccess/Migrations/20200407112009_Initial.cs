using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Smilebox.Data.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Posts",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(),
                    Text = table.Column<string>(),
                    PostDate = table.Column<DateTimeOffset>()
                },
                constraints: table => { table.PrimaryKey("PK_Posts", x => x.Id); });

            migrationBuilder.CreateTable(
                "Comments",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PostId = table.Column<int>(),
                    Text = table.Column<string>(),
                    CommentDate = table.Column<DateTimeOffset>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        "FK_Comments_Posts_PostId",
                        x => x.PostId,
                        "Posts",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                "Posts",
                new[] {"Id", "PostDate", "Text", "Title"},
                new object[,]
                {
                    {
                        1, new DateTimeOffset(new DateTime(2020, 4, 7, 0, 1, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                        "I'm a test post without comments", "Test post #1 without comments"
                    },
                    {
                        2, new DateTimeOffset(new DateTime(2020, 4, 7, 0, 10, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                        "I'm a test post without comments", "Test post #2 without comments"
                    },
                    {
                        3, new DateTimeOffset(new DateTime(2020, 4, 7, 0, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                        "I'm a test post without comments", "Test post #3 without comments"
                    },
                    {
                        4, new DateTimeOffset(new DateTime(2020, 4, 7, 0, 5, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                        "I'm a test post with comments", "Test post #1 with 1 comment"
                    },
                    {
                        5, new DateTimeOffset(new DateTime(2020, 4, 7, 0, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                        "I'm a test post with comments", "Test post #2 with 2 comments"
                    },
                    {
                        6, new DateTimeOffset(new DateTime(2020, 4, 7, 0, 25, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                        "I'm a test post with comments", "Test post #3 with 3 comments"
                    }
                });

            migrationBuilder.InsertData(
                "Comments",
                new[] {"Id", "CommentDate", "PostId", "Text"},
                new object[,]
                {
                    {
                        1, new DateTimeOffset(new DateTime(2020, 4, 7, 0, 5, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                        4, "I'm a test comment to post id #4"
                    },
                    {
                        2,
                        new DateTimeOffset(new DateTime(2020, 4, 7, 0, 15, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                        5, "I'm a test comment to post id #5"
                    },
                    {
                        3,
                        new DateTimeOffset(new DateTime(2020, 4, 7, 0, 15, 55, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                        5, "I'm a test comment to post id #5"
                    },
                    {
                        4,
                        new DateTimeOffset(new DateTime(2020, 4, 7, 0, 25, 15, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                        6, "I'm a test comment to post id #6"
                    },
                    {
                        5,
                        new DateTimeOffset(new DateTime(2020, 4, 7, 0, 25, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                        6, "I'm a test comment to post id #6"
                    },
                    {
                        6,
                        new DateTimeOffset(new DateTime(2020, 4, 7, 0, 25, 45, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                        6, "I'm a test comment to post id #6"
                    }
                });

            migrationBuilder.CreateIndex(
                "IX_Comments_PostId",
                "Comments",
                "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Comments");

            migrationBuilder.DropTable(
                "Posts");
        }
    }
}