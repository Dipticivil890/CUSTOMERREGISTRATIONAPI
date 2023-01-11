using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistration.Models
{
    public class State
    {
        public string encStateId { get; set; }
        /// <summary>
        /// State Id
        /// </summary>
        public int? StateId { get; set; }
        /// <summary>
        /// State Name
        /// </summary>
        public string StateName { get; set; } = null;
    }
}
