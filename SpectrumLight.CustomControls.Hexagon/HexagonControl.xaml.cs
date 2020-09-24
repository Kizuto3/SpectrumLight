using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SpectrumLight.CustomControls.Hexagon
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class HexagonControl : UserControl
    {

        public static readonly DependencyProperty LightOnOffComand = DependencyProperty.Register(nameof(LightOnOff), typeof(ICommand), typeof(HexagonControl), new PropertyMetadata(null));

        public ICommand LightOnOff
        {
            get => (ICommand)GetValue(LightOnOffComand);
            set => SetValue(LightOnOffComand, value);
        }

        public HexagonControl()
        {
            InitializeComponent();
        }
    }
}
