namespace ConformiTrain.Entities
{
    public class ComentariosDoTreinamento : BaseEntity
    {
        public ComentariosDoTreinamento(string conteudo, int idTreinamento, int idFuncionario)
            :base()
        {
            Conteudo = conteudo;
            IdTreinamento = idTreinamento;
            IdFuncionario = idFuncionario;
        }


        public string Conteudo { get; private set; }
        public int IdTreinamento { get; private set; }
        public Treinamento Treinamento  { get; private set; }
        public int IdFuncionario { get; private set; }
        public Funcionario Funcionario { get; private set; }


    }
}
