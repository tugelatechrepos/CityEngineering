using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityHostProject.Contracts
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

        public AreaInformation AreaInformation { get; set; }

        public UserTypeEnum UserType { get; set; }

        public ICollection<int> CompanyChosenCategoryList { get; set; }
    }
}
