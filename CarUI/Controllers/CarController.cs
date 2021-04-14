using CarUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CarUI.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> ViewAllCars()
        {
            //if (HttpContext.Session.GetString("token") == null)
            //{
            //    return RedirectToAction("Login", "Auth");
            //}
            List<Car> carlist = new List<Car>();
            using (var client = new HttpClient())
            {

                // string token = HttpContext.Session.GetString("token");

                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                //client.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue("Bearer", token);

                client.BaseAddress = new Uri("https://localhost:44393/api/");//(connecting http client to web api)
                //HTTP GET
                var result = await client.GetAsync("Car/GetAllCars");//(getting all the details)
                // responseTask.Wait();

                // var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //var readTask = result.Content.ReadAsAsync<BusinessMaster>();

                    var jsoncontent = await result.Content.ReadAsStringAsync();//( json to string)
                    List<Car> cars = JsonConvert.DeserializeObject<List<Car>>(jsoncontent);//(string to model)
                    //readTask.Wait();



                    carlist = cars;
                }
                else
                {
                    carlist = null;



                    //ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                return View(carlist);
            }



        }
        public ActionResult CreateCar()
        {
            return View();
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCar(Car car)
        {
            //if (HttpContext.Session.GetString("token") == null)
            //{
            //    return RedirectToAction("Login", "Auth");
            //}



            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    // string token = HttpContext.Session.GetString("token");



                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    //client.DefaultRequestHeaders.Authorization =
                    //    new AuthenticationHeaderValue("Bearer", token);




                    client.BaseAddress = new Uri("https://localhost:44393/");



                    var jsonstring = JsonConvert.SerializeObject(car);



                    var content = new StringContent(jsonstring, System.Text.Encoding.UTF8, "application/json");




                    var response = await client.PostAsync("api/Car/CreateCarDetails", content);





                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("ViewAllCars");//()
                    }
                    else
                    {
                        ViewBag.Message = "Failed to create Car";
                    }
                }




            }
            return View(car);
        }

        public async Task<ActionResult> GetByCarName(string name)
        {
            Car c = new Car();

            using (var client = new HttpClient())
            {
                // string token = HttpContext.Session.GetString("token");

                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                //client.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue("Bearer", token);



                client.BaseAddress = new Uri("https://localhost:44393/api/");
                //HTTP GET
                var result = await client.GetAsync("Car/GetCarByName/" + name);
                // responseTask.Wait();



                // var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //var readTask = result.Content.ReadAsAsync<BusinessMaster>();

                    var jsoncontent = await result.Content.ReadAsStringAsync();
                    Car cr = JsonConvert.DeserializeObject<Car>(jsoncontent);
                    //readTask.Wait();



                    c = cr;
                }
                else //web api sent error response 
                {
                    //log response status here..



                    c = null;



                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(c);
        }

        [HttpGet("UpdateCar/{id}")]
        public async Task<ActionResult> UpdateCar(int id)
        {
            //if (HttpContext.Session.GetString("token") == null)
            //{
            //    return RedirectToAction("Login", "Auth");
            //}
            var car = new Car();
            using (var client = new HttpClient())
            {
                //  string token = HttpContext.Session.GetString("token");



                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                //client.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue("Bearer", token);

                client.BaseAddress = new Uri("https://localhost:44393/api/");
                //HTTP GET
                var result = await client.GetAsync("Car/GetCarById/" + id);
                // responseTask.Wait();

                // var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //var readTask = result.Content.ReadAsAsync<BusinessMaster>();

                    var jsoncontent = await result.Content.ReadAsStringAsync();
                    Car c1 = JsonConvert.DeserializeObject<Car>(jsoncontent);
                    //readTask.Wait();
                    car = c1;


                    // return View(c1);
                }
                else
                {
                    return NotFound();



                    //ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

            }
            return View(car);
        }

        //this is for model binding

        [HttpPost("UpdateCar/{id}")]
        public async Task<ActionResult> UpdateCar(int id, Car car)
        {
            //    if (HttpContext.Session.GetString("token") == null)
            //    {
            //        return RedirectToAction("Login", "Auth");
            //    }
            if (ModelState.IsValid)
            {
                var client = new HttpClient();

                // string token = HttpContext.Session.GetString("token");

                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                //client.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue("Bearer", token);

                client.BaseAddress = new Uri("https://localhost:44393/");



                var jsonstring = JsonConvert.SerializeObject(car);



                var content = new StringContent(jsonstring, System.Text.Encoding.UTF8, "application/json");




                var response = await client.PutAsync("api/Car/UpdateCarDetails", content);





                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return RedirectToAction("ViewAllCars");
                }



                else
                {
                    ViewBag.Message = "Failed to update cars";
                }
            }
            return View(car);
        }

      //[HttpDelete("DeleteCar/{name}")]
        public async Task<ActionResult> DeleteCar(string name)
        {
            //    if (HttpContext.Session.GetString("token") == null)
            //    {
            //        return RedirectToAction("Login", "Auth");
            //    }
            if (ModelState.IsValid)
            {
                var client = new HttpClient();

                // string token = HttpContext.Session.GetString("token");

                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                //client.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue("Bearer", token);

                client.BaseAddress = new Uri("https://localhost:44393/");//(for connecting web api)



             //   var jsonstring = JsonConvert.SerializeObject(car);



               // var content = new StringContent(jsonstring, System.Text.Encoding.UTF8, "application/json");

                var response = await client.DeleteAsync("api/Car/DeleteCarDetails/" + name);





                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return RedirectToAction("ViewAllCars");
                }



                else
                {
                    ViewBag.Message = "Failed to delete cars";
                }
            }
            return  RedirectToAction("ViewAllCars");
        }
    }
}
