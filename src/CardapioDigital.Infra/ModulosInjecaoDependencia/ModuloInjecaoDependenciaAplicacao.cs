using Autofac;
using Autofac.Integration.WebApi;
using CardapioDigital.Aplicacao.Servicos;

namespace CardapioDigital.Infra.ModulosInjecaoDependencia
{
    internal class ModuloInjecaoDependenciaAplicacao : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GerenciamentoEstoque>().InstancePerRequest();
            builder.RegisterType<GerenciamentoConta>().InstancePerRequest();
            builder.RegisterType<GerenciamentoAtendimento>().InstancePerRequest();
        }
    }
}