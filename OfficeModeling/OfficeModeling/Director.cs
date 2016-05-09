using System;

namespace OfficeModeling
{
    class Director : Employee, IPosition
    {
        decimal _rate = 800;
        string _name = "Director";

        public Director() { }

        public Director(bool IsCombines, Random rand)
        {
            #region Добавление рабочих дней
            do
            {
                WorkingDay workingDay = new WorkingDay() { day = (DayOfWeek)rand.Next(7), startWorkingDay = rand.Next(8, 13), hours = rand.Next(6, 9) };
                if (!workingDays.Contains(workingDay))
                    workingDays.Add(workingDay);
            } while (workingDays.Count < 5);
            #endregion

            if (IsCombines)
                positions.Add(new Manager());
        }

        public decimal Rate
        {
            get { return _rate; }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public override bool Equals(object obj)
        {
            return ((IPosition)obj).Name == this.Name;
        }
    }
}
