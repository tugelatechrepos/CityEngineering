using Project.Core;
using SecurityAccess.Contracts;
using SecurityManager.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SecurityHost.Controllers
{
    public class SignUpController : ApiController
    {        
        public IHttpActionResult Post([FromBody] SignUpUserRequest signUpUserRequest)
        {
            var securityManager = IOCManager.Resolve<ISecurityManager>();
            var signUpUserResponse = securityManager.SignUpUser(signUpUserRequest);
            return Ok(signUpUserResponse);
        }

        [HttpGet]
        public string Get()
        {
            return "Test";
        }
    }
}
