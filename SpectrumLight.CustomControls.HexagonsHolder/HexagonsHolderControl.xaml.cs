using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CustomControls.Hexagon;
using System;
using System.Collections.Generic;
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
        private static double SIZE = 30;
        private static double SQRT3D2 = Math.Sqrt(3) / 2.0;
        private static double PADDING = 0.53;

        private List<HexagonControl> HexagonControls { get; set; } = new List<HexagonControl>();
        
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var width = Holder.RenderSize.Width;
            var height = Holder.RenderSize.Height;
            var shiftX = (width - SIZE) / 2;
            var shiftY = (height - 2 * SIZE) / 2;

            foreach (var hexagon in HexagonControls)
            {
                var x = PADDING * (1.5 * hexagon.X + 1) * SIZE + shiftX;
                var y = PADDING * (2 * hexagon.Y + hexagon.X + 0.5) * SIZE * SQRT3D2 + shiftY;

                Canvas.SetLeft(hexagon, x);
                Canvas.SetTop(hexagon, y);
            }

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

            if(e.OldValue != null)
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
            var list = sender as ObservableCollection<IHexagon>;
            var hexagon = list.Last();

            var width = control.Holder.RenderSize.Width;
            var height = control.Holder.RenderSize.Height;
            var shiftX = (width - SIZE) / 2;
            var shiftY = (height - 2 * SIZE) / 2;

            var x = PADDING * (1.5 * hexagon.X + 1) * SIZE + shiftX;
            var y = PADDING * (2 * hexagon.Y + hexagon.X + 0.5) * SIZE * SQRT3D2 + shiftY;

            var hexagonControl = new HexagonControl() 
            {
                Width = SIZE,
                Height = SIZE,
                X = hexagon.X,
                Y = hexagon.Y
            };
            Canvas.SetLeft(hexagonControl, x);
            Canvas.SetTop(hexagonControl, y);

            control.HexagonControls.Add(hexagonControl);
            control.Holder.Children.Add(hexagonControl);
        }
    }
}
