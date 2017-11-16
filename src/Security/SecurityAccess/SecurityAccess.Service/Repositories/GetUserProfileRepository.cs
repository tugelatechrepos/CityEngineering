using Project.Core;
using SecurityAccess.Contracts.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAccess.Service.Repositories
{
    public interface IGetUserProfileRepository
    {

    }

    public class GetUserProfileRepository
    {
        #region Declarations
        private GetUserProfileRequest _Request;
        private GetUserProfileResponse _Response;
        private UserAccount _UserAccount;
        private User _User;
        private IdentityDevEnvEntities _IdentityDevEnvEntities;
        private MainDevEnvEntities _MainDevEnvEntities;
        #endregion Declarations

        public GetUserProfileResponse GetUserProfile(GetUserProfileRequest GetUserProfileRequest)
        {
            _Request = GetUserProfileRequest;
            _Response = new GetUserProfileResponse { ValidationResults = new ValidationResults() };
            return _Response;
        }

        private void assignUserAccountDetails()
        {
            if (!_Response.ValidationResults.IsValid) return;

            using (_IdentityDevEnvEntities = new IdentityDevEnvEntities())
            {
                _UserAccount = _IdentityDevEnvEntities.UserAccounts.FirstOrDefault(x => x.Id == _Request.UserId);
            }
        }

        private void assignUserDetails()
        {
            if (!_Response.ValidationResults.IsValid || _UserAccount == null) return;

            _Response.UserProfile = new Contracts.UserInfo
            {
                Id = _UserAccount.Id,
                UserName = _UserAccount.UserName,
                Password = _UserAccount.Password,
                PhoneNumber = _UserAccount.Phone,
                Email = _UserAccount.Email,
            };
        }

        private void assignUser()
        {
            if (!_Response.ValidationResults.IsValid || _Response.UserProfile == null) return;

            using(_MainDevEnvEntities = new MainDevEnvEntities())
            {
               var Users = _MainDevEnvEntities.Users.Where(x => x.UserId == _Request.UserId)
                    .Include(x => x.CompanyUserDetails)
                    .Include(x => x.UserAreaCodes)
                    .Include(x => x.UserGeometries)
                    .Include(x => x.UserCategories);

                if (Users == null || !Users.Any()) return;

                _User = Users.First();
            }
        }

        private void assignCompanyDetails()
        {
            
        }
    }
}
