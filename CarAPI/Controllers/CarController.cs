using CarAPI.Models;
using CarAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepo repo;
        public CarController(ICarRepo repo)
        {
            this.repo = repo;
        }
        [HttpGet("GetAllCars")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ObjectResult> GetAllCars()
        {
            return Ok(await repo.GetAllCars());
        }
        [HttpGet("GetCarByName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ObjectResult> GetByCarName(string name)
        {
            return Ok(await repo.GetByCarName(name));
        }
        [HttpGet("GetCarById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ObjectResult> GetByCarId(int id)
        {
            return Ok(await repo.GetByCarId(id));
        }

        [HttpPost("CreateCarDetails")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ObjectResult> CreateCar(Car car)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            var Result = await repo.CreateCar(car);



            if (!Result)
            {
                return new ObjectResult("Database error") { StatusCode = 500 };
            }
            return new CreatedResult("FindCar", new { id = car.CarId});
          


        }
        [HttpDelete("DeleteCarDetails/{name}")]
        public async Task<ObjectResult> DeleteCar(string name)
        {
            var Obj = await repo.GetByCarName(name);



            if (Obj == null)
            {
                return new NotFoundObjectResult("No Car Found with name=" + name);
            }



            var Result = await repo.DeleteCar(Obj);



            if (!Result)
            {
                return new ObjectResult("Database error") { StatusCode = 500 };
            }
            return new ObjectResult("Object deleted") { StatusCode = 204 };



        }
        [HttpPut("UpdateCarDetails")]
        public async Task<ObjectResult> UpdateCar(Car car)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            var Result = await repo.UpdateCar(car);



            if (!Result)
            {
                return new ObjectResult("Database error") { StatusCode = 500 };
            }
            return Ok(car);



        }
    }
}
