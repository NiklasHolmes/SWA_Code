using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace VmResourceManagerServer.ViewModel
{
    public class InstanceVm : ViewModelBase
    {
        public string Type { get; set; }
        public int RAM { get; set; }
        public int CPUs { get; set; }
        public int StorageSize { get; set; }
        public StateType State { get; set; }
        public RelayCommand StateChangeBtnClickCmd { get; set; }

        private DateTime startUpTime;
        private DispatcherTimer timer;

        public string UpTime         // property auf das das binding durchgeführt wird          // TimeSpan wurde zu string gemacht
        {                               // up Time wird immer berechnet
            get { 
                if (State != StateType.Running)
                    {return "NA"; }
                else
                {
                return DateTime.Now.Subtract(startUpTime).TotalMinutes.ToString();
                }
            }
        }
        public InstanceVm()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += Timer_Tick;

            StateChangeBtnClickCmd = new RelayCommand(() =>
            {
                switch (State)
                {
                    case StateType.Running:
                        State = StateType.OnHold;
                        break;
                    case StateType.OnHold:
                        State = StateType.Stopped;
                        break;
                    case StateType.Stopped:
                        State = StateType.Running;
                        break;
                }
                RaisePropertyChanged("State");
            });

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            RaisePropertyChanged("UpTime");
        }

        public void StartUp()
        {
            State = StateType.Running;
            startUpTime = DateTime.Now;
            RaisePropertyChanged("UpTime");
            timer.Start();
        }
    }
}
