using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ConverterUebung.Converters
{
    class BoolToBrushConverter : IValueConverter        // Interface automatisch implementieren
    {
        //  Converter sollten SIMPEL sein!

        private static Brush orange = new SolidColorBrush(Colors.Orange);
        private static Brush green = new SolidColorBrush(Colors.Green);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            if (val)
            {
                //return green
                return green;
            }
            else
            {
                //Return orange
                return orange;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
