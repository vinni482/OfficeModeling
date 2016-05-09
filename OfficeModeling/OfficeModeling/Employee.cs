using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeModeling
{
    class Employee
    {
        public List<IPosition> positions = new List<IPosition>();
        public List<WorkingDay> workingDays = new List<WorkingDay>();

        public bool IsAvailable = true; //false - в случае, если в процессе выполнения задания
        public enum AdditionalPositions { Programmer, Designer, Tester, Manager, Cleaner };

        public bool IsWorking(DateTime time)
        {
            foreach (var day in workingDays)
            {
                if (day.day == time.DayOfWeek && time.Hour >= day.startWorkingDay && time.Hour < (day.startWorkingDay + day.hours))
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
