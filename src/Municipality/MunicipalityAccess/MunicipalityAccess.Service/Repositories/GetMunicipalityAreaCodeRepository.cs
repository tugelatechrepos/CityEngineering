using MunicipalityAccess.Contracts;
using Project.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityAccess.Service.Repositories
{
    public interface IGetMunicipalityAreaCodeRepository
    {
        GetMunicipalityAreaCodeResponse GetMunicipalityAreaCodes(GetMunicipalityAreaCodeRequest GetMunicipalityAreaCodeRequest);
    }

    [Export(typeof(IGetMunicipalityAreaCodeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GetMunicipalityAreaCodeRepository : IGetMunicipalityAreaCodeRepository
    {
        #region Declarations
        private GetMunicipalityAreaCodeRequest _Request;
        private GetMunicipalityAreaCodeResponse _Response;
        private ICollection<MunicipalityAreaCode> _MunicipalityAreaCodeList;
        private MunicipalityDevEnvEntities _MunicipalityDevEnvEntities;
        #endregion Declarations

        public GetMunicipalityAreaCodeResponse GetMunicipalityAreaCodes(GetMunicipalityAreaCodeRequest GetMunicipalityAreaCodeRequest)
        {
            _Request = GetMunicipalityAreaCodeRequest;
            _Response = new GetMunicipalityAreaCodeResponse { ValidationResults = new ValidationResults() };

            assignMunicipalityAreaCodeList();
            transferToDc();

            return _Response;
        }

        private void assignMunicipalityAreaCodeList()
        {
            if (!_Response.ValidationResults.IsValid) return;
            try
            {
                using (_MunicipalityDevEnvEntities = new MunicipalityDevEnvEntities())
                {
                    _MunicipalityAreaCodeList = _MunicipalityDevEnvEntities.MunicipalityAreaCodes
                        .Where(x => x.MunicipalityId == _Request.MunicipalityId).ToList();
                }
            }
            catch (Exception ex)
            {
                _Response.ValidationResults.Add(new ValidationResult { ValidationMessage = ex.Message });
            }
        }

        private void transferToDc()
        {
            if (!_Response.ValidationResults.IsValid) return;
            if (_MunicipalityAreaCodeList == null || !_MunicipalityAreaCodeList.Any()) return;

            _Response.MunicipalityAreaCodeList = _MunicipalityAreaCodeList.Select(x => new Contracts.MunicipalityAreaCode
            {
                Id = x.Id,
                AreaCode = x.AreaCode.Value,
                MunicipalityId = x.MunicipalityId,
                Place = x.Place
            }).ToList();
        }
    }
}
