namespace ConformiTrain.Models
{
    public class ComentarioViewModel
    {
        public int Id { get; set; }               
        public string Conteudo { get; set; }      
        public int IdTreinamento { get; set; }    
        public int IdFuncionario { get; set; }     
        public string NomeFuncionario { get; set; } 
        public DateTime DataComentario { get; set; }
    }
}
