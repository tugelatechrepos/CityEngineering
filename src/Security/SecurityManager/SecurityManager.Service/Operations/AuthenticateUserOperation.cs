using SecurityAccess.Client;
using SecurityAccess.Contracts;
using SecurityAccess.Contracts.Contracts;
using System.ComponentModel.Composition;

namespace SecurityManager.Service.Operations
{
    public interface IAuthenticateUserOperation
    {
        GetUserForAuthenticateResponse GetUserForAuthenticate(GetUserForAuthenticateRequest GetUserForAuthenticateRequest);
    }

    [Export(typeof(IAuthenticateUserOperation))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AuthenticateUserOperation : IAuthenticateUserOperation
    {
        #region Declarations
        private GetUserForAuthenticateRequest _Request;
        private GetUserForAuthenticateResponse _Response;

        [Import]
        public ISecurityAccess SecurityAccess { get; set; }
        #endregion Declarations

        public GetUserForAuthenticateResponse GetUserForAuthenticate(GetUserForAuthenticateRequest GetUserForAuthenticateRequest)
        {
            _Request = GetUserForAuthenticateRequest;
            _Response = new GetUserForAuthenticateResponse { ValidationResults = new ValidationResults() };

            transferToDc();

            return _Response;
        }

        private void transferToDc()
        {
            if (!_Response.ValidationResults.IsValid) return;

           _Response =  SecurityAccess.GetUserForAuthenticate(_Request);
        }
    }
}
