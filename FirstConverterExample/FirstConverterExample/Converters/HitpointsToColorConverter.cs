using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace FirstConverterExample.Converters
{
    public class HitpointsToColorConverter : IValueConverter        // Interface automatisch implementieren
    {
        //  Converter sollten SIMPEL sein!

        private static Brush red = new SolidColorBrush(Colors.Red);
        private static Brush yellow = new SolidColorBrush(Colors.Yellow);
        private static Brush green = new SolidColorBrush(Colors.Green);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int val = (int)value;
            if (val < 50)
            {
                //return Red
                return red;
            }
            else if (val < 100)
            {
                //return Yellow
                return yellow;
            }
            else
            {
                //Return green
                return green;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
