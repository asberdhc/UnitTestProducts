using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Products.Controllers;
using Products.Models;
using SerializeObjects;
using System;
using System.Collections.Generic;

namespace UnitTestProducts
{
    [TestClass]
    public class ProductsControllerTests
    {
        List<ProductDTO> ProductsFromXML;
        ProductsController pcMock;

        [TestInitialize]
        public void LoadData()
        {
            //obtaining data products dictionary from XML file
            XMLFile.Path = @"C:\Users\Curso\source\repos\UnitTestProducts\";
            ProductsFromXML = XMLFile.DeserializeList<List<ProductDTO>>("XMLProducctsFile.xml");

            //Using Mock data base
            pcMock = new ProductsController(new DataProductsContextMock());
        }

        [TestMethod]
        public void Add_ValidProduct_ReturnProductWithNewId()
        {
            //Arange
            var newProduct = ProductsFromXML[0];

            //Act
            ProductDTO actual = null;
            try
            {
                var response = pcMock.Add(newProduct);
                actual = (ProductDTO)((OkObjectResult)response).Value;
            }
            catch (InvalidCastException)
            {

            }

            //Assert
            newProduct.IdProduct = actual.IdProduct;
            Assert.AreNotEqual(0, actual.IdProduct);
        }

        [TestMethod]
        public void Update_ValidProduct_ReturnProductUpdated()
        {
            //Arange
            var newProduct = ProductsFromXML[0];
            var updatedProduct = ProductsFromXML[1];

            //Act
            ProductDTO product = new ProductDTO();
            try
            {
                var response = pcMock.Update(newProduct.IdProduct, updatedProduct);
                product = (ProductDTO)((OkObjectResult)response).Value;
                updatedProduct.IdProduct = product.IdProduct;
            }
            catch (InvalidCastException)
            {

            }

            //Assert
            Assert.AreEqual(updatedProduct, product);
        }

        [TestMethod]
        public void GetById_intExistingId_ReturnAnObjectBasedOnId()
        {
            //Arrange
            var newProduct = ProductsFromXML[0];

            //Act
            ProductDTO productDTO = new ProductDTO();
            try
            {
                var response = pcMock.GetById(newProduct.IdProduct);
                productDTO = (ProductDTO)((OkObjectResult)response).Value;
            }
            catch (InvalidCastException e)
            {
                
            }

            //Assert
            Assert.AreEqual(newProduct.IdProduct, productDTO.IdProduct);
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
                var response = pcMock.GetNumPages();
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
                var response = pcMock.GetAll(PositiveAndCompleteNumPage);
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
            string ExistingName = "Tenis";

            //Act
            OkObjectResult response = null;
            try
            {
                response = (OkObjectResult)(pcMock.GetByName(ExistingName));
            }
            catch (InvalidCastException)
            {

            }

            //Assert
            Assert.IsNotNull(response.Value);
        }

        [TestMethod]
        public void Delete_ValidId_CorrectStatusCode()
        {
            //Arange
            int expected = 200;
            var newProduct = ProductsFromXML[0];

            //Act
            int actual = 0;
            try
            {
                var response = pcMock.Delete(newProduct.IdProduct);
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
