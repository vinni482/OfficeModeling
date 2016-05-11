using System;

namespace OfficeModeling
{
    class OfficeTask
    {
        public Guid guid; //Уникальный идентификатор задачи
        public string name; //Название задачи
        public int priority; //Приоритет задачи
        public Type agent; //Исполнитель
        public decimal rate; //Стоимость выполнения часа задания

        public override bool Equals(object obj)
        {
            return this.guid == ((OfficeTask)obj).guid;
        }

        public override string ToString()
        {
            return name + " (priority: " + priority + ", rate: " + rate +  ")";
        }
    }
}
