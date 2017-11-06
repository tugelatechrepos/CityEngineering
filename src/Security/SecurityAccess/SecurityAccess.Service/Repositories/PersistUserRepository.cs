using SecurityAccess.Contracts;
using SecurityAccess.Contracts.Contracts;
using System;
using System.ComponentModel.Composition;
using System.Data.Entity;

namespace SecurityAccess.Service.Repositories
{
    public interface IPersistUserRepository
    {
        PersistUserResponse PersistUser(PersistUserRequest PersistUserRequest);
    }

    public class PersistUserRequest
    {
        public UserInfo UserInfo { get; set; }
    }

    public class PersistUserResponse
    {
        public int UserId { get; set; }

        public string VerificationCode { get; set; }

        public ValidationResults ValidationResults { get; set; }
    }

    [Export(typeof(IPersistUserRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersistUserRepository : IPersistUserRepository
    {
        #region Declarations
        private PersistUserRequest _Request;
        private PersistUserResponse _Response;
        private IdentityDevEnvEntities _IdentityDevEntities;
        private MainDevEnvEntities _MainDevEntities;
        private UserAccountDetail _UserAccountDetail;

        [Import]
        public IPersistUserAccountRepository PersistUserAccountRepository { get; set; }

        [Import]
        public IPersistUserDetailsRepository PersistUserDetailsRepository { get; set; }      

        #endregion Declarations

        public PersistUserResponse PersistUser(PersistUserRequest PersistUserRequest)
        {
            _Request = PersistUserRequest;
            _Response = new PersistUserResponse { ValidationResults = new ValidationResults() };

            managePersistUserTransaction();
            return _Response;
        }

        private void managePersistUserTransaction()
        {
            if (!_Response.ValidationResults.IsValid) return;
            DbContextTransaction IdentityTransaction = null, MainTransaction = null;
            try
            {
                _IdentityDevEntities = new IdentityDevEnvEntities();
                IdentityTransaction = _IdentityDevEntities.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                assignUserAccountDetail();
                managePersistUserAccount();
                assignUserId();

                if (_Response.ValidationResults.IsValid)
                {
                    _MainDevEntities = new MainDevEnvEntities();
                    MainTransaction = _MainDevEntities.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                    persistUserDetails();
                }

                if (_Response.ValidationResults.IsValid)
                {
                    IdentityTransaction.Commit();
                    MainTransaction.Commit();
                }
                else
                {
                    IdentityTransaction.Rollback();
                    MainTransaction.Rollback();
                }

                _IdentityDevEntities.Database.Connection.Close();
                _MainDevEntities.Database.Connection.Close();
            }
            catch (Exception ex)
            {
                IdentityTransaction.Rollback();
                MainTransaction.Rollback();
                _IdentityDevEntities.Database.Connection.Close();
                _MainDevEntities.Database.Connection.Close();
            }
        }

        private void assignUserAccountDetail()
        {
            if (!_Response.ValidationResults.IsValid) return;

            _UserAccountDetail = new UserAccountDetail
            {
                UserName = _Request.UserInfo.UserName,
                Id = _Request.UserInfo.Id,
                Password = _Request.UserInfo.Password,
                IsUserAccount = true,
                Email = _Request.UserInfo.Email,
                Phone = _Request.UserInfo.PhoneNumber,
                DateCreated = DateTime.Now,
            };
        }

        private void managePersistUserAccount()
        {
            if (!_Response.ValidationResults.IsValid) return;

            var persistUserAccountResponse = PersistUserAccountRepository.PersistUserAccount(new PersistUserAccountRepositoryRequest
            {
                UserAccountDetail = _UserAccountDetail,
                IdentityDevEnvEntities = _IdentityDevEntities
            });

            _Response.ValidationResults = persistUserAccountResponse.ValidationResults;
            _Response.UserId = persistUserAccountResponse.UserId;
            _Response.VerificationCode = persistUserAccountResponse.VerificationCode;
        }

        private void assignUserId()
        {
            if (!_Response.ValidationResults.IsValid) return;          
            _Request.UserInfo.Id = _Response.UserId;
        }

        private void persistUserDetails()
        {
            if (!_Response.ValidationResults.IsValid) return;

            var persistUserDetailsResponse = PersistUserDetailsRepository.PersistUserDetails(new PersistUserDetailsRequest
            {
                UserInfo = _Request.UserInfo,
                MainDevEnvEntities = _MainDevEntities
            });
            _Response.ValidationResults = persistUserDetailsResponse.ValidationResults;
        }
    }
}
