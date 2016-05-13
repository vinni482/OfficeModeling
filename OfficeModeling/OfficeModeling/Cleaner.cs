using System;

namespace OfficeModeling
{
    class Cleaner : Employee, IPosition
    {
        public static decimal _rate = 200;
        string _name = "Cleaner";

        void TaskCompleted(DateTime time)
        {
            if (time == _baseEmployee._endTaskTime)
            {
                Console.WriteLine(_baseEmployee._endTaskTime + " Завершено: " + _baseEmployee._task);
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

        public Cleaner(Employee baseEmployee)
            : base(baseEmployee)
        {
            _baseEmployee._office.onClock += TaskCompleted;
        }

        public Cleaner(bool ConstructorForTrueCleaner, Random rand, Office office, string employeeName)
            : base(rand, office, employeeName)
        {
            office.onClock += TaskCompleted;
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
