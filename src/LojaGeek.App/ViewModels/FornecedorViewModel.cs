using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaGeek.App.ViewModels
{
    // ViewModel são os espelhos das Models da camada de Business, assim filtrando as propriedades que serão apresentadas nas telas
    public class FornecedorViewModel
    {
        [Key]
        public Guid Id { get; set; } // Aqui tem referência do Id pois não terá herança como na Business para ser mapeada pelo Entity

        [Required(ErrorMessage = "O campo {0} é obrigatório")] // O {0} representa o nome do campo referido
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} a {1} caracteres", MinimumLength = 2)] // Ordem de parametros do StringLength {0}Campo, {1}Maximo e {2}Minimo
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")] // O {0} representa o nome do campo referido
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} a {1} caracteres", MinimumLength = 11)] // Ordem de parametros do StringLength {0}Campo, {1}Maximo e {2}Minimo
        public string Documento { get; set; }

        [Display(Name = "Tipo")]
        public int TipoFornecedor { get; set; }

        public EnderecoViewModel Endereco { get; set; }

        [Display(Name = "Ativo?")]
        public bool Ativo { get; set; }
        [NotMapped]
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
    }
}
