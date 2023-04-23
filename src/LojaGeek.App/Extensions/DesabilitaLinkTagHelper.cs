using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace LojaGeek.App.Extensions
{
    // Apontando para a tag que será usada o atributo do tag helper criado nesta classe, no caso usando "* = todos elementos pode ser usado" 
    [HtmlTargetElement("a", Attributes = "disable-by-claim-name")]
    [HtmlTargetElement("a", Attributes = "disable-by-claim-value")]
    // Classe criada para fazer o gerenciamento do elemento na tela através do TagHelper no caso desabilitando
    public class DesabilitaLinkByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public DesabilitaLinkByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("disable-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("disable-by-claim-value")]
        public string IdentityClaimValue { get; set; }
        // Método que fará o processo de gerenciamento de desabilitar o elemento através da validação da Claim do Usuário
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var temAcesso = CustomAuthorization.ValidarClaimsUsuario(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (temAcesso) return;

            output.Attributes.RemoveAll("href"); // Remove o atributo do link a quem este TagHelper referencia
            output.Attributes.Add(new TagHelperAttribute("style", "cursor: not-allowed")); // adicionando atributo css para desabilitar a tag referenciada
            output.Attributes.Add(new TagHelperAttribute("title", "Você não tem permissão")); // adicionando atributo de informação da tag ao posicionar o mouse a tag referenciada
        }
    }
}
