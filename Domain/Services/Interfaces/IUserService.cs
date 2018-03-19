using Domain.Entities.Users;
using Domain.Request;
using Domain.Requests;
using Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<ResponseBase> AddAsync(UserRequest userRequest);

        Task<UserResponse> AuthenticateAsync(LoginRequest loginRequest);

        Task<IEnumerable<UserResponse>> GetAllByAsync(UserRequest userRequest);

        Task<User> GetByIdAsync(string id);
    }
}