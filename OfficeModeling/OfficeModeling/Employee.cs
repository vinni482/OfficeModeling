using System;
using System.Collections.Generic;

namespace OfficeModeling
{
    class Employee
    {
        public List<IPosition> positions = new List<IPosition>();
        public List<WorkingDay> workingDays = new List<WorkingDay>();
        public List<CompletedTask> completedTasks = new List<CompletedTask>();
        public bool IsAvailable = true; //false - в случае, если в процессе выполнения задания

        public Office _office { set; get; }
        public OfficeTask _task { set; get; }
        public DateTime _startTaskTime { set; get; }
        public DateTime _endTaskTime { set; get; }
        public Random _rand { set; get; }
        public string _employeeName { set; get; }
        public decimal earned { set; get; }

        protected enum AdditionalPositions { Programmer, Designer, Tester, Manager, Cleaner };
        protected Employee _baseEmployee; //Ссылка на базовый класс для совмещаемых позиций

        public Employee(Employee baseEmployee)
        {
            _baseEmployee = baseEmployee;
        }

        public Employee(Random rand, Office office, string employeeName) //Стартовый конструктор для любого сотрудника, добавляющий рабочие дни
        {
            _baseEmployee = this;
            _rand = rand;
            _office = office;
            _employeeName = employeeName;

            #region Добавление рабочих дней
            do
            {
                WorkingDay workingDay = new WorkingDay() { day = (DayOfWeek)rand.Next(7), startWorkingDay = rand.Next(8, 13), hours = rand.Next(6, 9) };
                if (!workingDays.Contains(workingDay))
                    workingDays.Add(workingDay);
            } while (workingDays.Count < 5);
            #endregion
        }

        public bool IsWorking(DateTime time)
        {
            bool res = false;
            foreach (var day in workingDays)
            {
                if (day.day == time.DayOfWeek && time.Hour >= day.startWorkingDay && time.Hour < (day.startWorkingDay + day.hours))
                    res = true;
            }
            return res;
        }

        public virtual void Do(OfficeTask task, DateTime startTaskTime)
        {
            Console.WriteLine(startTaskTime + " " + _employeeName + " Выполняется: " + task);
            _task = task;
            _startTaskTime = startTaskTime;
            _endTaskTime = startTaskTime.AddHours(_rand.Next(1, 3));
            IsAvailable = false; 
        }
    }
}
