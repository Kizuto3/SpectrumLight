using SpectrumLight.CommonObjects.Abstractions.Models;
using SpectrumLight.Core.ViewModels;
using SpectrumLight.CustomControls.Hexagon;
using SpectrumLight.CustomControls.Hexagon.ViewModel;
using SpectrumLight.CustomControls.HexagonsHolder.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace SpectrumLight.CustomControls.HexagonsHolder
{
    /// <summary>
    /// Логика взаимодействия для HexagonsHolderControl.xaml
    /// </summary>
    public partial class HexagonsHolderControl : UserControl
    {
        private static double SIZE = 70;
        private static double SQRT3D2 = Math.Sqrt(3) / 2.0;
        private static double PADDING = 0.53;
        private double _sizeCoef = 0;
        private double _shiftX = 0;
        private double _shiftY = 0;
        private bool _isMoving;
        private Point? _mouseStart;
        private Point? _controlPosition;
        private List<HexagonControl> HexagonControls { get; set; } = new List<HexagonControl>();

        #region Overrides

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

        #endregion

        #region Dependency Properties

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
            if(e.NewValue != null)
            {
                var coll = e.NewValue as ObservableCollection<IHexagon>;
                coll.CollectionChanged += (s, e) => Hexagons_CollectionChanged(s, e, d);
            }
        }

        private static void Hexagons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e, DependencyObject d)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var control = d as HexagonsHolderControl;
                var list = sender as ObservableCollection<IHexagon>;
                var hexagon = list.Last();

                var hexagonControl = CreateHexagonControl(control.Model, hexagon);

                PlaceHexagonControl(hexagonControl, control, hexagon);
            }
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(nameof(Scale), typeof(double), typeof(HexagonsHolderControl), new PropertyMetadata(0d));

        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        public static readonly DependencyProperty RotationProperty = DependencyProperty.Register(nameof(Rotation), typeof(double), typeof(HexagonsHolderControl), new PropertyMetadata(0d));

        public double Rotation
        {
            get => (double)GetValue(RotationProperty);
            set => SetValue(RotationProperty, value);
        }

        public static readonly DependencyProperty IsApplyColorProperty = DependencyProperty.Register(nameof(IsApplyColor), typeof(bool), typeof(HexagonsHolderControl), new PropertyMetadata(false));

        public bool IsApplyColor
        {
            get => (bool)GetValue(IsApplyColorProperty);
            set => SetValue(IsApplyColorProperty, value);
        }

        #endregion

        public HexagonsHolderControl()
        {
            InitializeComponent();
        }

        public HexagonsHolderControlViewModel Model { get => DataContext as HexagonsHolderControlViewModel; }

        #region Creating and placing a hexagon control

        private static HexagonControl CreateHexagonControl(HexagonsHolderControlViewModel mainViewModel, IHexagon hexagon)
        {
            var hexagonControl = new HexagonControl
            {
                DataContext = new HexagonControlViewModel(mainViewModel.ApplicationModel, mainViewModel.ArduinoCommunicator, hexagon)
            };

            Binding binding = new Binding
            {
                Source = hexagonControl.Model.Hexagon,
                Path = new PropertyPath(nameof(hexagonControl.Model.Hexagon.X)),
                Mode = BindingMode.TwoWay
            };

            hexagonControl.SetBinding(HexagonControl.XProperty, binding);

            binding = new Binding
            {
                Source = hexagonControl.Model.Hexagon,
                Path = new PropertyPath(nameof(hexagonControl.Model.Hexagon.Y)),
                Mode = BindingMode.TwoWay
            };

            hexagonControl.SetBinding(HexagonControl.YProperty, binding);

            binding = new Binding
            {
                Source = hexagonControl.Model.Hexagon,
                Path = new PropertyPath(nameof(hexagonControl.Model.Hexagon.Index)),
                Mode = BindingMode.TwoWay
            };

            hexagonControl.SetBinding(HexagonControl.IndexProperty, binding);

            return hexagonControl;
        }

        private static void PlaceHexagonControl(HexagonControl hexagonControl, HexagonsHolderControl holder, IHexagon hexagon)
        {
            hexagonControl.Width = SIZE;
            hexagonControl.Height = SIZE;
            hexagonControl.X = hexagon.X;
            hexagonControl.Y = hexagon.Y;
            hexagonControl.Index = hexagon.Index.ToString();
            hexagonControl.Model.Hexagon.ARGB = hexagon.ARGB;

            holder.HexagonControls.Add(hexagonControl);
            holder.Holder.Children.Add(hexagonControl);

            holder.InvalidateMeasure();
            holder.InvalidateArrange();
        }

        #endregion

        #region Drag

        private void UserControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMoving || !Model.ApplicationModel.IsTransforming) return;

            var mousePoint = Mouse.GetPosition(this.Parent as Grid);

            var offsetX = _mouseStart.Value.X - _controlPosition.Value.X - mousePoint.X;
            var offsetY =  _mouseStart.Value.Y - _controlPosition.Value.Y - mousePoint.Y;

            var translateTransform = (this.RenderTransform as TransformGroup).Children.OfType<TranslateTransform>().Single();
            translateTransform.X = -offsetX;
            translateTransform.Y = -offsetY;
        }

        private void UserControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {         
            _mouseStart = Mouse.GetPosition(this.Parent as Grid);

            var translateTransform = (this.RenderTransform as TransformGroup).Children.OfType<TranslateTransform>().Single();
            _controlPosition = new Point(translateTransform.X, translateTransform.Y);

            _isMoving = true;
        }

        private void UserControl_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _isMoving = false;
        }

        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(var hexagon in Hexagons)
            {
                var hexagonControl = CreateHexagonControl(Model, hexagon);

                PlaceHexagonControl(hexagonControl, this, hexagon);
            }
        }
    }
}
