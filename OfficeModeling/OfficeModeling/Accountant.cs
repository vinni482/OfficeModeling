using System;

namespace OfficeModeling
{
    class Accountant : Employee, IPosition
    {
        public static decimal _rate = 500;
        string _name = "Accountant";

        public Accountant() { }

        public Accountant(bool IsCombines, Random rand)
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
            {
                positions.Add(new Manager());
            }
        }

        public override void Do(OfficeTask task, DateTime startTaskTime)
        {
            Console.WriteLine("Accountant");
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
