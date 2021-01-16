/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Dojo6"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
//using Microsoft.Practices.ServiceLocation;

namespace Dojo6.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();

            // die vier Vms:
            SimpleIoc.Default.Register<MyToysVm>(true); // true = damit verpflichtend erzeugt wird
            SimpleIoc.Default.Register<StatusBarVm>(true);
            SimpleIoc.Default.Register<CategoryVm>(true);

        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public MyToysVm SelectedToys
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MyToysVm>(); //schaut Gibt es die Instanz schon oder nicht?
            }
        }
        public StatusBarVm StatusBar
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StatusBarVm>(); //schaut Gibt es die Instanz schon oder nicht?
            }
        }
        public CategoryVm Categories
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CategoryVm>(); //schaut Gibt es die Instanz schon oder nicht?
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}