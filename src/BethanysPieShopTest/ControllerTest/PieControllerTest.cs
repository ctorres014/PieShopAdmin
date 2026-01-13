using BethanysPieShopAdmin.Controllers;
using BethanysPieShopAdmin.Models.Repositories;
using BethanysPieShopAdmin.ViewModels;
using BethanysPieShopTest.Mocks;
using Moq;

namespace BethanysPieShopTest.ControllerTest
{
    public class PieControllerTest
    {
        [Fact]
        public void GetPies_WithCategoryName_Is_Empty() 
        {
            // Arrange
            var pieRepositoryMock = RepositoryMocks.GetPiesRepositoryMock();
            var categoryRepositoryMock = RepositoryMocks.GetCategoriesRepositoryMock();

            var pieController = new PieController(pieRepositoryMock.Object, categoryRepositoryMock.Object);

            // Act
            var result = pieController.List(string.Empty).Result;

            // Assert
            var viewResult = Assert.IsType<Microsoft.AspNetCore.Mvc.ViewResult>(result);
            var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(viewResult.Model);
            Assert.Equal(16, pieListViewModel.Pies.Count());

        }
    }
}
