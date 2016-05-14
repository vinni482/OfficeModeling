using System;

namespace OfficeModeling
{
    class Accountant : Employee, IPosition
    {
        public static decimal _rate = 500;
        string _name = "Accountant";

        void TaskCompleted(object sender, DateTime time)
        {
            if (time == _baseEmployee._endTaskTime)
            {
                string info = _baseEmployee._endTaskTime + " " + _baseEmployee._employeeName + " Завершено: " + _baseEmployee._task;
                if (!(sender as Office).info.Contains(info))
                {
                    (sender as Office).info.Add(info);
                    Console.WriteLine(info);
                    _baseEmployee._office._runningTasks.Remove(_baseEmployee._task);
                    CompletedTask completedTask = new CompletedTask
                    {
                        startTime = _baseEmployee._startTaskTime,
                        endTime = _baseEmployee._endTaskTime,
                        name = _baseEmployee._task.name,
                        rate = _baseEmployee._task.rate
                    };
                    _baseEmployee.completedTasks.Add(completedTask);
                    _baseEmployee.IsAvailable = true;
                }
            }
        }

        public Accountant(Employee baseEmployee)
            : base(baseEmployee)
        {
            _baseEmployee._office.onClock += TaskCompleted;
        }

        public Accountant(bool IsCombines, Random rand, Office office, string employeeName)
            : base(rand, office, employeeName)
        {
            office.onClock += TaskCompleted;

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
