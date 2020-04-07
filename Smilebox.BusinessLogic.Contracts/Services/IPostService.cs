using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Smilebox.BusinessLogic.Contracts.Models.Common;
using Smilebox.BusinessLogic.Contracts.Models.Post;

namespace Smilebox.BusinessLogic.Contracts.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostModel>> GetPostsAsync(FilterModel filterModel, CancellationToken cancellationToken);
        Task<PostModel> GetPostAsync(int postId, CancellationToken cancellationToken);
        Task<PostModel> CreatePostAsync(CreatePostModel model, CancellationToken cancellationToken);
        Task DeletePostAsync(int postId, CancellationToken cancellationToken);
    }
}