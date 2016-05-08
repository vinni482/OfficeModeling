using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeModeling
{
    class Cleaner : Employee, IPosition
    {
        decimal _rate = 200;
        string _name = "Cleaner";

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public decimal Rate
        {
            get { return _rate; }
        }

        public override bool Equals(object obj)
        {
            return ((IPosition)obj).Name == this.Name;
        }
    }
}
