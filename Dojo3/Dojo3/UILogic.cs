﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Dojo3
{
    class UILogic : INotifyPropertyChanged                    //interface einfügen 
    {
        public event PropertyChangedEventHandler PropertyChanged;       //automatisch implementiert

        //DispatcherTimer timer;

        private DateTime _now;

        public UILogic()
        {
            _now = DateTime.Now;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);  //0 = h, 0 = min, dann sec
            timer.Tick += TimerReady;                //nach += Tabulator drücken!
            // += bedeutet trage mich in die Liste ein => in die Liste für dieses Event
            timer.Start();
        }

        public DateTime CurrentDateTime
        {
            get { return _now; }
            private set
            {
                _now = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TimeString"));
            }
        }

        public DateTime TimeString { get; private set; }

        private void TimerReady(object sender, EventArgs e)
        {
            TimeString = DateTime.Now;
            //DateTime.Now.ToString("hh:mm");
        }

    }
}
