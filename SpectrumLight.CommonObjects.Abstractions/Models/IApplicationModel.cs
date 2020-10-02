using System.ComponentModel;
using System.Windows.Input;

namespace SpectrumLight.CommonObjects.Abstractions.Models
{
    public interface IApplicationModel : INotifyPropertyChanged
    {
        ICommand MinimizeCommand { get; set; }
        ICommand MaximizeCommand { get; set; }
        ICommand CloseCommand { get; set; }
    }
}
