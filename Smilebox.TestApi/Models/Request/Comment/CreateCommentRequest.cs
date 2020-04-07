using System;
using FluentValidation;

namespace Smilebox.TestApi.Models.Request.Comment
{
    public class CreateCommentRequest
    {
        public string Text { get; set; }
        public DateTimeOffset CommentDate { get; set; }
    }

    public class CreateCommentRequestValidator : AbstractValidator<CreateCommentRequest>
    {
        public CreateCommentRequestValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .MinimumLength(10)
                .WithMessage("Text should be specified, minimum length is 10");
            RuleFor(x => x.CommentDate)
                .LessThan(DateTimeOffset.UtcNow)
                .WithMessage("Comment date can't be in future");
        }
    }
}