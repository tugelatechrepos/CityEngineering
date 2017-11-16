using MunicipalityAccess.Contracts;
using MunicipalityAccess.Service.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityAccess.Client
{
    public interface IMunicipalityAccess
    {
        GetMunicipalityAreaCodeResponse GetMunicipalityAreaCodes(GetMunicipalityAreaCodeRequest GetMunicipalityAreaCodeRequest);
    }

    [Export(typeof(IMunicipalityAccess))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MunicipalityAccess : IMunicipalityAccess
    {
        #region Declarations
        [Import]
        public IGetMunicipalityAreaCodesOperation GetMunicipalityAreaCodesOperation { get; set; }
        #endregion Declarations

        public GetMunicipalityAreaCodeResponse GetMunicipalityAreaCodes(GetMunicipalityAreaCodeRequest GetMunicipalityAreaCodeRequest)
        {
            return GetMunicipalityAreaCodesOperation.GetMunicipalityAreaCodes(GetMunicipalityAreaCodeRequest);
        }
    }
}
