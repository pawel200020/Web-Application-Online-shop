using AppCore.BusinessEntities;
using AppCore.Store;
using Data.Abstract;
using Data.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace AppCoreTests
{
    [TestFixture]
    internal class CategoriesTests
    {
         private ICategoriesManager _categories = null!;
         private Mock<ICategoriesRepository> _categoriesRepositoryMock = null!;
         [SetUp]
         public void Setup()
         {
             var loggerMock = new Mock<ILogger<ICategoriesManager>>();
             _categoriesRepositoryMock = new Mock<ICategoriesRepository>();
             _categories = new CategoriesManager(_categoriesRepositoryMock.Object);
             _categoriesRepositoryMock.Setup(x => x.GetAllCategories()).Returns(async()=> await TestDataHelper.GetCategories());
             _categoriesRepositoryMock.Setup(x => x.GetById(It.IsAny<int>()))
                 .Returns(async (int x) => (await TestDataHelper.GetCategories()).FirstOrDefault(y => y.Id == x));
         }
        [TestCase(1,5,5)]
        [TestCase(2,5,5)]
        [TestCase(3,5,3)]
        public void CheckPaginationQuantityResult(int page, int recordsPerPage,int result)
        {
            var paginationModel = new Mock<PaginationModel>();
            paginationModel.Setup(m => m.Page).Returns(page);
            paginationModel.Setup(m => m.RecordsPerPage).Returns(recordsPerPage);

            var (list,quantity) = _categories.GetAllCategoriesPaged(paginationModel.Object).Result;
            Assert.That(quantity, Is.EqualTo(result));

        }
        
         [TestCase(12)]
         public async Task GetAllCategories(int expected)
         {
             var result = await _categories.GetAllCategories();
             Assert.That(expected, Is.EqualTo(result.Count()));
         }
         [TestCase(1, "notebook")]
         [TestCase(4, "accessories")]
         [TestCase(5, "tablet")]
         [TestCase(7, "wireless charger")]
         [TestCase(9, "vegetable")]
         public void GetByIdExists(int id, string expected)
         {
             var result = _categories.GetById(id).Result;
             if (result != null) 
                 Assert.That(expected, Is.EqualTo(result.Name));
         }
         [TestCase(22)]
         [TestCase(54)]
         [TestCase(58)]
         [TestCase(47)]
         [TestCase(98)]
         public void EditNotExistingCategory(int id)
         {
             Assert.ThrowsAsync<InvalidOperationException>(()=>_categories.Edit(id, new Category()));
         }
         [TestCase(22)]
         [TestCase(54)]
         [TestCase(58)]
         [TestCase(47)]
         [TestCase(98)]
         public void DeleteNotExistingCategory(int id)
         {
             Assert.ThrowsAsync<InvalidOperationException>(() => _categories.Delete(id));
         }
    }
}
