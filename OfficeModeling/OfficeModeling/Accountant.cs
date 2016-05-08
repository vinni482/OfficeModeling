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

        public Accountant()
        { }

        public Accountant(bool IsCombines)
        {
            if(IsCombines)
                positions.Add(new Manager());
        }

        public decimal Rate
        {
            get { return _rate; }
        }
    }
}
