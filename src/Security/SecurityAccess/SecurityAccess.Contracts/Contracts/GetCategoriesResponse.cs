using Project.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAccess.Contracts.Contracts
{
    public class GetCategoriesResponse
    {
        public ICollection<Category> CategoryList { get; set; }

        public ValidationResults ValidationResults { get; set; }
    }
}
