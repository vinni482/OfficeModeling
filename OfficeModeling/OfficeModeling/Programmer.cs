using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeModeling
{
    class Programmer : Employee, IPosition
    {
        decimal _rate = 800;

        public Programmer()
        { }

        public Programmer(int combiningPositions)
        {
            positions.Add(new Designer());
        }

        public decimal Rate
        {
            get { return _rate; }
        }
    }
}
