using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Example11_2_Kanbanboard.Converter
{
    public class BoolToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool temp = (bool)value;

            if (temp)
            {
                BitmapImage urgent = new BitmapImage(new Uri("../Images/Urgent.png", UriKind.Relative));
                return urgent;
            }
            else
            {
                BitmapImage Ok = new BitmapImage(new Uri("../Images/Ok.png", UriKind.Relative));
                return Ok;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
