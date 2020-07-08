using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Thinktecture.Docker.WebAPI.Configuration;
using Thinktecture.Docker.WebAPI.Models;


namespace Thinktecture.Docker.WebAPI.Controllers
{
    [Route("cars")]
    [ApiController]
    public class CarsController : Controller
    {
        private static IList<CarDetailsModel> Store = new List<CarDetailsModel>
        {
            new CarDetailsModel { Id = Guid.NewGuid(), Make = "BMW", Model = "M240i", Power = 340, Transmission = Transmission.SemiAutomatic },
            new CarDetailsModel { Id = Guid.NewGuid(), Make = "Audi", Model = "A6 Avant Competition", Power = 336, Transmission = Transmission.SemiAutomatic },
            new CarDetailsModel { Id = Guid.NewGuid(), Make = "Mercedes Benz", Model = "CLA 45S Coupe", Power = 387, Transmission =  Transmission.SemiAutomatic},
            new CarDetailsModel { Id = Guid.NewGuid(), Make = "Volkswagen", Model = "Golf R", Power = 300, Transmission = Transmission.SemiAutomatic},
            new CarDetailsModel { Id = Guid.NewGuid(), Make = "BMW", Model = "M2 CS", Power = 450, Transmission = Transmission.Manual}
        };

        public CarsController(IMapper mapper, ApiConfiguration apiConfiguration, ILogger<CarsController> logger)
        {
            Mapper = mapper;
            ApiConfiguration = apiConfiguration;
            Logger = logger;
        }

        public IMapper Mapper { get; }
        public ApiConfiguration ApiConfiguration { get; }
        public ILogger<CarsController> Logger { get; }

        [HttpGet]
        [Route("")]
        public IActionResult GetCars()
        {
            try
            {
                var cars = Mapper.Map<IEnumerable<CarListModel>>(Store);
                return Ok(cars.Take(ApiConfiguration.Limit));
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Error while requesting list of cars");
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id:guid}", Name = "CarDetails")]
        public IActionResult GetCarById([FromRoute] Guid id)
        {
            var found = Store.FirstOrDefault(c => c.Id == id);
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteCarById([FromRoute] Guid id)
        {
            var found = Store.FirstOrDefault(c => c.Id == id);
            if (found == null)
            {
                return NotFound();
            }
            Store.Remove(found);
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateCar([FromBody] CarCreateModel model)
        {
            try
            {
                var newCar = Mapper.Map<CarDetailsModel>(model);
                newCar.Id = Guid.NewGuid();

                Store.Add(newCar);
                Logger.LogInformation($"New Car has been added with ID {newCar.Id}");

                return CreatedAtRoute("CarDetails", new { id = newCar.Id });
            }catch(Exception exception)
            {
                Logger.LogError(exception, "Error while creating new car");
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateCar([FromRoute]Guid id, [FromBody]CarUpdateModel model)
        {
            var found = Store.FirstOrDefault(c => c.Id == id);
            if (found == null)
            {
                return NotFound();
            }

            try
            {
                found.Make = model.Make;
                found.Model = model.Model;
                found.Power = model.Power;
                found.Transmission = model.Transmission;

                Logger.LogInformation($" Car with ID {found.Id} has been updated");
                return Ok();
            }catch(Exception exception)
            {
                Logger.LogError(exception, "Error while updating a car");
                return StatusCode(500);
            }
        }


        
    }
}
