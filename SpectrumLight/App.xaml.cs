using SpectrumLight.Views;
using Prism.Ioc;
using System.Windows;
using SpectrumLight.DependencyInjection;
using Prism.Unity;
using Prism.Mvvm;
using SpectrumLight.CustomControls.Hexagon;
using SpectrumLight.CustomControls.Hexagon.ViewModel;
using SpectrumLight.ViewModels;

namespace SpectrumLight
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);
            DiHelpers.RegisterApplicationTypes(containerRegistry);
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<MainWindow, MainViewModel>();
            ViewModelLocationProvider.Register<HexagonControl, HexagonControlViewModel>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
