namespace ConformiTrain.Entities
{
    public class Funcionario : BaseEntity
    {
        public Funcionario(string nomeCompleto, string email, string departamento, string cargo, DateTime dataContratacao) 
            :base()
        {
            NomeCompleto = nomeCompleto;
            Email = email;
            Departamento = departamento;
            Cargo = cargo;
            DataContratacao = dataContratacao;
            Ativo = true;
        }

        protected Funcionario() { }

        public string NomeCompleto { get; private set; }
        public string Email { get; private set; }
        public string Departamento { get; private set; }
        public string Cargo { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataContratacao { get; private set; }

        
        public ICollection<HistoricoTreinamento> HistoricoTreinamentos { get; private set; }
        public ICollection<ComentariosDoTreinamento> Comentarios { get; private set; }

        public void AtualizarDados(string email, string departamento, string cargo)
        {
            Email = email;
            Departamento = departamento;
            Cargo = cargo;
        }

        public void Inativar()
        {
            Ativo = false;
        }



    }

}
