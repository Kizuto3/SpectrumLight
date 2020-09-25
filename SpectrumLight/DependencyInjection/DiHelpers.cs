using Prism.Ioc;
using Prism.Unity;
using SpectrumLight.CommonObjects.Abstractions;
using Unity;

namespace SpectrumLight.DependencyInjection
{
    public static class DiHelpers
    {
        public static void RegisterApplicationTypes(IContainerRegistry containerRegistry)
        {
            var container = containerRegistry.GetContainer();

            container.RegisterSingleton<ISpectrumLightFactory, SpectrumLightFactory>();
        }
    }
}
