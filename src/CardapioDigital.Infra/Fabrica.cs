using System;
using System.Reflection;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CardapioDigital.Dominio.Core;
using CardapioDigital.Infra.ModulosInjecaoDependencia;

namespace CardapioDigital.Infra
{
    public class Fabrica
    {
        private static readonly object _lockObject = new object();

        private readonly ContainerBuilder _containerBuilder;

        private IContainer _autofacContainer;
        public IContainer AutofacContainer
        {
            get
            {
                if (_autofacContainer == null)
                {
                    lock (_lockObject)
                         _autofacContainer = _containerBuilder.Build();
                }

                return _autofacContainer;
            }
        }

        private static Fabrica _instancia;
        public static Fabrica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_lockObject)
                        _instancia = new Fabrica();
                }

                return _instancia;
            }
        }

        private Fabrica()
        {
            _containerBuilder = new ContainerBuilder();
            _containerBuilder.RegisterModule<ModuloInjecaoDependenciaPersistencia>();
            _containerBuilder.RegisterModule<ModuloInjecaoDependenciaAplicacao>();
        }

        public IDependencyResolver WebApiDependencyResolver
        {
            get { return new AutofacWebApiDependencyResolver(AutofacContainer); }
        }

        public void RegistrarControllersMvc(Assembly assemblyControllersMvc)
        {
            if (_autofacContainer != null)
                throw new InvalidOperationException("Não é possível registrar módulos após o container ter sido construído!");

            _containerBuilder.RegisterControllers(assemblyControllersMvc);
        }

        public void RegistrarControllersApi(Assembly assemblyControllersApi)
        {
            if (_autofacContainer != null)
                throw new InvalidOperationException("Não é possível registrar módulos após o container ter sido construído!");

            _containerBuilder.RegisterApiControllers(assemblyControllersApi);
        }

        public T Obter<T>()
        {
            return AutofacContainer.Resolve<T>();
        }

        public T Obter<T>(IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            return AutofacContainer.Resolve<T>(new NamedParameter("unidadeDeTrabalho", unidadeDeTrabalho));
        }
    }
}
