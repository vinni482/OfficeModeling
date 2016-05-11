﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OfficeModeling
{
    class Director : Employee, IPosition
    {
        public static decimal _rate = 800;
        string _name = "Director";
        Random _rand;
        Office _office;
        List<OfficeTask> _setOfDuties = new List<OfficeTask>()
        {
            new OfficeTask { agent = typeof(Programmer), name = "Write code", rate = Programmer._rate },
            new OfficeTask { agent = typeof(Designer), name = "Draw the layout", rate = Designer._rate },
            new OfficeTask { agent = typeof(Tester), name = "Test the program", rate = Tester._rate },
            new OfficeTask { agent = typeof(Manager), name = "Sell services", rate = Manager._rate },
            new OfficeTask { agent = typeof(Accountant), name = "Make report", rate = Accountant._rate },
            new OfficeTask { agent = typeof(Cleaner), name = "Clean the office", rate = Cleaner._rate }
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
                    _office._tasks = _office._tasks.OrderBy(a => a.priority).ThenByDescending(a=>a.rate).ToList();

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
        
        public string Name
        {
            get { return _name; }
        }

        public override bool Equals(object obj)
        {
            return ((IPosition)obj).Name == this.Name;
        }

        public override void Do(OfficeTask task, DateTime startTaskTime)
        {
            Console.WriteLine("Director");
        }
    }
}
