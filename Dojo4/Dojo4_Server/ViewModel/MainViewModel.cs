using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo4_Server.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public int MessagesCnt
        {
            get
            {
                return 90;
                //return Messages.Count;
            }
        }





    }
}
