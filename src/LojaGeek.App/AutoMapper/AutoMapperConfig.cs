using AutoMapper;
using LojaGeek.App.Models;
using LojaGeek.App.ViewModels;

namespace LojaGeek.App.AutoMapper
{
    // Pacote Nuget para mapear classes entre camadas, service adicionado na Startup
    public class AutoMapperConfig : Profile // Profile identifica que está classe é uma config. de personalização de perfil de Mapeamento
    {
        public AutoMapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap(); // Adicionando a transformação de uma classe da camada Business
                                                                       // para camada de Apresentação e vice versa
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}
