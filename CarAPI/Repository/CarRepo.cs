using CarAPI.Models;
using CarAPI.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarAPI.Repository
{
    public class CarRepo : ICarRepo
    {
        private readonly ICarProvider carprov;

        public CarRepo(ICarProvider carprov)
        {
            this.carprov=carprov;
        }
        public Task<bool> CreateCar(Car car)
        {
           var c= carprov.CreateCar(car);
            return c;
        }

        public Task<bool> DeleteCar(Car car)
        {
            var c = carprov.DeleteCar(car);
            return c;
        }

        public async Task<List<Car>> GetAllCars()
        {
            //carprov.GetAllCars();
            return await carprov.GetAllCars();
        }

        public async Task<Car> GetByCarId(int id)
        {
            return await carprov.GetByCarId(id);
        }

        public async Task<Car> GetByCarName(string name)
        {
            return await carprov.GetByCarName(name);
        }

        public Task<bool> UpdateCar(Car car)
        {
            var c = carprov.UpdateCar(car);
            return c;
        }
    }
}
