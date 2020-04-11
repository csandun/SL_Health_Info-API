using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaApi.Dto
{
    public class Location
    {
        public string date { get; set; }
        public string area { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    public class CaseDto
    {
        public string id { get; set; }
        public string caseNumber { get; set; }
        public bool isLocal { get; set; }
        public string detectedFrom { get; set; }
        public List<Location> locations { get; set; }
        public string message { get; set; }
        public string created { get; set; }
        public bool local { get; set; }
    }
}
