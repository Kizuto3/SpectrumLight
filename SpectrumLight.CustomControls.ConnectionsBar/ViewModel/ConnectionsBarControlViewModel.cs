using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CommonObjects.Wpf.Abstractions;
using System.Net.NetworkInformation;

namespace SpectrumLight.CustomControls.ConnectionsBar.ViewModel
{
    public class ConnectionsBarControlViewModel : BaseViewModel
    {
        public  IArduinoCommunicator ArduinoCommunicator;

        public bool IsConnected
        {
            get => ArduinoCommunicator.IsConnected;
        }

        public string ConnectionName
        {
            get => ArduinoCommunicator.ConnectionName;
        }

        public ConnectionsBarControlViewModel(IApplicationModel applicationModel,
                                              IArduinoCommunicator arduinoCommunicator) : base(applicationModel)
        {
            ArduinoCommunicator = arduinoCommunicator;
            ArduinoCommunicator.ConnectDevice();
        }
    }
}
