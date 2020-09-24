using Prism.Commands;
using Prism.Mvvm;
using System.Diagnostics;

namespace SpectrumLight.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand LightOnOffCommand { get; }

        public MainWindowViewModel()
        {
            LightOnOffCommand = new DelegateCommand(LightOnOff);
        }

        private void LightOnOff()
        {
            Debug.WriteLine("Still Working!");
        }
    }
}
