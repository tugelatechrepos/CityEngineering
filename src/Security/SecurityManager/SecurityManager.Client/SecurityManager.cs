using SecurityAccess.Contracts;
using SecurityAccess.Contracts.Contracts;
using SecurityManager.Service.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager.Client
{
    public interface ISecurityManager
    {
        SignUpUserResponse SignUpUser(SignUpUserRequest signUpUserRequest);

        GetUserForAuthenticateResponse AuthenticateUser(GetUserForAuthenticateRequest GetUserForAuthenticateRequest);
    }

    [Export(typeof(ISecurityManager))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SecurityManager : ISecurityManager
    {
        #region Declarations
        [Import]
        public ISignUpUserOperation SignUpUserOperation { get; set; }

        [Import]
        public IAuthenticateUserOperation AuthenticateUserOperation { get; set; }

        #endregion Declarations

        public SignUpUserResponse SignUpUser(SignUpUserRequest signUpUserRequest)
        {
           var response = SignUpUserOperation.SignUpUser(signUpUserRequest);
           return response;
        }

        public GetUserForAuthenticateResponse AuthenticateUser(GetUserForAuthenticateRequest GetUserForAuthenticateRequest)
        {
            var response = AuthenticateUserOperation.GetUserForAuthenticate(GetUserForAuthenticateRequest);
            return response;
        }
    }
}
