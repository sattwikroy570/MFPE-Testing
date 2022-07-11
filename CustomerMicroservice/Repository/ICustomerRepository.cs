using CustomerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Repository
{
    public interface ICustomerRepository
    {
        public CustomerAccountDetails createCustomer(CustomerDetails customer);
        public CustomerDetails getCustomerDetails(string customerId);
    }
}
