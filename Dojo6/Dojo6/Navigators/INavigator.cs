using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo6.Navigators
{
    public interface INavigator
    {
        ViewModelBase Navigate(string param);
    }
}
