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
            "Cruz", "Gene", "Dell", "Cool", "Mathew", "Tom", "Mack", "Hot", "Silverdo", "Scorching"
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
                 EmpType = (EmpType)Enum.Parse(typeof(EmpType),"0"),
                 Name = Names[rng.Next(Names.Length)],
                 VacAccumalate = 0,
                 WorkDays =0


            })
            .ToArray();
        }
    }
   
}


