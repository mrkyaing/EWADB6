using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories.Domain;
using CloudHRMS.Services;
using CloudHRMS.UnitOfWorks;
using Moq;

namespace CloudHRMS.UnitTests.Domains {
    public class PositionServiceUnitTest {
        //create the all of the mock objects
        private Mock<IPositionService> positonServiceMock = new Mock<IPositionService>();
        private Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        private Mock<IPositoryRepository> positoryRepositoryMock = new Mock<IPositoryRepository>();

        [Fact]
        public void ShouldCreateSuccessed() {
            //1)Arrange
            var expectedPostionViewModel = new PositionViewModel {
                Code = "HR",
                Description = "Human Resouce Manager",
                Level = 1,
            };

            var positionEntity = new PositionEntity() {
                Id = "1",
                Code = expectedPostionViewModel.Code,
                Description = expectedPostionViewModel.Description,
                Level = expectedPostionViewModel.Level,
                CreatedAt = DateTime.Now,
                CreatedBy = "system",
                Ip = "127.0.0.1",
                IsActive = true,
            };
            positoryRepositoryMock.Setup(r => r.Create(positionEntity));
            unitOfWorkMock.Setup(u => u.PositoryRepository).Returns(positoryRepositoryMock.Object);
            //2)Action
            var postionSerice = new PositionService(unitOfWorkMock.Object);
            //3)Assert
            var actualResult = postionSerice.Create(expectedPostionViewModel);
            Assert.Equal(expectedPostionViewModel, actualResult);
        }

        [Fact]
        public void ShouldCreateFailed() {
            //1) Arrange
            var expectedPostionViewModel = new PositionViewModel {
                Code = "HR",
                Description = "Human Resource Manager",
                Level = 1,
            };
            // Simulate failure by throwing an exception when Create is called
            positoryRepositoryMock.Setup(r => r.Create(It.IsAny<PositionEntity>())).Throws(new Exception("Creation failed"));
            unitOfWorkMock.Setup(u => u.PositoryRepository).Returns(positoryRepositoryMock.Object);
            //2) Action
            var positionService = new PositionService(unitOfWorkMock.Object);
            //3) Assert
            Assert.Throws<Exception>(() => positionService.Create(expectedPostionViewModel));
        }
    }
}
