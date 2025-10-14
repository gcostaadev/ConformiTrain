namespace ConformiTrain.Models
{
    public class CadastrarFuncionarioInputModel
    {
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Departamento { get; set; }
        public string Cargo { get; set; }
        public DateTime DataContratacao { get; set; }

    }
}
