using SecurityAccess.Contracts;
using SecurityAccess.Service.Repositories;
using System.ComponentModel.Composition;

namespace SecurityAccess.Service.Operations
{

    public interface IPersistSignUpUserOperation
    {
        SignUpUserResponse PersistSignUpUser(SignUpUserRequest SignUpUserRequest);
    }

    [Export(typeof(IPersistSignUpUserOperation))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersistSignUpUserOperation : IPersistSignUpUserOperation
    {
        #region Declarations
        private SignUpUserRequest _Request;
        private SignUpUserResponse _Response;

         [Import]
        public IPersistUserRepository PersistUserRepository { get; set; }

        #endregion Declarations

        public SignUpUserResponse PersistSignUpUser(SignUpUserRequest PersistUserAccountRequest)
        {
            _Request = PersistUserAccountRequest;
            _Response = new SignUpUserResponse { ValidationResults = new ValidationResults() };

            persistUser();

            return _Response;
        }

        private void persistUser()
        {
            var response = PersistUserRepository.PersistUser(new PersistUserRequest { UserInfo = _Request.UserInfo });
            _Response.ValidationResults = response.ValidationResults;
            _Response.UserId = response.UserId;
            _Response.VerficationCode = response.VerificationCode;
        } 
    }
}
