using CommonServiceLocator;
using Dojo6.ViewModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo6.Navigators
{
    public class Navigator : INavigator
    {
        public ViewModelBase Navigate(string param)
        {
            switch (param)
            {
                case "overview":
                    return ServiceLocator.Current.GetInstance<CategoryVm>();

                case "mytoys":
                    return ServiceLocator.Current.GetInstance<MyToysVm>();

                default:
                    return ServiceLocator.Current.GetInstance<CategoryVm>();

            }
        }
    }
}
