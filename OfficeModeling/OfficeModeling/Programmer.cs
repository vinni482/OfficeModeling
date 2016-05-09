using System;

namespace OfficeModeling
{
    class Programmer : Employee, IPosition
    {
        decimal _rate = 800;
        string _name = "Programmer";

        public Programmer() { }

        public Programmer(int combiningPositions, Random rand)
        {
            #region Добавление рабочих дней
            do
            {
                WorkingDay workingDay = new WorkingDay() { day = (DayOfWeek)rand.Next(7), startWorkingDay = rand.Next(8, 13), hours = rand.Next(6, 9) };
                if(!workingDays.Contains(workingDay))
                    workingDays.Add(workingDay);
            } while (workingDays.Count < 5);
            #endregion

            #region Добавление совмещаемых позиций
            for (int i = 0; i < combiningPositions; i++)
            {
                switch ((AdditionalPositions)rand.Next(0, 5))
                {
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
                    case AdditionalPositions.Manager:
                        {
                            Manager manager = new Manager();
                            if (!positions.Contains(manager))
                                positions.Add(manager);
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
            #endregion
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
