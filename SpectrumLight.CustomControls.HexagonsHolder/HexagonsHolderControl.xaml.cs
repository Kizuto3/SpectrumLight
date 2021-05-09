using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.CustomControls.Hexagon;
using SpectrumLight.CustomControls.Hexagon.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SpectrumLight.CustomControls.HexagonsHolder
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class HexagonsHolderControl : UserControl
    {
        private static double SIZE = 100;
        private static double SQRT3D2 = Math.Sqrt(3) / 2.0;
        private static double PADDING = 0.53;
        private double _sizeCoef = 0;
        private double _shiftX = 0;
        private double _shiftY = 0;

        private List<HexagonControl> HexagonControls { get; set; } = new List<HexagonControl>();

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var hex = HexagonControls.FirstOrDefault();

            var size = hex == null ? SIZE : hex.ActualWidth == 0 ? SIZE : hex.ActualWidth;

            double value = size * _sizeCoef;

            var centerX = 0; //(arrangeBounds.Width - value) / 2;
            var centerY = 0; //(arrangeBounds.Height - 2 * value) / 2;

            var shiftX = _shiftX < 0 ? Math.Abs(_shiftX) * _sizeCoef : 0;
            var shiftY = _shiftY < 0 ? Math.Abs(_shiftY) * _sizeCoef : 0;

            foreach (var hexagon in HexagonControls)
            {
                var x = PADDING * (1.5 * hexagon.X + 1) * value + centerX + shiftX;
                var y = PADDING * (2 * hexagon.Y + hexagon.X + 0.5) * value * SQRT3D2 + centerY + shiftY;

                hexagon.Width = value;
                hexagon.Height = value;

                Canvas.SetLeft(hexagon, x);
                Canvas.SetTop(hexagon, y);
            }

            return base.ArrangeOverride(arrangeBounds);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            //Measure the children...
            foreach (UIElement child in Holder.Children)
                child.Measure(constraint);

            double left = Double.MaxValue;
            double top = Double.MaxValue;
            double rigth = Double.MinValue;
            double bottom = Double.MinValue;

            var hex = HexagonControls.FirstOrDefault();

            var size = hex == null ? SIZE : hex.ActualWidth == 0 ? SIZE : hex.ActualWidth;

            foreach (var hexagon in HexagonControls)
            {
                var x = PADDING * (1.5 * hexagon.X + 1) * size;
                var y = PADDING * (2 * hexagon.Y + hexagon.X + 0.5) * size * SQRT3D2;

                if (top > y)
                {
                    top = y;
                }
                if (left > x)
                {
                    left = x;
                }
                if (rigth < x)
                {
                    rigth = x;
                }
                if (bottom < y)
                {
                    bottom = y;
                }
            }

            _shiftX = left;
            _shiftY = top;

            var requiredHeight = bottom + (size * 1.2) - top;
            var requiredWidth = rigth + (size * 1.7) - left;

            _sizeCoef = Math.Min(constraint.Width / requiredWidth, constraint.Height / requiredHeight);
            return base.MeasureOverride(constraint);  //The panel should take all the available space...
        }

        public static readonly DependencyProperty HexagonsProperty = DependencyProperty.Register(nameof(Hexagons), typeof(ICollection<IHexagon>),
            typeof(HexagonsHolderControl),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(HexagonsListChanged)));

        public ICollection<IHexagon> Hexagons
        {
            get => (ICollection<IHexagon>)GetValue(HexagonsProperty);
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
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                var control = d as HexagonsHolderControl;
                var list = sender as ObservableCollection<IHexagon>;
                var hexagon = list.Last();

                var hexagonControl = CreateHexagonControl();

                hexagonControl.Width = SIZE;
                hexagonControl.Height = SIZE;
                hexagonControl.X = hexagon.X;
                hexagonControl.Y = hexagon.Y;
                hexagonControl.Index = hexagon.Index.ToString();
                hexagonControl.Model.ARGB = hexagon.ARGB;

                control.HexagonControls.Add(hexagonControl);
                control.Holder.Children.Add(hexagonControl);

                control.InvalidateMeasure();
                control.InvalidateArrange();
            }
        }

        private static HexagonControl CreateHexagonControl()
        {
            var hexagonControl = new HexagonControl();

            Binding binding = new Binding
            {
                Source = hexagonControl.Model,
                Path = new PropertyPath(nameof(hexagonControl.Model.Width)),
                Mode = BindingMode.TwoWay
            };

            hexagonControl.SetBinding(HexagonControl.WidthProperty, binding);

            binding = new Binding
            {
                Source = hexagonControl.Model,
                Path = new PropertyPath(nameof(hexagonControl.Model.Height)),
                Mode = BindingMode.TwoWay
            };

            hexagonControl.SetBinding(HexagonControl.HeightProperty, binding);

            binding = new Binding
            {
                Source = hexagonControl.Model,
                Path = new PropertyPath(nameof(hexagonControl.Model.X)),
                Mode = BindingMode.TwoWay
            };

            hexagonControl.SetBinding(HexagonControl.XProperty, binding);

            binding = new Binding
            {
                Source = hexagonControl.Model,
                Path = new PropertyPath(nameof(hexagonControl.Model.Y)),
                Mode = BindingMode.TwoWay
            };

            hexagonControl.SetBinding(HexagonControl.YProperty, binding);

            binding = new Binding
            {
                Source = hexagonControl.Model,
                Path = new PropertyPath(nameof(hexagonControl.Model.Index)),
                Mode = BindingMode.TwoWay
            };

            hexagonControl.SetBinding(HexagonControl.IndexProperty, binding);

            return hexagonControl;
        }
    }
}
