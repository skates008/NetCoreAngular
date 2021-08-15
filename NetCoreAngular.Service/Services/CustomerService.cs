
using NetCoreAngular.Common;
using NetCoreAngular.Domain;
using NetCoreAngular.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAngular.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericUnitOfWork _genericUnitOfWork;
        public CustomerService(IGenericUnitOfWork genericUnitOfWork)
        {
            _genericUnitOfWork = genericUnitOfWork;
        }

        public async Task<CustomerViewModel> CreateAsync(CustomerViewModel model)
        {


            var repo = _genericUnitOfWork.GetRepository<Customer, string>();

            var emailExists = await repo.Exists(c => c.Email == model.Email.Trim());
            if (emailExists)
                throw new ArgumentException("customer with the email already exists in the system");


            var customer = new Customer();
            customer.Id = Guid.NewGuid();
            customer.Name = model.Name;
            customer.Email = model.Email;
            customer.Address = model.Address;
            customer.Website = model.Website;
            customer.CreatedDate = DateTime.UtcNow;

            repo.Add(customer);



            await _genericUnitOfWork.SaveChangesAsync();
            return getCustomer(customer);

        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _genericUnitOfWork.GetRepository<Customer, Guid>().FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
                throw new ArgumentException("customer does not exist");
            customer.IsDeleted = true;
            customer.IsActive = false;

            await _genericUnitOfWork.SaveChangesAsync();

        }

        public List<CustomerViewModel> GetAll()
        {
            var companies = _genericUnitOfWork.GetRepository<Customer, string>().GetAll(x => x.IsActive && !x.IsDeleted);

            var result = companies.Select(c => new CustomerViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Email = c.Email,
                Website = c.Website


            }).ToList();
            return result;


        }

        public async Task<CustomerViewModel> GetAsync(Guid id)
        {
            var customer = await _genericUnitOfWork.GetRepository<Customer, Guid>().FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
                throw new ArgumentException("Customer does not exist");

            return getCustomer(customer);

        }

        private static CustomerViewModel getCustomer(Customer customer)
        {
            return new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Email = customer.Email,
                Website = customer.Website


            };
        }

        public async Task<CustomerViewModel> UpdateAsync(CustomerViewModel model)
        {
             
            var repo = _genericUnitOfWork.GetRepository<Customer, Guid>();
            var customer = await repo.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (customer == null)
                throw new ArgumentException("Customer does not exist");
            
            customer.Name = model.Name;
            customer.Email = model.Email;
            customer.Address = model.Address;
            customer.Website = model.Website;
            customer.ModifiedDate = DateTime.UtcNow;


            repo.Update(customer);
            await _genericUnitOfWork.SaveChangesAsync();
            return getCustomer(customer);
        }
    }
}
