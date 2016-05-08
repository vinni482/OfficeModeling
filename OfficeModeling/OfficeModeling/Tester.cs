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
        string _name = "Tester";

        public Tester()
        {}

        public Tester(int combiningPositions)
        {
            Random rand = new Random();
            for (int i = 0; i < combiningPositions; i++)
            {
                switch ((AdditionalPositions)rand.Next(0, 5))
                {
                    case AdditionalPositions.Programmer:
                        {
                            Programmer prog = new Programmer();
                            if (!positions.Contains(prog))
                                positions.Add(prog);
                            break;
                        }
                    case AdditionalPositions.Designer:
                        {
                            Designer designer = new Designer();
                            if (!positions.Contains(designer))
                                positions.Add(designer);
                            break;
                        }
                    case AdditionalPositions.Manager:
                        {
                            Manager manager = new Manager();
                            if (!positions.Contains(manager))
                                positions.Add(manager);
                            break;
                        }
                    case AdditionalPositions.Cleaner:
                        {
                            Cleaner cleaner = new Cleaner();
                            if (!positions.Contains(cleaner))
                                positions.Add(cleaner);
                            break;
                        }
                }
            }
        }

        public decimal Rate
        {
            get { return _rate; }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public override bool Equals(object obj)
        {
            return ((IPosition)obj).Name == this.Name;
        }
    }
}
