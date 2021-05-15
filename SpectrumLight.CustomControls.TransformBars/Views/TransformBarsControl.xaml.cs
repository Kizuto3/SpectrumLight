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

namespace SpectrumLight.CustomControls.TransformBars
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TransformBarsControl : UserControl
    {
        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(nameof(Scale), typeof(double), typeof(TransformBarsControl), new PropertyMetadata(1d));

        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        public static readonly DependencyProperty RotationProperty = DependencyProperty.Register(nameof(Rotation), typeof(double), typeof(TransformBarsControl), new PropertyMetadata(0d));

        public double Rotation
        {
            get => (double)GetValue(RotationProperty);
            set => SetValue(RotationProperty, value);
        }

        public static readonly DependencyProperty StartTransformingProperty = DependencyProperty.Register(nameof(StartTransforming), typeof(ICommand), typeof(TransformBarsControl), new PropertyMetadata(null));

        public ICommand StartTransforming
        {
            get => (ICommand)GetValue(StartTransformingProperty);
            set => SetValue(StartTransformingProperty, value);
        }

        public static readonly DependencyProperty FinishTransformingProperty = DependencyProperty.Register(nameof(FinishTransforming), typeof(ICommand), typeof(TransformBarsControl), new PropertyMetadata(null));

        public ICommand FinishTransforming
        {
            get => (ICommand)GetValue(FinishTransformingProperty);
            set => SetValue(FinishTransformingProperty, value);
        }

        public static readonly DependencyProperty CancelTransformingProperty = DependencyProperty.Register(nameof(CancelTransforming), typeof(ICommand), typeof(TransformBarsControl), new PropertyMetadata(null));

        public ICommand CancelTransforming
        {
            get => (ICommand)GetValue(CancelTransformingProperty);
            set => SetValue(CancelTransformingProperty, value);
        }

        public TransformBarsControl()
        {
            InitializeComponent();
        }
    }
}
