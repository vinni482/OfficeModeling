using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeModeling
{
    class Designer : Employee, IPosition
    {
        decimal _rate = 400;

        public Designer()
        { }

        public Designer(int combiningPositions)
        {
            positions.Add(new Designer());
        }

        public decimal Rate
        {
            get { return _rate; }
        }
    }
}
