using CUSTOMERREGISTRATIONAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUSTOMERREGISTRATIONAPI.Service
{
   public interface ICustomerService
    {
        public Task<int> InsertCustomerAsync(Customer cust);
        public Task<int> UpdateCustomerAsync(Customer cust);
        public Task<int> DeleteCustopmerAsync(int id);
        public Task<List<Customer>> GetallCustomerAsync();
        public Task<Customer> GetCustomerbyIDAsync(int id);
        public Task<List<State>> GettallStateAsync();
        public Task<List<District>> GetDistbyIdAsync(int stateid);
        public Task<Errorlogs> InsertErrorLogsAsync(string ErrorMessage, DateTime? ErrorDate, string ErrorOn, string ErrorLineNo, string ErrorStackTrace);

    }
}
