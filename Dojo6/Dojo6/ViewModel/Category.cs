using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo6.ViewModel
{
    public class Category : ViewModelBase           // public nicht vergessen!!
    {
        public string BrandName { get; set; }
        public string Image { get; set; }


    }
}
