using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace Api.Controllers.Base
{
    [Authorize]
    public class BaseController : Controller
    {
        public string UserEmail
        {
            get => ((ClaimsIdentity)Request.HttpContext.User.Identity)
                .Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c?.Value).SingleOrDefault();
        }

        public Guid UserId
        {
            get => ((ClaimsIdentity)Request.HttpContext.User.Identity)
                .Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => Guid.Parse(c?.Value)).SingleOrDefault();
        }
    }
}