namespace ConformiTrain.Entities
{
    public class HistoricoTreinamento : BaseEntity
    {

        public HistoricoTreinamento(int funcionarioId, int treinamentoId, DateTime dataParticipacao)
            :base()
        {
            FuncionarioId = funcionarioId;
            TreinamentoId = treinamentoId;
            DataParticipacao = dataParticipacao;
            Concluido = false;
        }

        protected HistoricoTreinamento() { }


        public int FuncionarioId { get; private set; }
        public Funcionario Funcionario { get; private set; }

        public int TreinamentoId { get; private set; }
        public Treinamento Treinamento { get; private set; }

        public DateTime DataParticipacao { get; private set; }
        public bool Concluido { get; private set; }
        public DateTime? DataInicioTreinamento { get; private set; }
        public DateTime? DataFimTreinamento { get; private set; }

        public void MarcarComoConcluido(DateTime dataInicio, DateTime dataFim)
        {
            Concluido = true;
            DataInicioTreinamento = dataInicio;
            DataFimTreinamento = dataFim;
        }

        public void Atualizar(DateTime? dataInicio, DateTime? dataFim, bool concluido)
        {
            if (concluido)
            {
                if (dataInicio.HasValue && dataFim.HasValue)
                {
                    MarcarComoConcluido(dataInicio.Value, dataFim.Value);
                }
                else
                {
                    // Se quiser, lance exceção ou defina datas padrão
                    throw new ArgumentException("Data de início e fim são obrigatórias para concluir o treinamento.");
                }
            }
            else
            {
                Concluido = false;
                DataInicioTreinamento = dataInicio;
                DataFimTreinamento = dataFim;
            }
        }


    }
}
