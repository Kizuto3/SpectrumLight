using SpectrumLight.CustomControls.RoutinesWindow;
using System.Windows;

namespace SpectrumLight.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new RoutinesWindowControl();
            window.Show();
        }
    }
}
