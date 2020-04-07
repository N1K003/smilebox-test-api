using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Smilebox.TestApi.Models.Request.Common
{
    public class ByIdRequest
    {
        [FromRoute]
        public int Id { get; set; }
    }

    public class ByIdRequestValidator : AbstractValidator<ByIdRequest>
    {
        public ByIdRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id should be greater than 0");
        }
    }
}