using APIConsume.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace APIConsume.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            LoginViewModel user = new LoginViewModel
            {
                Username = username,
                Password = password
            };

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("https://localhost:7227/auth"))
                {
                    Console.WriteLine(response);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                       





                        //var reservation = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);

                        return View("Login", (object)"success");
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        return View("Login", (object)errorResponse);
                    }
                }
            }
        }

       
        //public async Task<IActionResult> Index()
        //{
        //    Console.WriteLine("Hii");
        //    var jwt = Request.Cookies["jwtcookie"];

        //    using (var httpClient = new HttpClient())
        //    {
        //        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);

        //        using (var response = await httpClient.GetAsync("http://localhost:5279/Reservation"))
        //        {
        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                string apiResponse = await response.Content.ReadAsStringAsync();
        //                List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
        //                Console.WriteLine(apiResponse);
        //                return View(reservations);
        //            }
        //            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        //            {
        //                return Unauthorized();
        //            }
        //        }
        //    }

        //    return Ok();
        //}














        //[HttpPost]

        //public async Task<IActionResult> DeleteReservation(int ReservationId)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.DeleteAsync("http://localhost:5279/Login/Delete/" + ReservationId))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //        }

        //        // Fetch the updated list of reservations
        //        using (var resResponse = await httpClient.GetAsync("http://localhost:5279/Login/GetAll"))
        //        {
        //            if (resResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                string resApiResponse = await resResponse.Content.ReadAsStringAsync();
        //                List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(resApiResponse);

        //                // Pass the updated reservations to the Index view
        //                return View("Index", reservations);
        //            }
        //            else
        //            {
        //                List<Reservation> reservations = new List<Reservation>();

        //                return View("Index", reservations); // Assuming you have an Error view to handle errors
        //            }
        //        }
        //    }
        //}



        //public async Task<ActionResult<List<Reservation>>> Index()
        //{
        //    List<Reservation> reservations = new List<Reservation>();

        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync("http://localhost:5279/Login/GetAll"))
        //        {
        //            Console.WriteLine(response);
        //            string apiResponse = await response.Content.ReadAsStringAsync();


        //            //reservations = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
        //        }
        //    }
        //    return View(reservations);

        //}
        //   public ViewResult GetReservation() => View();

        //   [HttpPost ]
        //   public async Task<IActionResult>GetReservation(int id)
        //   {
        //       Reservation reservation =null;
        //       using (var httpClient = new HttpClient())
        //       {
        //           using(var response = await httpClient.GetAsync("http://localhost:5018/api/Reservation/" + id))
        //           {
        //               if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //               {
        //                    string apiResponse = await response.Content.ReadAsStringAsync();
        //               reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
        //                   Console.WriteLine(apiResponse);
        //               }
        //               else
        //               {
        //                   ViewBag.StatusCode = response.StatusCode;
        //               }

        //           }
        //       }

        //       return View( reservation);
        //   }


        //   public ViewResult AddReservation() => View();


        //   [HttpPost]
        //public async Task<IActionResult> AddReservation(Reservation reservation)
        //   {
        //       Reservation newBooking = new Reservation();
        //       using (var httpClient = new HttpClient())
        //       {
        //           StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");
        //           using(var response = await httpClient.PostAsync("http://localhost:5018/api/Reservation/",content))
        //           {
        //               if(response.StatusCode == System.Net.HttpStatusCode.OK)
        //               {
        //                   string apiResponse = await response.Content.ReadAsStringAsync();
        //                   newBooking = JsonConvert.DeserializeObject<Reservation>(apiResponse);
        //                   Console.WriteLine(apiResponse);
        //               }

        //           }
        //       }

        //       return View(newBooking);

        //   }
        //   public ViewResult UpdateReservation() => View();

        //   [HttpGet]

        //   public async Task<IActionResult> UpdateReservation(int Id)
        //   {
        //       Reservation reservation = new Reservation();
        //       using (var httpClient = new HttpClient())
        //       {
        //           using (var response =
        //               await httpClient.GetAsync("http://localhost:5018/api/Reservation/" + Id))
        //           {

        //               if (response.StatusCode == System.Net.HttpStatusCode.OK)

        //               {
        //                   string apiResponse = await response.Content.ReadAsStringAsync();
        //                   Console.WriteLine(apiResponse);
        //                   reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
        //               }
        //               else
        //               {
        //                   ViewBag.StatusCode = response.StatusCode;
        //               }
        //           }

        //       }
        //       return View(reservation);
        //   }



        //   //[HttpPost]
        //   //public async Task<IActionResult> UpdateReservation(Reservation reservation)
        //   //{
        //   //    Reservation UpdateBooking = new Reservation();
        //   //    using (var httpClient = new HttpClient())
        //   //    {
        //   //        //var json = JsonConvert.SerializeObject(reservation);

        //   //        //var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

        //   //        //using (var response = await httpClient.PutAsync("http://localhost:5018/api/Reservation/" , jsonContent ))
        //   //        //{
        //   //        //    string apiResponse = await response.Content.ReadAsStringAsync();
        //   //        //    UpdateBooking = JsonConvert.DeserializeObject<Reservation>(apiResponse);
        //   //        //    Console.WriteLine(apiResponse);
        //   //        //    ViewBag.Result = "success";
        //   //        //}

        //   //        var content = new MultipartFormDataContent();
        //   //        content.Add(new StringContent(reservation.Id.ToString()), "id");
        //   //        content.Add(new StringContent(reservation.Name), "name");
        //   //        content.Add(new StringContent(reservation.StartLocation), "startLocation");
        //   //        content.Add(new StringContent(reservation.EndLocation), "endLocation");

        //   //        using (var response = await httpClient.PutAsync("http://localhost:5018/api/Reservation/", content))
        //   //        {
        //   //            string apiResponse = await response.Content.ReadAsStringAsync();
        //   //            UpdateBooking = JsonConvert.DeserializeObject<Reservation>(apiResponse);
        //   //            Console.WriteLine(apiResponse);
        //   //            ViewBag.Result = "success";
        //   //        }

        //   //    }
        //   //    return View(UpdateBooking);
        //   //}

        //   [HttpPost]
        //   public async Task<IActionResult> UpdateReservation(Reservation reservation)
        //   {
        //       Reservation UpdateBooking = new Reservation();
        //       using (var httpClient = new HttpClient())
        //       {
        //           var content = new MultipartFormDataContent();
        //           content.Add(new StringContent(reservation.Id.ToString()), "id");
        //           content.Add(new StringContent(reservation.Name), "Name");
        //           content.Add(new StringContent(reservation.StartLocation), "StartLocation");
        //           content.Add(new StringContent(reservation.EndLocation), "EndLocation");
        //           using (var response = await httpClient.PutAsync("http://localhost:5018/api/Reservation/", content))
        //           {
        //               string apiResponse = await response.Content.ReadAsStringAsync();
        //               UpdateBooking = JsonConvert.DeserializeObject<Reservation>(apiResponse);
        //               Console.WriteLine(apiResponse);
        //               ViewBag.Result = "Success";

        //           }


        //       }
        //       return View(UpdateBooking);
        //   }
        //   [HttpPost]

        //   public async Task<IActionResult> DeleteReservation(int ReservationId)
        //   {
        //       using (var httpClient = new HttpClient())
        //       {
        //           using (var response = await httpClient.DeleteAsync("http://localhost:5018/api/Reservation/"+ ReservationId))
        //           {
        //               string apiResponse = await response.Content.ReadAsStringAsync();
        //           }
        //       }

        //       return RedirectToAction("Index");
        //   }


        //   [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //   public IActionResult Error()
        //   {
        //       return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //   }



    }
}
