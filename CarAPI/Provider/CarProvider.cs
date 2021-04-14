using CarAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarAPI.Provider
{
    public class CarProvider : ICarProvider
    {
        private readonly CarsDbContext context;
        public CarProvider(CarsDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> CreateCar(Car car)
        {
            context.Cars.Add(car);
            int count = await context.SaveChangesAsync();
            return count > 0;
        }

        public async Task<bool> DeleteCar(Car car)
        {
            context.Cars.Remove(car);
            int count = await context.SaveChangesAsync();
            return count > 0;

        }

        public async Task<List<Car>> GetAllCars()
        {
            var cars = await context.Cars.ToListAsync();
            return cars;
        }

        public async Task<Car> GetByCarId(int id)
        {
            var car = await context.Cars.FirstOrDefaultAsync(x => x.CarId == id);
                
            return car;
        }
        public async Task<Car> GetByCarName(string name)
        {
            var car = await context.Cars.FirstOrDefaultAsync(x => x.CarName == name);

            return car;
        }

        public async Task<bool> UpdateCar(Car car)
        {
            // var carobj = await GetByCarName(car.CarName);
            var carobj = await GetByCarId(car.CarId);
            carobj.CarName = car.CarName;
            carobj.CarModel = car.CarModel;
            carobj.DateOfLaunch = car.DateOfLaunch;
            carobj.Price = car.Price;
            int count = await context.SaveChangesAsync();
            return count > 0;
        }
    }
}
