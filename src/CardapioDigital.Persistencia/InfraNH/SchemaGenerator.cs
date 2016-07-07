using System;
using System.IO;
using NHibernate.Tool.hbm2ddl;

namespace CardapioDigital.Persistencia.InfraNH
{
    public static class SchemaGenerator
    {
        public static void CriarBanco()
        {
            var config = SessionFactory.FluentlyConfig;

            config.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true));

            config.BuildSessionFactory();
        }

        public static void ExcluirBanco()
        {
            var config = SessionFactory.FluentlyConfig;

            config.ExposeConfiguration(cfg => new SchemaExport(cfg).Drop(true, true));

            config.BuildSessionFactory();
        }

        public static void AtualizarBanco()
        {
            var config = SessionFactory.FluentlyConfig;

            config.ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true));

            config.BuildSessionFactory();
        }

        public static void GerarScriptDeCriacaoDoSchema(string caminhoPastaDestino = null)
        {
            var config = SessionFactory.FluentlyConfig;

            var nomeArquivoScript = string.Format("Create_Schema_{0:yyyy-MM-dd_HHmmss}.sql", DateTime.Now);

            var caminhoFinalScript = Path.Combine(caminhoPastaDestino ?? @"D:\SchemasDB\", nomeArquivoScript);

            config.ExposeConfiguration(cfg => new SchemaExport(cfg).SetOutputFile(caminhoFinalScript).Create(false, false));

            config.BuildSessionFactory();
        }

        public static void GerarScriptDeAtualizacaoDoSchema(string caminhoPastaDestino = null)
        {
            var config = SessionFactory.FluentlyConfig;

            var nomeArquivoScript = string.Format("Update_Schema_{0:yyyy-MM-dd_HHmmss}.sql", DateTime.Now);

            var caminhoFinalScript = Path.Combine(caminhoPastaDestino ?? @"D:\SchemasDB\", nomeArquivoScript);

            using (var writer = new StreamWriter(caminhoFinalScript))
            {
                config.ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(script => writer.Write(script), false));

                config.BuildSessionFactory();
            }
        }
    }
}
