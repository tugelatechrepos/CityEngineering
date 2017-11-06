using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityAccess.Contracts
{
    public class Company
    {
        public int Id { get; set; }        

        public string CompanyName { get; set; }

        public string CompanyDescription { get; set; }

        public int CategoryId { get; set; }

        public ICollection<CompanySubscribedCategory> CompanySubscribedCategories { get; set; }

        public ICollection<CompanyBranchDetail> CompanyBranchDetailList { get; set; }

        public ICollection<CompanyUserDetail> CompanyUserDetailList { get; set; }
    }

    public class CompanyUserDetail
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CompanyId { get; set; }

        public bool IsBranchWildCard { get; set; }

        public int CompanyBranchDetailId { get; set; }
    }

    public class CompanyBranchDetail
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; }    

        public string Email { get; set; }

        public string Description { get; set; }

        public AreaInformation AreaInformation { get; set; }
    }

    public class CompanySubscribedCategory
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int CategoryId { get; set; }
    }
}
