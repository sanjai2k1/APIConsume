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

        public async Task<IActionResult> Index()
        {
            List<Reservation> reservations = new List<Reservation>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5018/api/Reservation"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservations = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
                }
            }
            return View(reservations);

        }
        public ViewResult GetReservation() => View();

        [HttpPost ]
        public async Task<IActionResult>GetReservation(int id)
        {
            Reservation reservation =null;
            using (var httpClient = new HttpClient())
            {
                using(var response = await httpClient.GetAsync("http://localhost:5018/api/Reservation/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                         string apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                        Console.WriteLine(apiResponse);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }

                }
            }

            return View( reservation);
        }


        public ViewResult AddReservation() => View();


        [HttpPost]
     public async Task<IActionResult> AddReservation(Reservation reservation)
        {
            Reservation newBooking = new Reservation();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");
                using(var response = await httpClient.PostAsync("http://localhost:5018/api/Reservation/",content))
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        newBooking = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                    }

                    return View(newBooking);
                }
            }


        }

        public async Task<IActionResult> UpdateReservation(int id)
        {
            Reservation reservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                using (var response =
                    await httpClient.GetAsync("http://localhost:5018/api/Reservation/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)

                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                        //Console.WriteLine(reservation.Id+" "+reservation.EndLocation);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }

            }
            return View(reservation);
        }



        [HttpPost]
        public async Task<IActionResult> UpdateReservation(Reservation reservation)
        {
            Reservation UpdateBooking = new Reservation();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(reservation.Id.ToString()), "id");
                content.Add(new StringContent(reservation.Name), "name");
                content.Add(new StringContent(reservation.StartLocation), "startLocation");
                content.Add(new StringContent(reservation.EndLocation), "endLocation");
                using (var response = await httpClient.GetAsync("http://localhost:5018/api/Reservation/" + content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    UpdateBooking = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                    Console.WriteLine(UpdateBooking.Id);
                    ViewBag.Result = "success";
                }


            }
            return View(UpdateBooking);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
