using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityAccess.Service.Operations;
using SecurityAccess.Contracts;
using Project.Core;
using System.Collections.Generic;
using SecurityAccess.Service.Repositories;

namespace SecurityAccess.Tests
{
    [TestClass]
    public class IdentityTests
    {
        [TestMethod]
        public void SaveUserAccount_Test()
        {
            var persistSignUpUserOperation = IOCManager.Resolve<IPersistSignUpUserOperation>();

            var signUpUserRequest  = new SignUpUserRequest
            {
                UserInfo = new UserInfo
                {
                    UserName = "JDoe",
                    Password = "Password",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "John.Doe@domain.com",
                    UserType = UserTypeEnum.Company,
                    CompanyUserType = CompanyUserTypeEnum.Admin,
                    CompanyList = getCompanyList(),
                    UserAreaCodeList = new List<Contracts.UserAreaCode> { new Contracts.UserAreaCode { FirstLineOfAddress = "Alberton", AreaCode = 77301 } },
                    UserCategoryList = new List<Contracts.UserCategory> {
                        new Contracts.UserCategory { Category = new Category { Id = 1}},
                        new Contracts.UserCategory { Category = new Category { Id = 2}},
                    }
                }
            };

            //var json = JsonConvert.SerializeObject(signUpUserRequest);
            var response = persistSignUpUserOperation.PersistSignUpUser(signUpUserRequest);
        }

        private ICollection<Contracts.Company> getCompanyList()
        {
            ICollection<Contracts.Company> companyList = new List<Contracts.Company>();

            var ABCCompany = new Company
            {
                CompanyName = "ABC Enterprise",
                CategoryId = 1,
                CompanyDescription = "ABC Clothing",
                CompanySubscribedCategories = new List<Contracts.CompanySubscribedCategory>
                {
                    new Contracts.CompanySubscribedCategory{ Category = new Category { Id = 1} },
                },
                CompanyUserDetailList = new List<Contracts.CompanyUserDetail>
                {

                    new Contracts.CompanyUserDetail{ IsBranchWildCard = true}
                },
                CompanyBranchDetailList = new List<Contracts.CompanyBranchDetail>
                {
                    new Contracts.CompanyBranchDetail
                    {
                        PhoneNumber = "3423432434",
                        Description = "Branch1",
                        Email = "Branch1Email",
                        AreaInformation = new AreaInformation{AreaCode = 77304 ,  FirstLineOfAddress = "Benoni"}
                    },
                     new Contracts.CompanyBranchDetail
                    {
                        PhoneNumber = "35454543543",
                        Description = "Branch2",
                        Email = "Branch2Email",
                        AreaInformation = new AreaInformation{AreaCode = 77304 ,  FirstLineOfAddress = "Benoni"}
                    },
                }
            };

            var XYZCompany = new Company
            {
                CompanyName = "XYZ Enterprise",
                CategoryId = 2,
                CompanyDescription = "XYZ Food",
                CompanySubscribedCategories = new List<Contracts.CompanySubscribedCategory>
                {
                    new Contracts.CompanySubscribedCategory{Category = new Category { Id = 2}},
                },
                CompanyUserDetailList = new List<Contracts.CompanyUserDetail>
                {
                    new Contracts.CompanyUserDetail{ IsBranchWildCard = true}
                },
                CompanyBranchDetailList = new List<Contracts.CompanyBranchDetail>
                {
                    new Contracts.CompanyBranchDetail
                    {
                        PhoneNumber = "434554543",
                        Description = "Branch1",
                        Email = "Branch1Email",
                        AreaInformation = new AreaInformation{AreaCode = 77304 ,  FirstLineOfAddress = "Benoni"}
                    },
                     new Contracts.CompanyBranchDetail
                    {
                        PhoneNumber = "35454543543",
                        Description = "Branch2",
                        Email = "Branch2Email",
                        AreaInformation = new AreaInformation{AreaCode = 77304 ,  FirstLineOfAddress = "Benoni"}
                    },
                }
            };

            companyList.Add(ABCCompany);
            companyList.Add(XYZCompany);
            return companyList;
        }

        [TestMethod]
        public void GetSearchService_Test()
        {
            var repository = IOCManager.Resolve<ISearchServiceRepository>();
            var response = repository.SearchService(new Contracts.Contracts.SearchServiceRequest
            {
                UserId = 1061,
                CategoryId = 1,
            });
        }

        [TestMethod]
        public void GetCategories_Test()
        {
            var respository = IOCManager.Resolve<IGetCategoriesRepository>();
            var response = respository.GetCategories();
        }
    }
}
