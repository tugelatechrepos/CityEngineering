using Newtonsoft.Json;
using Project.Core;
using RestSharp;
using SecurityAccess.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAccess.Service.Repositories
{
    public interface IGetDistanceMatrixRepository
    {
        GetDistanceMatrixRepositoryResponse GetDistanceMatrix(GetDistanceMatrixRepositoryRequest GetDistanceMatrixRepositoryRequest);
    }

    public class GetDistanceMatrixRepositoryRequest
    {
        public AreaInformation UserLocation { get; set; }

        public AreaInformation CompanyBranchLocation { get; set; }
    }

    public class GetDistanceMatrixRepositoryResponse
    {
        public DistanceMatrix DistanceMatrix { get; set; }

        public ValidationResults ValidationResults { get; set; }
    }

    [Export(typeof(IGetDistanceMatrixRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GetDistanceMatrixRepository : IGetDistanceMatrixRepository
    {
        #region Declarations
        private GetDistanceMatrixRepositoryRequest _Request;
        private GetDistanceMatrixRepositoryResponse _Response;
        private string _Origin;
        private string _Destination;
        #endregion Declarations

        public GetDistanceMatrixRepositoryResponse GetDistanceMatrix(GetDistanceMatrixRepositoryRequest GetDistanceMatrixRepositoryRequest)
        {
            _Request = GetDistanceMatrixRepositoryRequest;
            _Response = new GetDistanceMatrixRepositoryResponse { ValidationResults = new ValidationResults() };

            assignOrigin();
            assignDestination();
            assignDistanceMatrix();

            return _Response;
        }

        private void assignOrigin()
        {
            if (!_Response.ValidationResults.IsValid) return;

            _Origin = $"{_Request.UserLocation.FirstLineOfAddress} {_Request.UserLocation.AreaCode}";
        }

        private void assignDestination()
        {
            if (!_Response.ValidationResults.IsValid) return;

            _Destination = $"{_Request.CompanyBranchLocation.FirstLineOfAddress} {_Request.CompanyBranchLocation.AreaCode}";
        }

        private void assignDistanceMatrix()
        {
            if (!_Response.ValidationResults.IsValid) return;

            try
            {
                var client = new RestClient("https://maps.googleapis.com/maps/api/");
                var request = new RestRequest("distancematrix/json", Method.GET);
                request.AddParameter("origins", Uri.EscapeDataString(_Origin));
                request.AddParameter("destinations", Uri.EscapeDataString(_Destination));
                request.AddParameter("Key", "AIzaSyDvoP3_--EfBzqJCBtTFicqflHpx5qwCz0");
                var queryResult = client.Execute(request);
              
                dynamic distanceResult = JsonConvert.DeserializeObject(queryResult.Content);
                var distancematrix = distanceResult.rows[0].elements[0];
                var distanceMatrixString = JsonConvert.SerializeObject(distancematrix);
              
                _Response.DistanceMatrix = JsonConvert.DeserializeObject<DistanceMatrix>(distanceMatrixString);
            }
            catch (Exception ex)
            {
                _Response.ValidationResults.Add(new ValidationResult { ValidationMessage = ex.Message });
            }
        }
    }
}
