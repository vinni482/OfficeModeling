using System;
using System.Collections.Generic;

namespace OfficeModeling
{
    class Office
    {
        enum Employees { Programmers, Designers, Testers, Managers, Cleaners };

        List<Programmer> programmers = new List<Programmer>();
        List<Designer> designers = new List<Designer>();
        List<Tester> testers = new List<Tester>();
        List<Manager> managers = new List<Manager>();
        List<Director> directors = new List<Director>();
        List<Accountant> accountants = new List<Accountant>();
        List<Cleaner> cleaners = new List<Cleaner>();

        uint _employeesNumber { set; get; }

        public Office(uint employeesNumber)
        {
            _employeesNumber = employeesNumber;

            directors.Add(new Director()); //Директоров и бухгалтеров в офисе будет по одному
            managers.Add(new Manager());
            accountants.Add(new Accountant());

            if ((int)_employeesNumber - 3 > 0)
            {
                Random rand = new Random();

                for (int i = 0; i < _employeesNumber - 3; i++)
                {
                    switch ((Employees)rand.Next(0, 5))
                    {
                        case Employees.Programmers:
                            {
                                Console.WriteLine(Employees.Programmers);
                                break;
                            }
                        case Employees.Designers:
                            {
                                Console.WriteLine(Employees.Designers);
                                break;
                            }
                        case Employees.Testers:
                            {
                                Console.WriteLine(Employees.Testers);
                                break;
                            }
                        case Employees.Managers:
                            {
                                Console.WriteLine(Employees.Managers);
                                break;
                            }
                        case Employees.Cleaners:
                            {
                                Console.WriteLine(Employees.Cleaners);
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
                Console.WriteLine(_employeesNumber);
            }
        }
    }
}