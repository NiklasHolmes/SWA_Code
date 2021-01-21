using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2_Chat.ViewModel
{
    public class ClientUser : ViewModelBase
    {
        public string Username { get; set; }

        public ObservableCollection<string> singleUserMessage { get; set; }
        public ObservableCollection<string> userTimestamp { get; set; }

    }
}
