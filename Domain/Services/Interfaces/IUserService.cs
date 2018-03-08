using Api.Services.Base;
using Domain.Request;
using Domain.Requests;
using Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IUserService : IScopedServiceBase
    {
        Task<ResponseBase> AddAsync(UserRequest userRequest);

        Task<UserResponse> AuthenticateAsync(LoginRequest loginRequest);

        Task<IEnumerable<UserResponse>> GetAllByAsync(UserRequest userRequest);
    }
}