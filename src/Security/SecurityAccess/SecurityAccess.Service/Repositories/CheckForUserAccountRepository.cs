using Project.Core;
using SecurityAccess.Contracts;
using SecurityAccess.Contracts.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAccess.Service.Repositories
{
    public interface ICheckForUserAccountRepository
    {
       CheckForUserAccountResponse CheckForUserAccount(CheckForUserAccountRequest CheckForUserAccountRequest);
    }

    [Export(typeof(ICheckForUserAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CheckForUserAccountRepository : ICheckForUserAccountRepository
    {
        #region Declarations
        private CheckForUserAccountRequest _Request;
        private CheckForUserAccountResponse _Response;
        private IdentityDevEnvEntities _IdentityDevEnvEntities;
        #endregion Declarations

        public CheckForUserAccountResponse CheckForUserAccount(CheckForUserAccountRequest CheckForUserAccountRequest)
        {
            _Request = CheckForUserAccountRequest;
            _Response = new CheckForUserAccountResponse { ValidationResults = new ValidationResults() };

            managecheckForUserAccount();
            return _Response;
        }

        private void managecheckForUserAccount()
        {           
            using (_IdentityDevEnvEntities = new IdentityDevEnvEntities())
            {
                checkForUserName(_Request.UserName);
                checkForPhone(_Request.Phone);
                checkForEmail(_Request.Email);
            }          
        }

        private void checkForUserName(string UserName)
        {
            if (!_Response.ValidationResults.IsValid) return;
            if (string.IsNullOrEmpty(UserName)) return;

            if (!_IdentityDevEnvEntities.UserAccounts.Any(x => x.UserName == UserName)) return;

            _Response.ValidationResults.Add(new ValidationResult { ValidationMessage = "UserName Already Exists" });
        }

        private void checkForPhone(string PhoneNumber)
        {
            if (!_Response.ValidationResults.IsValid) return;
            if (string.IsNullOrEmpty(PhoneNumber)) return;

            if (!_IdentityDevEnvEntities.UserAccounts.Any(x => x.Phone == PhoneNumber)) return;

            _Response.ValidationResults.Add(new ValidationResult { ValidationMessage = "Phone Number Already Exists" });
        }

        private void checkForEmail(string Email)
        {
            if (!_Response.ValidationResults.IsValid) return;
            if (string.IsNullOrEmpty(Email)) return;

            if (!_IdentityDevEnvEntities.UserAccounts.Any(x => x.Email == Email)) return;

            _Response.ValidationResults.Add(new ValidationResult { ValidationMessage = "EmailId Already Exists" });
        }
    }
}
