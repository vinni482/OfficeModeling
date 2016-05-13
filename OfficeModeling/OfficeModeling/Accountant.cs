using System;

namespace OfficeModeling
{
    class Accountant : Employee, IPosition
    {
        public static decimal _rate = 500;
        string _name = "Accountant";

        void TaskCompleted(DateTime time)
        {
            if (time == _baseEmployee._endTaskTime) //Какое-то оповещение добавить?
            {
                _baseEmployee._office._runningTasks.Remove(_task);
                _baseEmployee.IsAvailable = true;
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
