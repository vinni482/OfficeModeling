using System;

namespace OfficeModeling
{
    class OfficeTask
    {
        public Guid guid; //Уникальный идентификатор задачи
        public string name; //Название задачи
        public int priority; //Приоритет задачи
        public string agent; //Исполнитель

        public override bool Equals(object obj)
        {
            return this.guid == ((OfficeTask)obj).guid;
        }

        public override string ToString()
        {
            return name + " (priority: " + priority + ")";
        }
    }
}
