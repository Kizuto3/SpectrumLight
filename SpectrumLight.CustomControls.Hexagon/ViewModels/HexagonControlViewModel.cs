using Prism.Commands;
using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CommonObjects.Wpf.Abstractions;
using System.Diagnostics;

namespace SpectrumLight.CustomControls.Hexagon.ViewModel
{
    public class HexagonControlViewModel : BaseViewModel
    {
        private IHexagon _hexagon;
        private IArduinoCommunicator _communicator;

        #region Public Properties

        public IHexagon Hexagon 
        {
            get => _hexagon;
            set => SetProperty(ref _hexagon, value);
        }
        #endregion

        public DelegateCommand LightOnOffCommand { get; private set; }

        public HexagonControlViewModel(IApplicationModel applicationModel,
                                       IArduinoCommunicator arduinoCommunicator,
                                       IHexagon hexagon) : base(applicationModel)
        {
            _communicator = arduinoCommunicator;
            Hexagon = hexagon;
            LightOnOffCommand = new DelegateCommand(LightOnOff);
        }

        public async void LightOnOff()
        {
            if (ApplicationModel.IsTransforming)
                return;

            Hexagon.ARGB = ApplicationModel.ARGB;
            Debug.WriteLine($"X = {Hexagon.X}, Y = {Hexagon.Y}");
            if (await _communicator.ConnectDevice())
            {
                _communicator.SendData($"{Hexagon.Index},{Hexagon.ARGB}");
            }
        }
    }
}
