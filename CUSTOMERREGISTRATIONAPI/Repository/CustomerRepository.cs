using CUSTOMERREGISTRATIONAPI.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CUSTOMERREGISTRATIONAPI.Repository
{
    public class CustomerRepository:BaseRepository,ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<int> Insert(Customer cust)
        {

                var cn = CreateConnection();
                if (cn.State == ConnectionState.Closed) cn.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("VCH_CUSTOMER_CODE", cust.CustomerCode);
                param.Add("VCH_CUSTOMER_NAME", cust.CustomerName);
                param.Add("VCH_CUSTOMER_MOBILE", cust.CustomerPhone);
                param.Add("VCH_CUSTOMER_EMAIL", cust.CustomerEmail);
                param.Add("INT_STATE_ID", cust.StateId);
                param.Add("INT_DISTRICT_ID", cust.DistrictId);
                param.Add("VCH_CITY", cust.City);
                param.Add("INT_PINCODE", cust.Pincode);
                param.Add("VCH_CUSTOMER_GENDER", cust.CustomerGender);
                param.Add("VCH_CUSTOMER_ADDRESS", cust.CustomerAddress);
                param.Add("ACTION", 'I');
                int x =await cn.ExecuteAsync("USP_M_CR_CUSTOMER", param, commandType: CommandType.StoredProcedure);
                cn.Close();
                return   x;

        }
        public async Task<int> Update(Customer cust)
        {

               var cn = CreateConnection();
                if (cn.State == ConnectionState.Closed) cn.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("INT_CUSTOMER_ID", cust.CustomerId);
                param.Add("VCH_CUSTOMER_CODE", cust.CustomerCode);
                param.Add("VCH_CUSTOMER_NAME", cust.CustomerName);
                param.Add("VCH_CUSTOMER_MOBILE", cust.CustomerPhone);
                param.Add("VCH_CUSTOMER_EMAIL", cust.CustomerEmail);
                param.Add("INT_STATE_ID", cust.StateId);
                param.Add("INT_DISTRICT_ID", cust.DistrictId);
                param.Add("VCH_CITY", cust.City);
                param.Add("INT_PINCODE", cust.Pincode);
                param.Add("VCH_CUSTOMER_GENDER", cust.CustomerGender);
                param.Add("VCH_CUSTOMER_ADDRESS", cust.CustomerAddress);
                param.Add("ACTION", 'U');
                int x =await cn.ExecuteAsync("USP_M_CR_CUSTOMER", param, commandType: CommandType.StoredProcedure);
                cn.Close();
                return x;

        }

        public async Task<int> Delete(int id)
        {

                var cn = CreateConnection();
                if (cn.State == ConnectionState.Closed) cn.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("INT_CUSTOMER_ID", id);
                param.Add("ACTION", 'D');
                int x =await cn.ExecuteAsync("USP_M_CR_CUSTOMER", param, commandType: CommandType.StoredProcedure);
                cn.Close();
                return x;

        }

        public async Task<List<Customer>> GetallCustomer()
        {

                 var cn = CreateConnection();
                if (cn.State == ConnectionState.Closed) cn.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("ACTION", 'V');
                var cust =await cn.QueryAsync<Customer>("USP_M_CR_CUSTOMER_VIEW", param, commandType: CommandType.StoredProcedure);
                cn.Close();
                return cust.ToList();

        }

        public async Task<Customer> GetCustomerbyID(int id)
        {

                 var cn = CreateConnection();
                if (cn.State == ConnectionState.Closed) cn.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("INT_CUSTOMER_ID", id);
                param.Add("ACTION", 'S');
                var cust =await cn.QueryAsync<Customer>("USP_M_CR_CUSTOMER_VIEW", param, commandType: CommandType.StoredProcedure);
                cn.Close();
                return cust.FirstOrDefault();

        }

        public async Task<List<District>> GetDistbyId(int stateid)
        {

                var cn = CreateConnection();
                if (cn.State == ConnectionState.Closed) cn.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("INT_STATE_ID", stateid);
                param.Add("ACTION", 'V');
                var dist =await cn.QueryAsync<District>("USP_M_CR_DISTRICT_VIEW", param, commandType: CommandType.StoredProcedure);
                cn.Close();
                return dist.ToList();

        }

        public async Task<List<State>> GettallState()
        {

                var cn = CreateConnection();
                if (cn.State == ConnectionState.Closed) cn.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("ACTION", 'V');
                var state =await cn.QueryAsync<State>("USP_M_CR_STATE_VIEW", param, commandType: CommandType.StoredProcedure);
                cn.Close();
                return state.ToList();
        }
        public async Task<Errorlogs> InsertErrorLogs(string ErrorMessage, DateTime? ErrorDate, string ErrorOn, string ErrorLineNo, string ErrorStackTrace)
        {

                var cn = CreateConnection();
                if (cn.State == ConnectionState.Closed) cn.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("VCH_ERROR_MESSAGE", ErrorMessage);
                param.Add("DTM_ERROR_DATE", ErrorDate);
                param.Add("VCH_PROCEDURE_NAME", ErrorOn);
                param.Add("VCH_LINE_NUMBER", ErrorLineNo);
                param.Add("VCH_ERROR_PROCEDURE", ErrorStackTrace);
                param.Add("ACTION", 'I');
                var result =await cn.QueryAsync<Errorlogs>("USP_T_CR_ERROR", param, commandType: CommandType.StoredProcedure);
                cn.Close();
                return result.FirstOrDefault();
        }

    }
}
