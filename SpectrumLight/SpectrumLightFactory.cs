using SpectrumLight.CommonObjects.Abstractions;
using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CommonObjects.Implementations.Models;
using Unity;

namespace SpectrumLight
{
    public class SpectrumLightFactory : ISpectrumLightFactory
    {
        public IUnityContainer ApplicationContainer { get; }

        public SpectrumLightFactory(IUnityContainer applicationContainer)
        {
            ApplicationContainer = applicationContainer;
        }


        public void RegisterTypes()
        {
            ApplicationContainer.RegisterType<IHexagon, Hexagon>();
            ApplicationContainer.RegisterType<IHexagonsContainer, HexagonsContainer>();
            ApplicationContainer.RegisterType<IArduinoCommunicator, ArduinoCommunicator>();
        }
    }
}
