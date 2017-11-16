using Newtonsoft.Json;
using Project.Core;
using SecurityAccess.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityAccess.Service.Repositories
{
    public interface IPersistUserDetailsRepository
    {
        PersistUserDetailsResponse PersistUserDetails(PersistUserDetailsRequest PersistUserDetailsRequest);
    }

    public class PersistUserDetailsRequest
    {
        public UserInfo UserInfo { get; set; }

        public MainDevEnvEntities MainDevEnvEntities { get; set; }
    }

    public class PersistUserDetailsResponse
    {
        public ValidationResults ValidationResults { get; set; }
    }

    [Export(typeof(IPersistUserDetailsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersistUserDetailsRepository : IPersistUserDetailsRepository
    {
        #region Declarations
        private PersistUserDetailsRequest _Request;
        private PersistUserDetailsResponse _Response;

        [Import]
        public IGetUserGeometryRepository GetUserGeometryRepository { get; set; }
        #endregion Declarations

        public PersistUserDetailsResponse PersistUserDetails(PersistUserDetailsRequest PersistUserDetailsRequest)
        {
            _Request = PersistUserDetailsRequest;
            _Response = new PersistUserDetailsResponse { ValidationResults = new ValidationResults() };
            managePersist();
            return _Response;
        }

        private void managePersist()
        {
            if (!_Response.ValidationResults.IsValid) return;

            persistUser();
            persistCompanyList();
        }

        private void persistUser()
        {
            if (!_Response.ValidationResults.IsValid) return;
            try
            {
                var user = new User
                {
                    UserId = _Request.UserInfo.Id,
                    FirstName = _Request.UserInfo.FirstName,
                    LastName = _Request.UserInfo.LastName,
                    EmailAddress = _Request.UserInfo.Email,
                    Phone = _Request.UserInfo.PhoneNumber,
                    UserTypeId = (int)_Request.UserInfo.UserType,
                    CompanyUserTypeId = (int)_Request.UserInfo.CompanyUserType,
                    UserAreaCodes = _Request.UserInfo.UserAreaCodeList.Select(userAreaCode => new UserAreaCode
                    {
                        Id = userAreaCode.Id,
                        UserId = _Request.UserInfo.Id,
                        AreaAdress = userAreaCode.FirstLineOfAddress,
                        AreaCode = userAreaCode.AreaCode
                    }).ToList(),
                    UserGeometries = getGeometry(_Request.UserInfo.UserAreaCodeList.First().FirstLineOfAddress,
                    _Request.UserInfo.UserAreaCodeList.First().AreaCode),
                    UserCategories = _Request.UserInfo.UserCategoryList?.Select(userCategory => new UserCategory
                    {
                        Id = userCategory.Id,
                        UserId = _Request.UserInfo.Id,
                        CategoryId = userCategory.Category.Id,
                    }).ToList()
                };
                _Request.MainDevEnvEntities.Users.Add(user);               
            }
            catch (Exception ex)
            {
                var validationResult = new ValidationResult { ValidationMessage = ex.Message };
                _Response.ValidationResults.Add(validationResult);
            }
        }

        private void persistCompanyList()
        {
            if (!_Response.ValidationResults.IsValid) return;

            try
            {
                var CompanyList = _Request.UserInfo.CompanyList.Select(company => new CompanyProfile
                {
                    Id = company.Id,
                    Name = company.CompanyName,
                    Description = company.CompanyDescription,
                    CategoryTypeId = company.CategoryId,

                    CompanySubscribedCategories = company.CompanySubscribedCategories.Select(subscribedCategory => new CompanySubscribedCategory
                    {
                        Id = subscribedCategory.Id,
                        CompanyId = subscribedCategory.CompanyId,
                        CategoryId = subscribedCategory.Category.Id
                    }).ToList(),

                    CompanyBranchDetails = company.CompanyBranchDetailList.Select(companyBranchDetail => new CompanyBranchDetail
                    {
                        Id = companyBranchDetail.Id,
                        CompanyId = company.Id,
                        FirstLineOfAddress = companyBranchDetail.AreaInformation.FirstLineOfAddress,
                        AreaCode = companyBranchDetail.AreaInformation.AreaCode,
                        Description = companyBranchDetail.Description,
                        Email = companyBranchDetail.Email,
                        Phone = companyBranchDetail.PhoneNumber,
                        BranchGeometries = new List<BranchGeometry>
                        {
                          getBranchGeometry(companyBranchDetail.AreaInformation.FirstLineOfAddress, 
                          Convert.ToInt32(companyBranchDetail.AreaInformation.AreaCode))
                        }
                    }).ToList(),
                   
                   
                    CompanyUserDetails = company.CompanyUserDetailList.Select(CompanyUserDetail => new CompanyUserDetail
                    {
                        Id = CompanyUserDetail.Id,
                        UserId = _Request.UserInfo.Id,
                        IsBranchWildCard = CompanyUserDetail.IsBranchWildCard
                    }).ToList()
                });

                _Request.MainDevEnvEntities.CompanyProfiles.AddRange(CompanyList);
                _Request.MainDevEnvEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                var validationResult = new ValidationResult { ValidationMessage = ex.Message };
                _Response.ValidationResults.Add(validationResult);
            }
        }

        private ICollection<UserGeometry> getGeometry(string firstLineOfAddress, int areaCode)
        {
           ICollection<UserGeometry> userGeometryList = null;
           var response = GetUserGeometryRepository.GetUserGeometry(new GetUserGeometryRequest
            {
                FirstLineOfAddress = firstLineOfAddress,
                AreaCode = areaCode
            });

            if (response.Geometry == null) return userGeometryList;

            var UserGeometry = new UserGeometry
            {
                UserId = _Request.UserInfo.Id,
                Geometry = JsonConvert.SerializeObject(response.Geometry)
            };
            userGeometryList = new List<UserGeometry>();
            userGeometryList.Add(UserGeometry);
            return userGeometryList;
        }

        private BranchGeometry getBranchGeometry(string firstLineOfAddress, int areaCode)
        {
            BranchGeometry branchGeometry = null;
            var response = GetUserGeometryRepository.GetUserGeometry(new GetUserGeometryRequest
            {
                FirstLineOfAddress = firstLineOfAddress,
                AreaCode = areaCode
            });
            
            if (response.Geometry == null)
            {
                return branchGeometry;
            }

            branchGeometry = new BranchGeometry
            {
                Geometry = JsonConvert.SerializeObject(response.Geometry)
            };

            return branchGeometry;
        }
    }
}
