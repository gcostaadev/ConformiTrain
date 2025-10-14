using ConformiTrain.Entities;

namespace ConformiTrain.Models
{
    public class TreinamentoViewModel
    {
        public TreinamentoViewModel(int id, string titulo, string descricao, int cargaHoraria, DateTime dataCriacao, DateTime? dataVencimentoPadrao, bool obrigatorio, string status)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            DataCriacao = dataCriacao;
            DataVencimentoPadrao = dataVencimentoPadrao;
            Obrigatorio = obrigatorio;
            Status = status;
           
        }

        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public int CargaHoraria { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataVencimentoPadrao { get; private set; }
        public bool Obrigatorio { get; private set; }
        public string Status { get; private set; }


        public static TreinamentoViewModel FromEntity(Treinamento entity)
    => new TreinamentoViewModel(
        entity.Id,
        entity.Titulo,
        entity.Descricao,
        entity.CargaHoraria,
        entity.DataCriacao,
        entity.DataVencimentoPadrao,
        entity.Obrigatorio,
        entity.Status.ToString()
    );

    }
}
