using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaApi.Models
{
   

    public class Hospital
    {
        public int id { get; set; }
        public string name { get; set; }
        public string name_si { get; set; }
        public string name_ta { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public object deleted_at { get; set; }
    }

    public class HospitalData
    {
        public int id { get; set; }
        public int hospital_id { get; set; }
        public int cumulative_local { get; set; }
        public int cumulative_foreign { get; set; }
        public int treatment_local { get; set; }
        public int treatment_foreign { get; set; }
        public string created_at { get; set; }
        public object updated_at { get; set; }
        public object deleted_at { get; set; }
        public int cumulative_total { get; set; }
        public int treatment_total { get; set; }
        public Hospital hospital { get; set; }
    }

    public class Data
    {
        public string update_date_time { get; set; }
        public int local_new_cases { get; set; }
        public int local_total_cases { get; set; }
        public int local_total_number_of_individuals_in_hospitals { get; set; }
        public int local_deaths { get; set; }
        public int local_new_deaths { get; set; }
        public int local_recovered { get; set; }
        public int global_new_cases { get; set; }
        public int global_total_cases { get; set; }
        public int global_deaths { get; set; }
        public int global_new_deaths { get; set; }
        public int global_recovered { get; set; }
        public List<HospitalData> hospital_data { get; set; }
    }

    public class CoronaGovResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
