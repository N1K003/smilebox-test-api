using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Smilebox.BusinessLogic.Contracts.Models.Comment;
using Smilebox.BusinessLogic.Contracts.Services;
using Smilebox.Common.Exceptions;
using Smilebox.Data.Contracts.Abstractions;
using Smilebox.Data.Contracts.Models;

namespace Smilebox.BusinessLogic.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateCommentAsync(CreateCommentModel model, CancellationToken cancellationToken)
        {
            var dbPost = await _unitOfWork.Get<DbPost>()
                .FirstOrDefaultAsync(x => x.Id == model.PostId, cancellationToken);

            if (dbPost == null)
            {
                throw new NotFoundException("post");
            }

            _unitOfWork.Create(new DbComment
            {
                PostId = dbPost.Id,
                Text = model.Text,
                CommentDate = model.CommentDate
            });

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}