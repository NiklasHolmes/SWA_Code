using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo6.ViewModel
{
    public class MyToysVm : ViewModelBase
    {

        public ObservableCollection<ToyVm> WishList { get; set; }

        public MyToysVm()
        {

            WishList = new ObservableCollection<ToyVm>();

            // Zu Testzwecken:
            //WishList.Add(new ToyVm() { Description = "Baby Yoda", AgeRecommendation = 10, Image = "../Images/Lego_BabyYoda.png", Brand = "Lego" });

            // Gegenstück zu Messenger.Default.Send<ToyVm>(toyToCart, "AddToCart"); : 
            Messenger.Default.Register<ToyVm>(this, "AddToCart", AddToWisList);

        }

        private void AddToWisList(ToyVm obj)
        {

            WishList.Add(obj);
            RaisePropertyChanged("WishList");
        }

    }
}
