using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core
{
    public class Geometry
    {
        public Location Location { get; set; }

        [JsonProperty("location_type")]
        public string LocationType { get; set; }

        public Bounds Bounds { get; set; }

        public ViewPort ViewPort { get; set; }
    }

    public class Location
    {
        [JsonProperty("lat")]
        public decimal Latitude { get; set; }

        [JsonProperty("lng")]
        public decimal Longitude { get; set; }
    }

    public class NorthEast
    {
        [JsonProperty("lat")]
        public decimal Latitude { get; set; }

        [JsonProperty("lng")]
        public decimal Longitude { get; set; }
    }

    public class SouthWest
    {
        [JsonProperty("lat")]
        public decimal Latitude { get; set; }

        [JsonProperty("lng")]
        public decimal Longitude { get; set; }
    }

    public class Bounds
    {
        public NorthEast NorthEast { get; set; }

        public SouthWest SouthWest { get; set; }
    }

    public class ViewPort
    {
        public NorthEast NorthEast { get; set; }

        public SouthWest SouthWest { get; set; }
    }
}
