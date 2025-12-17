using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Commands
{
    public class EmployeeCommands
    {
        public static String CreateCommand = @"INSERT INTO employee (firstname, lastname, mail, documentnumber, password, position_id, document_type_id) VALUES (@FirstName, @LastName, @Mail, @DocumentNumber, @Password, (SELECT id FROM position WHERE name = @PositionName), (SELECT id FROM documenttype WHERE name = @DocumentType)) RETURNING id;";
        public static String QueryCommand = @"SELECT empl.id, empl.firstname, empl.lastname,empl.mail, empl.documentnumber, empl.password, empl.position_id, pos.name as PositionName, doctyp.name as DocumentType FROM employee empl INNER JOIN documenttype doctyp on empl.document_type_id = doctyp.id INNER JOIN position pos on empl.position_id = pos.id WHERE empl.id = @id";
        public static String QueryAllCommand = @"SELECT empl.id, empl.firstname, empl.lastname,empl.mail, empl.documentnumber, empl.password, empl.position_id, pos.name as PositionName, doctyp.name as DocumentType FROM employee empl INNER JOIN documenttype doctyp on empl.document_type_id = doctyp.id INNER JOIN position pos on empl.position_id = pos.id";
        public static String DeleteCommand = @"DELETE FROM employee WHERE id = @id";
        public static String UpdateCommand = @"UPDATE employee SET firstname = @firstName, lastname = @lastName, mail = @mail, position_id = (SELECT id FROM position WHERE name = @positionName) WHERE id = @id;
";
    }
}
