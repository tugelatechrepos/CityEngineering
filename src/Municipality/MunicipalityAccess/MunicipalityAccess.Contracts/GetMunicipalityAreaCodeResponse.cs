using Project.Core;
using System.Collections;
using System.Collections.Generic;

namespace MunicipalityAccess.Contracts
{
    public class GetMunicipalityAreaCodeResponse
    {
        public ICollection<MunicipalityAreaCode> MunicipalityAreaCodeList { get; set; }

        public ValidationResults ValidationResults { get; set; }
    }
}
