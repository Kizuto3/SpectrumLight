using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SpectrumLight.CustomControls.RoutineMenu
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class RoutineMenuControl : UserControl
    {
        public static readonly DependencyProperty RoutineListProperty = DependencyProperty.Register(nameof(RoutineList), typeof(ICollection<string>), typeof(RoutineMenuControl), new PropertyMetadata(null));

        public ICollection<string> RoutineList
        {
            get => (ICollection<string>)GetValue(RoutineListProperty);
            set => SetValue(RoutineListProperty, value);
        }

        public RoutineMenuControl()
        {
            InitializeComponent();
        }
    }
}
