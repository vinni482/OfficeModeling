using System;
using System.Collections.Generic;
using System.Linq;

namespace OfficeModeling
{
    class Director : Employee, IPosition
    {
        public static decimal _monthRate = 4000;
        string _name = "Director";
        List<OfficeTask> _setOfDuties = new List<OfficeTask>()
        {
            new OfficeTask { agent = typeof(Programmer), name = "Write code", rate = Programmer._hourRate },
            new OfficeTask { agent = typeof(Designer), name = "Draw the layout", rate = Designer._hourRate },
            new OfficeTask { agent = typeof(Tester), name = "Test the program", rate = Tester._hourRate },
            new OfficeTask { agent = typeof(Manager), name = "Sell services", rate = Manager._hourRate },
            new OfficeTask { agent = typeof(Accountant), name = "Make report", rate = Accountant._hourRate },
            new OfficeTask { agent = typeof(Cleaner), name = "Clean the office", rate = Cleaner._hourRate }
        };

        void GiveOrders(object sender, DateTime time)
        {            
            if (this.IsWorking(time) && this.IsAvailable)
            {
                for (int i = 0; i < _rand.Next(1, 4); i++)
                {
                    Console.Write(time + " " + _employeeName + " " + _name + ": ");

                    OfficeTask officetask = _setOfDuties[_rand.Next(6)];
                    officetask.guid = Guid.NewGuid();
                    officetask.priority = _rand.Next(1, 7);
                    _office._tasks.Add(officetask);
                    _office._tasks = _office._tasks.OrderBy(a => a.priority).ThenByDescending(a=>a.rate).ToList();

                    Console.WriteLine(officetask);
                }                
            }
        }
        
        public Director(bool IsCombines, Random rand, Office office, string employeeName)
            : base(rand, office, employeeName)
        {
            office.onClock += GiveOrders;
            
            if (IsCombines)
                positions.Add(new Manager(this));
        }
        
        public string Name
        {
            get { return _name; }
        }
        
        public override bool Equals(object obj)
        {
            return ((IPosition)obj).Name == this.Name;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
