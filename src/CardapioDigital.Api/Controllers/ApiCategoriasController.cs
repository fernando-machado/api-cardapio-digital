using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;
using System.Collections.Generic;
using System.Web.Http;

namespace CardapioDigital.Api.Controllers
{
    public class CategoriasController : ApiController
    {
        private readonly GerenciamentoEstoque _gerenciamentoEstoque;

        public CategoriasController(GerenciamentoEstoque gerenciamentoEstoque)
        {
            _gerenciamentoEstoque = gerenciamentoEstoque;
        }

        // GET api/categorias
        public IEnumerable<CategoriaDto> Get()
        {
            return _gerenciamentoEstoque.ObterTodasCategorias();
        }

        // GET api/categorias/{idCategoria}
        public CategoriaDto Get(int idCategoria)
        {
            return _gerenciamentoEstoque.ObterCategoriaPorId(idCategoria);
        }

        // POST api/categorias
        public void Post([FromBody]CategoriaDto novaCategoria)
        {
        }

        // PUT api/categorias/{idCategoria}
        public void Put(int idCategoria, [FromBody]CategoriaDto categoria)
        {
        }

        // DELETE api/categorias/{idCategoria}
        public void Delete(int idCategoria)
        {
            _gerenciamentoEstoque.PrepararBancoDeDados();
        }
    }
}
