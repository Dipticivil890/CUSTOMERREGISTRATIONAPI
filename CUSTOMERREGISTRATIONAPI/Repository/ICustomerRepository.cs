using CUSTOMERREGISTRATIONAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUSTOMERREGISTRATIONAPI.Repository
{
   public interface ICustomerRepository
    {
        public Task<int> Insert(Customer cust);
        public Task<int> Update(Customer cust);
        public Task<int> Delete(int id);
        public Task<List<Customer>> GetallCustomer();
        public Task<Customer> GetCustomerbyID(int id);
        public Task<List<State>> GettallState();
        public Task<List<District>> GetDistbyId(int stateid);

        public Task<Errorlogs> InsertErrorLogs(string ErrorMessage, DateTime? ErrorDate, string ErrorOn, string ErrorLineNo, string ErrorStackTrace);


    }
}
