using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaApi.Models
{
    public class Case
    {
        public int Id { get; set; }
        public int GovId { get; set; }
        public int CaseNumber { get; set; }
        public bool IsLocal { get; set; }
        public string DetectedFrom { get; set; }
        public DateTime GovCreated { get; set; }
        public bool Local { get; set; }
        public DateTime ReportedDate { get; set; }
        public string Area { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
