using System;

namespace OfficeModeling
{
    class Program
    {
        static void Main(string[] args)
        {
            Office office = new Office(new Random().Next(10, 101));
            office.Start();
        }
    }
}
