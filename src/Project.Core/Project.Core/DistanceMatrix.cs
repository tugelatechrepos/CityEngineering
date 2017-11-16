using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core
{
    public class DistanceMatrix
    {
        public Distance Distance { get; set; }

        public Duration Duration { get; set; }
    }

    public class Distance
    {
        public string Text { get; set; }

        public string Value { get; set; }
    }

    public class Duration
    {
        public string Text { get; set; }

        public string Value { get; set; }
    }
}
