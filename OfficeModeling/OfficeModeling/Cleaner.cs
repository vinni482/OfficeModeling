﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeModeling
{
    class Cleaner : Employee, IPosition
    {
        decimal _rate = 200;

        public decimal Rate
        {
            get { return _rate; }
        }
    }
}
