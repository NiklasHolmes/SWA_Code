using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Client_wi19b040.Converter
{
    class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = (string)value;

            switch (temp)
            {
                case "normal":
                    return new SolidColorBrush(Colors.DarkOliveGreen);
                case "warning":
                    return new SolidColorBrush(Colors.DarkOrange);
                case "danger":
                    return new SolidColorBrush(Colors.Red);
                default:
                    return new SolidColorBrush(Colors.DarkOliveGreen);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
