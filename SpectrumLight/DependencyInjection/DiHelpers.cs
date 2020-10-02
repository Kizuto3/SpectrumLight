using Prism.Ioc;
using Prism.Unity;
using SpectrumLight.CommonObjects.Abstractions;
using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CommonObjects.Implementations.Models;
using SpectrumLight.ViewModels;
using Unity;

namespace SpectrumLight.DependencyInjection
{
    public static class DiHelpers
    {
        public static void RegisterApplicationTypes(IContainerRegistry containerProvider)
        {
            var container = containerProvider.GetContainer();

            container.RegisterSingleton<IApplicationModel, ApplicationModel>();
            container.RegisterType<IHexagon, Hexagon>();
            container.RegisterType<IHexagonsContainer, HexagonsContainer>();
            container.RegisterType<IArduinoCommunicator, ArduinoCommunicator>();
        }
    }
}
