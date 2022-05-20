using FCTeamTimesheet.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FCTeamTimesheet.Interfaces.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<CreateAppointmentResponse>> CreateMonthlyAppointments(string token, int month, int fromDay);
        Task DeleteAppointments(string token, int month, int[] days);
    }
}
