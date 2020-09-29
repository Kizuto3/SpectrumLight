using SpectrumLight.CommonObjects.Abstractions.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SpectrumLight.CustomControls.HexagonsHolder
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class HexagonsHolderControl : UserControl
    {
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            return base.ArrangeOverride(arrangeBounds);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        public static readonly DependencyProperty LightOnOffComand = DependencyProperty.Register(nameof(LightOnOff), typeof(ICommand),
            typeof(HexagonsHolderControl), new PropertyMetadata(null));

        public ICommand LightOnOff
        {
            get => (ICommand)GetValue(LightOnOffComand);
            set => SetValue(LightOnOffComand, value);
        }

        public static readonly DependencyProperty HexagonsProperty = DependencyProperty.Register(nameof(Hexagons), typeof(ObservableCollection<IHexagon>),
            typeof(HexagonsHolderControl),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange, new PropertyChangedCallback(HexagonsListChanged)));

        public ObservableCollection<IHexagon> Hexagons
        {
            get => (ObservableCollection<IHexagon>)GetValue(HexagonsProperty);
            set => SetValue(HexagonsProperty, value);
        }

        private static void HexagonsListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as HexagonsHolderControl;

            if(e.NewValue != null)
            {
                var coll = e.OldValue as INotifyCollectionChanged;
                coll.CollectionChanged -= (s, e) => Hexagons_CollectionChanged(s, e, d);
            }

            if(e.NewValue != null)
            {
                var coll = e.NewValue as ObservableCollection<IHexagon>;
                coll.CollectionChanged += (s, e) => Hexagons_CollectionChanged(s, e, d);
            }
        }

        public HexagonsHolderControl()
        {
            InitializeComponent();
        }

        private static void Hexagons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e, DependencyObject d)
        {
            var control = d as HexagonsHolderControl;
        }
    }
}
