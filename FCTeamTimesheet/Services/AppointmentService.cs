using FCTeamTimesheet.Configuration;
using FCTeamTimesheet.Contracts;
using Request = FCTeamTimesheet.DTOs.Request;
using Response = FCTeamTimesheet.DTOs.Response;
using FCTeamTimesheet.Interfaces.ApiClients;
using FCTeamTimesheet.Interfaces.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCTeamTimesheet.Services
{
    public class AppointmentService : IAppointmentService
    {
        public IFCTeamApiClient _fcTeamApiClient { get; set; }
        public AppointmentParameters _appointmentParameters { get; set; }
        public AppointmentService(IFCTeamApiClient fcTeamApiClient, IOptions<AppointmentParameters> options)
        {
            _fcTeamApiClient = fcTeamApiClient;
            _appointmentParameters = options.Value;
        }
        public async Task<IEnumerable<CreateAppointmentResponse>> CreateMonthlyAppointments(string token, int month, int fromDay)
        {
            var appointmentRequests = BuildAppointmentRequests(month, fromDay);
            var appointmentResults = new List<Response.AppointmentResult>();

            if (!token.Contains("Bearer"))
                token = $"Bearer {token}";

            foreach (var appointmentRequest in appointmentRequests)
            {
                var appointmentResult = await _fcTeamApiClient.CreateAppointment(token, appointmentRequest);
                
                appointmentResults.Add(appointmentResult);
            }

            return appointmentResults.Select(x => {

                return new CreateAppointmentResponse
                {
                    Message = x.message
                };
            });
        }

        public async Task DeleteAppointments(string token, int month, int[] days)
        {
            var year = DateTime.Now.Year;

            if (!token.Contains("Bearer"))
                token = $"Bearer {token}";

            foreach (var day in days)
            {
                var getAppointmentRequest = new Request.GetAppointmentRequest()
                {
                    startDate = new DateTime(year, month, day,0,0,0).ToString("ddd MMM dd yyy HH':'mm':'ss 'GMT-0300'", System.Globalization.CultureInfo.InvariantCulture),
                    endDate = new DateTime(year, month, day, 23, 59, 59).ToString("ddd MMM dd yyy HH':'mm':'ss 'GMT-0300'", System.Globalization.CultureInfo.InvariantCulture),
                    toDo = "false",
                    user = _appointmentParameters.User,
                    limit = 10,
                    offset = 0,
                    active = "true",
                    @params = "Time",
                    appointments = "false"
                };

                var getAppointmentResponse = await _fcTeamApiClient.GetAppointments(token, getAppointmentRequest);

                foreach (var item in getAppointmentResponse)
                {
                    await _fcTeamApiClient.DeleteAppointment(token, item._id);
                }
            }
        }

        private IEnumerable<Request.AppointmentRequest> BuildAppointmentRequests(int month, int fromDay = 1)
        {
            var appointmentRequests = new List<Request.AppointmentRequest>();
            
            var year = DateTime.Now.Year;

            var nonWorkingDays = new DayOfWeek[] { DayOfWeek.Sunday, DayOfWeek.Saturday };

            var daysInMonth = DateTime.DaysInMonth(year, month);

            for (int day = fromDay; day <= daysInMonth; day++)
            {
                var date = new DateTime(year, month, day);

                if (nonWorkingDays.Contains(date.DayOfWeek))
                    continue;

                var firstPeriod = new Request.AppointmentRequest
                {
                    appointmentType = "single",
                    appointment = new Request.Appointment
                    {
                        customer = _appointmentParameters.Customer,
                        project = _appointmentParameters.Project,
                        user = _appointmentParameters.User,
                        toDo = false,
                        task_description = "",
                        track_option = "start_and_end",
                        source = "manual",
                        day = new DateTime(year, month, day, 3, 0, 0),
                        start = new DateTime(year, month, day, 12, 0, 0),
                        stop = new DateTime(year, month, day, 16, 0, 0)
                    }
                };

                appointmentRequests.Add(firstPeriod);

                var secondPeriod = new Request.AppointmentRequest
                {
                    appointmentType = "single",
                    appointment = new Request.Appointment
                    {
                        customer = _appointmentParameters.Customer,
                        project = _appointmentParameters.Project,
                        user = _appointmentParameters.User,
                        toDo = false,
                        task_description = "",
                        track_option = "start_and_end",
                        source = "manual",
                        day = new DateTime(year, month, day, 3, 0, 0),
                        start = new DateTime(year, month, day, 17, 0, 0),
                        stop = new DateTime(year, month, day, 21, 0, 0)
                    }
                };

                appointmentRequests.Add(secondPeriod);
            }

            return appointmentRequests;
        }
    }
}
