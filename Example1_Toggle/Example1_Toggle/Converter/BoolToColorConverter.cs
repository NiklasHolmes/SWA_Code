using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Example1_Toggle.Converter
{
    public class BoolToColorConverter : IValueConverter     //using Windos.Data
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Vorher schon variablen Namen definieren:

            SolidColorBrush green = new SolidColorBrush(Colors.Green);      //using Windows.Media
            SolidColorBrush red = new SolidColorBrush(Colors.Red);

            var temp = (bool)value;

            if (temp == true)
            {
                return green;
            }
            else
            {
                return red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
