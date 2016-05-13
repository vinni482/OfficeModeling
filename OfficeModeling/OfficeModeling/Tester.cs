using System;

namespace OfficeModeling
{
    class Tester : Employee, IPosition
    {
        public static decimal _rate = 700;
        string _name = "Tester";

        void TaskCompleted(DateTime time)
        {
            if (time == _baseEmployee._endTaskTime) //Какое-то оповещение добавить?
            {
                _baseEmployee._office._runningTasks.Remove(_task);
                _baseEmployee.IsAvailable = true;
            }
        }

        public Tester(Employee baseEmployee)
            : base(baseEmployee)
        {
            _baseEmployee._office.onClock += TaskCompleted;
        }

        public Tester(int combiningPositions, Random rand, Office office, string employeeName)
            : base(rand, office, employeeName)
        {
            office.onClock += TaskCompleted;
            
            #region Добавление совмещаемых должностей
            for (int i = 0; i < combiningPositions; i++)
            {
                switch ((AdditionalPositions)rand.Next(0, 5))
                {
                    case AdditionalPositions.Programmer:
                        {
                            Programmer prog = new Programmer(this);
                            if (!positions.Contains(prog))
                                positions.Add(prog);
                            break;
                        }
                    case AdditionalPositions.Designer:
                        {
                            Designer designer = new Designer(this);
                            if (!positions.Contains(designer))
                                positions.Add(designer);
                            break;
                        }
                    case AdditionalPositions.Manager:
                        {
                            Manager manager = new Manager(this);
                            if (!positions.Contains(manager))
                                positions.Add(manager);
                            break;
                        }
                    case AdditionalPositions.Cleaner:
                        {
                            Cleaner cleaner = new Cleaner(this);
                            if (!positions.Contains(cleaner))
                                positions.Add(cleaner);
                            break;
                        }
                }
            } 
            #endregion
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
