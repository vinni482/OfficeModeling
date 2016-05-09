using System;

namespace OfficeModeling
{
    class Cleaner : Employee, IPosition
    {
        decimal _rate = 200;
        string _name = "Cleaner";

        public Cleaner() { }

        public Cleaner(bool ConstructorForTrueCleaner, Random rand)
        {
            #region Добавление рабочих дней
            do
            {
                WorkingDay workingDay = new WorkingDay() { day = (DayOfWeek)rand.Next(7), startWorkingDay = rand.Next(8, 13), hours = rand.Next(6, 9) };
                if (!workingDays.Contains(workingDay))
                    workingDays.Add(workingDay);
            } while (workingDays.Count < 5);
            #endregion
        }

        public string Name
        {
            get { return _name; }
        }

        public decimal Rate
        {
            get { return _rate; }
        }

        public override bool Equals(object obj)
        {
            return ((IPosition)obj).Name == this.Name;
        }
    }
}
