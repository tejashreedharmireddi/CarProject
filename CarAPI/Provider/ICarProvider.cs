using CarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarAPI.Provider
{
   public interface ICarProvider
    {
        Task<List<Car>> GetAllCars();
        Task<Car> GetByCarName(string name);
        Task<Car> GetByCarId(int id);
        Task<bool> CreateCar(Car car);
        Task<bool> UpdateCar(Car car);

        Task<bool> DeleteCar(Car car);
    }
}
