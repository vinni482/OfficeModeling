using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeModeling
{
    class Tester : Employee, IPosition
    {
        decimal _rate = 700;

        public Tester()
        {}

        public Tester(int combiningPositions)
        {
            positions.Add(new Designer());
        }

        public decimal Rate
        {
            get { return _rate; }
        }
    }
}
