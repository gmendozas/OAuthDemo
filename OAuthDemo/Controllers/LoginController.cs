using OAuthDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OAuthDemo.Controllers
{
    public class LoginController : ApiController
    {
        [Authorize]
        public IHttpActionResult Get()
        {
            return Ok(LoginModel.LoginUser());
        }

    }
}
