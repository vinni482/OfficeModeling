using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeModeling
{
    class Director : Employee, IPosition
    {
        decimal _rate = 800;

        public Director()
        { }

        public Director(bool IsCombines)
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
