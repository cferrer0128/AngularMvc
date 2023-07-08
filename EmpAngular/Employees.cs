using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAngular
{
    public class Employees:IEmployees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployeeType { get; set; }
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

        public enum EmpType
        {
            Hourly,
            Salaried,
            Managers
        }
    }

    
}
