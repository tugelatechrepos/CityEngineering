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
    public interface IGetUserForAuthenticateRepository
    {
        GetUserForAuthenticateResponse GetUserForAuthenticate(GetUserForAuthenticateRequest getUserForAuthenticateRequest);
    }

    [Export(typeof(IGetUserForAuthenticateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GetUserForAuthenticateRepository : IGetUserForAuthenticateRepository
    {
        #region Declarations
        private GetUserForAuthenticateRequest _Request;
        private GetUserForAuthenticateResponse _Response;
        private IdentityDevEnvEntities _IdentityDevEnvEntities;
        private UserAccount _UserAccount;
        private UserAccountDetail _UserAccountDetail;

        #endregion Declarations
        public GetUserForAuthenticateResponse GetUserForAuthenticate(GetUserForAuthenticateRequest getUserForAuthenticateRequest)
        {
            _Request = getUserForAuthenticateRequest;
            _Response = new GetUserForAuthenticateResponse { ValidationResults = new ValidationResults() };

            assignUserForAuthenticate();
            assignUserAccountDetail();
            transferToDc();

            return _Response;
        }

        private void assignUserForAuthenticate()
        {        
            using (_IdentityDevEnvEntities = new IdentityDevEnvEntities())
            {
                validateUserByEmail(_Request.Email, _Request.Password);
                validateUserByPhone(_Request.PhoneNumber, _Request.Password);
                validateUserByUserName(_Request.Username, _Request.Password);
            }         
        }

        private void validateUserByEmail(string Email, string requestPassword)
        {
            if (!_Response.ValidationResults.IsValid) return;
            if (string.IsNullOrEmpty(Email)) return;

            _UserAccount = _IdentityDevEnvEntities.UserAccounts.FirstOrDefault(x => x.Email == Email);
            if (_UserAccount == null) return;

             ValidatePassword(requestPassword, _UserAccount.Password);
        }

        private void validateUserByPhone(string Phone, string requestPassword)
        {
            if (!_Response.ValidationResults.IsValid) return;
            if (string.IsNullOrEmpty(Phone)) return;

            _UserAccount = _IdentityDevEnvEntities.UserAccounts.FirstOrDefault(x => x.Phone == Phone);
            if (_UserAccount == null) return;

            ValidatePassword(requestPassword, _UserAccount.Password);
        }

        private void validateUserByUserName(string UserName, string requestPassword)
        {
            if (!_Response.ValidationResults.IsValid) return;
            if (string.IsNullOrEmpty(UserName)) return;

            _UserAccount = _IdentityDevEnvEntities.UserAccounts.FirstOrDefault(x => x.UserName == UserName);
            if (_UserAccount == null) return;

            ValidatePassword(requestPassword, _UserAccount.Password);
        }

        private void ValidatePassword(string requestPassword, string userAccountPassword)
        {
            if (!Equals(requestPassword, DecodeFrom64(userAccountPassword)))
                _Response.ValidationResults.Add(new ValidationResult { ValidationMessage = "Invalid Logon Credentials" });
        }

        private void assignUserAccountDetail()
        {
            if (!_Response.ValidationResults.IsValid) return;
            if (_UserAccount == null) return;

            _UserAccountDetail = new UserAccountDetail
            {
                Id = _UserAccount.Id,
                UserName = _UserAccount.UserName,
                Email = _UserAccount.Email,
                Password = _UserAccount.Password,
                Phone = _UserAccount.Phone,
                IsUserAccount = _UserAccount.IsUserAccount,
                DateCreated = _UserAccount.DateCreated.HasValue ? _UserAccount.DateCreated.Value : DateTime.MinValue
            };
        }

        private void transferToDc()
        {
            if (!_Response.ValidationResults.IsValid) return;
            _Response.UserAccountDetail = _UserAccountDetail;
        }

        private string DecodeFrom64(string encodedData)
        {
            var encoder = new UTF8Encoding();
            var utf8Decode = encoder.GetDecoder();
            var todecode_byte = Convert.FromBase64String(encodedData);
            var charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            var decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            var result = new String(decoded_char);
            return result;
        }      

    }
}
