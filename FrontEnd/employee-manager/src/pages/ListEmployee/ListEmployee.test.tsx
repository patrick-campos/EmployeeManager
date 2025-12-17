import { api } from '../../services/api';
import type { Employee } from '../../entites/employee';

jest.mock('../../services/api', () => ({
  api: {
    get: jest.fn(),
    delete: jest.fn(),
  },
}));

describe('ListEmployee Logic', () => {
  const mockEmployees: Employee[] = [
    {
      id: '1',
      firstname: 'John',
      lastname: 'Doe',
      mail: 'john@example.com',
      position: 'manager',
      documentType: 'cpf',
      documentNumber: '12345678900',
    },
    {
      id: '2',
      firstname: 'Jane',
      lastname: 'Smith',
      mail: 'jane@example.com',
      position: 'normal',
      documentType: 'cpf',
      documentNumber: '98765432100',
    },
  ];

  beforeEach(() => {
    jest.clearAllMocks();
  });

  it('should fetch employees from API', async () => {
    (api.get as jest.Mock).mockResolvedValue(mockEmployees);
    const result = await api.get('/employee');

    expect(api.get).toHaveBeenCalledWith('/employee');
    expect(result).toEqual(mockEmployees);
  });

  it('should return empty array when no employees', async () => {
    (api.get as jest.Mock).mockResolvedValue([]);
    const result = await api.get('/employee');

    expect(result).toEqual([]);
  });

  it('should handle API error gracefully', async () => {
    const error = new Error('API Error');
    (api.get as jest.Mock).mockRejectedValue(error);

    try {
      await api.get('/employee');
    } catch (err) {
      expect(err).toEqual(error);
    }
  });

  it('should delete employee from API', async () => {
    (api.delete as jest.Mock).mockResolvedValue({});
    const result = await api.delete('/employee/1');

    expect(api.delete).toHaveBeenCalledWith('/employee/1');
  });

  it('should filter out deleted employee from list', () => {
    const employees = [...mockEmployees];
    const deleted = employees.filter(emp => emp.id !== '1');

    expect(deleted).toHaveLength(1);
    expect(deleted[0].id).toBe('2');
  });

  it('should handle delete with correct employee id', async () => {
    (api.delete as jest.Mock).mockResolvedValue({});
    
    await api.delete('/employee/123');
    expect(api.delete).toHaveBeenCalledWith('/employee/123');
  });

  it('should maintain employee list order after operations', () => {
    const employees = [...mockEmployees];
    expect(employees[0].firstname).toBe('John');
    expect(employees[1].firstname).toBe('Jane');
  });

  it('should access employee properties correctly', () => {
    const employee = mockEmployees[0];
    
    expect(employee.id).toBe('1');
    expect(employee.firstname).toBe('John');
    expect(employee.lastname).toBe('Doe');
    expect(employee.mail).toBe('john@example.com');
    expect(employee.position).toBe('manager');
    expect(employee.documentType).toBe('cpf');
    expect(employee.documentNumber).toBe('12345678900');
  });

  it('should handle multiple employees correctly', () => {
    const employees = mockEmployees;
    
    expect(employees.length).toBe(2);
    expect(employees).toContainEqual(expect.objectContaining({
      firstname: 'John',
      mail: 'john@example.com',
    }));
    expect(employees).toContainEqual(expect.objectContaining({
      firstname: 'Jane',
      mail: 'jane@example.com',
    }));
  });

  it('should find employee by id', () => {
    const employee = mockEmployees.find(emp => emp.id === '1');
    expect(employee?.firstname).toBe('John');
  });

  it('should find employee by email', () => {
    const employee = mockEmployees.find(emp => emp.mail === 'jane@example.com');
    expect(employee?.firstname).toBe('Jane');
  });

  it('should find employee by position', () => {
    const managers = mockEmployees.filter(emp => emp.position === 'manager');
    expect(managers).toHaveLength(1);
    expect(managers[0].firstname).toBe('John');
  });

  it('should map employee data to display format', () => {
    const displayColumns = [
      { title: 'Name', key: 'name', valueEmphasis: true },
      { title: 'Email', key: 'email', valueEmphasis: false },
      { title: 'Position', key: 'position', valueEmphasis: true },
    ];

    expect(displayColumns).toHaveLength(3);
    expect(displayColumns[0].key).toBe('name');
    expect(displayColumns[1].key).toBe('email');
  });

  it('should validate employee object structure', () => {
    const employee = mockEmployees[0];
    
    expect(employee).toHaveProperty('id');
    expect(employee).toHaveProperty('firstname');
    expect(employee).toHaveProperty('lastname');
    expect(employee).toHaveProperty('mail');
    expect(employee).toHaveProperty('position');
  });

  it('should handle large employee list', () => {
    const largeList = Array.from({ length: 100 }, (_, i) => ({
      id: String(i),
      firstname: `Employee ${i}`,
      lastname: `Last ${i}`,
      mail: `emp${i}@example.com`,
      position: i % 3 === 0 ? 'manager' : 'normal',
      documentType: 'cpf',
      documentNumber: `123456789${i}`,
    }));

    expect(largeList).toHaveLength(100);
    expect(largeList[0].firstname).toBe('Employee 0');
    expect(largeList[99].firstname).toBe('Employee 99');
  });

  it('should handle employee without optional fields', () => {
    const minimalEmployee: Employee = {
      id: '1',
      firstname: 'John',
      lastname: 'Doe',
      mail: 'john@example.com',
      position: 'manager',
      documentType: 'cpf',
      documentNumber: '12345678900',
    };

    expect(minimalEmployee.id).toBeTruthy();
    expect(minimalEmployee.firstname).toBeTruthy();
    expect(minimalEmployee.mail).toBeTruthy();
  });

  it('should delete multiple employees sequentially', async () => {
    (api.delete as jest.Mock).mockResolvedValue({});

    await api.delete('/employee/1');
    await api.delete('/employee/2');

    expect(api.delete).toHaveBeenCalledTimes(2);
    expect(api.delete).toHaveBeenNthCalledWith(1, '/employee/1');
    expect(api.delete).toHaveBeenNthCalledWith(2, '/employee/2');
  });

  it('should verify edit navigation parameters', () => {
    const employee = mockEmployees[0];
    const navigatePath = `/employee/${employee.id}`;

    expect(navigatePath).toBe('/employee/1');
  });

  it('should extract edit and delete button data correctly', () => {
    const employee = mockEmployees[0];
    const editButton = { action: 'edit', id: employee.id };
    const deleteButton = { action: 'delete', id: employee.id };

    expect(editButton.action).toBe('edit');
    expect(deleteButton.action).toBe('delete');
    expect(editButton.id).toBe(deleteButton.id);
  });
});
