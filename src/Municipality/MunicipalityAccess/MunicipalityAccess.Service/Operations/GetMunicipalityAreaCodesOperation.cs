using MunicipalityAccess.Contracts;
using MunicipalityAccess.Service.Repositories;
using Project.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityAccess.Service.Operations
{

    public interface IGetMunicipalityAreaCodesOperation
    {
        GetMunicipalityAreaCodeResponse GetMunicipalityAreaCodes(GetMunicipalityAreaCodeRequest GetMunicipalityAreaCodeRequest);
    }

    [Export(typeof(IGetMunicipalityAreaCodesOperation))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GetMunicipalityAreaCodesOperation : IGetMunicipalityAreaCodesOperation
    {
        #region Declarations
        private GetMunicipalityAreaCodeRequest _Request;
        private GetMunicipalityAreaCodeResponse _Response;

        [Import]
        public IGetMunicipalityAreaCodeRepository GetMunicipalityAreaCodeRepository { get; set; }
        #endregion Declarations

        public GetMunicipalityAreaCodeResponse GetMunicipalityAreaCodes(GetMunicipalityAreaCodeRequest GetMunicipalityAreaCodeRequest)
        {
            _Request = GetMunicipalityAreaCodeRequest;
            _Response = new GetMunicipalityAreaCodeResponse { ValidationResults = new ValidationResults() };

            transferToDc();
            return _Response;
        }

        private void transferToDc()
        {
            if (!_Response.ValidationResults.IsValid) return;

            _Response = GetMunicipalityAreaCodeRepository.GetMunicipalityAreaCodes(_Request);
        }
    }
}
