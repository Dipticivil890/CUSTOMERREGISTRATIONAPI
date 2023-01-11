using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistration.Models
{
    public class Customer
    {

        /// <summary>
        /// Customer Id
        /// </summary>
        public int CustomerId { get; set; }


        /// <summary>
        /// For EncryptedCustomerID
        /// </summary>
        [NotMapped]
        public string EncryptedCustomerId { get; set; }
        /// <summary>
        /// Customer 15 digits Code
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Customer Full Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer MobileNumber
        /// </summary>
        public string CustomerPhone { get; set; } = null;
        /// <summary>
        /// Customer Email
        /// </summary>
        public string CustomerEmail { get; set; } = null;

        [NotMapped]
        public string encStateId { get; set; } = null;

        /// <summary>
        /// Customer State Id
        /// </summary>
        public int? StateId { get; set; }

        /// <summary>
        /// Customer District Id
        /// </summary>
      
           public int? DistrictId { get; set; }
        /// <summary>
        /// Customer City
        /// </summary>
        [NotMapped]
        public string encDistrictId { get; set; } = null;
        public string City { get; set; } = null;
        /// <summary>
        /// Customer Address Pincode
        /// </summary>
        public int? Pincode { get; set; }
        /// <summary>
        /// Customer Gender
        /// </summary>
        public string CustomerGender { get; set; } = null;
        /// <summary>
        /// Customer Address
        /// </summary>
        public string CustomerAddress { get; set; } = null;
        public string StateName { get; set; } = null;
        public string DistrictName { get; set; } = null;
    }


    public class District
    {
        /// <summary>
        /// District Id
        /// </summary>
        public int? DistrictId { get; set; }
        /// <summary>
        /// Selected State Id
        /// </summary>
        [NotMapped]
        public string encDistrictId { get; set; } = null;
        public int? StateId { get; set; }
        /// <summary>
        /// District Name 
        /// </summary>
        public string DistrictName { get; set; } = null;
    }
    public class Errorlogs
    {
        public string ErrorMessage { get; set; }
        public DateTime ErrorDate { get; set; }
        public string ProcedureName { get; set; }
        public string LineNumber { get; set; }
        public string ErrorProcedure { get; set; }
    }

    //--------------------Encrypt & Decrypt-----------------------------
    //public class CustomDataProtection
    //{
    //    public readonly string customerIdValue = "customerIdValue";

    //}

}
