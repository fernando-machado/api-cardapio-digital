using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;
using System.Collections.Generic;
using System.Web.Http;

namespace CardapioDigital.Api.Controllers
{
    public class SubcategoriasController : ApiController
    {
        private readonly GerenciamentoEstoque _gerenciamentoEstoque;

        public SubcategoriasController(GerenciamentoEstoque gerenciamentoEstoque)
        {
            _gerenciamentoEstoque = gerenciamentoEstoque;
        }

        // GET api/categorias/{idCategoria}/subcategorias/{idSubcategoria}
        public IEnumerable<SubcategoriaDto> Get(int codigoCategoria)
        {
            return _gerenciamentoEstoque.ObterTodasSubcategorias(codigoCategoria);
        }

        // GET api/categorias/{idCategoria}/subcategorias/{idSubcategoria}
        public SubcategoriaDto Get(int codigoCategoria, int idSubcategoria)
        {
            return _gerenciamentoEstoque.ObterSubcategoriaPorId(codigoCategoria, idSubcategoria);
        }

        // POST api/categorias/{idCategoria}/subcategorias/{idSubcategoria}
        public void Post(int codigoCategoria, int idSubcategoria, [FromBody]SubcategoriaDto novaSubcategoria)
        {
        }

        // PUT api/categorias/{idCategoria}/subcategorias/{idSubcategoria}
        public void Put(int codigoCategoria, int idSubcategoria, [FromBody]SubcategoriaDto subcategoria)
        {
        }

        // DELETE api/categorias/{idCategoria}/subcategorias/{idSubcategoria}
        public void Delete(int codigoCategoria, int idSubcategoria)
        {
        }
    }
}
