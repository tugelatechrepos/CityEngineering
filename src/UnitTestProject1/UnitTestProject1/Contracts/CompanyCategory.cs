using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityHostProject.Contracts
{
    public class CompanyCategory
    {
        public int CategoryId { get; set; }

        public ICollection<AreaInformation> AreaInformationList { get; set; }
    }
}
