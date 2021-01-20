using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace FlugzeugBsp_2019.Converter
{
    public class ContainerToBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = (int)value;

            switch (temp)
            {
                case 1:
                    return new SolidColorBrush(Colors.Red);
                case 2:
                    return new SolidColorBrush(Colors.Yellow);
                case 3:
                    return new SolidColorBrush(Colors.Green);
                default:
                    return new SolidColorBrush(Colors.Red);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
