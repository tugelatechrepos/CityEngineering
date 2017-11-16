using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityAccess.Contracts
{
    public class MunicipalityAreaCode
    {
        public int Id { get; set; }

        public int MunicipalityId { get; set; }

        public int AreaCode { get; set; }

        public string Place { get; set; }
    }
}
