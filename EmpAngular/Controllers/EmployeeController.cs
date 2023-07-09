using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController: ControllerBase
    {
        private int top = 0;
        private float tempAccu = 0;
        private float tempEmp = 0;
        private float vacEmp = 0;
        private static readonly string[] Names = new[]
        {
            "Cruz", "Gene", "Dell", "Cool", "Mathew", "Tom", "Mack", "Hot", "Silverdo", "Scorching","Kim","Biden"
        };

        private static readonly int[] EmpTypes = new[]
        {
            0, 1, 2
        };

        private static readonly int[] VacAcummulates = new[]
        {
            0,10, 20,30,40,50
        };

        private readonly int maxDays =260;

        private readonly ILogger<WeatherForecastController> _logger;
        public EmployeeController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<Employees> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 10).Select(index => new Employees
            {
                 Id = index,
                 EmployeeType = ((Employees.EmpType) EmpTypes[rng.Next(EmpTypes.Length)]).ToString(),
                 Name = Names[index],
                 VacAccumulate = 0,
                 WorkDays =0


            })
            .ToArray();
        }

        [HttpPost]
        public IEnumerable<Employees> Post(Employees[] emp)
        {
            var modifiedEmp = emp.FirstOrDefault(f => f.Modified == true);
           
            if (modifiedEmp!=null){
                modifiedEmp.WorkDays = modifiedEmp.WorkDays > maxDays ? maxDays : modifiedEmp.WorkDays;
                switch (modifiedEmp.EmployeeType)
                {
                    case "Hourly":
                        top = 10;
                        tempAccu = (modifiedEmp.WorkDays * top) / maxDays;
                        tempEmp = modifiedEmp.Work(modifiedEmp.WorkDays, maxDays);                       
                        modifiedEmp.VacAccumulate =(int) tempAccu;
                        vacEmp = modifiedEmp.TakeVacation(modifiedEmp.VacDays);
                        modifiedEmp.VacAccumulate = vacEmp > 0 ? (int)vacEmp : modifiedEmp.VacAccumulate;
                        break;
                    case "Salaried":
                        top = 15;
                        tempAccu = (modifiedEmp.WorkDays * top) / maxDays;
                        tempEmp = modifiedEmp.Work(modifiedEmp.WorkDays, maxDays);                      
                        modifiedEmp.VacAccumulate =(int) tempAccu;
                        vacEmp = modifiedEmp.TakeVacation(modifiedEmp.VacDays);
                        modifiedEmp.VacAccumulate = vacEmp > 0 ? (int)vacEmp : modifiedEmp.VacAccumulate;
                        break;
                    case "Managers":
                        top = 30;                        
                        tempAccu = (modifiedEmp.WorkDays * top) / maxDays;
                        tempEmp = modifiedEmp.Work(modifiedEmp.WorkDays, maxDays);
                        modifiedEmp.VacAccumulate = (int)tempAccu;
                        vacEmp = modifiedEmp.TakeVacation(modifiedEmp.VacDays);
                        modifiedEmp.VacAccumulate = vacEmp > 0 ? (int)vacEmp : modifiedEmp.VacAccumulate;
                        break;
                        
                    default:
                        break;

                }                    
               
            }
            for(var i=0; i< emp.Length; i++)
            {
                var item = emp[i];
                if (modifiedEmp.Id == item.Id)
                {
                    modifiedEmp.Modified = false;
                    emp[i] = modifiedEmp;
                }

            }
            
            return emp.ToArray();
        }
    }
   
}


