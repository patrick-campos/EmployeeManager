using Moq;
using Xunit;
using EmployeeManager.Application.UseCases;
using EmployeeManager.Domain.DTO.Requests;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces;

namespace EmployeeManager.Test.UseCases
{
    public class UpdateEmployeeUseCaseTest
    {
        private readonly Mock<IRepository<Employee>> _mockRepository;
        private readonly UpdateEmployeeUseCase _useCase;

        public UpdateEmployeeUseCaseTest()
        {
            _mockRepository = new Mock<IRepository<Employee>>();
            _useCase = new UpdateEmployeeUseCase(_mockRepository.Object);
        }

        [Fact]
        public async Task Execute_WithValidIdAndRequest_ShouldUpdateEmployee()
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

            var updateRequest = new EmployeeRequestDTO
            {
                Name = "Patrick",
                LastName = "Campos Updated",
                Mail = "patrick.updated@example.com",
                PositionName = "manager"
            };

            _mockRepository.Setup(x => x.Get(employeeId))
                .ReturnsAsync(employee);

            _mockRepository.Setup(x => x.Update(It.IsAny<Employee>()))
                .Returns(Task.CompletedTask);

            await _useCase.Execute(updateRequest, employeeId);

            _mockRepository.Verify(x => x.Get(employeeId), Times.Once);
            _mockRepository.Verify(x => x.Update(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WithNullEmployee_ShouldThrowArgumentNullException()
        {
            var employeeId = "invalid-id";
            var updateRequest = new EmployeeRequestDTO
            {
                Name = "Test",
                LastName = "User",
                Mail = "test@example.com",
                PositionName = "normal"
            };

            _mockRepository.Setup(x => x.Get(employeeId))
                .ReturnsAsync((Employee)null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => _useCase.Execute(updateRequest, employeeId));

            _mockRepository.Verify(x => x.Get(employeeId), Times.Once);
            _mockRepository.Verify(x => x.Update(It.IsAny<Employee>()), Times.Never);
        }

        [Fact]
        public async Task Execute_UpdateDifferentFields_ShouldSucceed()
        {
            var employeeId = "employee-id";
            var employee = new Employee(
                Guid.NewGuid(),
                "Maria",
                "Silva",
                "maria@example.com",
                "987654321",
                "Secure#Pass2024",
                Guid.NewGuid(),
                "normal",
                "cpf"
            );

            var updateRequest = new EmployeeRequestDTO
            {
                Name = "Maria Updated",
                LastName = "Silva Updated",
                Mail = "maria.updated@example.com",
                PositionName = "manager"
            };

            _mockRepository.Setup(x => x.Get(employeeId))
                .ReturnsAsync(employee);

            _mockRepository.Setup(x => x.Update(It.IsAny<Employee>()))
                .Returns(Task.CompletedTask);

            await _useCase.Execute(updateRequest, employeeId);

            _mockRepository.Verify(x => x.Update(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WithValidEmails_ShouldUpdateSuccessfully()
        {
            var employeeId = "test-id";
            var emails = new[] {
                "newemail@example.com",
                "another.email@company.org",
                "test+tag@domain.io"
            };

            foreach (var email in emails)
            {
                var employee = new Employee(
                    Guid.NewGuid(),
                    "John",
                    "Doe",
                    "john@example.com",
                    "111111111",
                    "Test@Password123",
                    Guid.NewGuid(),
                    "normal",
                    "cpf"
                );

                var updateRequest = new EmployeeRequestDTO
                {
                    Name = "John",
                    LastName = "Doe",
                    Mail = email,
                    PositionName = "normal"
                };

                _mockRepository.Setup(x => x.Get(employeeId))
                    .ReturnsAsync(employee);

                _mockRepository.Setup(x => x.Update(It.IsAny<Employee>()))
                    .Returns(Task.CompletedTask);

                await _useCase.Execute(updateRequest, employeeId);

                _mockRepository.Verify(x => x.Update(It.IsAny<Employee>()), Times.AtLeastOnce);
            }
        }

        [Theory]
        [InlineData("normal")]
        [InlineData("manager")]
        [InlineData("director")]
        public async Task Execute_WithDifferentPositions_ShouldUpdateSuccessfully(string position)
        {
            var employeeId = "employee-id";
            var employee = new Employee(
                Guid.NewGuid(),
                "Test",
                "User",
                "test@example.com",
                "222222222",
                "Valid@Pass2024",
                Guid.NewGuid(),
                "normal",
                "cpf"
            );

            var updateRequest = new EmployeeRequestDTO
            {
                Name = "Test",
                LastName = "User",
                Mail = "test@example.com",
                PositionName = position
            };

            _mockRepository.Setup(x => x.Get(employeeId))
                .ReturnsAsync(employee);

            _mockRepository.Setup(x => x.Update(It.IsAny<Employee>()))
                .Returns(Task.CompletedTask);

            await _useCase.Execute(updateRequest, employeeId);

            _mockRepository.Verify(x => x.Get(employeeId), Times.Once);
            _mockRepository.Verify(x => x.Update(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldCallRepositoryGetExactlyOnce()
        {
            var employeeId = "unique-id";
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

            var updateRequest = new EmployeeRequestDTO
            {
                Name = "Jane",
                LastName = "Smith",
                Mail = "jane@example.com",
                PositionName = "normal"
            };

            _mockRepository.Setup(x => x.Get(employeeId))
                .ReturnsAsync(employee);

            _mockRepository.Setup(x => x.Update(It.IsAny<Employee>()))
                .Returns(Task.CompletedTask);

            await _useCase.Execute(updateRequest, employeeId);

            _mockRepository.Verify(x => x.Get(employeeId), Times.Exactly(1));
        }

        [Fact]
        public async Task Execute_ShouldCallRepositoryUpdateExactlyOnce()
        {
            var employeeId = "another-id";
            var employee = new Employee(
                Guid.NewGuid(),
                "Bob",
                "Johnson",
                "bob@example.com",
                "444444444",
                "MyPass@2024",
                Guid.NewGuid(),
                "normal",
                "cpf"
            );

            var updateRequest = new EmployeeRequestDTO
            {
                Name = "Bob Updated",
                LastName = "Johnson Updated",
                Mail = "bob.updated@example.com",
                PositionName = "manager"
            };

            _mockRepository.Setup(x => x.Get(employeeId))
                .ReturnsAsync(employee);

            _mockRepository.Setup(x => x.Update(It.IsAny<Employee>()))
                .Returns(Task.CompletedTask);

            await _useCase.Execute(updateRequest, employeeId);

            _mockRepository.Verify(x => x.Update(It.IsAny<Employee>()), Times.Exactly(1));
        }
    }
}
