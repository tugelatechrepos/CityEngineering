using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Project.Core;
using RestSharp;
using System;
using System.Net;

namespace APITestingProject
{
    [TestClass]
    public class GMapsTests
    {
        [TestMethod]
        public void GeoCode_Test()
        {
            //var webClient = new WebClient();
            //webClient.QueryString.Add("address", "Ekurhuleni");
            //webClient.QueryString.Add("key", "AIzaSyAe0qV4P9n-D5hZ1yk_Uw8Xw1e8wmOLd20");
            //var result = webClient.DownloadString("https://maps.googleapis.com/maps/api/geocode/json");


            var client = new RestClient("https://maps.googleapis.com/maps/api/");
            var request = new RestRequest("geocode/json", Method.GET);
            request.AddParameter("address",Uri.EscapeDataString("203,Ekurhuleni"));
            request.AddParameter("Key", "AIzaSyDerHKoWrsbTo-AkoM4cf4PZKcSz0I3ww8");
            var queryResult = client.Execute(request);

            dynamic geoCode = JsonConvert.DeserializeObject(queryResult.Content);
            var geometryJson = geoCode.results[0].geometry;
            var geometryString = JsonConvert.SerializeObject(geometryJson);
            var geometry = JsonConvert.DeserializeObject<Geometry>(geometryString);
        }

        [TestMethod]
        public void DistanceMatrix_Test()
        {
           
            //var client = new RestClient("https://maps.googleapis.com/maps/api/");
            //var request = new RestRequest("distancematrix/json", Method.GET);
            //request.AddParameter("origins", Uri.EscapeDataString(_Origin));
            //request.AddParameter("destinations", Uri.EscapeDataString(_Destination));
            //request.AddParameter("Key", "AIzaSyDvoP3_--EfBzqJCBtTFicqflHpx5qwCz0");
            //var queryResult = client.Execute(request);

            //dynamic distanceResult = JsonConvert.DeserializeObject(queryResult.Content);
            //var distancematrix = distanceResult.rows[0].elements[0];
            //var distanceMatrixString = JsonConvert.SerializeObject(distancematrix);

            //var distance = JsonConvert.DeserializeObject<DistanceMatrix>(distanceMatrixString);
        }
    }
}
