using LojaGeek.App.Extensions;
using LojaGeek.Business.Interfaces;
using LojaGeek.Business.Notificacoes;
using LojaGeek.Business.Services;
using LojaGeekWeb.Data.Context;
using LojaGeekWeb.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace LojaGeek.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<LojaGeekDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            // registrando e injetando o serviço de validação de moeda,
            // configurado na pasta Extensions na classe criada MoedaValidationAttributeAdapterProvider
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            return services;
        }
    }
}
