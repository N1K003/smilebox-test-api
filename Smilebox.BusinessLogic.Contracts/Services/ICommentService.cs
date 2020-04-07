using System.Threading;
using System.Threading.Tasks;
using Smilebox.BusinessLogic.Contracts.Models.Comment;

namespace Smilebox.BusinessLogic.Contracts.Services
{
    public interface ICommentService
    {
        Task CreateCommentAsync(CreateCommentModel model, CancellationToken cancellationToken);
    }
}