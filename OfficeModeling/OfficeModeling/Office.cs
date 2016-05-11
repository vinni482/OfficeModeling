using System;
using System.Collections.Generic;

namespace OfficeModeling
{
    class Office
    {
        enum Positions { Programmer, Designer, Tester, Manager, Director, Accountant, Cleaner };
        List<Employee> _employees = new List<Employee>();
        uint _employeesNumber { set; get; }

        public List<OfficeTask> _tasks = new List<OfficeTask>();

        public Office(uint employeesNumber)
        {
            Random rand = new Random();

            _employeesNumber = employeesNumber;
            
            _employees.Add(new Director(Convert.ToBoolean(rand.Next(2)), rand, this)); //Случайно генерируется выполняет ли директор обязанности менеджера
            _employees.Add(new Manager(rand.Next(3), rand)); //Случайно генерируется количество совмещаемых должностей (0-2)
            _employees.Add(new Accountant(Convert.ToBoolean(rand.Next(2)), rand));
            
            if ((int)_employeesNumber - 3 > 0)
            {
                for (int i = 0; i < _employeesNumber - 3; i++)
                {
                    switch ((Positions)rand.Next(0, 7)) //Случайно выбираются сотрудники в офис
                    {
                        case Positions.Programmer:
                            {
                                _employees.Add(new Programmer(rand.Next(3), rand)); 
                                break;
                            }
                        case Positions.Designer:
                            {
                                _employees.Add(new Designer(rand.Next(3), rand));
                                break;
                            }
                        case Positions.Tester:
                            {
                                _employees.Add(new Tester(rand.Next(3), rand));
                                break;
                            }
                        case Positions.Manager:
                            {
                                _employees.Add(new Manager(rand.Next(3), rand));
                                break;
                            }
                        case Positions.Director:
                            {
                                Director director = new Director(Convert.ToBoolean(rand.Next(2)), rand, this);
                                _employees.Add(director); 
                                break;
                            }
                        case Positions.Accountant:
                            {
                                _employees.Add(new Accountant(Convert.ToBoolean(rand.Next(2)), rand)); 
                                break;
                            }
                        case Positions.Cleaner:
                            {
                                _employees.Add(new Cleaner(true, rand));
                                break;
                            }
                    }
                }
            }
        }

        public delegate void ForEmployees(DateTime time);
        public event ForEmployees onClock;

        public void Start()
        {
            if (_employeesNumber < 10)
                throw new Exception("Can't start the Office. The number of employees must be greater than 10.");
            else
            {
                foreach (var item in _employees) //Проходим по списку сотрудников раз в час!
                {
                    Console.WriteLine("Основная должность - " + item);

                    Console.WriteLine(item.IsWorking(new DateTime(2016, 05, 9, 12, 30, 0)));

                    foreach (var days in item.workingDays)
                    {
                        Console.WriteLine("\t" + days.day + " " + days.startWorkingDay + "-" + (days.hours + days.startWorkingDay));
                    }

                    foreach (var item2 in item.positions)
                    {
                        Console.WriteLine("\t\tСовмещает: " + item2);
                    }
                }


                for (int day = 1; day < 31; day++)
                {
                    for (int hour = 0; hour < 24; hour++)
                    {
                        onClock(new DateTime(2016, 06, day, hour, 0, 0));
                    }
                }   
            }
        }
    }
}