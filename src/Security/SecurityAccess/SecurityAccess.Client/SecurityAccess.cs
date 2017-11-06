using SecurityAccess.Contracts;
using SecurityAccess.Contracts.Contracts;
using SecurityAccess.Service.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAccess.Client
{
    public interface ISecurityAccess
    {
        SignUpUserResponse PersistSignUpUser(SignUpUserRequest Request);

        GetUserForAuthenticateResponse GetUserForAuthenticate(GetUserForAuthenticateRequest Request);

        CheckForUserAccountResponse CheckForUserAccount(CheckForUserAccountRequest CheckForUserAccountRequest);
    }

    [Export(typeof(ISecurityAccess))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SecurityAccess : ISecurityAccess
    {
        #region Declarations

        [Import]
        public IPersistSignUpUserOperation PersistUserAccountOperation { get; set; }

        [Import]
        public IGetUserForAuthenticateOperation GetUserForAuthenticateOperation { get; set; }

        [Import]
        public ICheckForUserAccountOperation CheckForUserAccountOperation { get; set; }
        #endregion Declarations

        public SignUpUserResponse PersistSignUpUser(SignUpUserRequest Request)
        {
           return PersistUserAccountOperation.PersistSignUpUser(Request);
        }

        public GetUserForAuthenticateResponse GetUserForAuthenticate(GetUserForAuthenticateRequest Request)
        {
            return GetUserForAuthenticateOperation.GetUserForAuthenticate(Request);
        }

        public CheckForUserAccountResponse CheckForUserAccount(CheckForUserAccountRequest CheckForUserAccountRequest)
        {
            return CheckForUserAccountOperation.CheckForUserAccount(CheckForUserAccountRequest);
        }
    }
}
