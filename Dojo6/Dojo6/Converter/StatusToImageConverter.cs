using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Dojo6.Converter
{
    public class StatusToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = (string)value;

            BitmapImage ok = new BitmapImage(new Uri("../Images/State_Ok.png", UriKind.Relative));

            switch (temp)
            {
                case "Error":
                    BitmapImage error = new BitmapImage(new Uri("../Images/State_Error.png", UriKind.Relative));
                    return error;
                case "Info":
                    BitmapImage info = new BitmapImage(new Uri("../Images/State_Info.png", UriKind.Relative));
                    return info;
                case "Ok":
                    return ok;
                case "Warning":
                    BitmapImage warning = new BitmapImage(new Uri("../Images/State_Warning.png", UriKind.Relative));
                    return warning;
                default:
                    return ok;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
