using EmployeeManager.Domain.DTO.Requests;
using EmployeeManager.Domain.Enums;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace EmployeeManager.Domain.Entities
{
    public class Employee
    {
        private Guid Id { get; set; }
        public String FirstName { get; private set; }
        public String LastName { get; private set; }
        public String Mail { get; private set; }
        public String DocumentType { get; private set; }
        public String DocumentNumber { get; private set; }
        public String PositionName { get; private set; }
        public String Password { get; private set; }

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

        public bool CheckIfPositionOfCurrentUserIsAboveThanUserHowWillBeModified(string currentUserPosition, string positionOfUserModified)
        {
            Position.TryParse(currentUserPosition, out Position PositionCurrentUser);

            return CheckIfUserCanCreateAnotherUserWithThisLevel(PositionCurrentUser, positionOfUserModified);
        }

        public Employee(Guid id, String firstname, String lastname, String mail, String documentnumber, String password, Guid position_id, String positionname, String documenttype)
        {
            this.Id = id;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Mail = mail;
            this.DocumentType = documenttype.ToLower();
            this.DocumentNumber = documentnumber;
            this.Password = password;
            this.PositionName = positionname;
        }

        public Employee(EmployeeRequestDTO newUser): this(newUser.Name, newUser.LastName, newUser.Mail, newUser.DocumentTypeName, newUser.DocumentNumber, newUser.Password, newUser.PositionName, Position.director)
        {
        }

        public Employee(string firstName, string lastName, string mail, string documentType, string documentNumber, string password, string positionOfNewUser, Position positionActualUser)
        {
            Dictionary<string, string> errorsValidation = ValidateFieldsWithInvalidValue(firstName, lastName, mail, documentType, documentNumber, password, positionOfNewUser);

            if (errorsValidation.Count > 0)
                throw new ArgumentException($"One or more arguments has invalid value: {string.Join("; ", errorsValidation.Select(x => $"{x.Key}: {x.Value}"))}");


            if (!CheckIfUserCanCreateAnotherUserWithThisLevel(positionActualUser, positionOfNewUser))
                throw new UnauthorizedAccessException("You can't access or create another employer with equal or higher level than yourself");

            FirstName = firstName;
            LastName = lastName;
            Mail = mail;
            DocumentType = documentType.ToLower();
            DocumentNumber = documentNumber;
            Password = password;
            PositionName = positionOfNewUser.ToLower();
        }

        public void update(EmployeeRequestDTO emplWithModification)
        {
            this.Mail = emplWithModification.Mail;
            this.FirstName = emplWithModification.Name;
            this.LastName = emplWithModification.LastName;
            this.PositionName = emplWithModification.PositionName;
        }

        private void setId(string id)
        {
            Guid.TryParse(id, out Guid idParsed);
            this.Id = idParsed;
        }

        public Guid GetId()
        {
            return this.Id;
        }

    }
}
