namespace ConformiTrain.Models
{
    public class FuncionarioViewModel
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Departamento { get; set; }
        public string Cargo { get; set; }
        public DateTime DataContratacao { get; set; }
        public bool Ativo { get; set; }

        public static FuncionarioViewModel FromEntity(Entities.Funcionario funcionario)
        {
            return new FuncionarioViewModel
            {
                Id = funcionario.Id,
                NomeCompleto = funcionario.NomeCompleto,
                Email = funcionario.Email,
                Departamento = funcionario.Departamento,
                Cargo = funcionario.Cargo,
                DataContratacao = funcionario.DataContratacao,
                Ativo = funcionario.Ativo
            };
        }
    }
}
