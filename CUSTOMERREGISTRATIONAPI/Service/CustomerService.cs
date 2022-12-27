using CUSTOMERREGISTRATIONAPI.Model;
using CUSTOMERREGISTRATIONAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUSTOMERREGISTRATIONAPI.Service
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _IRepo;

        public CustomerService(ICustomerRepository IRepo)
        {
            _IRepo = IRepo;
        }
        public async Task<int> InsertCustomerAsync(Customer cust)
        {
            return await _IRepo.Insert(cust);
        }

        public async Task<int> UpdateCustomerAsync(Customer cust)
        {
            return await _IRepo.Update(cust);
        }
        public async Task<int> DeleteCustopmerAsync(int id)
        {
            return await _IRepo.Delete(id);
        }

        public async Task<List<Customer>> GetallCustomerAsync()
        {
            return await _IRepo.GetallCustomer();
        }

        public async Task<Customer> GetCustomerbyIDAsync(int id)
        {
            return await _IRepo.GetCustomerbyID(id);
        }

        public async Task<List<District>> GetDistbyIdAsync(int stateid)
        {
            return await _IRepo.GetDistbyId(stateid);
        }

        public async Task<List<State>> GettallStateAsync()
        {
            return await _IRepo.GettallState();
        }
        public async Task<Errorlogs> InsertErrorLogsAsync(string ErrorMessage, DateTime? ErrorDate, string ErrorOn, string ErrorLineNo, string ErrorStackTrace)
        {
            return await _IRepo.InsertErrorLogs(ErrorMessage,ErrorDate,ErrorOn,ErrorLineNo,ErrorStackTrace);
        }

    }
}
