using System;
using System.Collections.Generic;

namespace OfficeModeling
{
    class Director : Employee, IPosition
    {
        decimal _rate = 800;
        string _name = "Director";
        Random _rand;
        Office _office;
        List<OfficeTask> _setOfDuties = new List<OfficeTask>()
        {
            new OfficeTask { agent = "Programmer", name = "Write code" },
            new OfficeTask { agent = "Designer", name = "Draw the layout" },
            new OfficeTask { agent = "Tester", name = "Test the program" },
            new OfficeTask { agent = "Manager", name = "Sell services" },
            new OfficeTask { agent = "Accountant", name = "Make report" },
            new OfficeTask { agent = "Cleaner", name = "Clean the office" }
        };

        void GiveOrders(DateTime time)
        {
            if (this.IsWorking(time) && this.IsAvailable)
            {
                for (int i = 0; i < _rand.Next(3); i++)
                {
                    Console.WriteLine(time + " " + _name + ": ");

                    OfficeTask officetask = _setOfDuties[_rand.Next(6)];
                    officetask.guid = Guid.NewGuid();
                    officetask.priority = _rand.Next(1, 7);
                    _office._tasks.Add(officetask);

                    Console.WriteLine("\t" + officetask);
                }                
            }
        }

        public Director() { }

        public Director(bool IsCombines, Random rand, Office office)
        {
            _rand = rand;
            _office = office;
            office.onClock += GiveOrders;

            #region Добавление рабочих дней
            do
            {
                WorkingDay workingDay = new WorkingDay() { day = (DayOfWeek)rand.Next(7), startWorkingDay = rand.Next(8, 13), hours = rand.Next(6, 9) };
                if (!workingDays.Contains(workingDay))
                    workingDays.Add(workingDay);
            } while (workingDays.Count < 5);
            #endregion

            if (IsCombines)
                positions.Add(new Manager());
        }

        public decimal Rate
        {
            get { return _rate; }
        }

        public string Name
        {
            get { return _name; }
        }

        public override bool Equals(object obj)
        {
            return ((IPosition)obj).Name == this.Name;
        }
    }
}
