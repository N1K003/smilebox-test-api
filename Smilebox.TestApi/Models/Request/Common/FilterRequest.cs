using System;
using FluentValidation;
using Smilebox.BusinessLogic.Contracts.Models.Common;

namespace Smilebox.TestApi.Models.Request.Common
{
    public class FilterRequest
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public SortDirection? SortDirection { get; set; }
    }

    public class FilterRequestValidator : AbstractValidator<FilterRequest>
    {
        public FilterRequestValidator()
        {
            When(x => x.Limit.HasValue, () =>
            {
                RuleFor(x => x.Limit.Value)
                    .GreaterThan(0)
                    .WithMessage("Limit should be greater than 0");
            });

            When(x => x.Offset.HasValue, () =>
            {
                RuleFor(x => x.Offset.Value)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Limit should be greater than -1");
            });


            When(x => x.SortDirection.HasValue, () =>
            {
                RuleFor(x => x.SortDirection.Value)
                    .IsInEnum()
                    .WithMessage($"Sort direction can be only [{string.Join(",", Enum.GetNames(typeof(SortDirection)))}]");
            });
        }
    }
}