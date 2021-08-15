using Microsoft.AspNetCore.Mvc;
using NetCoreAngular.Common;
using NetCoreAngular.Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NetCoreAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerViewModel model)
        {

            await _customerService.CreateAsync(model);

            return new OkObjectResult(new Response
            {
                Message = "customer Added Successfully!",
                StatusCode = HttpStatusCode.OK
            });

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromBody] CustomerViewModel model, Guid id)
        {

            model.Id = id;
            await _customerService.UpdateAsync(model);

            return new OkObjectResult(new Response
            {
                Message = "customer Updated Successfully!",
                StatusCode = HttpStatusCode.OK
            });


        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var companies = _customerService.GetAll();
            //return Ok(companies);

            var toReturn = companies.Select(x => ExpandSingleFoodItem(x));

            return Ok( toReturn);

        }

        private dynamic ExpandSingleFoodItem(CustomerViewModel foodItem)
        {



            var resourceToReturn = foodItem.ToDynamic() as IDictionary<string, object>;

            return resourceToReturn;
        }




        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var customer = await _customerService.GetAsync(id);


            return Ok(customer);

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _customerService.DeleteAsync(id);

            return new OkObjectResult(new Response<CustomerViewModel>
            {
                Message = "customer Deleted Successfully!",
                StatusCode = HttpStatusCode.OK
            });

        }
    }
}


public static class DynamicExtensions
{
    public static dynamic ToDynamic(this object value)
    {
        IDictionary<string, object> expando = new ExpandoObject();

        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
            expando.Add(property.Name, property.GetValue(value));

        return expando as ExpandoObject;
    }
}