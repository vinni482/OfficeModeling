using System;
using System.Collections.Generic;

namespace OfficeModeling
{
    class Office
    {
        enum Positions { Programmer, Designer, Tester, Manager, Director, Accountant, Cleaner };
        List<Employee> _employees = new List<Employee>();
        uint _employeesNumber { set; get; }

        public Office(uint employeesNumber)
        {
            Random rand = new Random();

            _employeesNumber = employeesNumber;
            
            _employees.Add(new Director(Convert.ToBoolean(rand.Next(2)))); //Случайно генерируется выполняет ли директор обязанности менеджера
            _employees.Add(new Manager(rand.Next(3))); //Случайно генерируется количество совмещаемых должностей (0-2)
            _employees.Add(new Accountant(Convert.ToBoolean(rand.Next(2))));
            
            if ((int)_employeesNumber - 3 > 0)
            {
                for (int i = 0; i < _employeesNumber - 3; i++)
                {
                    switch ((Positions)rand.Next(0, 7)) //Случайно выбираются сотрудники в офис
                    {
                        case Positions.Programmer:
                            {
                                _employees.Add(new Programmer(rand.Next(3))); 
                                break;
                            }
                        case Positions.Designer:
                            {
                                _employees.Add(new Designer(rand.Next(3)));
                                break;
                            }
                        case Positions.Tester:
                            {
                                _employees.Add(new Tester(rand.Next(3)));
                                break;
                            }
                        case Positions.Manager:
                            {
                                _employees.Add(new Manager(rand.Next(3)));
                                break;
                            }
                        case Positions.Director:
                            {
                                _employees.Add(new Director(Convert.ToBoolean(rand.Next(2)))); 
                                break;
                            }
                        case Positions.Accountant:
                            {
                                _employees.Add(new Accountant(Convert.ToBoolean(rand.Next(2)))); 
                                break;
                            }
                        case Positions.Cleaner:
                            {
                                _employees.Add(new Cleaner());
                                break;
                            }
                    }
                }
            }
        }

        public void Start()
        {
            if (_employeesNumber < 10)
                throw new Exception("Can't start the Office. The number of employees must be greater than 10.");
            else
            {
                Console.WriteLine(_employees[9].positions.Count);
                
            }
        }
    }
}