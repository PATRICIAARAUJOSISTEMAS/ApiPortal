using Data.Context;
using Domain.Entities.Users;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infra.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DataContext context)
           : base(context) => _context = context;
    }
}