using Prism.Commands;
using SpectrumLight.CommonObjects.Abstractions.Enums;
using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CommonObjects.Implementations.Helpers;
using SpectrumLight.CommonObjects.Wpf.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SpectrumLight.ViewModels
{
    public class MainWindowViewModel : BaseViewModel, IDisposable
    {
        private static int i = 0;
        private int _brighntess;

        public int Brightness
        {
            get => _brighntess;
            set => SetProperty(ref _brighntess, value);
        }

        private IArduinoCommunicator ArduinoCommunicator { get; }
        public IHexagonsContainer HexagonContainer { get; }

        public ObservableCollection<IHexagon> Hexagons { get => HexagonContainer.Hexagons; }
        public ObservableCollection<string> Routines { get; set; }
        public DelegateCommand AddHexagonCommand { get; }

        public MainWindowViewModel(IApplicationModel applicationModel, 
                                   IHexagonsContainer hexagonsContainer,
                                   IArduinoCommunicator arduinoCommunicator) : base(applicationModel)
        {
            HexagonContainer = hexagonsContainer;
            ArduinoCommunicator = arduinoCommunicator;
            Routines = new ObservableCollection<string>
            {
                "Option 1",
                "Option 2",
                "Option 3",
                "Option 4",
                "Settings"
            };
            FindSpectrumLightPortAndConnect();

            AddHexagonCommand = new DelegateCommand(AddHexagon);
        }

        private async void FindSpectrumLightPortAndConnect()
        {
            int failedAttempt = 0;
            await Task.Run(() =>
            {
                BluetoothComPortsSeeker.GetBluetoothCOMPort().Wait();
                var bluetoothPorts = BluetoothComPortsSeeker.GetBluetoothCOMPort().Result;

                ArduinoCommunicator.ComPort = bluetoothPorts.Where(p => p.DirectionType == DirectionType.Outgoing && p.Port.Contains("SpectrumLight")).FirstOrDefault();

                if(ArduinoCommunicator.ComPort == null)
                {
                    //TODO: do something if spectrumLight port was't found
                    return;
                }

                ArduinoCommunicator.ComPort.DataReceived += ComPort_DataReceived;
                ArduinoCommunicator.ConnectDevice().Wait();

                while(ArduinoCommunicator.ConnectDevice().Result != true)
                {
                    ArduinoCommunicator.ConnectDevice().Wait();
                    failedAttempt++;
                    if(failedAttempt >= 5)
                    {
                        //TODO: do something if failed to connect
                        return;
                    }
                }

            });
        }

        private void ComPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //TODO: you know what to do here
        }

        private void AddHexagon()
        {
            var argb = new byte[] { 0xff, 0x00, 0x00, 0x00 };
            HexagonContainer.AddHexagon(i, 0, 0, 0, i + 0, argb);
            HexagonContainer.AddHexagon(i, 1, 0, 0, i + 1, argb);
            HexagonContainer.AddHexagon(i, 2, 0, 0, i + 2, argb);
            HexagonContainer.AddHexagon(i++, 3, 0, 0, i + 2, argb);
        }

        public void Dispose()
        {
            ArduinoCommunicator.DisconnectDevice();
        }
    }
}
