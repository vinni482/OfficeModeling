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
        List<OfficeTask> _runningTasks = new List<OfficeTask>();

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
                        DateTime time = new DateTime(2016, 06, day, hour, 0, 0);
                        onClock(time);

                        //Проходим по списку задач, раздавая задания соответствующим сотрудникам, если есть в наличии, и свободен, и если подходит по должности
                        for(int i=0; i<_tasks.Count; i++)
                        {
                            for(int j=0; j<_employees.Count; j++)
                            {
                                //Вызываем метод у сотрудника. В него передаем задачу. В сотруднике генерируем случайное время 1-2 часа. Ставим флаг available=false
                                //Из задач переносим в список выполняющихся.
                                //И подписаться ранее надо на сотрудника. Он сгенерирует потом событие завершения выполнения задания и вернет задачу, которую удалить
                                if (_employees[j].IsAvailable && _employees[j].IsWorking(time)) //Если сотрудник в смене и не занят
                                {
                                    if (_employees[j].GetType() == _tasks[i].agent) //Проверяем, подходит ли по должности
                                    {
                                        _employees[j].Do(_tasks[i], time);
                                        _runningTasks.Add(_tasks[i]);
                                        _tasks.Remove(_tasks[i]);
                                        i--;
                                        break; //Завершает этот цикл, берем следующую задачу
                                    }
                                    else //Если не подходит
                                    {
                                        foreach (var position in _employees[j].positions) //Проверяем совмещаемые должности
                                        {
                                            if (position.GetType() == _tasks[i].agent)
                                            {
                                                _employees[j].Do(_tasks[i], time);
                                                _runningTasks.Add(_tasks[i]);
                                                _tasks.Remove(_tasks[i]);
                                                i--;
                                                j = _employees.Count; //Чтобы выйти из цикла проверки оставшихся сотрудников для задачи, которую удалили
                                                break; //Завершаем цикл проверки совмещаемых должностей
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (var item in _tasks)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}