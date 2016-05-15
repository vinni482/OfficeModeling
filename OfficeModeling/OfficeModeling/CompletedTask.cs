using System;

namespace OfficeModeling
{
    class CompletedTask
    {
        public string name { set; get; }
        public DateTime startTime { set; get; }
        public DateTime endTime { set; get; }
        public decimal rate { set; get; }

        public override string ToString()
        {
            return startTime + " - " + endTime + " " + name + " " + rate + "$ per hour";
        }
    }
}
