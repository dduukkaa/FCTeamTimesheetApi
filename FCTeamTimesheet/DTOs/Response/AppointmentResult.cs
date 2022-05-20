using System;
using System.Collections.Generic;

namespace FCTeamTimesheet.DTOs.Response
{
    public class AppointmentResult 
    {
        public Appointment appointment { get; set; }
        public string message { get; set; }
    }
    public class Appointment
    {
        public int __v { get; set; }
        public int total { get; set; }
        public string company { get; set; }
        public string created_by { get; set; }
        public string user { get; set; }
        public bool toDo { get; set; }
        public string project { get; set; }
        public string task_description { get; set; }
        public string track_option { get; set; }
        public string source { get; set; }
        public DateTime start { get; set; }
        public DateTime stop { get; set; }
        public string customer { get; set; }
        public DateTime day { get; set; }
        public DateTime created_at { get; set; }
        public string _id { get; set; }
        public bool blocked { get; set; }
        public bool timer_is_running { get; set; }
        public List<object> billing_updated { get; set; }
        public List<object> status_updated { get; set; }
        public List<object> tags { get; set; }
    }
}
