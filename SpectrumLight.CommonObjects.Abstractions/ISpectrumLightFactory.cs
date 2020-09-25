using Unity;

namespace SpectrumLight.CommonObjects.Abstractions
{
    public interface ISpectrumLightFactory
    {
        IUnityContainer ApplicationContainer { get; }

        void RegisterTypes();
    }
}
