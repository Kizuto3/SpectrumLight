using Prism.Commands;
using Prism.Mvvm;
using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CommonObjects.Implementations.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SpectrumLight.ViewModels
{
    public class MainWindowViewModel : BindableBase //BaseViewModel
    {
        private int _brighntess;
        public HexagonsContainer HexagonContainer { get; }

        public int Brightness
        {
            get => _brighntess;
            set => SetProperty(ref _brighntess, value);
        }

        public ObservableCollection<IHexagon> Hexagons { get => HexagonContainer.Hexagons; }

        public ObservableCollection<string> Routines { get; set; }

        public DelegateCommand LightOnOffCommand { get; }
        public DelegateCommand AddHexagonCommand { get; }

        public MainWindowViewModel(/*IApplicationModel applicationModel*/) //: base(applicationModel)
        {
            //ApplicationModel.CreateSession();
            HexagonContainer = new HexagonsContainer();
            Routines = new ObservableCollection<string>();
            Routines.Add("Option 1");
            Routines.Add("Option 2");
            Routines.Add("Option 3");
            Routines.Add("Option 4");
            Routines.Add("Settings");
            LightOnOffCommand = new DelegateCommand(LightOnOff);
            AddHexagonCommand = new DelegateCommand(AddHexagon);
        }

        private void LightOnOff()
        {
            Debug.WriteLine("Still Working!");
        }

        private void AddHexagon()
        {
            HexagonContainer.AddHexagon(0, 0, 0, 0, 0);
            HexagonContainer.AddHexagon(0, 1, 0, 0, 0);
            HexagonContainer.AddHexagon(2, 2, 0, 0, 0);
            HexagonContainer.AddHexagon(3, 1, 0, 0, 0);
        }
    }
}
