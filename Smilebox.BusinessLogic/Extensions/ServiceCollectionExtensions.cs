using Microsoft.Extensions.DependencyInjection;
using Smilebox.BusinessLogic.Contracts.Services;
using Smilebox.BusinessLogic.Services;

namespace Smilebox.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            return services.AddTransient<IPostService, PostService>()
                .AddTransient<ICommentService, CommentService>();
        }
    }
}