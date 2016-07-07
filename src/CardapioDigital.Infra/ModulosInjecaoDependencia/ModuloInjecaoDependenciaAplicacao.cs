using Autofac;
using Autofac.Integration.WebApi;
using CardapioDigital.Aplicacao.Servicos;

namespace CardapioDigital.Infra.ModulosInjecaoDependencia
{
    internal class ModuloInjecaoDependenciaAplicacao : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GerenciamentoEstoque>().InstancePerApiRequest();
            builder.RegisterType<GerenciamentoConta>().InstancePerApiRequest();
            builder.RegisterType<GerenciamentoAtendimento>().InstancePerApiRequest();
        }
    }
}