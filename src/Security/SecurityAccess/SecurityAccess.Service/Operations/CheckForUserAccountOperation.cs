using SecurityAccess.Contracts;
using SecurityAccess.Contracts.Contracts;
using SecurityAccess.Service.Repositories;
using System.ComponentModel.Composition;

namespace SecurityAccess.Service.Operations
{
    public interface ICheckForUserAccountOperation
    {
        CheckForUserAccountResponse CheckForUserAccount(CheckForUserAccountRequest CheckForUserAccountRequest);
    }

    [Export(typeof(ICheckForUserAccountOperation))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CheckForUserAccountOperation : ICheckForUserAccountOperation
    {
        #region Declarations
        private CheckForUserAccountRequest _Request;
        private CheckForUserAccountResponse _Response;

        public ICheckForUserAccountRepository CheckForUserAccountRepository { get; set; }        
        #endregion Declarations

        public CheckForUserAccountResponse CheckForUserAccount(CheckForUserAccountRequest CheckForUserAccountRequest)
        {
            _Request = CheckForUserAccountRequest;
            _Response = new CheckForUserAccountResponse { ValidationResults = new ValidationResults() };

            transferToDc();

            return _Response;
        }

        private void transferToDc()
        {
            if (!_Response.ValidationResults.IsValid) return;

            _Response = CheckForUserAccountRepository.CheckForUserAccount(_Request);
        }
    }
}
