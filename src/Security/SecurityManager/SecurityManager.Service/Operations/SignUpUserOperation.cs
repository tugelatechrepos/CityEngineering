using SecurityAccess.Client;
using SecurityAccess.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager.Service.Operations
{

    public interface ISignUpUserOperation
    {
        SignUpUserResponse SignUpUser(SignUpUserRequest signUpUserRequest);
    }

    [Export(typeof(ISignUpUserOperation))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SignUpUserOperation : ISignUpUserOperation
    {
        #region Declarations
        private SignUpUserRequest _Request;
        private SignUpUserResponse _Response;     

        [Import]
        public ISecurityAccess SecurityAccess { get; set; }
        #endregion Declarations

        public SignUpUserResponse SignUpUser(SignUpUserRequest signUpUserRequest)
        {
            _Request = signUpUserRequest;
            _Response = new SignUpUserResponse();

            assignEncryptedPassword();
            persistUserForSignUp();

            return _Response;
        }

        private void persistUserForSignUp()
        {
            _Response =  SecurityAccess.PersistSignUpUser(_Request);
        }

        private void assignEncryptedPassword()
        {
            var userPassword =  _Request.UserInfo.Password;
            var encData_byte = new byte[userPassword.Length];
            var encryptedPassword = Convert.ToBase64String(encData_byte);
            _Request.UserInfo.Password = encryptedPassword;
        } 
    }
}
