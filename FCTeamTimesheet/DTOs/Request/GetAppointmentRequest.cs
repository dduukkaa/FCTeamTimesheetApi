using FCTeamTimesheet.Configuration;
using Newtonsoft.Json;
using System;

namespace FCTeamTimesheet.DTOs.Request
{
    public class GetAppointmentRequest
    {
        public string projects { get; set; }
        public string tags { get; set; }
        public string customers { get; set; }
        public string approvalSteps { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string toDo { get; set; }
        public string user { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public string active { get; set; }
        public string approval_steps { get; set; }
        public string @params { get; set; }
        public string appointments { get; set; }
    }
}
