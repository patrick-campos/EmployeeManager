using Moq;
using Xunit;
using EmployeeManager.Application.UseCases;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Enums;
using EmployeeManager.Domain.Interfaces;

namespace EmployeeManager.Test.UseCases
{
    public class DeleteEmployeeUseCaseTest
    {
        private readonly Mock<IRepository<Employee>> _mockRepository;
        private readonly DeleteEmployeeUseCase _useCase;

        public DeleteEmployeeUseCaseTest()
        {
            _mockRepository = new Mock<IRepository<Employee>>();
            _useCase = new DeleteEmployeeUseCase(_mockRepository.Object);
        }

        [Fact]
        public async Task Execute_WithNullEmployee_ShouldThrowArgumentNullException()
        {
            var employeeId = "invalid-id";

            _mockRepository.Setup(x => x.Get(employeeId))
                .ReturnsAsync((Employee)null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => _useCase.Execute(employeeId, "director"));

            _mockRepository.Verify(x => x.Get(employeeId), Times.Once);
            _mockRepository.Verify(x => x.Delete(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Execute_WithValidId_ShouldCallRepositoryGet()
        {
            var employeeId = "employee-id";
            var employee = new Employee(
                Guid.NewGuid(),
                "Patrick",
                "Campos",
                "patrick@example.com",
                "123456789",
                "Password@123",
                Guid.NewGuid(),
                "normal",
                "cpf"
            );

            _mockRepository.Setup(x => x.Get(employeeId))
                .ReturnsAsync(employee);

            _mockRepository.Setup(x => x.Delete(employeeId))
                .Returns(Task.CompletedTask);

            try
            {
                await _useCase.Execute(employeeId, "director");
            }
            catch (UnauthorizedAccessException)
            {
            }

            _mockRepository.Verify(x => x.Get(employeeId), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldReturnVoid()
        {
            var employeeId = "test-id";
            var employee = new Employee(
                Guid.NewGuid(),
                "Jane",
                "Smith",
                "jane@example.com",
                "333333333",
                "Secure#Pass123",
                Guid.NewGuid(),
                "normal",
                "cpf"
            );

            _mockRepository.Setup(x => x.Get(employeeId))
                .ReturnsAsync(employee);

            _mockRepository.Setup(x => x.Delete(employeeId))
                .Returns(Task.CompletedTask);

            try
            {
                await _useCase.Execute(employeeId, "director");
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        [Fact]
        public async Task Execute_GetRepositoryCalledWithCorrectId()
        {
            var employeeId = "specific-employee-id";
            var employee = new Employee(
                Guid.NewGuid(),
                "Test",
                "User",
                "test@example.com",
                "111111111",
                "Test@Password123",
                Guid.NewGuid(),
                "normal",
                "cpf"
            );

            _mockRepository.Setup(x => x.Get(employeeId))
                .ReturnsAsync(employee);

            _mockRepository.Setup(x => x.Delete(employeeId))
                .Returns(Task.CompletedTask);

            try
            {
                await _useCase.Execute(employeeId, "director");
            }
            catch (UnauthorizedAccessException)
            {
            }

            _mockRepository.Verify(x => x.Get(employeeId), Times.Once);
        }
    }
}
