using AutoMapper;
using LojaGeek.App.Models;
using LojaGeek.App.ViewModels;

namespace LojaGeek.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap(); // Adicionando a transformação de uma classe da camada Data a camada de Apresentação e vice versa
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}
