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
        private readonly CustomIDataProtection protector;
        public HomeController(CustomIDataProtection customIDataProtection)
        {
            client = new HttpClient();
            client.BaseAddress = baseurl;
            protector = customIDataProtection;
        }
        public List<Customer> FillState()
        {
            List<Customer> lstcat = new List<Customer>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/State").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                lstcat = JsonConvert.DeserializeObject<List<Customer>>(data);
                lstcat.Insert(0, new Customer { StateId = 0, StateName = "-- Select --" });
                ViewBag.State = lstcat;
            }
            return ViewBag.State;
        }
        public List<District> FillDistrict(int? id)
        {
            List<District> lstdist = new List<District>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/District/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                lstdist = JsonConvert.DeserializeObject<List<District>>(data);
                lstdist.Insert(0, new District { DistrictId = 0, DistrictName = "Select One" });
                ViewBag.dist = lstdist;
            }
            return ViewBag.dist;
        }

        public async Task <IActionResult> Registration(int Id)
        {
            try
            {
                Customer _custDtls = new Customer();
                if (Id != 0) // Edit
                {
                    // Edit  Data Bind Code
                    FillState();
                    HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "/Customer/" + Id);
                    
                    if (response.IsSuccessStatusCode)
                    {   
                        string data = response.Content.ReadAsStringAsync().Result;
                        _custDtls = JsonConvert.DeserializeObject<Customer>(data);
                        if(_custDtls.StateId != null)
                        {
                            FillDistrict(_custDtls.StateId);
                        }
                    }
                    return View(_custDtls);
                }
                else
                {
                    FillState();
                    return View(_custDtls);
                }
            }
            catch(Exception ex)
            {
                

            }
            return View();
        }
        public async Task<IActionResult> Show()
        {
           return View();
        }
        public async Task<JsonResult> District_Bind(int stateid)
        {
            HttpResponseMessage response =await client.GetAsync(client.BaseAddress + "/District/" + stateid);
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
            var jsonres =JsonConvert.SerializeObject(distlist);
             return Json(jsonres);
        }

        public async Task<JsonResult> GetCustomers()
        {
            string data = null;
            HttpResponseMessage response =await client.GetAsync(client.BaseAddress + "/Customer");
            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
            }
            return Json(data);

        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer cust)
        {
            if (cust.CustomerId == 0)
            {
                string data = JsonConvert.SerializeObject(cust);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response =await client.PostAsync(client.BaseAddress + "/Customer/", content);
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
                response =await client.PutAsync(client.BaseAddress + "/Customer/" + cust.CustomerId, content);
                if (response.IsSuccessStatusCode)
                {
                    return View("Show");
                }
            }

            return View();
        }
        //public async Task<IActionResult> Edit(int id)
        //{
        //    Customer custlist = new Customer();
        //    HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Customer/" + id).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = response.Content.ReadAsStringAsync().Result;
        //        custlist = JsonConvert.DeserializeObject<Customer>(data);
        //    }
        //    var jsonres = JsonConvert.SerializeObject(custlist);
        //   // return Json (jsonres);
        //    return View("Registration", jsonres);

        //}
        public int Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Customer/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return 1;
            }
            return 0;
        }
       
    }
}
