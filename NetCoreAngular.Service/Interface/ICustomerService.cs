using NetCoreAngular.Common;
using NetCoreAngular.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreAngular.Service.Interface
{
    public interface ICustomerService
    {
        Task<CustomerViewModel> GetAsync(Guid id);
        List<CustomerViewModel> GetAll();
        Task<CustomerViewModel> CreateAsync(CustomerViewModel model);
        Task<CustomerViewModel> UpdateAsync(CustomerViewModel model);
        Task  DeleteAsync(Guid id);
    }
}
