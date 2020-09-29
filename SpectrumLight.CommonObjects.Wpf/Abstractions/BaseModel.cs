using CommonServiceLocator;
using Prism.Mvvm;
using SpectrumLight.CommonObjects.Abstractions;
using System;
using System.ComponentModel;
using Unity;

namespace SpectrumLight.CommonObjects.Wpf.Abstractions
{
    public abstract class BaseModel : BindableBase, IBaseInfrastructure
    {
        private readonly IUnityContainer _container;

        public ISpectrumLightFactory SpectrumLightFactory { get; }

        protected BaseModel()
        {
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>();

            SpectrumLightFactory = Resolve<ISpectrumLightFactory>();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        protected virtual void OnChildModelPropertyChanged(object sender, PropertyChangedEventArgs args) => RaisePropertyChanged(args.PropertyName);

    }
}
