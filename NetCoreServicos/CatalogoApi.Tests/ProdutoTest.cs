using CatalogoApi.Controllers;
using CatalogoApi.Model.View;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Xunit;

namespace CatalogoApi.Tests
{
    public class ProdutoTest
    {
        [Fact]
        public void Lista_Produto_Sucess()
        {
            //Arrange
            

            //Act
            var produtoController = new ProdutoController(null);

            List<ProdutoView> actionResult = (List<ProdutoView>)produtoController.Listar();

            //Assert
            Assert.True(actionResult.Count > 0);
            
        }
    }
}
