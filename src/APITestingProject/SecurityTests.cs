using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using SecurityAccess.Contracts;
using System.IO;
using System.Collections.Generic;

namespace APITestingProject
{
    [TestClass]
    public class SecurityTests
    {
        [TestMethod]
        public void SignUpUser_SanityTests()
        {        

            var client = new HttpClient();
            
            var baseaddress = new Uri("http://localhost:49808/");
            client.BaseAddress = baseaddress;
            var param = getSignUpUserRequest();
            var content = JsonConvert.SerializeObject(param);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PostAsync("api/SignUp", byteContent);
            var signUpUserResponse = response.Result.Content.ReadAsStringAsync();
            var result = signUpUserResponse.Result;

            var signUpUserResponseDc = JsonConvert.DeserializeObject<SignUpUserResponse>(result);
        }

        private SignUpUserRequest getSignUpUserRequest()
        {
            var signUpUserRequest = new SignUpUserRequest
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
                    UserAreaCodeList = new List<SecurityAccess.Contracts.UserAreaCode> { new UserAreaCode { FirstLineOfAddress = "KempCity", AreaCode = 324234 } },
                    UserCategoryList = new List<UserCategory> {
                        new UserCategory { CategoryId = 1},
                        new UserCategory { CategoryId = 2},
                    }
                }
            };
            return signUpUserRequest;
        }

        private ICollection<Company> getCompanyList()
        {
            ICollection<Company> companyList = new List<Company>();

            var ABCCompany = new Company
            {
                CompanyName = "ABC Enterprise",
                CategoryId = 1,
                CompanyDescription = "ABC Clothing",
                CompanySubscribedCategories = new List<CompanySubscribedCategory>
                {
                    new CompanySubscribedCategory{ CategoryId = 1},
                },
                CompanyUserDetailList = new List<CompanyUserDetail>
                {

                    new CompanyUserDetail{ IsBranchWildCard = true}
                },
                CompanyBranchDetailList = new List<CompanyBranchDetail>
                {
                    new CompanyBranchDetail
                    {
                        PhoneNumber = "3423432434",
                        Description = "Branch1",
                        Email = "Branch1Email",
                        AreaInformation = new AreaInformation{AreaCode = "213213" ,  FirstLineOfAddress = "Kemp City"}
                    },
                     new CompanyBranchDetail
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
                CompanySubscribedCategories = new List<CompanySubscribedCategory>
                {
                    new CompanySubscribedCategory{ CategoryId = 2},
                },
                CompanyUserDetailList = new List<CompanyUserDetail>
                {
                    new CompanyUserDetail{ IsBranchWildCard = true}
                },
                CompanyBranchDetailList = new List<CompanyBranchDetail>
                {
                    new CompanyBranchDetail
                    {
                        PhoneNumber = "434554543",
                        Description = "Branch1",
                        Email = "Branch1Email",
                        AreaInformation = new AreaInformation{AreaCode = "213213" ,  FirstLineOfAddress = "Kemp City"}
                    },
                     new CompanyBranchDetail
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
