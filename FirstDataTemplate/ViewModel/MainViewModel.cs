using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;

namespace FirstDataTemplate.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ItemVm item;

        public ObservableCollection<ItemVm> Items { get; set; }

        public ItemVm Item { get => item; set {
                item = value;
                RaisePropertyChanged(); }       // nachdem Wert gesetzt wurde wird gleich ein leere Objekt gemacht => 
            //die Werte werden rausgenommen => man kann neues Item hinzufügen
        }
        public RelayCommand AddBtnClickCmd { get; set; }                // WPF usen/nehmen!!!
        public RelayCommand DeleteBtnClickCmd { get; set; }
        public RelayCommand<ItemVm> DeleteBtnClickCmd2 { get; set; }


        public ObservableCollection<string> PrioList { get; set; }
        public ItemVm SelectedItem { get; set; }

        public MainViewModel()
        {
            Items = new ObservableCollection<ItemVm>();
            if (IsInDesignMode)
            {
                GeneralDemoData();
            }
            Item = new ItemVm();

            PrioList = new ObservableCollection<string>() { "Hoch", "Mittel", "Niedrig" };

            AddBtnClickCmd = new RelayCommand(
                // Action
                () =>
                {
                    Items.Add(Item);
                    Item = new ItemVm();
                },
                // Can execute
                () => { return Item.OrderId.Length > 0; }
                );
            DeleteBtnClickCmd = new RelayCommand(DeleteEntry, CanDeleteEntry);

            DeleteBtnClickCmd2 = new RelayCommand<ItemVm>(DeleteSpecificEntry);     // can delete unnötig

        }

        private void GeneralDemoData()
        {
            Items.Add(new ItemVm() { OrderId = "ID-01", Description = "Demo", Amount = 3, Price = 12 });
            Items.Add(new ItemVm() { OrderId = "ID-02", Description = "Demo", Amount = 3, Price = 12 });
            Items.Add(new ItemVm() { OrderId = "ID-03", Description = "Demo", Amount = 3, Price = 12 });
            Items.Add(new ItemVm() { OrderId = "Demo-04", Description = "Demo", Amount = 3, Price = 12 });
            Items.Add(new ItemVm() { OrderId = "Demo-05", Description = "Demo", Amount = 3, Price = 12 });
            Items.Add(new ItemVm() { OrderId = "Demo-06", Description = "Demo", Amount = 3, Price = 12 });
            Items.Add(new ItemVm() { OrderId = "Demo-07", Description = "Demo", Amount = 3, Price = 12 });
            Items.Add(new ItemVm() { OrderId = "Demo-08", Description = "Demo", Amount = 3, Price = 12 });
            Items.Add(new ItemVm() { OrderId = "Demo-09", Description = "Demo", Amount = 3, Price = 12 });
            Items.Add(new ItemVm() { OrderId = "Demo-10", Description = "Demo", Amount = 3, Price = 12 });
            Items.Add(new ItemVm() { OrderId = "Demo-11", Description = "Demo", Amount = 3, Price = 12 });
            Items.Add(new ItemVm() { OrderId = "Demo-12", Description = "Demo", Amount = 3, Price = 12 });
        }

        private void DeleteSpecificEntry(ItemVm obj)
        {
            Items.Remove(obj);
        }

        private void DeleteEntry()
        {
            Items.Remove(SelectedItem);
        }

        private bool CanDeleteEntry()
        {
            return SelectedItem != null;
        }


    }
}