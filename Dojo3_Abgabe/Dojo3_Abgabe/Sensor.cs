using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo3_Abgabe
{
    public class Sensor : MyItemBase
    {
        public Sensor(Delegates.Informer counterEllapsedInformer)
        {
        }

        public string ValueType { get; set; }
        public bool Status { get; set; }
        public string Mode { get; set; }
        public double Value { get; set; }

    }
}
