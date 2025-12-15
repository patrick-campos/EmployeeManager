using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Commands
{
    public class EmployeeCommands
    {
        public static String CreateCommand = "@INSERT INTO employee(firstname,lastname,mail,documentnumber,password, position_id, document_type_id) VALUES(@FirstName,@LastName,@Mail,@DocumentNumber,@Password,@PositionId,@DocumentTypeId); RETURNING id;";
        public static String QueryCommand = "@SELECT empl.id, empl.firstname, empl.lastname,mail, empl.documentnumber, empl.password, empl.position_id, pos.name, doctyp.name FROM employee empl INNER JOIN documenttype doctyp on empl.document_type_id = doctyp.id INNER JOIN position pos on empl.position_id = pos.id WHERE empl.id == @Id";
    }
}
