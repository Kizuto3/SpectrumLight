using Prism.Commands;
using SpectrumLight.CustomControls.Hexagon.ViewModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SpectrumLight.CustomControls.Hexagon
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class HexagonControl : UserControl
    {
        public static readonly DependencyProperty XProperty = DependencyProperty.Register(nameof(X), typeof(double), typeof(HexagonControl), new PropertyMetadata(0d));

        public double X
        {
            get => (double)GetValue(XProperty);
            set => SetValue(XProperty, value);
        }

        public static readonly DependencyProperty YProperty = DependencyProperty.Register(nameof(Y), typeof(double), typeof(HexagonControl), new PropertyMetadata(0d));

        public double Y
        {
            get => (double)GetValue(YProperty);
            set => SetValue(YProperty, value);
        }

        public static readonly DependencyProperty IndexProperty = DependencyProperty.Register(nameof(Index), typeof(string), typeof(HexagonControl), new PropertyMetadata("index"));

        public string Index
        {
            get => (string)GetValue(IndexProperty);
            set => SetValue(IndexProperty, value);
        }

        public HexagonControl()
        {
            InitializeComponent();
        }

        public HexagonControlViewModel Model
        {
            get => DataContext as HexagonControlViewModel;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Model.LightOnOff();
        }
    }
}
