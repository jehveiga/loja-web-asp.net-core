using Microsoft.AspNetCore.Mvc.Razor;
using System;

namespace LojaGeek.App.Extensions
{
    public static class RazorExtensions
    {
        /// <summary>
        /// Método de Extensão - Convertendo o documento de fornecedor para o formato específico de acordo com o tipo de documento Jurídico/Físico
        /// </summary>
        /// <param name="page"></param>
        /// <param name="tipoPessoa"></param>
        /// <param name="documento"></param>
        /// <returns>retorna a string convertida</returns>
        public static string FormataDocumento(this RazorPage page, int tipoPessoa, string documento)
        {
            return tipoPessoa == 1 ? Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00") :
                Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00");

        }
    }
}
