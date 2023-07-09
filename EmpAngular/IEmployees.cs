using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAngular
{
    public interface IEmployees
    {
        public int Work(int days, int top);
        public float TakeVacation(float days);

    }
}
