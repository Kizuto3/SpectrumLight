﻿using System;
using System.Collections.Generic;
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

namespace SpectrumLight.CustomControls.MenuSettings
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MenuSettingsControl : UserControl
    {
        public static readonly DependencyProperty IsApplyColorProperty = DependencyProperty.Register(nameof(IsApplyColor), typeof(bool), typeof(MenuSettingsControl), new PropertyMetadata(false));

        public bool IsApplyColor
        {
            get => (bool)GetValue(IsApplyColorProperty);
            set => SetValue(IsApplyColorProperty, value);
        }

        public MenuSettingsControl()
        {
            InitializeComponent();
        }
    }
}