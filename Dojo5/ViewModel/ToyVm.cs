using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo5.ViewModel
{
    public class ToyVm : ViewModelBase
    {
        public int AgeRecommendation { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Brand { get; set; }

        public ToyVm()
        {
            AgeRecommendation = 0;
            Description = "";
            Image = "";
            Brand = "";
        }
    }
}
