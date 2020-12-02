using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using VmResourceManagerServer.ViewModel;

namespace VmResourceManagerServer.Converters
{
    public class StateToImageConverter : IValueConverter
    {
        // auch so: (vom Speicher bessere Variante: 
        //private static runningImage = new BitmapImage(new Uri("../Assets/Images/running-stick-figure.png", UriKind.Relative));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StateType temp = (StateType) value;

            switch (temp)                       // tab und Enter nach temp => case automatisch da!
            {
                case StateType.Running:
                    return new BitmapImage(new Uri("../Assets/Images/running-stick-figure.png", UriKind.Relative));
                case StateType.OnHold:
                    return new BitmapImage(new Uri("../Assets/Images/open-hands.png", UriKind.Relative));
                case StateType.Stopped:
                    return new BitmapImage(new Uri("../Assets/Images/cancel-button", UriKind.Relative));
                default:
                    return null;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
