using System;
using CardapioDigital.Dominio.Core;
using CardapioDigital.Dominio.Core.Idioma;
using CardapioDigital.Dominio.Estoque;
using CardapioDigital.Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardapioDigital.Aplicacao.Tests
{
    [TestClass]
    public class DatabaseHelper
    {
        private IUnidadeDeTrabalho _unidadeDeTrabalho;
        private IRepositorioIdiomas _idiomas;
        private ICategorias _categorias;
        private IProdutos _produtos;
        private Idioma _portugues;
        private Idioma _ingles;

        [TestInitialize]
        public void Initialize()
        {
            _unidadeDeTrabalho = Fabrica.Instancia.Obter<IUnidadeDeTrabalho>();
            _idiomas = Fabrica.Instancia.Obter<IRepositorioIdiomas>(_unidadeDeTrabalho);
            _categorias = Fabrica.Instancia.Obter<ICategorias>(_unidadeDeTrabalho);
            _produtos = Fabrica.Instancia.Obter<IProdutos>(_unidadeDeTrabalho);
        }

        [TestMethod]
        public void DeveRecriarBancoDeDados()
        {
            PrepararBancoDeDados();
        }


        private void PrepararBancoDeDados()
        {
            CriaIdiomas();

            #region Categoria01
            var categoria01 = new Categoria("Categoria 01", "Descrição Categoria 01", _portugues);
            
            var subcategoria01 = categoria01.AdicionarSubcategoria(new Subcategoria("Subcategoria 01", "Descrição Subcategoria 01", categoria01, _portugues));
            subcategoria01.AdicionarOpcao(new OpcaoSubcategoria("Opção 01 Subcategoria 01", "Descrição Opção 01 Subcategoria 01", _portugues));
            subcategoria01.AdicionarOpcao(new OpcaoSubcategoria("Opção 02 Subcategoria 01", "Descrição Opção 02 Subcategoria 01", _portugues));

            var produto01 = subcategoria01.AdicionarProduto("Produto 01", "Descrição Produto 01", 11.0m, null, true, _portugues);
            produto01.AdicionarOpcao(new OpcaoProduto("Opção 01 Produto 01", "Descrição Opção 01 Produto 01", _portugues));
            produto01.AdicionarOpcao(new OpcaoProduto("Opção 02 Produto 01", "Descrição Opção 02 Produto 01", _portugues));

            var produto02 = subcategoria01.AdicionarProduto("Produto 02", "Descrição Produto 02", 12.0m, null, false, _portugues);
            produto02.AdicionarOpcao(new OpcaoProduto("Opção 01 Produto 02", "Descrição Opção 01 Produto 02", _portugues));
            
            subcategoria01.AdicionarProduto(produto01);
            subcategoria01.AdicionarProduto(produto02);
            subcategoria01.AdicionarProduto(new Produto("Produto 03", "Descrição Produto 03", 13.0m, null, _portugues, subcategoria01));
            subcategoria01.AdicionarProduto(new Produto("Produto 04", "Descrição Produto 04", 14.0m, null, true, _portugues, subcategoria01));
            subcategoria01.AdicionarProduto(new Produto("Produto 05", "Descrição Produto 05", 15.0m, null, true, _portugues, subcategoria01));
            subcategoria01.AdicionarProduto(new Produto("Produto 06", "Descrição Produto 06", 11.0m, null, _portugues, subcategoria01));
            subcategoria01.AdicionarProduto(new Produto("Produto 07", "Descrição Produto 07", 12.0m, null, false, _portugues, subcategoria01));

            var subcategoria02 = categoria01.AdicionarSubcategoria(new Subcategoria("Subcategoria 02", "Descrição Subcategoria 02", categoria01, _portugues));
            subcategoria02.AdicionarProduto(new Produto("Produto 08", "Descrição Produto 08", 13.0m, null, _portugues, subcategoria02));
            subcategoria02.AdicionarProduto(new Produto("Produto 09", "Descrição Produto 09", 14.0m, null, _portugues, subcategoria02));
            #endregion Categoria01

            #region Categoria02
            var categoria02 = new Categoria("Categoria 02", "Descrição Categoria 02", _portugues);
            
            var subcategoria03 = categoria02.AdicionarSubcategoria(new Subcategoria("Subcategoria 03", "Descrição Subcategoria 03", categoria02, _portugues));
            subcategoria03.AdicionarProduto(new Produto("Produto 10", "Descrição Produto 10", 15.0m, null, true, _portugues, subcategoria03));
            subcategoria03.AdicionarProduto(new Produto("Produto 11", "Descrição Produto 11", 11.0m, null, _portugues, subcategoria03));
            subcategoria03.AdicionarProduto(new Produto("Produto 12", "Descrição Produto 12", 12.0m, null, true, _portugues, subcategoria03));
            subcategoria03.AdicionarProduto(new Produto("Produto 13", "Descrição Produto 13", 13.0m, null, false, _portugues, subcategoria03));

            var subcategoria04 = categoria02.AdicionarSubcategoria(new Subcategoria("Subcategoria 04", "Descrição Subcategoria 04", categoria02, _portugues));
            subcategoria04.AdicionarProduto(new Produto("Produto 14", "Descrição Produto 14", 14.0m, null, false, _portugues, subcategoria04));
            subcategoria04.AdicionarProduto(new Produto("Produto 15", "Descrição Produto 15", 15.0m, null, false, _portugues, subcategoria04));
            subcategoria04.AdicionarProduto(new Produto("Produto 16", "Descrição Produto 16", 15.0m, null, true, _portugues, subcategoria04));
            #endregion Categoria02

            _categorias.Salvar(categoria01);
            _categorias.Salvar(categoria02);

            _unidadeDeTrabalho.Commit();
        }

        private void CriaIdiomas()
        {
            _portugues = _idiomas.ObterPorSigla("pt-BR");
            if (_portugues == null)
            {
                _portugues = new Idioma("Português", "pt-BR");
                _idiomas.Salvar(_portugues);
            }

            _ingles = _idiomas.ObterPorSigla("en-US");
            if (_portugues == null)
            {
                _portugues = new Idioma("Inglês", "en-US");
                _idiomas.Salvar(_portugues);
            }
        }

        [TestCleanup]
        public void DisposeAll()
        {
            _unidadeDeTrabalho.Dispose();
        }

    }
}
