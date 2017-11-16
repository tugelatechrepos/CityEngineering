using Project.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAccess.Contracts.Contracts
{
    public class SearchServiceResponse
    {
        public ICollection<ServiceDetail> ServiceDetailList { get; set; }

        public ValidationResults ValidationResults { get; set; }
    }

    public class ServiceDetail
    {
        public CompanyBranchDetail CompanyBranchDetail { get; set; }

        public Geometry CompanyLocation { get; set; }

        public string RelativeDistance { get; set; }
    }

     
}
