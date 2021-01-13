using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace FarbconverterTestVorbereitung.Converter
{
    public class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = (string)value;

            switch (temp)
            {
                case "Blue":
                    return new SolidColorBrush(Colors.Blue);
                case "Green":
                    return new SolidColorBrush(Colors.Green);
                case "Brown":
                    return new SolidColorBrush(Colors.Brown);
                case "White":
                    return new SolidColorBrush(Colors.White);
                case "Red":
                    return new SolidColorBrush(Colors.Red);
                default:
                    return new SolidColorBrush(Colors.Black);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
