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
        public int VacAccumulate { get; set; }
        public int WorkDays { get; set; }
        public  float TakeVacation(float days)
        {
            var total = VacAccumulate - days;
            return total<0?0:total;
        }
        public bool Modified { get; set; }
        public int Work(int days , int top)
        {
            WorkDays = days;
            return WorkDays > top ? top : WorkDays;
        }
        public int VacDays { get; set; }
        public enum EmpType
        {
            Hourly,
            Salaried,
            Managers
        }
    }

    
}
