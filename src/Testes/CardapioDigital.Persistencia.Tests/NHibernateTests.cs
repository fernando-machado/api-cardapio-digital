//using System.Linq;
//using CardapioDigital.Dominio.Core;
//using CardapioDigital.Dominio.Core.Idioma;
//using CardapioDigital.Dominio.Estoque;
//using CardapioDigital.Infra;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace CardapioDigital.Persistencia.Tests
//{
//    [TestClass]
//    public class NHibernateTests
//    {
//        private Idioma _portugues;
//        private Idioma _ingles;

//        [TestInitialize]
//        public void CriaOuRecuperaIdiomas()
//        {
//            using (var unidadeDeTrabalho = Fabrica.Instancia.Obter<IUnidadeDeTrabalho>())
//            {
//                var repositorioIdiomas = Fabrica.Instancia.Obter<IRepositorioIdiomas>(unidadeDeTrabalho);

//                _portugues = repositorioIdiomas.ObterPorSigla("pt-BR");
//                if (_portugues == null)
//                {
//                    _portugues = new Idioma("Português", "pt-BR");
//                    repositorioIdiomas.Salvar(_portugues);
//                }

//                _ingles = repositorioIdiomas.ObterPorSigla("en-US");
//                if (_ingles == null)
//                {
//                    _ingles = new Idioma("Inglês", "en-US");
//                    repositorioIdiomas.Salvar(_ingles);
//                }

//                unidadeDeTrabalho.Commit();
//            }

//            Assert.IsNotNull(_portugues);
//            Assert.IsNotNull(_ingles);
//        }

//        [TestMethod]
//        public void TestesDeCriacao()
//        {
//            #region Categoria01
//            var categoria01 = new Categoria("Categoria 01", "Descrição Categoria 01", _portugues);
//            var subcategoria01 = categoria01.AdicionarSubcategoria(new Subcategoria("Subcategoria 01", "Descrição Subcategoria 01", categoria01, _portugues));
//            subcategoria01.AdicionarProduto(new Produto("Produto 01", "Descrição Produto 01", 11.0m, null, _portugues, subcategoria01));
//            subcategoria01.AdicionarProduto(new Produto("Produto 02", "Descrição Produto 02", 12.0m, null, _portugues, subcategoria01));
//            subcategoria01.AdicionarProduto(new Produto("Produto 03", "Descrição Produto 03", 13.0m, null, _portugues, subcategoria01));
//            subcategoria01.AdicionarProduto(new Produto("Produto 04", "Descrição Produto 04", 14.0m, null, _portugues, subcategoria01));
//            subcategoria01.AdicionarProduto(new Produto("Produto 05", "Descrição Produto 05", 15.0m, null, _portugues, subcategoria01));
//            subcategoria01.AdicionarProduto(new Produto("Produto 06", "Descrição Produto 06", 11.0m, null, _portugues, subcategoria01));
//            subcategoria01.AdicionarProduto(new Produto("Produto 07", "Descrição Produto 07", 12.0m, null, _portugues, subcategoria01));
            
//            var subcategoria02 = categoria01.AdicionarSubcategoria(new Subcategoria("Subcategoria 02", "Descrição Subcategoria 02", categoria01, _portugues));
//            subcategoria02.AdicionarProduto(new Produto("Produto 08", "Descrição Produto 08", 13.0m, null, _portugues, subcategoria02));
//            subcategoria02.AdicionarProduto(new Produto("Produto 09", "Descrição Produto 09", 14.0m, null, _portugues, subcategoria02));
//            #endregion Categoria01

//            #region Categoria02
//            var categoria02 = new Categoria("Categoria 02", "Descrição Categoria 02", _portugues);
//            var subcategoria03 = categoria01.AdicionarSubcategoria(new Subcategoria("Subcategoria 03", "Descrição Subcategoria 03", categoria02, _portugues));
//            subcategoria03.AdicionarProduto(new Produto("Produto 10", "Descrição Produto 10", 15.0m, null, _portugues, subcategoria03));
//            subcategoria03.AdicionarProduto(new Produto("Produto 11", "Descrição Produto 11", 11.0m, null, _portugues, subcategoria03));
//            subcategoria03.AdicionarProduto(new Produto("Produto 12", "Descrição Produto 12", 12.0m, null, _portugues, subcategoria03));
//            subcategoria03.AdicionarProduto(new Produto("Produto 13", "Descrição Produto 13", 13.0m, null, _portugues, subcategoria03));
            
//            var subcategoria04 = categoria01.AdicionarSubcategoria(new Subcategoria("Subcategoria 04", "Descrição Subcategoria 04", categoria02, _portugues));
//            subcategoria04.AdicionarProduto(new Produto("Produto 14", "Descrição Produto 14", 14.0m, null, _portugues, subcategoria04));
//            subcategoria04.AdicionarProduto(new Produto("Produto 15", "Descrição Produto 15", 15.0m, null, _portugues, subcategoria04));
//            subcategoria04.AdicionarProduto(new Produto("Produto 16", "Descrição Produto 16", 15.0m, null, _portugues, subcategoria04));
//            #endregion Categoria02

//            using (var unidadeDeTrabalho = Fabrica.Instancia.Obter<IUnidadeDeTrabalho>())
//            {
//                var categorias = Fabrica.Instancia.Obter<ICategorias>(unidadeDeTrabalho);

//                categorias.Salvar(categoria01);
//                categorias.Salvar(categoria02);

//                unidadeDeTrabalho.Commit();
//            }
//        }

//        [TestMethod]
//        public void TesteDeAtualizacao()
//        {
//            using (var unidadeDeTrabalho = Fabrica.Instancia.Obter<IUnidadeDeTrabalho>())
//            {
//                var categorias = Fabrica.Instancia.Obter<ICategorias>(unidadeDeTrabalho);
//                var estoque = Fabrica.Instancia.Obter<IProdutos>(unidadeDeTrabalho);

//                var categoria02 = categorias.ObterPorId(2);

//                var produto01 = estoque.ObterPorId(1);
//                var produto02 = estoque.ObterPorId(2);

//                //produto01.AlterarSubcategoria(categoria02);
//                produto01.AdicionarTraducao(_ingles, "Product 01", "Description of product 01");
//                //produto01.Imagem = File.ReadAllBytes(@"D:\DOCUMENTS\Pictures\VSTS2008.png");

//                produto02.AlterarTraducao(_portugues, "Produto 01 com nome alterado", "Descrição do produto 01 alterada");

//                estoque.Salvar(produto01);
//                estoque.Salvar(produto02);

//                unidadeDeTrabalho.Commit();
//            }
//        }

//        [TestMethod]
//        public void TesteDeConsulta()
//        {
//            // Teste ObterPorId
//            using (var unidadeDeTrabalho = Fabrica.Instancia.Obter<IUnidadeDeTrabalho>())
//            {
//                var estoque = Fabrica.Instancia.Obter<IProdutos>(unidadeDeTrabalho);

//                var prod01 = estoque.ObterPorId(1);

//                Assert.IsNotNull(prod01);
//                Assert.AreEqual(1, prod01.Codigo);
//            }

//            // Teste ObterTodos
//            using (var unidadeDeTrabalho = Fabrica.Instancia.Obter<IUnidadeDeTrabalho>())
//            {
//                var estoque = Fabrica.Instancia.Obter<IProdutos>(unidadeDeTrabalho);
//                var todos = estoque.ObterTodos();

//                Assert.IsNotNull(todos);
//                Assert.AreEqual(5, todos.Count());
//            }

//            // Teste ObterTodosOnde com filtro simples
//            using (var unidadeDeTrabalho = Fabrica.Instancia.Obter<IUnidadeDeTrabalho>())
//            {
//                var estoque = Fabrica.Instancia.Obter<IProdutos>(unidadeDeTrabalho);

//                var prod02 = estoque.ObterTodosOnde(p => p.Codigo == 2).Single();

//                Assert.IsNotNull(prod02);
//                Assert.AreEqual(2, prod02.Codigo);
//            }

//            // Teste ObterTodosOnde com filtro complexo
//            using (var unidadeDeTrabalho = Fabrica.Instancia.Obter<IUnidadeDeTrabalho>())
//            {
//                var categorias = Fabrica.Instancia.Obter<ICategorias>(unidadeDeTrabalho);
//                var estoque = Fabrica.Instancia.Obter<IProdutos>(unidadeDeTrabalho);

//                var categoria01 = categorias.ObterPorId(1);

//                var produtosComCategoria01 = estoque.ObterTodosOnde(p => p.Subcategoria == categoria01);

//                Assert.IsNotNull(produtosComCategoria01);
//                Assert.AreEqual(2, produtosComCategoria01.Count());
//                Assert.AreEqual(2, produtosComCategoria01.OrderBy(p => p.Codigo).First().Codigo);
//                Assert.AreEqual(3, produtosComCategoria01.OrderByDescending(p => p.Codigo).First().Codigo);
//            }
//        }

//        [TestMethod]
//        public void TesteDeLazyLoading()
//        {
//        }


//    }
//}
