namespace ConformiTrain.Models
{
    public class AtualizarHistoricoInputModel
    {
        public bool Concluido { get; set; }
        public DateTime? DataParticipacao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
