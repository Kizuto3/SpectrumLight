using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CustomControls.HexagonsHolder;
using SpectrumLight.CustomControls.HexagonsHolder.ViewModels;
using SpectrumLight.CustomControls.RoutinesWindow.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpectrumLight.CustomControls.RoutinesWindow
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class RoutinesWindowControl : Window
    {
        private static bool _isLoaded;
        private static NotifyCollectionChangedEventHandler CollectionChangedEventHandler;
        private List<IHexagonsContainer> RoutinesList { get; set; } = new List<IHexagonsContainer>();

        public static readonly DependencyProperty RoutinesProperty = DependencyProperty.Register(nameof(Routines), typeof(ICollection<IHexagonsContainer>),
                                                                                                 typeof(RoutinesWindowControl),
                                                                                                 new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure, 
                                                                                                                               new PropertyChangedCallback(RoutinesChanged)));

        public ICollection<IHexagonsContainer> Routines
        {
            get => (ICollection<IHexagonsContainer>)GetValue(RoutinesProperty);
            set => SetValue(RoutinesProperty, value);
        }

        private static void RoutinesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (CollectionChangedEventHandler == null)
            {
                CollectionChangedEventHandler = new NotifyCollectionChangedEventHandler((sender, args) =>
                {
                    Hexagons_CollectionChanged(sender, args, d);
                });
            }

            var control = d as RoutinesWindowControl;

            if (e.OldValue != null)
            {
                var coll = e.OldValue as ObservableCollection<IHexagonsContainer>;
                coll.CollectionChanged -= CollectionChangedEventHandler;

                RemoveHexagons(control);
            }

            if (e.NewValue != null)
            {
                var coll = e.NewValue as ObservableCollection<IHexagonsContainer>;
                coll.CollectionChanged += CollectionChangedEventHandler;

                if (!_isLoaded)
                    return;

                var model = control.DataContext as RoutinesWindowControlViewModel;

                CreateAndPlaceHexagons(coll, model, control);
            }
        }

        private static void Hexagons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e, DependencyObject d)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var control = d as RoutinesWindowControl;
                var list = sender as ObservableCollection<IHexagonsContainer>;
                var hexagon = list.Last();

                var hexagonControl = CreateHexagonControl(control.Model, hexagon);

                PlaceHexagonControl(hexagonControl, control, hexagon);
            }
        }

        #region Creating, placing and removing a hexagon control / controls

        private static HexagonsHolderControl CreateHexagonControl(RoutinesWindowControlViewModel model, IHexagonsContainer container)
        {
            var hexagonsHolderControl = new HexagonsHolderControl
            {
                DataContext = new HexagonsHolderControlViewModel(model.ApplicationModel, container, null, null, true)
            };

            Binding binding = new Binding
            {
                Source = hexagonsHolderControl.Model,
                Path = new PropertyPath(nameof(hexagonsHolderControl.Model.Hexagons)),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };

            hexagonsHolderControl.SetBinding(HexagonsHolderControl.HexagonsProperty, binding);

            return hexagonsHolderControl;
        }

        private static void PlaceHexagonControl(HexagonsHolderControl hexagonHolderControl, RoutinesWindowControl holder, IHexagonsContainer container)
        {
            hexagonHolderControl.Width = 800;
            hexagonHolderControl.Height = 400;
            hexagonHolderControl.Model.HexagonsContainer = container;
            hexagonHolderControl.IsEnabled = false;

            var border = new Border
            {
                BorderThickness = new Thickness(3),
                BorderBrush = Brushes.Black,
                Child = hexagonHolderControl,
                IsEnabled = true
            };

            holder.RoutinesHolder.Children.Add(border);
            holder.RoutinesList.Add(container);

            holder.InvalidateMeasure();
            holder.InvalidateArrange();
        }

        private static void CreateAndPlaceHexagons(ICollection<IHexagonsContainer> routines, RoutinesWindowControlViewModel model, RoutinesWindowControl control)
        {
            foreach (var container in routines)
            {
                var hexagonHolderControl = CreateHexagonControl(model, container);

                PlaceHexagonControl(hexagonHolderControl, control, container);
            }
        }

        private static void RemoveHexagons(RoutinesWindowControl control)
        {
            control.RoutinesHolder.Children.Clear();
        }

        #endregion

        public RoutinesWindowControl()
        {
            InitializeComponent();

            Binding binding = new Binding
            {
                Source = Model,
                Path = new PropertyPath(nameof(Model.Routines)),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };

            SetBinding(RoutinesProperty, binding);
        }

        public RoutinesWindowControlViewModel Model { get => DataContext as RoutinesWindowControlViewModel; }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Model.CancelCommand.Execute();
            //this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _isLoaded = true;

            if (RoutinesList == null)
                return;

            CreateAndPlaceHexagons(RoutinesList, Model, this);
        }
    }
}
