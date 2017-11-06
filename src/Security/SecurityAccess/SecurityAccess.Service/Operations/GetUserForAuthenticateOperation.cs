using SecurityAccess.Contracts;
using SecurityAccess.Contracts.Contracts;
using SecurityAccess.Service.Repositories;
using System.ComponentModel.Composition;

namespace SecurityAccess.Service.Operations
{
    public interface IGetUserForAuthenticateOperation
    {
       GetUserForAuthenticateResponse GetUserForAuthenticate(GetUserForAuthenticateRequest GetUserForAuthenticateRequest);
    }


    [Export(typeof(IGetUserForAuthenticateOperation))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GetUserForAuthenticateOperation : IGetUserForAuthenticateOperation
    {
        #region Declarations
        private GetUserForAuthenticateRequest _Request;
        private GetUserForAuthenticateResponse _Response;       

        [Import]
        public IGetUserForAuthenticateRepository GetUserForAuthenticateRepository { get; set; }
        #endregion Declarations

        public GetUserForAuthenticateResponse GetUserForAuthenticate(GetUserForAuthenticateRequest GetUserForAuthenticateRequest)
        {
            _Request = GetUserForAuthenticateRequest;
            _Response = new GetUserForAuthenticateResponse { ValidationResults = new ValidationResults() };

            getUserForAuthenticate();
            validateUserAccount();       
            return _Response;
        }

        private void getUserForAuthenticate()
        {
            if (!_Response.ValidationResults.IsValid) return;

            _Response = GetUserForAuthenticateRepository.GetUserForAuthenticate(_Request);
        }

        private void validateUserAccount()
        {
            if (!_Response.ValidationResults.IsValid) return;

            if (_Response.UserAccountDetail != null) return;

            _Response.ValidationResults.Add(new ValidationResult { ValidationMessage = "No User Found" });
        }      
    }
}
