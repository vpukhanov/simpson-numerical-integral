using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NumericalIntegral.Converters
{
    [ValueConversion(typeof(double), typeof(string))]
    class DoubleToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(string) && targetType != typeof(object))
                throw new InvalidOperationException("The target must be a string");

            return ((double)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(double))
                throw new InvalidOperationException("The target must be a double");

            double realValue;
            if (double.TryParse((string)value, out realValue))
            {
                return realValue;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}
