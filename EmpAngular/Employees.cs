using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAngular
{
    public class Employees:IEmployees
    {
        public string Name { get; set; }
        public EmpType EmpType { get; set; }
        public int VacAccumalate { get; set; }
        public int WorkDays { get; set; }
        public float TakeVacation(float days)
        {
            return VacAccumalate - days;
        }

        public int Work(int days)
        {
            return WorkDays + days;
        }
    }

    public class EmpType
    {
        public enum Type : int
        {
          
            Hourly,
            Salaried,
            Managers

        }
    }
}
