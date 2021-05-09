using Prism.Commands;
using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CommonObjects.Wpf.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectrumLight.CustomControls.Hexagon.ViewModel
{
    public class HexagonControlViewModel : BaseViewModel
    {
        private IHexagon _hexagon;
        private IArduinoCommunicator _communicator;

        public int Index 
        { 
            get => _hexagon.Index;
            set
            {
                if (_hexagon.Index != value)
                {
                    _hexagon.Index = value;
                    RaisePropertyChanged(nameof(Index));
                }
            }
        }

        public double X
        {
            get => _hexagon.X;
            set
            {
                if (_hexagon.X != value)
                {
                    _hexagon.X = value;
                    RaisePropertyChanged(nameof(X));
                }
            }
        }

        public double Y
        {
            get => _hexagon.Y;
            set
            {
                if (_hexagon.Y != value)
                {
                    _hexagon.Y = value;
                    RaisePropertyChanged(nameof(Y));
                }
            }
        }

        public double Width
        {
            get => _hexagon.Width;
            set
            {
                if (_hexagon.Width != value)
                {
                    _hexagon.Width = value;
                    RaisePropertyChanged(nameof(Width));
                }
            }
        }

        public double Height
        {
            get => _hexagon.Height;
            set
            {
                if (_hexagon.Height != value)
                {
                    _hexagon.Height = value;
                    RaisePropertyChanged(nameof(Height));
                }
            }
        }

        public byte[] ARGB
        {
            get => _hexagon.ARGB;
            set
            {
                if (_hexagon.ARGB != value)
                {
                    _hexagon.ARGB = value;
                    RaisePropertyChanged(nameof(ARGB));
                }
            }
        }

        public DelegateCommand LightOnOffCommand { get; private set; }

        public HexagonControlViewModel(IApplicationModel applicationModel,
                                       IArduinoCommunicator arduinoCommunicator,
                                       IHexagon hexagon) : base(applicationModel)
        {
            _communicator = arduinoCommunicator;
            _hexagon = hexagon;
            LightOnOffCommand = new DelegateCommand(LightOnOff);
        }

        public async void LightOnOff()
        {
            Debug.WriteLine($"X = {X}, Y = {Y}");
            if (await _communicator.ConnectDevice())
            {
                _communicator.SendData("da");
            }
        }
    }
}
