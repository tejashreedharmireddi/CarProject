using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarUI.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string CarModel { get; set; }
        public int Price { get; set; }
        public DateTime DateOfLaunch { get; set; }
    }
}
