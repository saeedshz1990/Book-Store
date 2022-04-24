using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BookStore.Infrastructure.Application
{
    public interface UnitOfWork
    {
        void Commit();
    }
}
