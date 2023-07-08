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
                 VacAccumalate = VacAcummulates[rng.Next(VacAcummulates.Length)],
                 WorkDays =0


            })
            .ToArray();
        }

        [HttpPost]
        public IEnumerable<Employees> Post(Employees emp)
        {
            var rng = new Random();
            return Enumerable.Range(1, 10).Select(index => new Employees
            {
                Id = index,
                EmployeeType = ((Employees.EmpType)EmpTypes[rng.Next(EmpTypes.Length)]).ToString(),
                Name = Names[index],
                VacAccumalate = VacAcummulates[rng.Next(VacAcummulates.Length)],
                WorkDays = 0


            })
            .ToArray();
        }
    }
   
}


