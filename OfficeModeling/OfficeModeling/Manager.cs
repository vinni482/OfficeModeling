using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeModeling
{
    class Manager : Employee, IPosition
    {
        decimal _rate = 600;

        public Manager()
        { }

        public Manager(int combiningPositions)
        {
            positions.Add(new Designer());
        }

        public decimal Rate
        {
            get { return _rate; }
        }
    }
}
