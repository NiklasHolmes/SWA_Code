using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo3
{
    class Aktor
    {
        public Aktor(Delegates.Informer counterEllapsedInformer)
        {
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public string ValueType { get; set; }
        public string ItemType { get; set; }
        public string Mode { get; set; }
        public int Value { get; set; }
        public string IsInDesignMode { get; set; }

    }
}
