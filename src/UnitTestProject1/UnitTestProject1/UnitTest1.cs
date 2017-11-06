using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SecurityHostProject.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {       
        [TestMethod]
        public void SignUpUserTest_SanityCheckAsync()
        {
            var client = new HttpClient();
            var baseaddress = new Uri("http://localhost:49808/");
            client.BaseAddress = baseaddress;
            var param = getSignUpUserRequest();
            var content = JsonConvert.SerializeObject(param);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response =  client.PostAsync("api/SignUp", byteContent);
            var signUpUserResponse =  response.Result.Content.ReadAsStringAsync();
            var result = signUpUserResponse.Result;

            var signUpUserResponseDc = JsonConvert.DeserializeObject<SignUpUserResponse>(result);
        }

        private SignUpUserRequest getSignUpUserRequest()
        {
            var signUpUserRequest = new SignUpUserRequest
            {
                UserInfo = new UserInfo
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "John.Doe@gmail.com",
                    Password = "MyPass",
                    PhoneNumber = "43435435",
                    UserName = "JDoe",
                    UserType = UserTypeEnum.Individual,
                    AreaInformation = new AreaInformation
                    {
                        AreaCode = "45895",
                        FirstLineOfAddress = "212,ParkStreet"
                    }
                }
            };
            return signUpUserRequest;
        }
    }
}
