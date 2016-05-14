using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeModeling
{
    class Freelance
    {
        string _name = "Freelance";
        Random _rand;
        Office _office;
        OfficeTask _task;
        DateTime _startTaskTime;
        DateTime _endTaskTime;

        public List<CompletedTask> completedTasks = new List<CompletedTask>();

        public Freelance(Random rand, Office office)
        {
            _rand = rand;
            _office = office;
            office.onClock += TaskCompleted;
        }

        private void TaskCompleted(object sender, DateTime time)
        {
            if(time == _endTaskTime)
            {
                string info = _endTaskTime + " " + _name + " Завершено: " + _task;
                _office.info.Add(info);
                Console.WriteLine(info);
                _office._runningTasks.Remove(_task);
                CompletedTask completedTask = new CompletedTask
                {
                    startTime = _startTaskTime,
                    endTime = _endTaskTime,
                    name = _task.name,
                    rate = _task.rate
                };
                completedTasks.Add(completedTask);
            }
        }

        public void Do(OfficeTask task, DateTime startTaskTime)
        {
            Console.WriteLine(startTaskTime + " " + _name + " Выполняется: " + task);
            _task = task;
            _startTaskTime = startTaskTime;
            _endTaskTime = new DateTime(startTaskTime.Year, startTaskTime.Month, startTaskTime.Day, startTaskTime.Hour + _rand.Next(1, 3), startTaskTime.Minute, startTaskTime.Second);
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
