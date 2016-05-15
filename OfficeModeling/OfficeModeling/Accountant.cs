using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OfficeModeling
{
    class Accountant : Employee, IPosition
    {
        public static decimal _monthRate = 1000;
        public static decimal _hourRate = 15; //Для фрилансеров
        string _name = "Accountant";

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

        public static void PayFreelancers(Office office, DateTime time)
        {
            decimal summ = 0;
            foreach (var freelancer in office._freelancers)
            {
                if(freelancer.completedTask.endTime.ToShortDateString() == time.ToShortDateString())
                {
                    freelancer.earned = freelancer.completedTask.rate * (freelancer.completedTask.endTime - freelancer.completedTask.startTime).Hours;
                    summ += freelancer.earned;
                }
            }
            Console.WriteLine(time.ToShortDateString() + " Выплачено фрилансерам: " + summ);
        }

        public static void PayEmployees(Office office, DateTime time)
        {
            decimal summ = 0;
            foreach (var emp in office._employees)
            {
                decimal week = 0;
                foreach (var task in emp.completedTasks)
                {
                    if(task.endTime > time.AddDays(-7) && emp.GetType() != typeof(Director) && emp.GetType() != typeof(Accountant) && emp.GetType() != typeof(Manager))
                    {
                        if(task.endTime.DayOfWeek == DayOfWeek.Saturday || task.endTime.DayOfWeek == DayOfWeek.Sunday)
                        {
                            task.rate *= 2;
                            week += task.rate * (task.endTime - task.startTime).Hours;
                        }
                        else
                            week += task.rate * (task.endTime - task.startTime).Hours;
                    }
                }
                emp.earned += week; //Сотруднику добавляем сумму стоимости всей выполненной работы за последнюю неделю
                summ += week;
            }
            Console.WriteLine(time.ToShortDateString() + " Выплачено сотрудникам: " + summ);
        }

        public static void SummaryReport(Office office)
        {
            List<CompletedTask> monthTasks = new List<CompletedTask>();
            foreach (var emp in office._employees) 
            {
                if(emp.GetType() == typeof(Accountant)) //Начислить зарплату бухгалтеру, директору и менеджеру
                    emp.earned = Accountant._monthRate;
                else if(emp.GetType() == typeof(Director))
                    emp.earned = Director._monthRate;
                else if (emp.GetType() == typeof(Manager))
                    emp.earned = Manager._monthRate;
                else //Всем остальным остаток с 28 до конца месяца
                {
                    foreach (var task in emp.completedTasks)
                    {
                        if (task.endTime.Day > 28)
                        {
                            if(task.endTime.DayOfWeek == DayOfWeek.Saturday || task.endTime.DayOfWeek == DayOfWeek.Sunday)
                            {
                                task.rate *= 2;
                                emp.earned += task.rate * (task.endTime - task.startTime).Hours;
                            }
                            else
                            {
                                emp.earned += task.rate * (task.endTime - task.startTime).Hours;
                            }
                        }
                        monthTasks.Add(task);
                    }
                }
            }
            foreach (var freelancer in office._freelancers)
            {
                monthTasks.Add(freelancer.completedTask);
            }
            monthTasks = monthTasks.OrderBy(a => a.startTime).ToList();

            string report = "\t\tВыполнено за месяц:\r\n";
            foreach (var task in monthTasks)
                report += task + "\r\n";
            decimal total = 0;
            foreach (var emp in office._employees)
                total += emp.earned;
            foreach (var freelancer in office._freelancers)
                total += freelancer.earned;
            report += "\tВыдано за месяц (в т.ч. директора/менеджеры/бухгалтеры): " + total;

            foreach (var freelancer in office._freelancers)
            {
                report += "\r\n\r\n" + "\t\tFreelancer" +
                    "\r\nВыполнено:\r\n" +
                    freelancer.completedTask + 
                    "\r\nПолучено: " + freelancer.earned;
            }

            foreach (var emp in office._employees)
            {
                report += "\r\n\r\n" + "\t\t" + emp._employeeName +
                    "\t\tОсновная должность - " + emp + 
                    "\r\nВыполнено:\r\n";
                foreach (var task in emp.completedTasks)
                {
                    report += task + "\r\n";
                }
                report += "Получено: " + emp.earned;
            }

            Console.WriteLine(report);
            File.WriteAllText("SummaryReport.txt", report);
        }

        public Accountant(Employee baseEmployee)
            : base(baseEmployee)
        {
            _baseEmployee._office.onClock += TaskCompleted;
        }

        public Accountant(bool IsCombines, Random rand, Office office, string employeeName)
            : base(rand, office, employeeName)
        {
            office.onClock += TaskCompleted;

            if (IsCombines)
                positions.Add(new Manager(this));
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
