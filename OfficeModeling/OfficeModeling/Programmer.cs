using System;

namespace OfficeModeling
{
    class Programmer : Employee, IPosition
    {
        public static decimal _rate = 800;
        string _name = "Programmer";

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

        public Programmer(Employee baseEmployee)
            : base(baseEmployee)
        {
            _baseEmployee._office.onClock += TaskCompleted;
        }

        public Programmer(int combiningPositions, Random rand, Office office, string employeeName)
            : base(rand, office, employeeName)
        {
            office.onClock += TaskCompleted;
            
            #region Добавление совмещаемых должностей
            for (int i = 0; i < combiningPositions; i++)
            {
                switch ((AdditionalPositions)rand.Next(0, 5))
                {
                    case AdditionalPositions.Designer:
                        {
                            Designer designer = new Designer(this);
                            if (!positions.Contains(designer))
                                positions.Add(designer);
                            break;
                        }
                    case AdditionalPositions.Tester:
                        {
                            Tester tester = new Tester(this);
                            if (!positions.Contains(tester))
                                positions.Add(tester);
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
