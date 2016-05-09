using System;

namespace OfficeModeling
{
    class Manager : Employee, IPosition
    {
        decimal _rate = 600;
        string _name = "Manager";

        public Manager() { }

        public Manager(int combiningPositions, Random rand)
        {
            #region Добавление рабочих дней
            do
            {
                WorkingDay workingDay = new WorkingDay() { day = (DayOfWeek)rand.Next(7), startWorkingDay = rand.Next(8, 13), hours = rand.Next(6, 9) };
                if (!workingDays.Contains(workingDay))
                    workingDays.Add(workingDay);
            } while (workingDays.Count < 5);
            #endregion

            for (int i = 0; i < combiningPositions; i++)
            {
                switch ((AdditionalPositions)rand.Next(0, 5))
                {
                    case AdditionalPositions.Programmer:
                        {
                            Programmer prog = new Programmer();
                            if (!positions.Contains(prog))
                                positions.Add(prog);
                            break;
                        }
                    case AdditionalPositions.Designer:
                        {
                            Designer designer = new Designer();
                            if (!positions.Contains(designer))
                                positions.Add(designer);
                            break;
                        }
                    case AdditionalPositions.Tester:
                        {
                            Tester tester = new Tester();
                            if (!positions.Contains(tester))
                                positions.Add(tester);
                            break;
                        }
                    case AdditionalPositions.Cleaner:
                        {
                            Cleaner cleaner = new Cleaner();
                            if (!positions.Contains(cleaner))
                                positions.Add(cleaner);
                            break;
                        }
                }
            }
        }

        public decimal Rate
        {
            get { return _rate; }
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
