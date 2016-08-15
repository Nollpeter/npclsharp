using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace testWPF.ViewModel.Converters
{
    public class LogScaleConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            var stringValue = value.ToString();
            if (String.IsNullOrWhiteSpace(stringValue)) return null;

            var intValue = Int32.Parse(stringValue);
            return Math.Log(intValue);
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return (Int32)Math.Exp((Double)value);
        }
    }
}
