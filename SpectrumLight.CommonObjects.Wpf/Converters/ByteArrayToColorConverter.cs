using SpectrumLight.CommonObjects.Wpf.Abstractions;
using System.Globalization;
using System.Windows.Media;

namespace SpectrumLight.CommonObjects.Wpf.Converters
{
    public class ByteArrayToColorConverter : BaseValueConverter<byte[], Color>
    {
        public override Color Convert(byte[] value, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Color.FromArgb(0xff, 0x00, 0x00, 0x00);
            }

            return Color.FromArgb(value[0], value[1], value[2], value[3]);
        }

        public override byte[] ConvertBack(Color value, object parameter, CultureInfo culture)
        {
            return new byte[] { value.A, value.R, value.G, value.B };
        }
    }
}
