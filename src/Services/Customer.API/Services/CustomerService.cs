﻿using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;

namespace Customer.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IResult> GetCustomerByUsernameAsync(string username)
        {
            return Results.Ok(await _customerRepository.GetCustomerByUsernameAsync(username));
        }

        public async Task<IResult> GetCustomersAsync()
        {
            return Results.Ok(await _customerRepository.GetCustomersAsync());
        }
    }
}
