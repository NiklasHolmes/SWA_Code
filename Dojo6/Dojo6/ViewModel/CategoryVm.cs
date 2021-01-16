using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo6.ViewModel
{
    public class CategoryVm : ViewModelBase
    {
        private ToyVm toy;
        public ObservableCollection<ToyVm> Toys { get; set; }
        public ObservableCollection<ToyVm> LegoToys { get; set; }
        public ObservableCollection<ToyVm> PlaymobilToys { get; set; }
        public ObservableCollection<ToyVm> DisplayedToys { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        public ToyVm Toy
        {
            get => toy; set
            {
                toy = value;
            }
        }
        public RelayCommand<ToyVm> AddToWishListBtnClickCmd { get; set; }

        private Category selectedCategory { get; set; }
        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                RaisePropertyChanged(); // 
                DisplayToys(value.BrandName);
                RaisePropertyChanged("DisplayedToys");
            }
        }


        public CategoryVm() {

            # region Dojo5
            Toys = new ObservableCollection<ToyVm>();
            LegoToys = new ObservableCollection<ToyVm>();
            PlaymobilToys = new ObservableCollection<ToyVm>();
            DisplayedToys = new ObservableCollection<ToyVm>();
            Categories = new ObservableCollection<Category>();

            if (true)
            {
                GenerateDemoDate();
            }

            //AddToWishListBtnClickCmd = new RelayCommand<ToyVm>(AddToWisList, true);
            /*
            AddToWishListBtnClickCmd = new RelayCommand<ToyVm>(
                // Action
                ()=>
                {
                    WishList.Add(ToyVm Item);
                },
                // Can always execute
                () => { return true; }
            );
            */
            #endregion

            AddToWishListBtnClickCmd = new RelayCommand<ToyVm>(
                (toyToCart) => {

                    Messenger.Default.Send<ToyVm>(toyToCart, "AddToCart");

                });

        }

        private void GenerateDemoDate()
        {
            Categories.Add(new Category() { BrandName = "Lego", Image = "../Images/Lego_Logo.png" });
            Categories.Add(new Category() { BrandName = "Playmobil", Image = "../Images/Playmobil_Logo.png" });

            Toys.Add(new ToyVm() { Description = "Baby Yoda", AgeRecommendation = 10, Image = "../Images/Lego_BabyYoda.png", Brand = "Lego" });
            Toys.Add(new ToyVm() { Description = "Donut Shop", AgeRecommendation = 6, Image = "../Images/Lego_DonutShop.png", Brand = "Lego" });
            Toys.Add(new ToyVm() { Description = "Iron Mans\nWerkstatt", AgeRecommendation = 7, Image = "../Images/Lego_IronManWerkstatt.png", Brand = "Lego" });
            Toys.Add(new ToyVm() { Description = "Mindstorm", AgeRecommendation = 10, Image = "../Images/Lego_Mindstorms.png", Brand = "Lego" });
            Toys.Add(new ToyVm() { Description = "Antike Arena", AgeRecommendation = 4, Image = "../Images/Playmobil_AntikeArena.png", Brand = "Playmobil" });
            Toys.Add(new ToyVm() { Description = "Bauernhof", AgeRecommendation = 4, Image = "../Images/Playmobil_Bauernhof.png", Brand = "Playmobil" });
            Toys.Add(new ToyVm() { Description = "Ghostbusters", AgeRecommendation = 6, Image = "../Images/Playmobil_Ghostbusters.png", Brand = "Playmobil" });
            Toys.Add(new ToyVm() { Description = "Weihnachts-\nhaus", AgeRecommendation = 4, Image = "../Images/Playmobil_Weihnachtshaus.png", Brand = "Playmobil" });

            // Toys nach Brand in kleinere Listen teilen:
            foreach (ToyVm item in Toys)
            {
                if (item.Brand == "Lego")
                {
                    LegoToys.Add(item);
                }
                else if (item.Brand == "Playmobil")
                {
                    PlaymobilToys.Add(item);
                }
            }
            // für den Anfang: 
            DisplayedToys = Toys;
        }
        public void DisplayToys(string brandName)
        {
            switch (brandName)
            {
                case "Lego":
                    DisplayedToys = LegoToys;
                    break;
                case "Playmobil":
                    DisplayedToys = PlaymobilToys;
                    break;
            }
            //RaisePropertyChanged("DisplayedToys");
        }


    }
}
