using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FarbconverterTestVorbereitung.Converter
{
    public class BoolToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            if (val)
            {
                //alive:
                BitmapImage alive = new BitmapImage(new Uri("../Images/Alive.png", UriKind.Relative));
                return alive;
            }
            else
            {
                // RIP:
                return new BitmapImage(new Uri("../Images/Rip.png", UriKind.Relative));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
