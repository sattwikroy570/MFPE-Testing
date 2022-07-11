using CustomerMicroservice.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMicroservice.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public static List<CustomerDetails> customers = new List<CustomerDetails>
        {
            new CustomerDetails{CustomerId = "JhonSmith",Name="Jhonathon Smith",Address="Dumdum",DateOfBirth="05-09-1997",PanNumber="CGLBP002"}
        };
        Uri baseAddress = new Uri("http://52.159.81.113/api/Account");
        HttpClient client;
        public CustomerRepository()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public CustomerAccountDetails createCustomer(CustomerDetails customer)
        {
            customers.Add(customer);
            string data = JsonConvert.SerializeObject(new{ customerId = customer.CustomerId});
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            string token = TokenInfo.StringToken;
            client.DefaultRequestHeaders.Add("Authorization", token);
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/createAccount/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                CustomerAccountDetails details = JsonConvert.DeserializeObject<CustomerAccountDetails>(data1);
                return details;
            }
            return null;
        }

        public CustomerDetails getCustomerDetails(string CustomerId)
        {
            return customers.Where(c => c.CustomerId == CustomerId).FirstOrDefault();          
        }
    }
}
