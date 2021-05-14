using SpectrumLight.Views;
using Prism.Ioc;
using System.Windows;
using SpectrumLight.Core.DependencyInjection;
using Prism.Unity;
using Prism.Mvvm;
using SpectrumLight.Core.ViewModels;
using SpectrumLight.CustomControls.ConnectionsBar;
using SpectrumLight.CustomControls.ConnectionsBar.ViewModel;
using SpectrumLight.CustomControls.HexagonsHolder;
using SpectrumLight.CustomControls.HexagonsHolder.ViewModels;

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
            ViewModelLocationProvider.Register<HexagonsHolderControl, HexagonsHolderControlViewModel>();
            ViewModelLocationProvider.Register<ConnectionsBarControl, ConnectionsBarControlViewModel>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
