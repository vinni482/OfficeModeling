using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeModeling
{
    class Accountant : Employee, IPosition
    {
        decimal _rate = 500;
        string _name = "Accountant";

        public Accountant()
        { }

        public Accountant(bool IsCombines)
        {
            if(IsCombines)
                positions.Add(new Manager());
        }

        public string Name
        {
            get { return _name; }
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
