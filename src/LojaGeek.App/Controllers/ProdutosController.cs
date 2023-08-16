using AutoMapper;
using LojaGeek.App.Extensions;
using LojaGeek.App.Models;
using LojaGeek.App.ViewModels;
using LojaGeek.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaGeek.App.Controllers
{
    [Authorize] // DataAnotation para informar que para ter acesso a está classe tem que está autorizado
    public class ProdutosController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoRepository produtoRepository,
                                  IFornecedorRepository fornecedorRepository,
                                  IMapper mapper,
                                  IProdutoService produtoService,
                                  INotificador _notificador) : base(_notificador)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _fornecedorRepository = fornecedorRepository;
            _produtoService = produtoService;
        }

        [AllowAnonymous] // Abre este método para não precisar de autenticação
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores()));
        }

        [AllowAnonymous]
        [Route("dados-do-produto/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel is null)
                return NotFound();


            return View(produtoViewModel);
        }

        [ClaimsAuthorize("Produto", "Adicionar")]
        [Route("novo-produto")]
        public async Task<IActionResult> Create()
        {
            var produtoViewModel = await PopularFornecedores(new ProdutoViewModel());
            return View(produtoViewModel);
        }

        [ClaimsAuthorize("Produto", "Adicionar")]
        [Route("novo-produto")]
        [HttpPost]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopularFornecedores(produtoViewModel);
            if (!ModelState.IsValid)
                return View(produtoViewModel);

            var imgPrefixo = String.Concat(Guid.NewGuid(), "_"); //Criando um nome único com uma concatenação para o arquivo de imagem
            if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo)) //Validando Upload do arquivo com um método
            {
                return View(produtoViewModel);
            }

            // Passando para o objeto/campo imagem o nome do arquivo para persistencia no banco
            produtoViewModel.Imagem = String.Concat(imgPrefixo, produtoViewModel.ImagemUpload.FileName);
            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            if (!OperacaoValida())
                return View(produtoViewModel);

            return RedirectToAction("Index");

        }

        [ClaimsAuthorize("Produto", "Editar")]
        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);
            if (produtoViewModel is null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(produtoViewModel);

            return View(produtoViewModel);
        }

        [ClaimsAuthorize("Produto", "Editar")]
        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            // Validando o Id passado na URL com o Id passado via formulário
            if (id != produtoViewModel.Id)
                return NotFound();

            // Chamando o método para popular a ViewModel pelo banco com informações que não vieram do front no momento do Post
            // para se precisar devolver o produto atualizado pelo banco em um response se necessário. Ex: Fornecedor, Imagem
            var produtoAtualizacao = await ObterProduto(id);
            produtoViewModel.Fornecedor = produtoAtualizacao.Fornecedor;
            produtoViewModel.Imagem = produtoAtualizacao.Imagem;

            if (!ModelState.IsValid)
                return View(produtoViewModel);

            if (produtoViewModel.ImagemUpload != null)
            {
                var imgPrefixo = String.Concat(Guid.NewGuid(), "_"); //Criando um nome único com uma concatenação para o arquivo de imagem
                if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo)) //Validando Upload do arquivo com um método
                {
                    return View(produtoViewModel);
                }

                produtoAtualizacao.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName; // Atualizando a edição da imagem para ser salva no banco
            }

            // Adicionando as informações atualizadas que vieram do formulário e atualizando as que não podem ser alteradas pelo submit do formulário
            // somente atualizadas pelo banco no caso fornecedor de  forma segura
            produtoAtualizacao.Nome = produtoViewModel.Nome; // Atribuindo os valores para o método de edição utilizando o objeto que veio do banco
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Ativo = produtoViewModel.Ativo;

            // Persistindo os dados do produto editado passando o produtoAtualizacao
            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));

            if (!OperacaoValida())
                return View(produtoViewModel);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Produto", "Excluir")]
        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto is null)
                return NotFound();

            return View(produto);
        }

        [ClaimsAuthorize("Produto", "Excluir")]
        [Route("excluir-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")] // Informando ao qual nome de Ação a ser executada quando for chamado o método
        // O método abaixo tem nome diferente pois recebe os mesmo parâmetros que o método Get Delete não podendo conter o mesmo nome
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto is null)
                return NotFound();

            await _produtoService.Remover(id);

            if (!OperacaoValida())
                return View(produto);

            TempData["Sucesso"] = "Produto excluido com sucesso!";

            return RedirectToAction("Index");
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
            produto.Fornecedores =
                _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }

        private async Task<ProdutoViewModel> PopularFornecedores(ProdutoViewModel produto)
        {
            produto.Fornecedores =
                _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo) // Método de validação de imagem upload
        {
            if (arquivo.Length <= 0)
                return false;

            // Criando caminho do arquivo que será salvo a imagem Passando (Diretorio local da aplicação, caminho fixo do servidor, código gerado prefixo + nome do arquivo))
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path)) // Verificando se o caminho/nome arquivo já existe no diretório informado
            {
                // Se existir retornará a View com a mensagem informada com retorno no método falso, adicionando no modelo a mensagem de erro abaixo
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome");
                return false;
            }

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                // Gravando o arquivo no disco que foi recebido no método, copiando o conteúdo do parametro informado no método CopyToAsync() 
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}
