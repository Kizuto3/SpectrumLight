using System.Windows;
using System.Windows.Controls;

namespace SpectrumLight.CustomControls.BrightnessBar
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class BrightessBarControl : UserControl
    {
        public static readonly DependencyProperty SliderValueProperty = DependencyProperty.Register(nameof(SliderValue), typeof(int), typeof(BrightessBarControl), new PropertyMetadata(255));

        public int SliderValue
        {
            get => (int)GetValue(SliderValueProperty);
            set => SetValue(SliderValueProperty, value);
        }

        public BrightessBarControl()
        {
            InitializeComponent();
        }
    }
}
