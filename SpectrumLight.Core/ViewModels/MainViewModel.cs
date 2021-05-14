using Prism.Commands;
using SpectrumLight.CommonObjects.Abstractions.Enums;
using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CommonObjects.Implementations.Helpers;
using SpectrumLight.CommonObjects.Implementations.Models;
using SpectrumLight.CommonObjects.Wpf.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SpectrumLight.Core.ViewModels
{
    public class MainViewModel : BaseViewModel, IDisposable
    {
        private int _brighntess;
        private byte[] _color;

        #region Public Properties

        public IArduinoCommunicator ArduinoCommunicator;

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
                ApplicationModel.ARGB = new byte[] { Convert.ToByte(_brighntess), value[1], value[2], value[3] };
            }
        }

        #endregion

        #region Commands

        public DelegateCommand AddHexagonCommand { get; }

        public DelegateCommand StartTransformingCommand { get; }

        public DelegateCommand CancelTransformingCommand { get; }

        #endregion

        #region Constructor

        public MainViewModel(IApplicationModel applicationModel, 
                             IArduinoCommunicator arduinoCommunicator) : base(applicationModel)
        {
            ArduinoCommunicator = arduinoCommunicator;

            Color = new byte[] { 0xff, 0x00, 0x00, 0x00 };
            Brightness = 255;
            ApplicationModel.IsTransforming = false;

            StartTransformingCommand = new DelegateCommand(StartTransforming);
            CancelTransformingCommand = new DelegateCommand(CancelTransforming);
        }

        #endregion

        #region Private Methods

        private void StartTransforming()
        {
            ApplicationModel.IsTransforming = !ApplicationModel.IsTransforming;
        }

        private void CancelTransforming()
        {
        }

        #endregion

        public void Dispose()
        {
            ArduinoCommunicator.DisconnectDevice();
        }
    }
}
