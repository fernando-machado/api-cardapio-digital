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
            builder.RegisterType<UnidadeDeTrabalho>().As<IUnidadeDeTrabalho>().InstancePerRequest();

            builder.RegisterType<Produtos>().As<IProdutos>().InstancePerRequest();
            builder.RegisterType<Categorias>().As<ICategorias>().InstancePerRequest();
            builder.RegisterType<RepositorioIdiomas>().As<IRepositorioIdiomas>().InstancePerRequest();
            builder.RegisterType<RepositorioDeContas>().As<IRepositorioDeContas>().InstancePerRequest();
            builder.RegisterType<RepositorioDeOpcoes>().As<IRepositorioDeOpcoes>().InstancePerRequest();
            builder.RegisterType<RepositorioSolicitacoes>().As<IRepositorioSolicitacoes>().InstancePerRequest();
            builder.RegisterType<RepositorioAvaliacoes>().As<IRepositorioAvaliacoes>().InstancePerRequest();
        }
    }
}
