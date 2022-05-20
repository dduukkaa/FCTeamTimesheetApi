using System;

namespace FCTeamTimesheet.DTOs.Request
{
    public class AppointmentRequest
    {
        public Appointment appointment { get; set; }
        public string appointmentType { get; set; }
    }
    public class Appointment
    {
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
    }
}
