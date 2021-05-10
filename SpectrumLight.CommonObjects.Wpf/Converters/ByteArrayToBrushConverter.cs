using SpectrumLight.CommonObjects.Wpf.Abstractions;
using System.Globalization;
using System.Windows.Media;

namespace SpectrumLight.CommonObjects.Wpf.Converters
{
    public class ByteArrayToBrushConverter : BaseValueConverter<byte[], SolidColorBrush>
    {
        public override SolidColorBrush Convert(byte[] value, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00));
            }

            return new SolidColorBrush(Color.FromArgb(value[0], value[1], value[2], value[3]));
        }

        public override byte[] ConvertBack(SolidColorBrush value, object parameter, CultureInfo culture)
        {
            return new byte[] { value.Color.A, value.Color.R, value.Color.G, value.Color.B };
        }        
    }
}
