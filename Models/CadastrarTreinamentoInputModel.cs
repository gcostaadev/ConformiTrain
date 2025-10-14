namespace ConformiTrain.Models
{
    public class CadastrarTreinamentoInputModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public DateTime? DataVencimentoPadrao { get; set; }
        public bool Obrigatorio { get; set; }
    }
}
