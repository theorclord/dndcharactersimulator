using DndCharacterSimulator.Generators;
using DndCharacterSimulator.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        [HttpPost]
        public ActionResult<City> GenerateCity(int populationSize, Dictionary<Race.RaceType, int> raceDistribution)
        {
            var city = CityGeneratorFactory.GenerateCity(2000, raceDistribution);

            return Ok(city);
        }
    }
}
