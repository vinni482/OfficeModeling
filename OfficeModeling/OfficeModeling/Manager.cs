using System;

namespace OfficeModeling
{
    class Manager : Employee, IPosition
    {
        public static decimal _rate = 600;
        string _name = "Manager";

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

        public Manager(Employee baseEmployee)
            : base(baseEmployee)
        {
            _baseEmployee._office.onClock += TaskCompleted;
        }

        public Manager(int combiningPositions, Random rand, Office office, string employeeName)
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
                    case AdditionalPositions.Tester:
                        {
                            Tester tester = new Tester(this);
                            if (!positions.Contains(tester))
                                positions.Add(tester);
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
