using CustomerRegistration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Controllers
{
    public class HomeController : Controller
    {
        Uri baseurl = new Uri("http://localhost:31916/api");

        HttpClient client;
        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = baseurl;
        }
        public IActionResult Index()
        {
            return View();
        }
        public List<Customer> FillState()
        {
            List<Customer> lstcat = new List<Customer>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/State").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                lstcat = JsonConvert.DeserializeObject<List<Customer>>(data);
                lstcat.Insert(0, new Customer { StateId = 0, StateName = "Select One" });
                ViewBag.State = lstcat;
            }
            return ViewBag.State;
        }
        //[HttpPost]
        public IActionResult Registration()
        {
            FillState();
            //Customer custlist = new Customer();
            //HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Customer/" + id).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    string data = response.Content.ReadAsStringAsync().Result;
            //    custlist = JsonConvert.DeserializeObject<Customer>(data);
            //}
            //var jsonres = JsonConvert.SerializeObject(custlist);
            //return Json(jsonres);
            return View();

        }
        public IActionResult Show()
        {
            return View();
        }
        public async Task<JsonResult> District_Bind(int stateid)
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/District/" + stateid).Result;
            List<SelectListItem> distlist = new List<SelectListItem>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var listcat = JsonConvert.DeserializeObject<List<District>>(data);
                foreach(District dr in listcat)
                {
                    distlist.Add(new SelectListItem
                    {
                        Text = dr.DistrictName,
                        Value = dr.DistrictId.ToString()
                    });
                }
            }
            var jsonres = JsonConvert.SerializeObject(distlist);
             return Json(jsonres);
        }

        public async Task<JsonResult> GetCustomers()
        {
            string data = null;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Customer").Result;
            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
            }
            return Json(data);

        }

        [HttpPost]
        public async Task<IActionResult>Create( int id, Customer cust)
        {
            if (cust.CustomerId == 0)
            {
                string data = JsonConvert.SerializeObject(cust);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response =client.PostAsync(client.BaseAddress + "/Customer/", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                string data = JsonConvert.SerializeObject(cust);
                HttpResponseMessage response;
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                response = client.PutAsync(client.BaseAddress + "/Customer/" + cust.CustomerId, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            
            Customer custlist = new Customer();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Customer/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                custlist = JsonConvert.DeserializeObject<Customer>(data);
            }
            var jsonres = JsonConvert.SerializeObject(custlist);
           // return Json (jsonres);
            return View("Registration", jsonres);

        }
        public int Delete(int id)
        {
            //Customers custlist = new Customers();
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Customer/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return 1;
            }
            return 0;
            
        }
       
    }
}
