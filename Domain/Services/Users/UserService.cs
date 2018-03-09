using Api.Settings;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using AutoMapper;
using Domain.Entities.Users;
using Domain.Interfaces;
using Domain.Request;
using Domain.Requests;
using Domain.Resources;
using Domain.Responses;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Users
{
    public class UserService : IUserService
    {
        private readonly AppSettings _configuration;
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;
        private ResponseBase _responseBase;

        public UserService(IUserRepository userRepository,
            IOptions<AppSettings> configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration.Value;
            _responseBase = new ResponseBase();
            _mapper = mapper;
        }

        public async Task<ResponseBase> AddAsync(UserRequest userRequest)
        {
            var userInfo = _mapper.Map<UserInfo>(userRequest);
            userInfo.UserId = Guid.NewGuid().ToString();

            var user = Create(userInfo, userRequest.Password);

            if (user.IsFailure)
            {
                _responseBase.AddMessages(user.Errors);
                return _responseBase;
            }
            await _userRepository.PostAsync(user);
            return _responseBase;
        }

        public async Task<ResponseBase> AddAsyncAuth0(UserInfo userInfo, string password)
        {
            var user = Create(userInfo, password);
            if (user.IsFailure)
            {
                _responseBase.AddMessages(user.Errors);
                return _responseBase;
            }
            await _userRepository.PostAsync(user);

            return _responseBase;
            //_context.SaveChanges(); não esquecer de mandar salvar essas porra
        }

        public async Task<UserResponse> AuthenticateAsync(LoginRequest loginRequest)
        {
            var user = (await _userRepository.GetAsync(f => f.NickName == loginRequest.NickName.ToUpper())).FirstOrDefault();

            if (!VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
            {
                var responseError = _mapper.Map<UserResponse>(user);
                responseError.AddMessages(user.Errors);
                return responseError;
            };

            if (user == null || string.IsNullOrEmpty(user.NickName))
                user = _mapper.Map<User>(await AuthenticateAuth0Async(loginRequest));

            return _mapper.Map<UserResponse>(user);
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user != null)
            {
                user.SetDeleted(true);
                _userRepository.Put(user);
            }
        }

        public async Task<IEnumerable<UserResponse>> GetAllByAsync(UserRequest userRequest)
        {
            var user = await _userRepository.GetAsync(f => f.Email.Contains(userRequest.Email.ToUpper())
                || userRequest.FullName.Contains(userRequest.FullName.ToUpper())
                || userRequest.FullName == userRequest.FullName.ToUpper()
                || userRequest.Email.Contains(userRequest.Email.ToUpper()));

            return _mapper.Map<IEnumerable<UserResponse>>(user);
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<ResponseBase> UpdateAsync(UserRequest userRequest)
        {
            var user = await _userRepository.GetByIdAsync(userRequest.Id);

            if (user == null)
            {
                _responseBase.AddMessage(string.Format(Message.X0_X1_NAO_ENCONTRADO, null, Message.Usuario));
                return _responseBase;
            }

            user.SetEmail(userRequest.Email);
            user.SetFirstName(userRequest.FirstName);
            user.SetNickName(userRequest.NickName);
            user.SetFullName(userRequest.FullName);

            if (user.IsFailure)
            {
                _responseBase.AddMessages(user.Errors);
                return _responseBase;
            }

            _userRepository.Put(user);
            return _responseBase;
        }

        private static User Create(UserInfo userInfo, string password)
        {
            var user = new User(userInfo.UserId, userInfo.NickName, password);

            if (user == null)
                return user;

            user.SetEmail(userInfo.Email);
            user.SetFirstName(userInfo.FirstName);
            user.SetFullName(userInfo.FullName);

            return user;
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }

            return true;
        }

        private async Task<UserInfo> AuthenticateAuth0Async(LoginRequest loginRequest)
        {
            var teste = $"https://{_configuration.Domain}";

            var client = new AuthenticationApiClient(new Uri(teste));

            var result = await client.GetTokenAsync(new ResourceOwnerTokenRequest
            {
                ClientId = _configuration.ClientId,
                ClientSecret = _configuration.ClientSecret,
                Scope = _configuration.Scope,
                Username = loginRequest?.NickName,
                Realm = _configuration.Realm,
                Password = loginRequest?.Password,
                Audience = _configuration.Audience
            });

            var user = await client.GetUserInfoAsync(result.AccessToken);

            await AddAsyncAuth0(user, loginRequest.Password);
            return user;
        }
    }
}