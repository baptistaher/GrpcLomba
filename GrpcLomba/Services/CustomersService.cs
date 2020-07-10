using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcLomba.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {

            _logger = logger;
        }

        public override  Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if(request.UserId == 1)
            {
                output.FirstName = "Stenio";
                output.LastName = "Lizardo";
            }else if(request.UserId == 2)
            {
                output.FirstName = "lomba";
                output.LastName = "penuria";
            }else
            {
                output.FirstName = "West";
                output.LastName = "Side";
            }

            return Task.FromResult(output);

        }



        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Franciu",
                    LastName = "de Alibaba",
                    EmailAdress = "deAlibaba@franciu.cv",
                    Age = 27,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Closeted",
                    LastName = "Obesophie",
                    EmailAdress = "obesophie@closeted.gay",
                    Age = 27,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Slenderman",
                    LastName = "anão",
                    EmailAdress = "anao@slenderman.pouser",
                    Age = 32,
                    IsAlive = true
                },


            };

            foreach (var cust in customers) 
            {
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
