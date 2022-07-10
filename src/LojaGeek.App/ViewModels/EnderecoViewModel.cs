using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace LojaGeek.App.ViewModels
{
    public class EnderecoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")] // O {0} representa o nome do campo referido
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} a {1} caracteres", MinimumLength = 2)] // Ordem de parametros do StringLength {0}Campo, {1}Maximo e {2}Minimo
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")] // O {0} representa o nome do campo referido
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} a {1} caracteres", MinimumLength = 2)] // Ordem de parametros do StringLength {0}Campo, {1}Maximo e {2}Minimo
        [Display(Name = "Número")]
        public string Numero { get; set; }
        public string Complemento { get; set; }

        [Display(Name = "CEP")]

        [Required(ErrorMessage = "O campo {0} é obrigatório")] // O {0} representa o nome do campo referido
        [StringLength(8, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 8)] // Ordem de parametros do StringLength {0}Campo, {1}Maximo
        public string Cep { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")] // O {0} representa o nome do campo referido
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} a {1} caracteres", MinimumLength = 2)] // Ordem de parametros do StringLength {0}Campo, {1}Maximo e {2}Minimo
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")] // O {0} representa o nome do campo referido
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} a {1} caracteres", MinimumLength = 2)] // Ordem de parametros do StringLength {0}Campo, {1}Maximo e {2}Minimo
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")] // O {0} representa o nome do campo referido
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} a {1} caracteres", MinimumLength = 2)] // Ordem de parametros do StringLength {0}Campo, {1}Maximo e {2}Minimo
        public string Estado { get; set; }

        [HiddenInput] // Informando para aplicação que este campo sempre será tratado como um campo Hidden
        public Guid FornecedorId { get; set; }
    }
}
