using NetCoreAngular.Service.Interface;
using NetCoreAngular.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using NetCoreAngular.Common;
using System.Collections.Generic;
using System;
using NetCoreAngular.Domain;
using System.Linq;

namespace NetCoreAnguar.Tests
{
    [TestFixture]
    public class CustomerServiceTests:TestBase
    {
        CustomerController _controller;
        ICustomerService _service;

        public CustomerServiceTests()
        {
            _service = new CustomerServiceFake();
            _controller = new CustomerController(_service);
        }

       

        [Test]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        //[Test]
        //public void Get_WhenCalled_ReturnsAllItems()
        //{
        //    // Act
        //    var okResult = _controller.Get().Result as OkObjectResult;

        //    // Assert
        //    var items = Assert.IsInstanceOf<List<CustomerViewModel>>(okResult.Value);
        //    Assert.Equals(3, items.Count);
        //}

        [Test]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(notFoundResult.Result);
        }

        [Test]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResult = _controller.Get(testGuid);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        [Test]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResult = _controller.Get(testGuid).Result as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<CustomerViewModel>(okResult.Value);
            Assert.Equals(testGuid, (okResult.Value as CustomerViewModel).Id);
        }

        [Test]
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
            Assert.IsInstanceOf<BadRequestObjectResult>(badResponse);
        }


        [Test]
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
            Assert.IsInstanceOf<CreatedAtActionResult>(createdResponse);
        }


        [Test]
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
            Assert.IsInstanceOf<Customer>(item);
            Assert.Equals("Guinness Original 6 Pack", item.Name);
        }

        [Test]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();

            // Act
            var badResponse = _controller.Delete(notExistingGuid);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(badResponse);
        }

        [Test]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResponse = _controller.Delete(existingGuid);

            // Assert
            Assert.IsInstanceOf<OkResult>(okResponse);
        }

        [Test]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResponse = _controller.Delete(existingGuid);

            // Assert
            Assert.Equals(2, _service.GetAll().Count());
        }

    }
}
