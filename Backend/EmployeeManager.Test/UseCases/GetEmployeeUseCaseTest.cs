using Moq;
using Xunit;
using EmployeeManager.Application.UseCases;
using EmployeeManager.Domain.DTO.Responses;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces;

namespace EmployeeManager.Test.UseCases
{
    public class GetEmployeeUseCaseTest
    {
        private readonly Mock<IRepository<Employee>> _mockRepository;
        private readonly GetEmployeeUseCase _useCase;

        public GetEmployeeUseCaseTest()
        {
            _mockRepository = new Mock<IRepository<Employee>>();
            _useCase = new GetEmployeeUseCase(_mockRepository.Object);
        }

        [Fact]
        public async Task Execute_WithValidId_ShouldReturnEmployeeResponseDTO()
        {
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
            var employeeId = employee.ToString();

            _mockRepository.Setup(x => x.Get(employeeId))
                .ReturnsAsync(employee);

            var result = await _useCase.Execute(employeeId);

            Assert.NotNull(result);
            Assert.IsType<EmployeeResponseDTO>(result);
            _mockRepository.Verify(x => x.Get(employeeId), Times.Once);
        }

        [Fact]
        public async Task Execute_WithNullEmployee_ShouldThrowArgumentNullException()
        {
            var employeeId = "invalid-id";

            _mockRepository.Setup(x => x.Get(employeeId))
                .ReturnsAsync((Employee)null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => _useCase.Execute(employeeId));
            _mockRepository.Verify(x => x.Get(employeeId), Times.Once);
        }

        [Fact]
        public async Task Execute_WithoutParameters_ShouldReturnAllEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee(
                    Guid.NewGuid(),
                    "Patrick",
                    "Campos",
                    "patrick@example.com",
                    "123456789",
                    "Password@123",
                    Guid.NewGuid(),
                    "normal",
                    "cpf"
                ),
                new Employee(
                    Guid.NewGuid(),
                    "Maria",
                    "Silva",
                    "maria@example.com",
                    "987654321",
                    "Secure#Pass2024",
                    Guid.NewGuid(),
                    "manager",
                    "cnpj"
                )
            };

            _mockRepository.Setup(x => x.Get())
                .ReturnsAsync(employees);

            var result = await _useCase.Execute();

            Assert.NotNull(result);
            Assert.IsType<List<EmployeeResponseDTO>>(result);
            Assert.Equal(2, result.Count);
            _mockRepository.Verify(x => x.Get(), Times.Once);
        }

        [Fact]
        public async Task Execute_WithoutParameters_ShouldReturnEmptyList()
        {
            var employees = new List<Employee>();

            _mockRepository.Setup(x => x.Get())
                .ReturnsAsync(employees);

            var result = await _useCase.Execute();

            Assert.NotNull(result);
            Assert.IsType<List<EmployeeResponseDTO>>(result);
            Assert.Empty(result);
            _mockRepository.Verify(x => x.Get(), Times.Once);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("abc-def-ghi")]
        [InlineData("550e8400-e29b-41d4-a716-446655440000")]
        public async Task Execute_WithDifferentValidIds_ShouldReturnEmployee(string employeeId)
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

            _mockRepository.Setup(x => x.Get(employeeId))
                .ReturnsAsync(employee);

            var result = await _useCase.Execute(employeeId);

            Assert.NotNull(result);
            _mockRepository.Verify(x => x.Get(employeeId), Times.Once);
        }
    }
}
