using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SpectrumLight.CustomControls.HexagonsHolder
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class HexagonsHolderControl : UserControl
    {

        public static readonly DependencyProperty LightOnOffComand = DependencyProperty.Register(nameof(LightOnOff), typeof(ICommand), typeof(HexagonsHolderControl), new PropertyMetadata(null));

        public ICommand LightOnOff
        {
            get => (ICommand)GetValue(LightOnOffComand);
            set => SetValue(LightOnOffComand, value);
        }

        /*public static readonly DependencyProperty LightOnOffComand = DependencyProperty.Register(nameof(LightOnOff), typeof(ICollection), typeof(HexagonsHolderControl), new PropertyMetadata(null));

        public ObservableCollection<Hexagon> Hexagons
        {
            get => (ICollection)GetValue(LightOnOffComand);
            set => SetValue(LightOnOffComand, value);
        }*/

        public HexagonsHolderControl()
        {
            InitializeComponent();
        }
    }
}
