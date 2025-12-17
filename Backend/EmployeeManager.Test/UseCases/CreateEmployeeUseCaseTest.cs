using Moq;
using Xunit;
using EmployeeManager.Application.UseCases;
using EmployeeManager.Domain.DTO.Requests;
using EmployeeManager.Domain.DTO.Responses;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces;

namespace EmployeeManager.Test.UseCases
{
    public class CreateEmployeeUseCaseTest
    {
        private readonly Mock<IRepository<Employee>> _mockRepository;
        private readonly CreateEmployeeUseCase _useCase;

        public CreateEmployeeUseCaseTest()
        {
            _mockRepository = new Mock<IRepository<Employee>>();
            _useCase = new CreateEmployeeUseCase(_mockRepository.Object);
        }

        [Fact]
        public async Task Execute_WithValidRequest_ShouldCreateAndReturnEmployee()
        {
            var employeeRequest = new EmployeeRequestDTO
            {
                Name = "Patrick",
                LastName = "Campos",
                Mail = "patrick@example.com",
                DocumentTypeName = "cpf",
                DocumentNumber = "123456789",
                Password = "Password@123",
                PositionName = "normal"
            };

            var newEmployeeId = "new-employee-id";

            _mockRepository.Setup(x => x.Create(It.IsAny<Employee>()))
                .ReturnsAsync(newEmployeeId);

            var result = await _useCase.Execute(employeeRequest);

            Assert.NotNull(result);
            Assert.IsType<EmployeeResponseDTO>(result);
            _mockRepository.Verify(x => x.Create(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WithValidDocumentTypes_ShouldSucceed()
        {
            var documentTypes = new[] { "cpf", "cnpj", "rg", "passport" };

            foreach (var docType in documentTypes)
            {
                var employeeRequest = new EmployeeRequestDTO
                {
                    Name = "Maria",
                    LastName = "Silva",
                    Mail = "maria@example.com",
                    DocumentTypeName = docType,
                    DocumentNumber = "123456789",
                    Password = "MyP@ss123456",
                    PositionName = "normal"
                };

                var employeeId = $"employee-{docType}";

                _mockRepository.Setup(x => x.Create(It.IsAny<Employee>()))
                    .ReturnsAsync(employeeId);

                var result = await _useCase.Execute(employeeRequest);

                Assert.NotNull(result);
                _mockRepository.Verify(x => x.Create(It.IsAny<Employee>()), Times.AtLeastOnce);
            }
        }

        [Fact]
        public async Task Execute_WithValidEmail_ShouldReturnResponseWithEmployeeData()
        {
            var validEmails = new[] {
                "user@example.com",
                "test.name@company.org",
                "employee+tag@domain.io"
            };

            foreach (var email in validEmails)
            {
                var employeeRequest = new EmployeeRequestDTO
                {
                    Name = "Test",
                    LastName = "User",
                    Mail = email,
                    DocumentTypeName = "cpf",
                    DocumentNumber = "111111111",
                    Password = "Valid@Password123",
                    PositionName = "normal"
                };

                _mockRepository.Setup(x => x.Create(It.IsAny<Employee>()))
                    .ReturnsAsync("employee-id");

                var result = await _useCase.Execute(employeeRequest);

                Assert.NotNull(result);
                _mockRepository.Verify(x => x.Create(It.IsAny<Employee>()), Times.AtLeastOnce);
            }
        }

        [Fact]
        public async Task Execute_RepositoryShouldBeCalledOnce()
        {
            var employeeRequest = new EmployeeRequestDTO
            {
                Name = "Patrick",
                LastName = "Campos",
                Mail = "patrick@example.com",
                DocumentTypeName = "cpf",
                DocumentNumber = "123456789",
                Password = "Password@123",
                PositionName = "normal"
            };

            _mockRepository.Setup(x => x.Create(It.IsAny<Employee>()))
                .ReturnsAsync("new-id");

            await _useCase.Execute(employeeRequest);

            _mockRepository.Verify(x => x.Create(It.IsAny<Employee>()), Times.Exactly(1));
        }
    }
}
