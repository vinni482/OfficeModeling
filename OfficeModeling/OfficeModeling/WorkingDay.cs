using System;

namespace OfficeModeling
{
    class WorkingDay
    {
        public DayOfWeek day { set; get; } //День недели
        public int startWorkingDay { set; get; } //Время начала рабочего дня (час).
        public int hours { set; get; } //Количество рабочих часов.

        public override bool Equals(object obj) //Перегрузка эквивалентности обьектов рабочего дня для исключения возможности добавления в список рабочих дней уже имеющегося дня
        {
            return this.day == ((WorkingDay)obj).day;
        }
    }
}
