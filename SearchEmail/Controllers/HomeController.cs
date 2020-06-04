using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SearchEmail.Models;

namespace SearchEmail.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [System.Web.Http.HttpPost]
        public async Task<IActionResult> Search(SearchCriteria criteria)
        {
            List<EmailItem> emailItems = null;

            //Check if all validations passed
            if (ModelState.IsValid == false)
                return View("Index", criteria);

            try
            {
                var baseApiAddress = _configuration.GetValue<string>("API:BaseAddress");                

                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiAddress);
                    client.DefaultRequestHeaders.Add("Accept", "Application/JSON");

                    //Serializing object to JSON
                    var json = JsonConvert.SerializeObject(criteria);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var responseTask = await client.PostAsync("SearchEmail", data);

                    responseTask.EnsureSuccessStatusCode();
                    string result = responseTask.Content.ReadAsStringAsync().Result;

                    emailItems = JsonConvert.DeserializeObject<List<EmailItem>>(result);
                }

                                
            }
            catch(Exception ex)
            {
                //Log exception and redirect to error page
            }
            finally
            {
                if (emailItems != null && emailItems.Count == 0)
                    emailItems = new List<EmailItem>();
            }

            return View(emailItems);
        }
    }
}
