using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace SpectrumLight.CommonObjects.Abstractions
{
    public abstract class BaseInfrastructure : IBaseInfrastructure
    {
        private static readonly IUnityContainer Container;

        public ISpectrumLightFactory SpectrumLightFactory { get; }

        static BaseInfrastructure()
        {
            Container = ServiceLocator.Current.GetInstance<IUnityContainer>();
        }

        protected BaseInfrastructure()
        {
            SpectrumLightFactory = Resolve<ISpectrumLightFactory>();
        }

        public T Resolve<T>()
        {
            return ResolveStatic<T>();
        }

        protected static T ResolveStatic<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
