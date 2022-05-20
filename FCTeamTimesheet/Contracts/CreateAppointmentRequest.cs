namespace FCTeamTimesheet.Contracts
{
    public class CreateAppointmentRequest
    {
        /// <summary>
        /// Token de autenticação do portal FCTeam.
        /// </summary>
        public string BearerToken { get; set; }
        /// <summary>
        /// Mês a ser preenchido. Valores aceitos: numéricos de 1 a 12.
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// Dia a partir do qual serão criados os apontamentos. Valores aceitos: numéricos de 1 a 31.
        /// </summary>
        public int FromDay { get; set; }
    }
}
