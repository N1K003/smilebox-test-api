using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Smilebox.BusinessLogic.Contracts.Models.Common;
using Smilebox.BusinessLogic.Contracts.Models.Post;
using Smilebox.BusinessLogic.Contracts.Services;
using Smilebox.BusinessLogic.Extensions;
using Smilebox.Common.Exceptions;
using Smilebox.Data.Contracts.Abstractions;
using Smilebox.Data.Contracts.Models;

namespace Smilebox.BusinessLogic.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PostModel>> GetPostsAsync(FilterModel filterModel, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Get<DbPost>();

            if (filterModel.Offset.HasValue)
            {
                query = query.Skip(filterModel.Offset.Value);
            }

            if (filterModel.Limit.HasValue)
            {
                query = query.Take(filterModel.Limit.Value);
            }

            if (filterModel.SortDirection == SortDirection.Asc)
            {
                query = query.OrderBy(x => x.PostDate);
            }
            else if (filterModel.SortDirection == SortDirection.Desc)
            {
                query = query.OrderByDescending(x => x.PostDate);
            }

            var dbPosts = await query
                .Include(x => x.Comments)
                .ToListAsync(cancellationToken);

            return dbPosts.Select(x => x.ToBlModel());
        }

        public async Task<PostModel> GetPostAsync(int postId, CancellationToken cancellationToken)
        {
            var dbPost = await _unitOfWork.Get<DbPost>()
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == postId, cancellationToken);

            if (dbPost == null)
            {
                throw new NotFoundException("post");
            }

            return dbPost.ToBlModel();
        }

        public async Task<PostModel> CreatePostAsync(CreatePostModel model, CancellationToken cancellationToken)
        {
            var post = _unitOfWork.Create(new DbPost
            {
                Title = model.Title,
                Text = model.Text,
                PostDate = model.PostDate
            });

            await _unitOfWork.CommitAsync(cancellationToken);

            return post.ToBlModel();
        }

        public async Task DeletePostAsync(int postId, CancellationToken cancellationToken)
        {
            var dbPost = await _unitOfWork.Get<DbPost>()
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == postId, cancellationToken);

            if (dbPost == null)
            {
                throw new NotFoundException("post");
            }

            if (EnumerableExtensions.Any(dbPost.Comments))
            {
                throw new ValidationException("It is not allowed to delete post with comments");
            }

            _unitOfWork.Delete(dbPost);

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}