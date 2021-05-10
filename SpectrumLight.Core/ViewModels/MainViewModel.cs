using Prism.Commands;
using SpectrumLight.CommonObjects.Abstractions.Enums;
using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CommonObjects.Implementations.Helpers;
using SpectrumLight.CommonObjects.Wpf.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SpectrumLight.Core.ViewModels
{
    public class MainViewModel : BaseViewModel, IDisposable
    {
        private static int i = 0;
        private int _brighntess;
        private double _storedScale;
        private double _storedRotation;
        private double[] _storedTranslation = new double[] { 0.0, 0.0 };
        private double _scale;
        private double _rotation;
        private double[] _translation = new double[] { 0.0, 0.0};
        private byte[] _color;
        private IArduinoCommunicator _arduinoCommunicator;

        #region Public Properties

        public int Brightness
        {
            get => _brighntess;
            set
            {
                SetProperty(ref _brighntess, value);
                ApplicationModel.ARGB = new byte[] { Convert.ToByte(value), Color[1], Color[2], Color[3] };
            }
        }

        public byte[] Color
        {
            get => _color;
            set 
            { 
                SetProperty(ref _color, value);
                ApplicationModel.ARGB = new byte[] { Convert.ToByte(_brighntess),  value[1], value[2], value[3] };
            }
        }

        public double Scale
        {
            get => _scale;
            set => SetProperty(ref _scale, value);
        }

        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }

        public double TranslateX
        {
            get => _translation[0];
            set => SetProperty(ref _translation[0], value);
        }

        public double TranslateY
        {
            get => _translation[1];
            set => SetProperty(ref _translation[1], value);
        }

        public IHexagonsContainer HexagonContainer { get; }

        public ObservableCollection<IHexagon> Hexagons { get => HexagonContainer.Hexagons; }

        public ObservableCollection<string> Routines { get; set; }

        #endregion

        #region Commands

        public DelegateCommand AddHexagonCommand { get; }
        public DelegateCommand StartTransformingCommand { get; }
        public DelegateCommand CancelTransformingCommand { get; }

        #endregion

        #region Constructor

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
            Brightness = 255;
            Scale = 1;
            ApplicationModel.IsTransforming = false;

            AddHexagonCommand = new DelegateCommand(AddHexagon);
            StartTransformingCommand = new DelegateCommand(StartTransforming);
            CancelTransformingCommand = new DelegateCommand(CancelTransforming);
        }

        #endregion

        #region Private Methods

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

        private void StartTransforming()
        {
            ApplicationModel.IsTransforming = !ApplicationModel.IsTransforming;

            _storedRotation = Rotation;
            _storedScale = Scale;
            _storedTranslation = new double[] { TranslateX, TranslateY };

            AddHexagon();
        }

        private void CancelTransforming()
        {
            Rotation = _storedRotation;
            Scale = _storedScale;
            TranslateX = _storedTranslation[0];
            TranslateY = _storedTranslation[1];
        }

        #endregion

        public void Dispose()
        {
            _arduinoCommunicator.DisconnectDevice();
        }
    }
}
