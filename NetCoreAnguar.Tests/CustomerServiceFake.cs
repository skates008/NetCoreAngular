using NetCoreAngular.Common;
using NetCoreAngular.Domain;
using NetCoreAngular.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAnguar.Tests
{
    public class CustomerServiceFake : ICustomerService
    {
        private readonly List<Customer> _customer;

        public CustomerServiceFake()
        {
            _customer = new List<Customer>()
            {
                new Customer() { Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "Orange Juice", Address="Orange Tree", Email = "admin@admin.com" },
                new Customer() { Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    Name = "Diary Milk", Address="Mad Cow",  Email = "admin1@admin.com" },
                new Customer() { Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                    Name = "Frozen Pizza", Address="Uncle Mickey's", Email = "admin1@admin.com" }
            };
        }



        List<CustomerViewModel> ICustomerService.GetAll()
        {
            return _customer.Select(c => new CustomerViewModel()
            {
                Id = c.Id,
                Address = c.Address,
                Email = c.Email

            }).ToList();
        }


        Task ICustomerService.DeleteAsync(Guid id)
        {
            var existing = _customer.First(a => a.Id == id);
            _customer.Remove(existing);

            return Task.FromResult(0);
        }

        Task<CustomerViewModel> ICustomerService.GetAsync(Guid id)
        {
            var customer = _customer.Where(a => a.Id == id)
               .FirstOrDefault();
            var result =  new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Email = customer.Email,
                Website = customer.Website


            };
            return Task.FromResult(result);
        }

        Task<CustomerViewModel> ICustomerService.CreateAsync(CustomerViewModel model)
        {

            model.Id = Guid.NewGuid();
            var customer = new Customer()
            {

                Id = model.Id,
                Address = model.Address,
                Email = model.Email,
            };
            _customer.Add(customer);
            return Task.FromResult(model);
        }

        Task<CustomerViewModel> ICustomerService.UpdateAsync(CustomerViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
