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
    public class MainViewModel : BaseViewModel, IDisposable
    {
        private static int i = 0;
        private int _brighntess;
        private byte[] _color;
        private IArduinoCommunicator _arduinoCommunicator;

        public int Brightness
        {
            get => _brighntess;
            set => SetProperty(ref _brighntess, value);
        }

        public byte[] Color
        {
            get => _color;
            set 
            { 
                SetProperty(ref _color, value);
                ApplicationModel.ARGB = value;
            }
        }

        public IHexagonsContainer HexagonContainer { get; }

        public ObservableCollection<IHexagon> Hexagons { get => HexagonContainer.Hexagons; }
        public ObservableCollection<string> Routines { get; set; }

        public DelegateCommand AddHexagonCommand { get; }

        public MainViewModel(IApplicationModel applicationModel, 
                             IHexagonsContainer hexagonsContainer,
                             IArduinoCommunicator arduinoCommunicator) : base(applicationModel)
        {
            _arduinoCommunicator = arduinoCommunicator;
            HexagonContainer = hexagonsContainer;
            Routines = new ObservableCollection<string>
            {
                "Option 1",
                "Option 2",
                "Option 3",
                "Option 4",
                "Settings"
            };
            FindSpectrumLightPortAndConnect();

            Color = new byte[] { 0xff, 0x00, 0x00, 0x00 };

            AddHexagonCommand = new DelegateCommand(AddHexagon);
        }

        private async void FindSpectrumLightPortAndConnect()
        {
            int failedAttempt = 0;
            await Task.Run(() =>
            {
                BluetoothComPortsSeeker.GetBluetoothCOMPort().Wait();
                var bluetoothPorts = BluetoothComPortsSeeker.GetBluetoothCOMPort().Result;

                _arduinoCommunicator.ComPort = bluetoothPorts.Where(p => p.DirectionType == DirectionType.Outgoing && p.Port.Contains("SpectrumLight")).FirstOrDefault();

                if(_arduinoCommunicator.ComPort == null)
                {
                    //TODO: do something if spectrumLight port was't found
                    return;
                }

                _arduinoCommunicator.ComPort.DataReceived += ComPort_DataReceived;
                _arduinoCommunicator.ConnectDevice().Wait();

                while(_arduinoCommunicator.ConnectDevice().Result != true)
                {
                    _arduinoCommunicator.ConnectDevice().Wait();
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
            HexagonContainer.AddHexagon(i, 0, 0, 0, i + 0, Color);
            HexagonContainer.AddHexagon(i, 1, 0, 0, i + 1, Color);
            HexagonContainer.AddHexagon(i, 2, 0, 0, i + 2, Color);
            HexagonContainer.AddHexagon(i++, 3, 0, 0, i + 2, Color);
        }

        public void Dispose()
        {
            _arduinoCommunicator.DisconnectDevice();
        }
    }
}
