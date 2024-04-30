using System;
using System.Globalization;
using System.Windows.Data;

namespace AbonentList.Converters
{
    [ValueConversion(sourceType: typeof(object), targetType: typeof(bool))]
    internal class ReferenceIdToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is int aIntValue) && (aIntValue > 0) || (value is long aLongValue) && (aLongValue > 0L);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
