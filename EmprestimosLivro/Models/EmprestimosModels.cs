﻿using System.ComponentModel.DataAnnotations;

namespace EmprestimosLivro.Models
{
    public class EmprestimosModels
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do Recebedor!")]
        public string Recebedor { get; set; }

        [Required(ErrorMessage = "Digite o nome do Fornecedor!")]
        public string Fornecedor { get; set; }

        [Required(ErrorMessage = "Digite o nome do Livro!")]
        public string LivroEmprestado { get; set; }
        public DateTime DataUtimaAtualizacao { get; set; } = DateTime.Now;


    }
}
