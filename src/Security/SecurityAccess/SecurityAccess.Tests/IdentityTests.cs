using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityAccess.Service.Operations;
using SecurityAccess.Contracts;
using SecurityAccess.Service;
using Project.Core;
using System.Collections.Generic;
using Newtonsoft.Json;

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
                    UserAreaCodeList = new List<Contracts.UserAreaCode> { new Contracts.UserAreaCode { FirstLineOfAddress = "KempCity", AreaCode = 324234 } },
                    UserCategoryList = new List<Contracts.UserCategory> {
                        new Contracts.UserCategory { CategoryId = 1},
                        new Contracts.UserCategory { CategoryId = 2},
                    }
                }
            };

            var json = JsonConvert.SerializeObject(signUpUserRequest);
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
                    new Contracts.CompanySubscribedCategory{ CategoryId = 1},
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
                        AreaInformation = new AreaInformation{AreaCode = "213213" ,  FirstLineOfAddress = "Kemp City"}
                    },
                     new Contracts.CompanyBranchDetail
                    {
                        PhoneNumber = "35454543543",
                        Description = "Branch2",
                        Email = "Branch2Email",
                        AreaInformation = new AreaInformation{AreaCode = "324324" ,  FirstLineOfAddress = "Ohio City"}
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
                    new Contracts.CompanySubscribedCategory{ CategoryId = 2},
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
                        AreaInformation = new AreaInformation{AreaCode = "213213" ,  FirstLineOfAddress = "Kemp City"}
                    },
                     new Contracts.CompanyBranchDetail
                    {
                        PhoneNumber = "35454543543",
                        Description = "Branch2",
                        Email = "Branch2Email",
                        AreaInformation = new AreaInformation{AreaCode = "324324" ,  FirstLineOfAddress = "Ohio City"}
                    },
                }
            };

            companyList.Add(ABCCompany);
            companyList.Add(XYZCompany);
            return companyList;
        }
    }
}
