using System;

namespace OfficeModeling
{
    class Cleaner : Employee, IPosition
    {
        public static decimal _rate = 200;
        string _name = "Cleaner";

        void TaskCompleted(DateTime time)
        {
            if (time == _baseEmployee._endTaskTime) //Какое-то оповещение добавить?
            {
                _baseEmployee._office._runningTasks.Remove(_task);
                _baseEmployee.IsAvailable = true;
            }
        }

        public Cleaner(Employee baseEmployee)
            : base(baseEmployee)
        {
            _baseEmployee._office.onClock += TaskCompleted;
        }

        public Cleaner(bool ConstructorForTrueCleaner, Random rand, Office office)
            : base(rand, office)
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
    }
}
