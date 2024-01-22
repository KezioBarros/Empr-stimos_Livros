namespace EmprestimosLivro.Models
{
    public class EmprestimosModels
    {
        public int Id { get; set; }
        public string Recebedor { get; set; }
        public string Fornecedor { get; set; }
        public string LivroEmprestado { get; set; }
        public DateTime DataUtimaAtualizacao { get; set; } = DateTime.Now;


    }
}
