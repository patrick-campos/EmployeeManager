using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Enums;

namespace EmployeeManager.Test.Entities
{
    public class EmployeeTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("", "campos")]
        [InlineData(" ", "campos")]
        [InlineData(null, "campos")]
        [InlineData("Patrick", "")]
        [InlineData("Patrick", " ")]
        [InlineData("Patrick", null)]
        public void ValidateEmptyEmplName_ShouldThrowErro(string firstName, string lastName)
        {
            Assert.Throws<ArgumentException>(() => new Employee(firstName, lastName, "email@ficticio.com.br", "cpf", "123456789", "Teste@123456", "normal", Position.manager));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("patrickcampos")]
        [InlineData("@teste.com")]
        [InlineData("patrickcampos@")]
        [InlineData("patrickcampos@teste")]
        [InlineData("patrickcampos@teste.")]
        [InlineData("patrickcampos@.")]
        public void ValidateEmptyEmplMail_ShouldThrowErro(string mail)
        {
            Assert.Throws<ArgumentException>(() => new Employee("patrick", "campos", mail, "cpf", "123456789", "Teste@123456", "normal", Position.manager));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("", "123456")]
        [InlineData(" ", "123456")]
        [InlineData(null, "123456")]
        [InlineData("Cpf", "")]
        [InlineData("Cpf", " ")]
        [InlineData("Cpf", null)]
        public void ValidateEmptyEmplDocument_ShouldThrowErro(string documentType, string documentNumber)
        {
            Assert.Throws<ArgumentException>(() => new Employee("patrick", "campos", "email@ficticio.com.br", documentType, documentNumber, "Teste@123456", "normal", Position.manager));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("patrickcamposaaaaaaaaaa")]
        [InlineData("pPcamposaaaaaa")]
        [InlineData("pP1campos123456")]
        [InlineData("PPCAMPOS24POO4")]
        [InlineData("@PPPPASJDIPSAP1")]
        [InlineData("@trpriowir11123")]
        [InlineData("@trRS11123")]
        public void ValidateInvalidEmplPassword_ShouldThrowErro(string password)
        {
            Assert.Throws<ArgumentException>(() => new Employee("patrick", "campos", "email@ficticio.com.br", "cpf", "123456789", password, "normal", Position.manager));
        }

        [Theory]
        [InlineData("", Position.director)]
        [InlineData(" ", Position.director)]
        [InlineData(null, Position.director)]
        [InlineData("Director", Position.manager)]
        [InlineData("Manager", Position.normal)]
        [InlineData("Director", Position.normal)]
        public void ValidatePositionOfNewUser_ShouldThrowErro(string positionOfNewUser, Position positionActualUser)
        {
            Assert.Throws<ArgumentException>(() => new Employee("patrick", "campos", "email@ficticio.com.br", "cpf", "123456789", "Teste@123456", positionOfNewUser, positionActualUser));
        }

        [Theory]
        [InlineData("director", Position.manager)]
        [InlineData("manager", Position.normal)]
        [InlineData("director", Position.normal)]
        public void ValidateIfUserHasPermissionToCreateAnotherUserWithPosition_ShouldThrowErro(string positionOfNewUser, Position positionActualUser)
        {
            Assert.Throws<UnauthorizedAccessException>(() => new Employee("patrick", "campos", "email@ficticio.com.br", "cpf", "123456789", "Teste@123456", positionOfNewUser, positionActualUser));
        }

        // ===== SUCCESS SCENARIOS =====

        [Theory]
        [InlineData("Patrick", "Campos", "normal", Position.director)]
        [InlineData("Patrick", "Campos", "normal", Position.manager)]
        [InlineData("Patrick", "Campos", "manager", Position.director)]
        [InlineData("João", "Silva", "normal", Position.manager)]
        [InlineData("Maria", "Santos", "manager", Position.director)]
        public void CreateEmployee_WithValidData_ShouldSucceed(string firstName, string lastName, string positionOfNewUser, Position positionActualUser)
        {
            // Arrange & Act
            var employee = new Employee(firstName, lastName, "email@ficticio.com.br", "cpf", "123456789", "Teste@123456", positionOfNewUser, positionActualUser);

            // Assert
            Assert.NotNull(employee);
            Assert.Equal(firstName, employee.FirstName);
            Assert.Equal(lastName, employee.LastName);
            Assert.Equal("email@ficticio.com.br", employee.Mail);
            Assert.Equal("cpf", employee.DocumentType);
            Assert.Equal("123456789", employee.DocumentNumber);
            Assert.Equal("Teste@123456", employee.Password);
            Assert.Equal(positionOfNewUser.ToLower(), employee.PositionName);
        }

        [Theory]
        [InlineData("normal", Position.director)]
        [InlineData("normal", Position.manager)]
        [InlineData("manager", Position.director)]
        public void CreateEmployee_WithValidPassword_ShouldSucceed(string positionOfNewUser, Position positionActualUser)
        {
            // Arrange - Test various valid passwords
            var validPasswords = new[]
            {
                "Teste@123456",      // lowercase, uppercase, digit, symbol
                "MyPassword@1",      // Different pattern
                "SecurePass#9abc",   // Different symbol
                "Complex$Pass2023"   // Another valid format
            };

            // Act & Assert
            foreach (var password in validPasswords)
            {
                var employee = new Employee("Patrick", "Campos", "email@ficticio.com.br", "cpf", "123456789", password, positionOfNewUser, positionActualUser);
                Assert.NotNull(employee);
                Assert.Equal(password, employee.Password);
            }
        }

        [Theory]
        [InlineData("normal", Position.director)]
        [InlineData("normal", Position.manager)]
        [InlineData("manager", Position.director)]
        public void CreateEmployee_WithValidEmail_ShouldSucceed(string positionOfNewUser, Position positionActualUser)
        {
            // Arrange - Test various valid emails
            var validEmails = new[]
            {
                "patrick@example.com",
                "user.name@company.co.uk",
                "test+tag@domain.org",
                "valid_email@test.io"
            };

            // Act & Assert
            foreach (var email in validEmails)
            {
                var employee = new Employee("Patrick", "Campos", email, "cpf", "123456789", "Teste@123456", positionOfNewUser, positionActualUser);
                Assert.NotNull(employee);
                Assert.Equal(email, employee.Mail);
            }
        }

        [Theory]
        [InlineData("cpf", "normal", Position.director)]
        [InlineData("cnpj", "normal", Position.manager)]
        [InlineData("rg", "manager", Position.director)]
        [InlineData("passport", "normal", Position.director)]
        public void CreateEmployee_WithValidDocumentType_ShouldSucceed(string documentType, string positionOfNewUser, Position positionActualUser)
        {
            // Arrange & Act
            var employee = new Employee("Patrick", "Campos", "email@ficticio.com.br", documentType, "123456789", "Teste@123456", positionOfNewUser, positionActualUser);

            // Assert
            Assert.NotNull(employee);
            Assert.Equal(documentType.ToLower(), employee.DocumentType);
            Assert.Equal("123456789", employee.DocumentNumber);
        }

        [Theory]
        [InlineData("Patrick", "Campos", "employee@company.com", "cpf", "12345678900", "Password@123", "normal", Position.director)]
        [InlineData("Maria", "Silva", "maria@example.org", "cnpj", "98765432100191", "Secure#Pass2024", "manager", Position.director)]
        [InlineData("João", "Santos", "joao@test.io", "rg", "987654321", "MyP@ss123456", "normal", Position.manager)]
        public void CreateEmployee_WithAllValidFields_ShouldSucceed(
            string firstName, string lastName, string mail, string documentType,
            string documentNumber, string password, string positionOfNewUser, Position positionActualUser)
        {
            // Act
            var employee = new Employee(firstName, lastName, mail, documentType, documentNumber, password, positionOfNewUser, positionActualUser);

            // Assert
            Assert.NotNull(employee);
            Assert.Equal(firstName, employee.FirstName);
            Assert.Equal(lastName, employee.LastName);
            Assert.Equal(mail, employee.Mail);
            Assert.Equal(documentType.ToLower(), employee.DocumentType);
            Assert.Equal(documentNumber, employee.DocumentNumber);
            Assert.Equal(password, employee.Password);
            Assert.Equal(positionOfNewUser.ToLower(), employee.PositionName);
        }

        [Theory]
        [InlineData("normal", Position.director)]
        [InlineData("manager", Position.director)]
        public void CreateEmployee_WithSpecialCharactersInName_ShouldSucceed(string positionOfNewUser, Position positionActualUser)
        {
            // Arrange - Names with valid special characters
            var names = new[] {
                ("José", "da Silva"),
                ("André", "Pereira"),
                ("François", "Dupont")
            };

            // Act & Assert
            foreach (var (firstName, lastName) in names)
            {
                var employee = new Employee(firstName, lastName, "email@ficticio.com.br", "cpf", "123456789", "Teste@123456", positionOfNewUser, positionActualUser);
                Assert.NotNull(employee);
                Assert.Equal(firstName, employee.FirstName);
                Assert.Equal(lastName, employee.LastName);
            }
        }

        [Fact]
        public void CreateEmployee_DirectorCreatingNormalUser_ShouldSucceed()
        {
            // Act
            var employee = new Employee("Patrick", "Campos", "email@ficticio.com.br", "cpf", "123456789", "Teste@123456", "normal", Position.director);

            // Assert
            Assert.NotNull(employee);
            Assert.Equal("normal", employee.PositionName);
        }

        [Fact]
        public void CreateEmployee_DirectorCreatingManager_ShouldSucceed()
        {
            // Act
            var employee = new Employee("Patrick", "Campos", "email@ficticio.com.br", "cpf", "123456789", "Teste@123456", "manager", Position.director);

            // Assert
            Assert.NotNull(employee);
            Assert.Equal("manager", employee.PositionName);
        }

        [Fact]
        public void CreateEmployee_ManagerCreatingNormalUser_ShouldSucceed()
        {
            // Act
            var employee = new Employee("Patrick", "Campos", "email@ficticio.com.br", "cpf", "123456789", "Teste@123456", "normal", Position.manager);

            // Assert
            Assert.NotNull(employee);
            Assert.Equal("normal", employee.PositionName);
        }

        [Fact]
        public void CreateEmployee_DocumentNumberWithLeadingZeros_ShouldSucceed()
        {
            // Act
            var employee = new Employee("Patrick", "Campos", "email@ficticio.com.br", "cpf", "00012345678", "Teste@123456", "normal", Position.director);

            // Assert
            Assert.NotNull(employee);
            Assert.Equal("00012345678", employee.DocumentNumber);
        }

        [Fact]
        public void CreateEmployee_LongValidDocumentNumber_ShouldSucceed()
        {
            // Act
            var employee = new Employee("Patrick", "Campos", "email@ficticio.com.br", "cnpj", "12345678901234", "Teste@123456", "normal", Position.director);

            // Assert
            Assert.NotNull(employee);
            Assert.Equal("12345678901234", employee.DocumentNumber);
        }
    }
}