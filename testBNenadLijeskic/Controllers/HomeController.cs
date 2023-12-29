using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using testBNenadLijeskic.Models;

namespace testBNenadLijeskic.Controllers
{
    public class HomeController : Controller
    {

        public async Task<ActionResult> Index()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpClient client = new HttpClient();
            string key = "vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";
            string api = $"https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code={key}";
            HttpResponseMessage response = await client.GetAsync(api);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var employeeData = JsonConvert.DeserializeObject<List<EmployeeData>>(json);
                var groupedResults = employeeData.GroupBy(
                 p => p.EmployeeName,
                 p => p.TotalWorkedHours,
                  (name, total) => new KeyValuePair<string,double>(name, total.Sum()));
                ViewBag.Employees = groupedResults;
                
            }
            return View();
        }

        
    }
}
