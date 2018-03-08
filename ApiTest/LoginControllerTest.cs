using Api.Controllers.Users;
using Api.Services.Interfaces;
using Domain.Request;
using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest
{
    [TestClass]
    public class LoginControllerTest
    {
        private readonly Mock<IUserService> _repoMock;

        public LoginControllerTest()
        {
            _repoMock = new Mock<IUserService>();
        }

        [TestMethod]
        public async Task GetUsers()
        {
            var userRequest = CreateUserRequest();
            var userResponse = GetUser();

            var user = _repoMock.Setup(f => f.GetAllByAsync(userRequest)).Returns(Task.FromResult(userResponse));
            Assert.IsNotNull(user, "Usuario invalido");

            var usersController = new UsersController(_repoMock.Object);
            var actionResult = await usersController.GetUser(userRequest);
            Assert.IsNotNull(actionResult, "usuario não encontrado");

            Assert.IsInstanceOfType(actionResult, typeof(ActionResult));
        }

        [TestMethod]
        public async Task LoginTest()
        {
            var loginRequest = CreateLoginRequest();
            var loginFake = GetUser().FirstOrDefault();

            var user = _repoMock.Setup(f => f.AuthenticateAsync(loginRequest)).Returns(Task.FromResult(loginFake));
            Assert.IsNotNull(user, "Usuario invalido");

            var usersController = new UsersController(_repoMock.Object);
            var actionResult = await usersController.Authenticate(loginRequest);
            Assert.IsNotNull(actionResult, "usuario não encontrado");

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task LogoutTest()
        {
            var loginRequest = CreteUserRequestFUll();
            var baseResponse = GetResponse().FirstOrDefault();

            var user = _repoMock.Setup(f => f.AddAsync(loginRequest)).Returns(Task.FromResult(baseResponse));
            Assert.IsNotNull(user, "Usuario Invalido");

            var usersController = new UsersController(_repoMock.Object);
            var actionResult = await usersController.SingUp(loginRequest);
            Assert.IsNotNull(actionResult, "usuario não encontrado");

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }

        private static IEnumerable<ResponseBase> GetResponse() => new List<ResponseBase>();

        private static IEnumerable<UserResponse> GetUser() => new List<UserResponse>();

        private LoginRequest CreateLoginRequest()
        {
            return new LoginRequest()
            {
                NickName = "patriciaaraujo",
                Password = "p20c12a91"
            };
        }

        private UserRequest CreateUserRequest()
        {
            return new UserRequest()
            {
                NickName = "patriciaaraujo",
                Password = "tste",
                Email = "patricia.araujosistemas@gmail.com",
                FullName = "Patricia Araujo"
            };
        }

        private UserRequest CreteUserRequestFUll()
        {
            return new UserRequest
            {
                NickName = "patriciaaraujo",
                Password = "teste",
                Email = "patricia.araujosistemas@gmail.com",
                FullName = "Patricia Araujo"
            };
        }
    }
}