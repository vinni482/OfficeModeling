using System;

namespace OfficeModeling
{
    class Manager : Employee, IPosition
    {
        public static decimal _rate = 600;
        string _name = "Manager";

        void TaskCompleted(DateTime time)
        {
            if (time == _baseEmployee._endTaskTime) //Какое-то оповещение добавить?
            {
                _baseEmployee._office._runningTasks.Remove(_task);
                _baseEmployee.IsAvailable = true;
            }
        }

        public Manager(Employee baseEmployee)
            : base(baseEmployee)
        {
            _baseEmployee._office.onClock += TaskCompleted;
        }

        public Manager(int combiningPositions, Random rand, Office office)
            : base(rand, office)
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
    }
}
