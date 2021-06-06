using Prism.Commands;
using Prism.Events;
using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CommonObjects.EventAggregators;
using SpectrumLight.CommonObjects.Implementations.Models;
using SpectrumLight.CommonObjects.Wpf.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectrumLight.CustomControls.RoutinesWindow.ViewModels
{
    public class RoutinesWindowControlViewModel : BaseViewModel
    {
        private int i = 0;
        private IEventAggregator _eventAggregator;
        private IHexagonsContainer hexagonsContainer;

        public ObservableCollection<IHexagonsContainer> Routines { get; }

        public DelegateCommand CancelCommand { get; }

        public RoutinesWindowControlViewModel(IApplicationModel applicationModel,
                                              IEventAggregator eventAggregator) : base(applicationModel)
        {
            _eventAggregator = eventAggregator;

            CancelCommand = new DelegateCommand(Cancel);

            Routines = new ObservableCollection<IHexagonsContainer>();

            hexagonsContainer = new HexagonsContainer();

            //var container2 = new HexagonsContainer();

            //AddHexagon(container2);

            //Routines.Add(container2);

            //var container3 = new HexagonsContainer();

            //AddHexagon(container3);

            //Routines.Add(container3);
        }

        private void AddHexagon(IHexagonsContainer container)
        {
            container.AddHexagon(i, 1, 0, 0, i + 0, ApplicationModel.ARGB);
            container.AddHexagon(i, 3, 0, 0, i + 1, ApplicationModel.ARGB);
            container.AddHexagon(i, 5, 0, 0, i + 2, ApplicationModel.ARGB);
        }

        private void Cancel()
        {
            AddHexagon(hexagonsContainer);

            Routines.Add(hexagonsContainer);
            //_eventAggregator.GetEvent<MainContainerChangedEvent>().Publish(null);
        }

        private void SelectionChanged()
        {
            //_eventAggregator.GetEvent<MainContainerChangedEvent>().Publish(_hexagonsContainer);
        }
    }
}
