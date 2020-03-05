using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Products.Controllers;
using Products.Models;
using System;
using System.Collections.Generic;

namespace UnitTestProducts
{
    [TestClass]
    public class ProductsControllerTests
    {
        ProductsController pc = new ProductsController( new DataProductsContextMock() );

        [TestMethod]
        public void GetById_intExistingId_ReturnAnObjectSameId()
        {
            //Arrange
            int expected = 777;
            int intExistingId = 777;

            //Act
            var response = new ProductsController().GetById(intExistingId);
            Products.Models.ProductDTO productDTO = new Products.Models.ProductDTO();
            try
            {
                productDTO = (Products.Models.ProductDTO)((OkObjectResult)response).Value;
            }
            catch (InvalidCastException e)
            {
                
            }

            //Assert
            Assert.AreEqual(expected, productDTO.IdProduct);
        }

        [TestMethod]
        public void GetNumPages_void_ReturnNumberPages()
        {
            //Arange
            int notExpected = 0;

            //Act
            int actual = 0;
            try
            {
                var response = pc.GetNumPages();
                //var response = new Products.Controllers.ProductsController().GetNumPages();
                actual = (int)((OkObjectResult)response).Value;
            }
            catch (InvalidCastException)
            {

            }

            //Assert
            Assert.AreNotEqual(notExpected, actual);
        }

        [TestMethod]
        public void GetAll_NumPositiveAndCompletePage_ReturnTenProducts()
        {
            //Arange
            int expected = 10;
            int PositiveAndCompleteNumPage = 1;

            //Act
            List<Products.Models.ProductDTO> products = new List<Products.Models.ProductDTO>();
            try
            {
                var response = new Products.Controllers.ProductsController().GetAll(PositiveAndCompleteNumPage);
                products = (List<Products.Models.ProductDTO>)((OkObjectResult)response).Value;
            }
            catch (InvalidCastException)
            {

            }

            //Assert
            Assert.AreEqual(expected, products.Count);
        }

        [TestMethod]
        public void GetByName_ExistingName_ReturnListProducts()
        {
            //Arange
            string ExistingName = "tenis";

            //Act
            OkObjectResult response = null;
            try
            {
                response = (OkObjectResult)(new Products.Controllers.ProductsController().GetByName(ExistingName));
            }
            catch (InvalidCastException)
            {

            }

            //Assert
            Assert.IsNotNull(response.Value);
        }

        [TestMethod]
        public void Add_ValidProduct_ReturnProductWithNewId()
        {
            //Arange
            var ValidProduct = new Products.Models.ProductDTO
            {
                Name = "Valid Test",
                Description = "Only a test",
                Price = 3.2m,
                Image = ""
            };

            //Act
            Products.Models.ProductDTO actual = null;
            try
            {
                var response = new Products.Controllers.ProductsController().Add(ValidProduct);
                actual = (Products.Models.ProductDTO)((OkObjectResult)response).Value;
            }
            catch (InvalidCastException)
            {

            }

            //Assert
            Assert.AreNotEqual(0, actual.IdProduct);
        }

        [TestMethod]
        public void Update_ValidProduct_ReturnProductUpdated()
        {
            //Arange
            int idProduct = 777;
            var ValidProduct = new Products.Models.ProductDTO
            {
                Name = "Update test",
                Description = "description description",
                Price = 3456.2m,
                Image = "new image"
            };

            //Act
            Products.Models.ProductDTO product = new Products.Models.ProductDTO();
            try
            {
                var response = new Products.Controllers.ProductsController().Update(idProduct, ValidProduct);
                product = (Products.Models.ProductDTO)((OkObjectResult)response).Value;
            }
            catch (InvalidCastException)
            {

            }

            //Assert
            ValidProduct.IdProduct = idProduct;
            Assert.AreEqual(ValidProduct, product);
        }

        [TestMethod]
        public void Delete_ValidId_CorrectStatusCode()
        {
            //Arange
            int expected = 200;
            int ValidId = 777;

            //Act
            int actual = 0;
            try
            {
                var response = new Products.Controllers.ProductsController().Delete(ValidId);
                actual = ((OkResult)response).StatusCode;
            }
            catch (InvalidCastException)
            {

            }

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
