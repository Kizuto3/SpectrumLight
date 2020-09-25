using SpectrumLight.Views;
using Prism.Ioc;
using System.Windows;
using SpectrumLight.DependencyInjection;

namespace SpectrumLight
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            DiHelpers.RegisterApplicationTypes(containerRegistry);
        }
    }
}
