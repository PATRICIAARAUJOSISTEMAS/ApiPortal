﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Api.Controllers.Base
{
    // [Authorize]
    public class BaseController : Controller
    {
        [HttpGet]
        public Guid UserId()
        {
            var user = User as ClaimsPrincipal;
            return user.Claims.GetType().GUID;
        }
    }
}