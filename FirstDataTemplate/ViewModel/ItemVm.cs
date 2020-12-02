using GalaSoft.MvvmLight;

namespace FirstDataTemplate.ViewModel
{
    public class ItemVm : ViewModelBase         // use from GalaSoft...
    {
        private decimal price;
        private int amount;

        public string OrderId { get; set; }
        public string Description { get; set; }
        public int Amount { get => amount; set
            {
                amount = value;
                RaisePropertyChanged("FullPrice");
            }
        }
        public string Prio { get; set; }
        public decimal Price
        {
            get => price; set
            {
                price = value;
                RaisePropertyChanged("FullPrice");
            }
        } // => convert to full property

        public decimal FullPrice            //fullprop => tab
        {
            get { return Price * Amount; }
        }

        public ItemVm()
        {
            OrderId = "";
        }

    }

}