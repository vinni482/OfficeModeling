using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OfficeModeling
{
    class Office
    {
        enum Positions { Programmer, Designer, Tester, Manager, Director, Accountant, Cleaner };
        List<Employee> _employees = new List<Employee>();

        Freelance _freelance { set; get; }
        uint _employeesNumber { set; get; }

        public List<OfficeTask> _tasks = new List<OfficeTask>();
        public List<OfficeTask> _runningTasks = new List<OfficeTask>();
        public List<string> info = new List<string>();

        public Office(uint employeesNumber)
        {
            Random rand = new Random();

            _employeesNumber = employeesNumber;

            _employees.Add(new Director(Convert.ToBoolean(rand.Next(2)), rand, this, "Employee1")); //Случайно генерируется выполняет ли директор обязанности менеджера
            _employees.Add(new Manager(rand.Next(3), rand, this, "Employee2")); //Случайно генерируется количество совмещаемых должностей (0-2)
            _employees.Add(new Accountant(Convert.ToBoolean(rand.Next(2)), rand, this, "Employee3"));
            _freelance = new Freelance(rand, this);

            if ((int)_employeesNumber - 3 > 0)
            {
                for (int i = 0; i < _employeesNumber - 3; i++)
                {
                    switch ((Positions)rand.Next(0, 7)) //Случайно выбираются сотрудники в офис
                    {
                        case Positions.Programmer:
                            {
                                Programmer programmer = new Programmer(rand.Next(3), rand, this, "Employee" + (i+4));
                                _employees.Add(programmer);
                                break;
                            }
                        case Positions.Designer:
                            {
                                Designer designer = new Designer(rand.Next(3), rand, this, "Employee" + (i+4));
                                _employees.Add(designer);
                                break;
                            }
                        case Positions.Tester:
                            {
                                Tester tester = new Tester(rand.Next(3), rand, this, "Employee" + (i+4));
                                _employees.Add(tester);
                                break;
                            }
                        case Positions.Manager:
                            {
                                Manager manager = new Manager(rand.Next(3), rand, this, "Employee" + (i+4));
                                _employees.Add(manager);
                                break;
                            }
                        case Positions.Director:
                            {
                                Director director = new Director(Convert.ToBoolean(rand.Next(2)), rand, this, "Employee" + (i+4));
                                _employees.Add(director);
                                break;
                            }
                        case Positions.Accountant:
                            {
                                Accountant accountant = new Accountant(Convert.ToBoolean(rand.Next(2)), rand, this, "Employee" + (i+4));
                                _employees.Add(accountant);
                                break;
                            }
                        case Positions.Cleaner:
                            {
                                Cleaner cleaner = new Cleaner(true, rand, this, "Employee" + (i+4));
                                _employees.Add(cleaner);
                                break;
                            }
                    }
                }
            }
        }

        public delegate void ForEmployees(object sender, DateTime time);
        public event ForEmployees onClock;

        public void Start()
        {
            if (_employeesNumber < 10)
                throw new Exception("Can't start the Office. The number of employees must be greater than 10.");
            else
            {
                #region Отображение и сохранение списка сотрудников
                string info = string.Empty;
                foreach (var item in _employees)
                {
                    info += item._employeeName + " - " + item + "\r\n";
                    info += "\t\tГрафик работы:\r\n";
                    foreach (var day in item.workingDays)
                        info += "\t" + day.day + " " + day.startWorkingDay + "-" + (day.hours + day.startWorkingDay) + "\r\n";
                    info += "\t\tСовмещаемые позиции: ";
                    foreach (var position in item.positions)
                        info += position + "/";
                    info += "\r\n\r\n";
                }
                File.WriteAllText("EmployeesList.txt", info);
                Console.WriteLine(info);
                #endregion

                for (int day = 1; day < 31; day++)
                {
                    for (int hour = 0; hour < 24; hour++)
                    {
                        //System.Threading.Thread.Sleep(50);
                        DateTime time = new DateTime(2016, 06, day, hour, 0, 0);
                        onClock(this, time);

                        //Проходим по списку задач, раздавая задания соответствующим сотрудникам, если есть в наличии, и свободен, и если подходит по должности
                        for(int i=0; i<_tasks.Count; i++)
                        {
                            for(int j=0; j<_employees.Count; j++)
                            {
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

                        for (int i = 0; i < _tasks.Count; i++) //Оставшиеся задания отдаем на фриланс
                        {
                            if (_tasks[i].agent != typeof(Cleaner))
                            {
                                _freelance.Do(_tasks[i], time);
                                _runningTasks.Add(_tasks[i]);
                                _tasks.Remove(_tasks[i]);
                                i--;
                                break;
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