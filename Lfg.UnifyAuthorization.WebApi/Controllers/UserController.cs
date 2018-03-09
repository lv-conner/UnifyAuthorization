using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lfg.UnifyAuthorization.Application.Interface;

namespace Lfg.UnifyAuthorization.WebApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserApp _userApp;
        public UserController() { }
        public UserController(IUserApp userApp)
        {
            _userApp = userApp;
        }
        public int GetUserCount()
        {
            return 10;
        }
    }
}
