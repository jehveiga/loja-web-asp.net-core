using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace LojaGeek.App.Configurations
{
    // Extensão de método de configuração de localização no caso mudando para pt-BR
    public static class GlobalizationConfig
    {
        // método a ser usado para configuração da globalização da aplicação, 
        // exemplo de mudança: padrão moeda BR
        public static IApplicationBuilder UseGlobalizationConfig(this IApplicationBuilder app)
        {
            // definindo a varíavel que vai ser a padrão da globalização
            var defaultCulture = new CultureInfo("pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                // Lista de cultura suportadas na aplicação, pode ser passado mais de uma config. de cultura a ser suportada
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                // Lista de cultura suportadas na aplicação aqui forçãndo a app trabalhar com a informada,
                // pode ser passado mais de uma config. de cultura a ser suportada
                SupportedUICultures = new List<CultureInfo>() { defaultCulture }
            };
            // Aplicando as configurações informadas acima passando a varíavel como parâmetro
            app.UseRequestLocalization(localizationOptions);

            return app;
        }
    }
}