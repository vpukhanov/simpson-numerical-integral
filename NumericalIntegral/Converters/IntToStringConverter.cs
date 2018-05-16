using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NumericalIntegral.Converters
{
    [ValueConversion(typeof(int), typeof(string))]
    class IntToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("The target must be a string");

            return ((int)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(int))
                throw new InvalidOperationException("The target must be a int");

            int realValue;
            if (int.TryParse((string)value, out realValue))
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
