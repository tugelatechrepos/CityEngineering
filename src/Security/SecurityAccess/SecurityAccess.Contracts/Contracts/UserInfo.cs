using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityAccess.Contracts
{
    public class UserInfo
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public ICollection<Company> CompanyList { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
     
        public UserTypeEnum UserType { get; set; }

        public CompanyUserTypeEnum CompanyUserType { get; set; }

        public ICollection<UserAreaCode> UserAreaCodeList { get; set; } 
        
        public ICollection<UserCategory> UserCategoryList { get; set; }
    }

    public class UserAreaCode
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int AreaCode { get; set; }

        public string FirstLineOfAddress { get; set; }
    }

    public class UserCategory
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public Category Category { get; set; }
    }
}
