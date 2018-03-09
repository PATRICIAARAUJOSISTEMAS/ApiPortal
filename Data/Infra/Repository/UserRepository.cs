using Data.Context;
using Domain.Entities.Users;
using Domain.Interfaces;

namespace Data.Infra.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DataContext context)
           : base(context) => _context = context;
    }
}