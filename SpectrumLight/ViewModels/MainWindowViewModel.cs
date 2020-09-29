using Prism.Commands;
using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CommonObjects.Implementations.Models;
using SpectrumLight.CommonObjects.Wpf.Abstractions;
using System.Diagnostics;

namespace SpectrumLight.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public HexagonsContainer HexagonContainer { get; }

        public DelegateCommand LightOnOffCommand { get; }

        public MainWindowViewModel(IApplicationModel applicationModel) : base (applicationModel)
        {
            ApplicationModel.CreateSession();
            HexagonContainer = new HexagonsContainer();
            LightOnOffCommand = new DelegateCommand(LightOnOff);
            HexagonContainer.AddHexagon(0, 0, 0, 0, 0);
            HexagonContainer.AddHexagon(0, 0, 0, 0, 0);
            HexagonContainer.AddHexagon(0, 0, 0, 0, 0);
            HexagonContainer.AddHexagon(0, 0, 0, 0, 0);
        }

        private void LightOnOff()
        {
            Debug.WriteLine("Still Working!");
        }
    }
}
