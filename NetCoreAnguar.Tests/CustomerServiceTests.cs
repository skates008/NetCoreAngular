using System;
using System.Collections.Generic;
using Xunit;
using NetCoreAngular.Service.Interface;
using NetCoreAngular.Controllers;
using Microsoft.AspNetCore.Mvc;
using NetCoreAngular.Common;
using NetCoreAngular.Domain;
using System.Linq;

namespace NetCoreAnguar.Tests
{
    public class CustomerServiceTests:TestBase
    {
        CustomerController _controller;
        ICustomerService _service;

        public CustomerServiceTests()
        {
            _service = new CustomerServiceFake();
            _controller = new CustomerController(_service);
        }

       

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<CustomerViewModel>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResult = _controller.Get(testGuid);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResult = _controller.Get(testGuid).Result as OkObjectResult;

            // Assert
            Assert.IsType<CustomerViewModel>(okResult.Value);
            Assert.Equal(testGuid, (okResult.Value as CustomerViewModel).Id);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new CustomerViewModel()
            {
                Address = "Guinness",
                Email = "admin5@admin.com"
            };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _controller.Create(nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            CustomerViewModel testItem = new CustomerViewModel()
            {
                Name = "Guinness Original 6 Pack",
                Address = "Guinness",
                Email = "admin5@admin.com"
            };

            // Act
            var createdResponse = _controller.Create(testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new CustomerViewModel()
            {
                Name = "Guinness Original 6 Pack",
                Address = "Guinness",
                Email = "admin4@admin.com"
            };

            // Act
            var createdResponse = _controller.Create(testItem).Result as OkObjectResult;
            var item = createdResponse.Value as CustomerViewModel;

            // Assert
            Assert.IsType<Customer>(item);
            Assert.Equal("Guinness Original 6 Pack", item.Name);
        }

        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();

            // Act
            var badResponse = _controller.Delete(notExistingGuid);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResponse = _controller.Delete(existingGuid);

            // Assert
            Assert.IsType<OkResult>(okResponse);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResponse = _controller.Delete(existingGuid);

            // Assert
            Assert.Equal(2, _service.GetAll().Count());
        }

    }
}
