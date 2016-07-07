using CardapioDigital.Persistencia.InfraNH;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardapioDigital.Persistencia.Tests
{
    [TestClass]
    public class SchemaGeneratorTests
    {
        [TestMethod]
        public void Deve_Testar_CriarBanco()
        {
            SchemaGenerator.CriarBanco();
        }

        [TestMethod]
        public void Deve_Testar_ExcluirBanco()
        {
            SchemaGenerator.ExcluirBanco();
        }

        [TestMethod]
        public void Deve_Testar_AtualizarBanco()
        {
            SchemaGenerator.AtualizarBanco();
        }

        [TestMethod]
        public void Deve_Testar_GerarScriptDeCriacaoDoSchema()
        {
            SchemaGenerator.GerarScriptDeCriacaoDoSchema();
        }

        [TestMethod]
        public void Deve_Testar_GerarScriptDeAtualizacaoDoSchema()
        {
            SchemaGenerator.GerarScriptDeAtualizacaoDoSchema();
        }

        [TestMethod]
        public void Deve_Testar_GerarScriptDeCriacaoDoSchema_Passando_Pasta_Destino()
        {
            SchemaGenerator.GerarScriptDeCriacaoDoSchema(@"D:\SchemasDB\Testes");
        }

        [TestMethod]
        public void Deve_Testar_GerarScriptDeAtualizacaoDoSchema_Passando_Pasta_Destino()
        {
            SchemaGenerator.GerarScriptDeAtualizacaoDoSchema(@"D:\SchemasDB\Testes");
        }
    }
}
