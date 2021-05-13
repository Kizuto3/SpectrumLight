using SpectrumLight.CommonObjects.Wpf.Abstractions;
using System;
using System.Globalization;
using System.Windows.Media;

namespace SpectrumLight.CommonObjects.Wpf.Converters
{
    public class BoolToBrushConverter : BaseValueConverter<bool, SolidColorBrush>
    {
        public override SolidColorBrush Convert(bool value, object parameter, CultureInfo culture)
        {
            return value ? new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0xff)) : new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0x00, 0x00));
        }

        public override bool ConvertBack(SolidColorBrush value, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
