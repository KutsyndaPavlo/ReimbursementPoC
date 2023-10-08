using Moq;
using NUnit.Framework;
using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Program;

namespace ReimbursementPoC.Administration.API.UnitTests
{
    [TestFixture]
    public class ProgramEntityTest
    {
        private Mock<IProgramService> _programServiceMock;

        [SetUp]
        public void SetUp()
        {
            _programServiceMock = new Mock<IProgramService>();
        }

        [Test]
        public void CreateNew_IsSinglePerStatePerPeriod_ReturnsNewProgramEntity()
        {
            // Arrange
            var name = "programName";
            var description = "";
            var stateId = 0;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddYears(1);
            _programServiceMock.Setup(x => x.IsSinglePerStatePerPeriod(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

            // Act
            var actualResult = ProgramEntity.CreateNew(name, description, stateId, startDate, endDate, _programServiceMock.Object);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOf<ProgramEntity>(actualResult);
            Assert.AreNotEqual(Guid.Empty, actualResult.Id);
            Assert.AreEqual(name, actualResult.Name);
            Assert.AreEqual(description, actualResult.Description);
            //Assert.AreEqual(stateId, actualResult.State.Id);
            Assert.AreEqual(startDate, actualResult.Period.StartDate);
            Assert.AreEqual(endDate, actualResult.Period.EndDate);
            Assert.False(actualResult.IsCanceled);
            CollectionAssert.IsNotEmpty(actualResult.DomainEvents);
        }

        [Test]
        public void CreateNew_ProgramIsNotSinglePerStatePerPeriod_ThrowsBusinessRuleValidationException()
        {
            // Arrange
            var name = "programName";
            var description = "";
            var stateId = 0;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddYears(1);
            _programServiceMock.Setup(x => x.IsSinglePerStatePerPeriod(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(false);

            // Act 
            // Assert 
            Assert.Throws<BusinessRuleValidationException>(
                () => { ProgramEntity.CreateNew(name, description, stateId, startDate, endDate, _programServiceMock.Object); },
                "Program already exists.");

            _programServiceMock.Verify(x => x.IsSinglePerStatePerPeriod(stateId, startDate, endDate), Times.Once);
        }
    }
}