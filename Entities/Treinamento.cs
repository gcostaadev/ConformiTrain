using ConformiTrain.Enums;

namespace ConformiTrain.Entities

{
    public class Treinamento : BaseEntity
    {

        public Treinamento(string titulo, string descricao, int cargaHoraria, DateTime? dataVencimentoPadrao, bool obrigatorio)
            :base()
        {
            Titulo = titulo;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            DataCriacao = DateTime.UtcNow;
            DataVencimentoPadrao = dataVencimentoPadrao;
            Obrigatorio = obrigatorio;
            Status = StatusTreinamento.Ativo;

            Comentarios = new List<ComentariosDoTreinamento>();

            HistoricoTreinamentos = new List<HistoricoTreinamento>();
        }

        protected Treinamento() { }
                
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public int CargaHoraria { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataVencimentoPadrao { get; private set; }
        public bool Obrigatorio { get; private set; }
        public StatusTreinamento Status { get; private set; }

        public List<ComentariosDoTreinamento> Comentarios { get; private set; }

        public ICollection<HistoricoTreinamento> HistoricoTreinamentos { get; private set; }

        public void Inativo()
        {
            if (Status != StatusTreinamento.Inativo)
            {
                Status = StatusTreinamento.Inativo;
            }
        }

        public void Ativo()
        {
            if (Status != StatusTreinamento.Ativo)
            {
                Status = StatusTreinamento.Ativo;
            }
        }

        public void Concluido()
        {
            if (Status != StatusTreinamento.Concluido)
            {
                Status = StatusTreinamento.Concluido;
            }

        }

        public void Atualizar(string titulo, string descricao, int cargaHoraria, bool obrigatorio, DateTime dataDeVencimento)
        {
            Titulo = titulo;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            Obrigatorio = obrigatorio;
            DataVencimentoPadrao = dataDeVencimento;
        }




    }
}
