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

    public interface IPersistUserAccountRepository
    {
        PersistUserAccountRepositoryResponse PersistUserAccount(PersistUserAccountRepositoryRequest PersistUserAccountRepositoryRequest);
    }

    public class PersistUserAccountRepositoryRequest
    {
        public UserAccountDetail UserAccountDetail { get; set; }

        public IdentityDevEnvEntities IdentityDevEnvEntities { get; set; }
    }

    public class PersistUserAccountRepositoryResponse
    {
        public int UserId { get; set; }

        public string VerificationCode { get; set; }

        public ValidationResults ValidationResults { get; set; }
    }

    [Export(typeof(IPersistUserAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersistUserAccountRepository : IPersistUserAccountRepository
    {
        #region Declarations
        private PersistUserAccountRepositoryRequest _Request;
        private PersistUserAccountRepositoryResponse _Response;
        private UserAccount _UserAccount;
        private UserAccountVerificationDetail _UserAccountVerificationDetail;
        #endregion Declarations

        public PersistUserAccountRepositoryResponse PersistUserAccount(PersistUserAccountRepositoryRequest PersistUserAccountRepositoryRequest)
        {
            _Request = PersistUserAccountRepositoryRequest;
            _Response = new PersistUserAccountRepositoryResponse { ValidationResults = new ValidationResults() };
            persistUserAccountDetails();
            return _Response;
        }

        private void persistUserAccountDetails()
        {
            if (!_Response.ValidationResults.IsValid) return;

            persistUserAccount(_Request.UserAccountDetail);
            assignUserAccountVerificationDetail();
            persistUserAccountVerificationDetail(_UserAccountVerificationDetail);
            transferToDc();
        }
      
        private void persistUserAccount(UserAccountDetail userAccountDetail)
        {
            if (!_Response.ValidationResults.IsValid) return;
            try
            {
                _UserAccount = new UserAccount
                {
                    UserName = userAccountDetail.UserName,
                    Password = userAccountDetail.Password,
                    Email = userAccountDetail.Email,
                    Phone = userAccountDetail.Phone,
                    IsUserAccount = true,
                    IsSystemAccount = false,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                };
                _Request.IdentityDevEnvEntities.UserAccounts.Add(_UserAccount);
                _Request.IdentityDevEnvEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                var validationResult = new ValidationResult { ValidationMessage = ex.Message };
                _Response.ValidationResults.Add(validationResult);
            }
        }

        private void assignUserAccountVerificationDetail()
        {
            if (!_Response.ValidationResults.IsValid) return;
            if (_UserAccount == null) return;

            _UserAccountVerificationDetail = new UserAccountVerificationDetail
            {
                UserId = _UserAccount.Id,
                VerificationText = getVerificationCode(),
                VerificationTextDateTime = DateTime.Now,
                VerificationTextExpiryInSeconds = 30,
            };
        }

        private string getVerificationCode()
        {
            var randomPin = new Random();
            var randomPINResult = randomPin.Next(0, 9999).ToString();
            return randomPINResult.PadLeft(4, '0');
        }

        private void persistUserAccountVerificationDetail(UserAccountVerificationDetail userAccountVerificationDetail)
        {
            if (!_Response.ValidationResults.IsValid) return;

            try
            {
                var userAccountVerification = new UserAccountActivation
                {
                    UserId = userAccountVerificationDetail.UserId,
                    VerificationText = userAccountVerificationDetail.VerificationText,
                    VerificationTextDateTime = DateTime.Now,
                    VerificationTextExpiryInSeconds = userAccountVerificationDetail.VerificationTextExpiryInSeconds,
                    DateCreated = DateTime.Now
                };
                _Request.IdentityDevEnvEntities.UserAccountActivations.Add(userAccountVerification);
                _Request.IdentityDevEnvEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                var validationResult = new ValidationResult { ValidationMessage = ex.Message };
                _Response.ValidationResults.Add(validationResult);
            }
        }

        private void transferToDc()
        {
            if (!_Response.ValidationResults.IsValid) return;
            if (_UserAccountVerificationDetail == null) return;

            _Response.UserId = _UserAccountVerificationDetail.UserId;
            _Response.VerificationCode = _UserAccountVerificationDetail.VerificationText;
        }
    }
}
