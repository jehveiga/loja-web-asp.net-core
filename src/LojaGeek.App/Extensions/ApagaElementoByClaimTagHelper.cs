using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;

namespace LojaGeek.App.Extensions
{
    // Apontando para a tag que será usada o atributo do tag helper criado nesta classe, no caso usando "* = todos elementos pode ser usado" 
    [HtmlTargetElement("*", Attributes = "supress-by-claim-name")]
    [HtmlTargetElement("*", Attributes = "supress-by-claim-value")]
    // Classe criada para fazer o gerenciamento do elemento na tela através do TagHelper
    public class ApagaElementoByClaimTagHelper : TagHelper
    {
        // Injetando o ContextHttp para ter acesso ao usuário logado para efetuar a validação
        private readonly IHttpContextAccessor _contextAcessor;

        public ApagaElementoByClaimTagHelper(IHttpContextAccessor contextAcessor)
        {
            _contextAcessor = contextAcessor;
        }

        [HtmlAttributeName("supress-by-claim-name")]

        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("supress-by-claim-value")]

        public string IdentityClaimValue { get; set; }

        // Método que fará o processo de gerenciamento da visibilidade do elemento através da validação da Claim do Usuário
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var temAcesso = CustomAuthorization.ValidarClaimsUsuario(_contextAcessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (temAcesso)
                return;

            output.SuppressOutput(); // Escondendo o elemento no caso não gerando
        }
    }

    // Apontando para a tag que será usada o atributo do tag helper criado nesta classe, no caso usando "* = todos elementos pode ser usado"
    // Para apresentar o elemento tem que está em uma a~ction passada do valor escolhido passado ao TagHelper
    [HtmlTargetElement("*", Attributes = "supress-by-action")]
    public class ApagaElementoByActionTagHelper : TagHelper
    {
        // Injetando o ContextHttp para ter acesso ao usuário logado para efetuar a validação
        private readonly IHttpContextAccessor _contextAccessor;

        public ApagaElementoByActionTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("supress-by-action")]
        public string ActionName { get; set; }

        // Método que fará o processo de desabilitar a visibilidade egeração do elemento através da validação da Claim do Usuário por meio da Action
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            // Obtendo a action em um request, para ser validado se vai apresentar a tag referenciada que está o TagHelper de autorização
            var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString(); //Retorna uma coleção sendo convertido em string obtendo a action

            if (ActionName.Contains(action)) return; // Validando a action se contém a ActionName passada como TagHelper

            output.SuppressOutput(); // Escondendo o elemento no caso não gerando
        }
    }


}
