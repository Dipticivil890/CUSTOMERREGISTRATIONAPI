using CustomerRegistration.Models;
using Microsoft.AspNetCore.DataProtection;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomerRegistration.Controllers
{
    public class HomeController : Controller
    {
        CommonHelper ch = new CommonHelper();
        Uri baseurl = new Uri("http://localhost:31916/api");

        HttpClient client;
        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = baseurl;
        }

        #region------------------User Registation Page------------------------------

        public IActionResult NewUserRegistation(string encCustId)
        {
            return View("NewUserRegistation");
        }
        #endregion------------------------------------------------------------------


        #region------------------------Bind State-----------------------------------
        public JsonResult FillStateAgain()
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/State").Result;
            List<SelectListItem> statelist = new List<SelectListItem>();
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var statelst= JsonConvert.DeserializeObject<List<State>>(data);
                foreach (State st in statelst)
                {

                    statelist.Add(new SelectListItem
                    {
                        Text = st.StateName,
                        Value =ch.Encrypt(st.StateId.ToString())
                    });

                }
               var _statelist = JsonConvert.SerializeObject(statelist);
                return Json(_statelist);
            }
            return Json("");
        }

        
        #endregion-----------------------------------------------------------


        #region-----------------------Bind District---------------------------------
        public JsonResult FillDistrictAgain(string id)
        {
            var stateid = ch.Decrypt(id);
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/District/" + stateid).Result;
            List<SelectListItem> Districtlist = new List<SelectListItem>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var Distlst = JsonConvert.DeserializeObject<List<District>>(data);
                foreach (District dt in Distlst)
                {

                    Districtlist.Add(new SelectListItem
                    {
                        Text = dt.DistrictName,
                        Value = ch.Encrypt(dt.DistrictId.ToString())
                    });

                }
                var _distlist = JsonConvert.SerializeObject(Districtlist);
                return Json(_distlist);
            }
            return Json("");
        }
        #endregion------------------------------------------------------------------


        #region---------------------Save & Update Customer--------------------------
        [HttpPost]
        public async Task<JsonResult> CreateCustomer(Customer cust)
        {
            try
            {
                if (cust.EncryptedCustomerId == null)
                {
                    cust.StateId = Convert.ToInt32(ch.Decrypt(cust.encStateId));
                    cust.DistrictId = Convert.ToInt32(ch.Decrypt(cust.encDistrictId));
                    string data = JsonConvert.SerializeObject(cust);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(client.BaseAddress + "/Customer/", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(1);
                    }
                }
                else
                {
                    
                    cust.CustomerId = Convert.ToInt32(ch.Decrypt(cust.EncryptedCustomerId));
                    cust.StateId = Convert.ToInt32(ch.Decrypt(cust.encStateId));
                    cust.DistrictId = Convert.ToInt32(ch.Decrypt(cust.encDistrictId));
                    string data = JsonConvert.SerializeObject(cust);
                    HttpResponseMessage response;
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    response = await client.PutAsync(client.BaseAddress + "/Customer/" + cust.CustomerId, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(2);
                    }
                }

            }
            catch(Exception ex)
            {

            }
            return Json("");
        }

        #endregion------------------------------------------------------------------


        #region-----------------------View Customers Page---------------------------

        public IActionResult ViewUserRegistation()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        #endregion-------------------------------------------------------------------


        #region----------------------BindCustomers----------------------------------
        public async Task<JsonResult> GetAllCustomers()
        {
            List<Customer> CustomerList = new List<Customer>();
            string data = null;
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "/Customer");
            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                var custlist = JsonConvert.DeserializeObject<List<Customer>>(data);
                foreach (Customer cust in custlist)
                {
                    CustomerList.Add(new Customer
                    {
                        EncryptedCustomerId = ch.Encrypt(cust.CustomerId.ToString()),
                        CustomerCode=cust.CustomerCode,
                        CustomerName=cust.CustomerName,
                        CustomerPhone=cust.CustomerPhone,
                        CustomerEmail=cust.CustomerEmail,
                        StateName=cust.StateName,
                        DistrictName=cust.DistrictName,
                        City=cust.City,
                        Pincode=cust.Pincode,
                        CustomerGender=cust.CustomerGender,
                        CustomerAddress=cust.CustomerAddress
                    });;
                }
            }
            var _CustList = JsonConvert.SerializeObject(CustomerList);
            return Json(_CustList);

        }

        #endregion------------------------------------------------------------------


        #region-------------------Bind Customer Data--------------------------------
        public async Task<JsonResult> GetCustomerbyID(string encCustId)
        {
           
            var x = encCustId + "=";
            var id = ch.Decrypt(x);
            Customer custlist = new Customer();
            //Customer customerlist = new Customer();
            HttpResponseMessage response =await client.GetAsync(client.BaseAddress + "/Customer/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                custlist = JsonConvert.DeserializeObject<Customer>(data);
                custlist.EncryptedCustomerId = ch.Encrypt(custlist.CustomerId.ToString());
                custlist.encStateId = ch.Encrypt(custlist.StateId.ToString());
                custlist.encDistrictId = ch.Encrypt(custlist.DistrictId.ToString());

            }
            var jsonres = JsonConvert.SerializeObject(custlist);
            return Json(jsonres);
        }
        #endregion------------------------------------------------------------------


        #region----------------Delete Customer Details------------------------------
        public int Delete(string encCustId)
        {
            var custid = ch.Decrypt(encCustId);
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Customer/" + custid).Result;
            if (response.IsSuccessStatusCode)
            {
                return 1;
            }
            return 0;
        }
        #endregion------------------------------------------------------------------

        
    }
}
