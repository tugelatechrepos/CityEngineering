using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityHostProject.Contracts
{
    public class Company
    {
        public int Id { get; set; }        

        public string CompanyName { get; set; }

        public string CompanyDescription { get; set; }

        public string CompanyPhoneNumber { get; set; }

        public string CompanyEmail { get; set; }

        public CompanyCategory CompanyCategory { get; set; }

    }
}
