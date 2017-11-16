using Newtonsoft.Json;
using Project.Core;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAccess.Service.Repositories
{
    public interface IGetUserGeometryRepository
    {
        GetUserGeometryResponse GetUserGeometry(GetUserGeometryRequest GetUserGeometryRequest);
    }

    public class GetUserGeometryRequest
    {
        public string FirstLineOfAddress { get; set; }

        public string Place { get; set; }

        public int AreaCode { get; set; }
    }

    public class GetUserGeometryResponse
    {
       public Geometry Geometry { get; set; }

       public IRestResponse RestResponse { get; set; }

       public ValidationResults ValidationResults { get; set; }
    }

    [Export(typeof(IGetUserGeometryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GetUserGeometryRepository : IGetUserGeometryRepository
    {
        #region Declarations
        private GetUserGeometryRequest _Request;
        private GetUserGeometryResponse _Response;
        private string _Address;
        #endregion Declarations

        public GetUserGeometryResponse GetUserGeometry(GetUserGeometryRequest GetUserGeometryRequest)
        {
            _Request = GetUserGeometryRequest;
            _Response = new GetUserGeometryResponse { ValidationResults = new ValidationResults() };

            assignAddress();
            assignGeometry();
            return _Response;
        }

        private void assignAddress()
        {
            if (!_Response.ValidationResults.IsValid) return;

            _Address = $"{_Request.FirstLineOfAddress} {_Request.AreaCode}";
        }

        private void assignGeometry()
        {
            if (!_Response.ValidationResults.IsValid) return;

            try
            {
                var client = new RestClient("https://maps.googleapis.com/maps/api/");
                var request = new RestRequest("geocode/json", Method.GET);
                request.AddParameter("address", Uri.EscapeDataString(_Address));
                request.AddParameter("Key", "AIzaSyDerHKoWrsbTo-AkoM4cf4PZKcSz0I3ww8");
                var queryResult = client.Execute(request);
                _Response.RestResponse = queryResult;
                dynamic geoCode = JsonConvert.DeserializeObject(queryResult.Content);
                var geometryJson = geoCode.results[0].geometry;
                var geometryString = JsonConvert.SerializeObject(geometryJson);
                _Response.Geometry = JsonConvert.DeserializeObject<Geometry>(geometryString);
            }
            catch(Exception ex)
            {
                _Response.ValidationResults.Add(new ValidationResult { ValidationMessage = ex.Message });
            }           
        }
    }
}
