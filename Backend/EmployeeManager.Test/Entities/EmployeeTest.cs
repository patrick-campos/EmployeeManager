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
    }
}