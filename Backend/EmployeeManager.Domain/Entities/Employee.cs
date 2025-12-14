using EmployeeManager.Domain.Enums;
using System.Text.RegularExpressions;

namespace EmployeeManager.Domain.Entities
{
    public class Employee
    {
        private String FirstName { get; set; }
        private String LastName { get; set; }
        private String Email { get; set; }
        private String DocumentType { get; set; }
        private String DocumentNumber { get; set; }
        private Position Position { get; set; }
        private String Password { get; set; }

        private Regex PasswordValidator = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{12,}$");
        private Regex MailValidator = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");

        public Dictionary<string, string> ValidateFieldsWithInvalidValue(string firstName, string lastName, string mail, string documentType, string documentNumber, string password, string positionOfNewUser)
        {
            Dictionary<string, string> erros = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                erros.Add("Name", "Name Cannot be empty");

            if (!MailValidator.IsMatch(mail))
                erros.Add("Mail", "Mail is not valid");

            if (string.IsNullOrWhiteSpace(documentType) || string.IsNullOrWhiteSpace(documentNumber))
                erros.Add("Document", "Documento cannot be empty");

            if (!PasswordValidator.IsMatch(password))
                erros.Add("Password", "Password does not respect the rules: at least 1 upper letter, 1 lower letter, 1 number, 1 symbol and with minimum 12 characters");

            if(!Position.TryParse(positionOfNewUser, out Position PositionNewUser))
                erros.Add("Position", "Invalid value to position");

            return erros;
        }

        private bool CheckIfUserCanCreateAnotherUserWithThisLevel(Position positionActualUser, string positionOfNewUser)
        {
            Position.TryParse(positionOfNewUser, out Position PositionNewUser);

            return positionActualUser > PositionNewUser;
        }

        public Employee(string firstName, string lastName, string mail, string documentType, string documentNumber, string password, string positionOfNewUser, Position positionActualUser)
        {
            Dictionary<string, string> errorsValidation = ValidateFieldsWithInvalidValue(firstName, lastName, mail, documentType, documentNumber, password, positionOfNewUser);

            if (errorsValidation.Count > 0)
                throw new ArgumentException($"One or more arguments has invalid value: {errorsValidation.Select(x=>x.Key)}");

            if (!CheckIfUserCanCreateAnotherUserWithThisLevel(positionActualUser, positionOfNewUser))
                throw new UnauthorizedAccessException("User cannot create another user with a higher leval than yourself");

            FirstName = firstName;
            LastName = lastName;
            Email = mail;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            Password = password;
        }

    }
}
