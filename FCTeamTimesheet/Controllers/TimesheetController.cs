using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FCTeamTimesheet.Contracts;
using FCTeamTimesheet.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FCTeamTimesheet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimesheetController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TimesheetController> _logger;
        private readonly IAppointmentService _appointmentService;

        public TimesheetController(ILogger<TimesheetController> logger,
                                        IAppointmentService appointmentService)
        {
            _logger = logger;
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Efetua o preenchimento do timesheet para o mês informado.
        /// </summary>
        /// <param name="request">Dados de requisição</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateAppointmentResponse))]
        public async Task<ActionResult> Post([FromBody] CreateAppointmentRequest request)
        {
            if (string.IsNullOrEmpty(request.BearerToken))
                return BadRequest("BearerToken é um campo obrigatorio.");

            if (request.Month < 1)
                return BadRequest("Valor do campo Month é inválido.");

            if (request.FromDay < 1)
                request.FromDay = 1;

            var response = await _appointmentService.CreateMonthlyAppointments(request.BearerToken, request.Month, request.FromDay);

            return Ok(response);
        }

        /// <summary>
        /// Efetua a exclusão dos apontamentos.
        /// </summary>
        /// <param name="request">Dados de requisição</param>
        [HttpPost("Clear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Clear([FromBody] DeleteAppointmentsRequest request)
        {
            if (string.IsNullOrEmpty(request.BearerToken))
                return BadRequest("BearerToken é um campo obrigatorio.");

            if (request.Month < 1)
                return BadRequest("Valor do campo Month é inválido.");

            if (request.Days == null || !request.Days.Any())
                return BadRequest("Days é um campo obrigatorio.");

            await _appointmentService.DeleteAppointments(request.BearerToken, request.Month, request.Days);

            return Ok();
        }
    }
}
