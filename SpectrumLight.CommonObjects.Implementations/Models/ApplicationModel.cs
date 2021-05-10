using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CommonObjects.Wpf.Abstractions;
using System;
using System.Windows.Input;

namespace SpectrumLight.CommonObjects.Implementations.Models
{
    public class ApplicationModel : BaseModel, IApplicationModel
    {
        private bool _isTransforming;

        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public byte[] ARGB { get; set; }
        public bool IsTransforming
        {
            get => _isTransforming;
            set
            {
                _isTransforming = value;
                RaisePropertyChanged(nameof(IsTransforming));
            }
        }
    }
}
