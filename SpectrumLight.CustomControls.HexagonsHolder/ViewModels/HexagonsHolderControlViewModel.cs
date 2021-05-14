﻿using SpectrumLight.CommonObjects.Abstractions.Enums;
using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CommonObjects.Implementations.Helpers;
using SpectrumLight.CommonObjects.Wpf.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectrumLight.CustomControls.HexagonsHolder.ViewModels
{
    public class HexagonsHolderControlViewModel : BaseViewModel
    {
        private int i = 0;
        private bool _isApplyColor;
        private double _storedScale;
        private double _storedRotation;
        private double[] _storedTranslation = new double[] { 0.0, 0.0 };
        private double _scale;
        private double _rotation;
        private double[] _translation = new double[] { 0.0, 0.0 };
        private IApplicationModel _applicationModel;

        public IArduinoCommunicator ArduinoCommunicator { get; }

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

        public bool IsApplyColor
        {
            get => _isApplyColor;
            set
            {
                if (value)
                {
                    HexagonsContainer.ApplyColor(ApplicationModel.ARGB);
                }

                SetProperty(ref _isApplyColor, value);
            }
        }

        public IHexagonsContainer HexagonsContainer { get; }

        public ObservableCollection<IHexagon> Hexagons { get => HexagonsContainer.Hexagons; }

        public HexagonsHolderControlViewModel(IApplicationModel applicationModel,
                                              IHexagonsContainer hexagonContainer,
                                              IArduinoCommunicator arduinoCommunicator) : base(applicationModel)
        {
            _applicationModel = applicationModel;
            HexagonsContainer = hexagonContainer;
            ArduinoCommunicator = arduinoCommunicator;

            Scale = 1;

            FindSpectrumLightPortAndConnect();

            AddHexagon();
            AddHexagon();
            AddHexagon();
            AddHexagon();
        }

        private async void FindSpectrumLightPortAndConnect()
        {
            int failedAttempt = 0;
            await Task.Run(() =>
            {
                BluetoothComPortsSeeker.GetBluetoothCOMPort().Wait();
                var bluetoothPorts = BluetoothComPortsSeeker.GetBluetoothCOMPort().Result;

                ArduinoCommunicator.ComPort = bluetoothPorts.Where(p => p.DirectionType == DirectionType.Outgoing && p.Port.Contains("SpectrumLight")).FirstOrDefault();

                if (ArduinoCommunicator.ComPort == null)
                {
                    //TODO: do something if spectrumLight port was't found
                    return;
                }

                ArduinoCommunicator.ComPort.DataReceived += ComPort_DataReceived;
                ArduinoCommunicator.ConnectDevice().Wait();

                while (ArduinoCommunicator.ConnectDevice().Result != true)
                {
                    ArduinoCommunicator.ConnectDevice().Wait();
                    failedAttempt++;
                    if (failedAttempt >= 5)
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
            HexagonsContainer.AddHexagon(i, 0, 0, 0, i + 0, _applicationModel.ARGB);
            HexagonsContainer.AddHexagon(i, 1, 0, 0, i + 1, _applicationModel.ARGB);
            HexagonsContainer.AddHexagon(i, 2, 0, 0, i + 2, _applicationModel.ARGB);
            HexagonsContainer.AddHexagon(i++, 3, 0, 0, i + 2, _applicationModel.ARGB);
        }
    }
}