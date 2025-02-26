using Koneshgar.DataLayer.Contexts;
using Koneshgar.Domain.Interfaces;
using Koneshgar.Domain.Models.Tasks;

namespace Koneshgar.DataLayer.Repositories
{
    public class CommentRepository : EfEntityRepositoryBase<Comment, TaskContext>, ICommentRepository
    {
        public CommentRepository(TaskContext context) : base(context)
        {

        }
    }
}
