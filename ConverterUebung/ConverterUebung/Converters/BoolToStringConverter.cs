using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ConverterUebung.Converters
{
    class BoolToStringConverter : IValueConverter        // Interface automatisch implementieren
    {
        //  Converter sollten SIMPEL sein!

        private static string textTrue = "true";
        private static string textFalse= "false";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            if (val)
            {
                //return true
                return textTrue;
            }
            else
            {
                //Return false
                return textFalse;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string state = (string)value;
            if (state.Equals("True")) return true;
            else return false;
        }
    }
}
