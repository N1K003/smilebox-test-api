using System;
using FluentValidation;

namespace Smilebox.TestApi.Models.Request.Post
{
    public class CreatePostRequest
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTimeOffset PostDate { get; set; }
    }

    public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(5, 150)
                .WithMessage("Title should be specified, minimum length is 5, maximum length is 150");

            RuleFor(x => x.Text)
                .NotEmpty()
                .MinimumLength(10)
                .WithMessage("Text should be specified, minimum length is 10");

            RuleFor(x => x.PostDate)
                .LessThan(DateTimeOffset.UtcNow)
                .WithMessage("Post date can't be in future");
        }
    }
}