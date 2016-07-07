using Autofac;
using Autofac.Integration.WebApi;
using CardapioDigital.Dominio.Conta;
using CardapioDigital.Dominio.Core;
using CardapioDigital.Dominio.Core.Idioma;
using CardapioDigital.Dominio.Estoque;
using CardapioDigital.Dominio.Atendimento;
using CardapioDigital.Persistencia.InfraNH;
using CardapioDigital.Persistencia.Repositorios;

namespace CardapioDigital.Infra.ModulosInjecaoDependencia
{
    public class ModuloInjecaoDependenciaPersistencia : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnidadeDeTrabalho>().As<IUnidadeDeTrabalho>().InstancePerApiRequest();

            builder.RegisterType<Produtos>().As<IProdutos>().InstancePerApiRequest();
            builder.RegisterType<Categorias>().As<ICategorias>().InstancePerApiRequest();
            builder.RegisterType<RepositorioIdiomas>().As<IRepositorioIdiomas>().InstancePerApiRequest();
            builder.RegisterType<RepositorioDeContas>().As<IRepositorioDeContas>().InstancePerApiRequest();
            builder.RegisterType<RepositorioDeOpcoes>().As<IRepositorioDeOpcoes>().InstancePerApiRequest();
            builder.RegisterType<RepositorioSolicitacoes>().As<IRepositorioSolicitacoes>().InstancePerApiRequest();
            builder.RegisterType<RepositorioAvaliacoes>().As<IRepositorioAvaliacoes>().InstancePerApiRequest();
        }
    }
}
