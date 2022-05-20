using System.Threading.Tasks;
using Refit;
using FCTeamTimesheet.DTOs.Response;
using FCTeamTimesheet.DTOs.Request;

namespace FCTeamTimesheet.Interfaces.ApiClients
{
    public interface IFCTeamApiClient
    {
        [Post("/v1/appointments")]
        Task<AppointmentResult> CreateAppointment([Header("Authorization")] string token, AppointmentRequest request);
        [Delete("/v1/appointments/{id}")]
        Task<AppointmentResult> DeleteAppointment([Header("Authorization")] string token, string id);

        [Get("/v1/appointments")]
        Task<GetAppointmentResponse[]> GetAppointments([Header("Authorization")] string token, [Query] GetAppointmentRequest request);
    }
}
