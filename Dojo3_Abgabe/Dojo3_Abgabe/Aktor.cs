using Shared.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dojo3_Abgabe.Delegates;

namespace Dojo3_Abgabe
{
    public class Aktor : MyItemBase
    {
        public Aktor(Delegates.Informer counterEllapsedInformer)
        {
        }

        public string ValueType { get; set; }
        public string ItemType { get; set; }
        public string Mode { get; set; }
        public bool Value { get; set; }
        public string IsInDesignMode { get; set; }

    }
}