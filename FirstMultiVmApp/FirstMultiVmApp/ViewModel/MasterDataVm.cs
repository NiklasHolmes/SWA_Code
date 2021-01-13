using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMultiVmApp.ViewModel
{
    public class MasterDataVm : ViewModelBase
    {
        public string DemoData { get; set; }

        private PersonVm person;

        public RelayCommand SaveBtnClickCmd { get; set; }

        public MasterDataVm()
        {
            person = new PersonVm() { Age = 10, Firstname = "Jane", Lastname = "Doe" };
            
            SaveBtnClickCmd = new RelayCommand(() => {
                person.Lastname = DemoData;         // zuweisen

                Messenger.Default.Send<PersonVm>(person, "MasterDataInfo");
            });

            /*
            SaveBtnClickCmd = new RelayCommand(() => {
                Messenger.Default.Send<string>(DemoData, "MasterDataInfo");
            });
            */

        }


    }
}
