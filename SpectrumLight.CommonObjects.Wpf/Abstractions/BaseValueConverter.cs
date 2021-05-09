using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace SpectrumLight.CommonObjects.Wpf.Abstractions
{
    public abstract class BaseValueConverter<TFrom, TTo> : MarkupExtension, IValueConverter
    {
        public abstract TTo Convert(TFrom value, object parameter, CultureInfo culture);

        public abstract TFrom ConvertBack(TTo value, object parameter, CultureInfo culture);

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TryConvert(value, targetType, parameter, culture, false);
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TryConvert(value, targetType, parameter, culture, true);
        }

        private object TryConvert(object value, Type targetType, object parameter, CultureInfo culture, bool convertBack)
        {
            try
            {
                return convertBack
                    ? ConvertBack((TTo)value, parameter, culture)
                    : (object)Convert((TFrom)value, parameter, culture);
            }
            catch
            {
                return value;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
