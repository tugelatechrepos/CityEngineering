using Newtonsoft.Json;
using Project.Core;
using SecurityAccess.Contracts;
using SecurityAccess.Contracts.Contracts;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;

namespace SecurityAccess.Service.Repositories
{
    public interface ISearchServiceRepository
    {
        SearchServiceResponse SearchService(SearchServiceRequest SearchServiceRequest);
    }

    [Export(typeof(ISearchServiceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SearchServiceRepository : ISearchServiceRepository
    {
        #region Declarations
        private SearchServiceRequest _Request;
        private SearchServiceResponse _Response;
        private MainDevEnvEntities _MainDevEnvEntities;
        private ICollection<Contracts.CompanyBranchDetail> CompanyBranchDetailList { get; set; }
        private AreaInformation _UserAreaInformation;

        [Import]
        public IGetDistanceMatrixRepository GetDistanceMatrixRepository { get; set; }
        #endregion Declarations

        public SearchServiceResponse SearchService(SearchServiceRequest SearchServiceRequest)
        {
            _Request = SearchServiceRequest;
            _Response = new SearchServiceResponse { ValidationResults = new ValidationResults() };

            assignBranchAndUserDetails();
            assignResponse();

            return _Response;
        }

        private void assignBranchAndUserDetails()
        {
            if (!_Response.ValidationResults.IsValid) return;

            using (_MainDevEnvEntities = new MainDevEnvEntities())
            {
                assignBranchDetails();
                assignUserAreaInformation();
            }
        }

        private void assignBranchDetails()
        {
            if (!_Response.ValidationResults.IsValid) return;

            var companyProfileList = _MainDevEnvEntities.CompanyProfiles
                      .Where(x => x.CategoryTypeId == _Request.CategoryId)
                      .Include(x => x.CompanyBranchDetails);

            foreach (var companyProfile in companyProfileList)
            {
                CompanyBranchDetailList = companyProfile.CompanyBranchDetails.Select(x => new Contracts.CompanyBranchDetail
                {
                    Id = x.Id,
                    Email = x.Email,
                    PhoneNumber = x.Phone,
                    CompanyName = companyProfile.Name,
                    Description = x.Description,
                    AreaInformation = new Contracts.AreaInformation { AreaCode = x.AreaCode, FirstLineOfAddress = x.FirstLineOfAddress },
                    BranchGeometry = x.BranchGeometries?.FirstOrDefault()?.Geometry
                }).ToList();
            }
        }        

        private void assignUserAreaInformation()
        {
            if (!_Response.ValidationResults.IsValid) return;

            var users = _MainDevEnvEntities.Users.Where(x => x.UserId == _Request.UserId);
            if (users == null || !users.Any()) return;
            var UserAreaCodes = users?.First().UserAreaCodes;
            if (UserAreaCodes == null || !UserAreaCodes.Any()) return;
            _UserAreaInformation = new AreaInformation { AreaCode = UserAreaCodes.First().AreaCode, FirstLineOfAddress = UserAreaCodes.First().AreaAdress };
        }

        private void assignResponse()
        {
            if (!_Response.ValidationResults.IsValid) return;

            _Response.ServiceDetailList = CompanyBranchDetailList.Select(x => new ServiceDetail
            {
                CompanyBranchDetail = x,
                CompanyLocation = getBranchGeometry(x.BranchGeometry),
                RelativeDistance = getRelativeDistance(x.AreaInformation)
            }).ToList();
        }

        private Geometry getBranchGeometry(string geometryJson)
        {
           Geometry branchGeometry = null;
           if (string.IsNullOrEmpty(geometryJson)) return branchGeometry;
           branchGeometry =  JsonConvert.DeserializeObject<Geometry>(geometryJson);
           return branchGeometry;
        }

        private string getRelativeDistance(AreaInformation branchAreaInformation)
        {
            string relativeDistance = string.Empty;
            var response = GetDistanceMatrixRepository.GetDistanceMatrix(new GetDistanceMatrixRepositoryRequest
            {
                UserLocation = _UserAreaInformation,
                CompanyBranchLocation = branchAreaInformation
            });
            if (!response.ValidationResults.IsValid || response.DistanceMatrix == null) return relativeDistance;
            relativeDistance = response.DistanceMatrix.Distance?.Text;
            return relativeDistance;
        }
    }
}
