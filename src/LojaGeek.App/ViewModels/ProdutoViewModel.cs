using LojaGeek.App.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaGeek.App.ViewModels
{
    public class ProdutoViewModel
    {
        [Key] // Identificando este campo como um campo chave
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")] // O {0} representa o nome do campo referido
        [Display(Name = "Fornecedor")]
        public Guid FornecedorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")] // O {0} representa o nome do campo referido
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} a {1} caracteres", MinimumLength = 2)] // Ordem de parametros do StringLength {0}Campo, {1}Maximo e {2}Minimo
        public string Nome { get; set; }

        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} a {1} caracteres", MinimumLength = 2)]
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }

        [Display(Name = "Imagem do Produto")]
        [NotMappedAttribute]
        public IFormFile ImagemUpload { get; set; } // Campo que será referenciado com a imagem dos produtos

        public string Imagem { get; set; }

        [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }

        [ScaffoldColumn(false)] // Para informar  ao Scaffold que este campo será desconsiderado quando ele mapear está classe 
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Ativo?")]
        public bool Ativo { get; set; }
        public FornecedorViewModel Fornecedor { get; set; }
        public IEnumerable<FornecedorViewModel> Fornecedores { get; set; }
    }
}
