//using CardapioDigital.Aplicacao.DTO;
//using CardapioDigital.Aplicacao.Servicos;
//using System.Collections.Generic;
//using System.Web.Http;

//namespace CardapioDigital.Api.Controllers
//{
//    public class ProdutosSubcategoriaController : ApiController
//    {
//        private readonly GerenciamentoEstoque _gerenciamentoEstoque;

//        public ProdutosSubcategoriaController(GerenciamentoEstoque gerenciamentoEstoque)
//        {
//            _gerenciamentoEstoque = gerenciamentoEstoque;
//        }

//        // GET api/categorias/{idCategoria}/subcategorias/{idSubcategoria}/produtos/{idProduto}
//        public IHttpActionResult Get(int idCategoria, int idSubcategoria)
//        {
//            var produtos  = _gerenciamentoEstoque.ObterProdutosDaSubcategoria(idSubcategoria);
            
//            return Ok(produtos);
//        }

//        // GET api/categorias/{idCategoria}/subcategorias/{idSubcategoria}/produtos/{idProduto}
//        public IHttpActionResult Get(int idCategoria, int idSubcategoria, int idProduto)
//        {
//            var produto = _gerenciamentoEstoque.ObterProdutoPorId(idProduto);
//            if (produto == null)
//                return NotFound();

//            return Ok(produto);
//        }

//        //// POST api/categorias/{idCategoria}/subcategorias/{idSubcategoria}/produtos/{idProduto}
//        //public void Post(int idCategoria, int idSubcategoria, [FromBody]ProdutoDto novoProduto)
//        //{

//        //}

//        //// PUT api/categorias/{idCategoria}/subcategorias/{idSubcategoria}/produtos/{idProduto}
//        //public void Put(int idCategoria, int idSubcategoria, int idProduto, [FromBody]ProdutoDto produto)
//        //{
//        //}

//        //// DELETE api/categorias/{idCategoria}/subcategorias/{idSubcategoria}/produtos/{idProduto}
//        //public void Delete(int idCategoria, int idSubcategoria, int idProduto)
//        //{
//        //}
//    }
//}