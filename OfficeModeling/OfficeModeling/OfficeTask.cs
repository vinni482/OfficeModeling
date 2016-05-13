using System;

namespace OfficeModeling
{
    class OfficeTask
    {
        public Guid guid { set; get; } //Уникальный идентификатор задачи
        public string name { set; get; } //Название задачи
        public int priority { set; get; } //Приоритет задачи
        public Type agent { set; get; } //Исполнитель
        public decimal rate { set; get; } //Стоимость выполнения часа задания

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
