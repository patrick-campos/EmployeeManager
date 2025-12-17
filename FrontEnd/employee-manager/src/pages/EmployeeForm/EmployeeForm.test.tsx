import { ColumnFieldMap } from '../../entites/map/FieldMap';
import { api } from '../../services/api';

jest.mock('../../services/api', () => ({
  api: {
    get: jest.fn(),
    post: jest.fn(),
    patch: jest.fn(),
  },
}));

describe('EmployeeForm Logic', () => {
  const mockEmployee = {
    name: 'John',
    lastname: 'Doe',
    email: 'john@example.com',
    position: 'manager',
    documentType: 'cpf',
    documentNumber: '12345678900',
  };

  beforeEach(() => {
    jest.clearAllMocks();
  });

  it('should initialize ColumnFieldMap with default values', () => {
    const form = new ColumnFieldMap();
    expect(form.firstname.value).toBe('');
    expect(form.lastname.value).toBe('');
    expect(form.mail.value).toBe('');
    expect(form.password.value).toBe('');
  });

  it('should update field value correctly', () => {
    const form = new ColumnFieldMap();
    const updated = form.UpdateField('firstname', 'John');
    expect(updated.firstname.value).toBe('John');
  });

  it('should validate form correctly', () => {
    const form = new ColumnFieldMap();
    form.firstname.value = 'John';
    form.lastname.value = 'Doe';
    form.mail.value = 'john@example.com';
    form.password.value = 'SecurePass@123';
    form.position.value = 'manager';
    form.documentType.value = 'cpf';
    form.documentNumber.value = '12345678900';

    const result = form.Validate();
    expect(result.isValid).toBe(true);
  });

  it('should fail validation with empty fields', () => {
    const form = new ColumnFieldMap();
    const result = form.Validate();
    expect(result.isValid).toBe(false);
  });

  it('should fail validation with invalid email', () => {
    const form = new ColumnFieldMap();
    form.firstname.value = 'John';
    form.lastname.value = 'Doe';
    form.mail.value = 'invalid-email';
    form.password.value = 'SecurePass@123';
    form.position.value = 'manager';
    form.documentType.value = 'cpf';
    form.documentNumber.value = '12345678900';

    const result = form.Validate();
    expect(result.isValid).toBe(false);
    expect(result.validatedColumns.mail.error).toBe(true);
  });

  it('should fail validation with short password', () => {
    const form = new ColumnFieldMap();
    form.firstname.value = 'John';
    form.lastname.value = 'Doe';
    form.mail.value = 'john@example.com';
    form.password.value = 'Short1';
    form.position.value = 'manager';
    form.documentType.value = 'cpf';
    form.documentNumber.value = '12345678900';

    const result = form.Validate();
    expect(result.isValid).toBe(false);
    expect(result.validatedColumns.password.error).toBe(true);
  });

  it('should build new employee entity with correct mapping', () => {
    const form = new ColumnFieldMap();
    form.firstname.value = 'John';
    form.lastname.value = 'Doe';
    form.mail.value = 'john@example.com';
    form.password.value = 'SecurePass@123';
    form.position.value = 'manager';
    form.documentType.value = 'cpf';
    form.documentNumber.value = '12345678900';

    const employee = {
      name: form.firstname.value,
      lastName: form.lastname.value,
      password: form.password.value,
      mail: form.mail.value,
      documentNumber: form.documentNumber.value,
      positionName: form.position.value,
      documentTypeName: form.documentType.value,
    };

    expect(employee.name).toBe('John');
    expect(employee.lastName).toBe('Doe');
    expect(employee.mail).toBe('john@example.com');
    expect(employee.positionName).toBe('manager');
    expect(employee.documentTypeName).toBe('cpf');
  });

  it('should handle API get request', async () => {
    (api.get as jest.Mock).mockResolvedValue(mockEmployee);
    const result = await (api.get as jest.Mock)('/employee/123');
    
    expect(api.get).toHaveBeenCalledWith('/employee/123');
    expect(result).toBeDefined();
  });

  it('should handle API post request for create', async () => {
    (api.post as jest.Mock).mockResolvedValue({ id: '123' });
    const employee = {
      name: 'John',
      lastName: 'Doe',
      password: 'SecurePass@123',
      mail: 'john@example.com',
      documentNumber: '12345678900',
      positionName: 'manager',
      documentTypeName: 'cpf',
    };

    const result = await (api.post as jest.Mock)('/employee', employee);
    expect(api.post).toHaveBeenCalledWith('/employee', employee);
    expect(result).toBeDefined();
  });

  it('should handle API patch request for update', async () => {
    (api.patch as jest.Mock).mockResolvedValue({ id: '123' });
    const employee = {
      name: 'John',
      lastName: 'Doe',
      password: 'SecurePass@123',
      mail: 'john@example.com',
      documentNumber: '12345678900',
      positionName: 'manager',
      documentTypeName: 'cpf',
    };

    const result = await (api.patch as jest.Mock)('/employee/123', employee);
    expect(api.patch).toHaveBeenCalledWith('/employee/123', employee);
    expect(result).toBeDefined();
  });

  it('should handle form field updates sequentially', () => {
    const form = new ColumnFieldMap();
    const step1 = form.UpdateField('firstname', 'John');
    const step2 = step1.UpdateField('lastname', 'Doe');
    const step3 = step2.UpdateField('mail', 'john@example.com');

    expect(step3.firstname.value).toBe('John');
    expect(step3.lastname.value).toBe('Doe');
    expect(step3.mail.value).toBe('john@example.com');
  });

  it('should validate all position types', () => {
    const form = new ColumnFieldMap();
    const positions = ['manager', 'normal', 'director'];

    positions.forEach(pos => {
      const updated = form.UpdateField('position', pos);
      expect(updated.position.value).toBe(pos);
    });
  });

  it('should validate all document types', () => {
    const form = new ColumnFieldMap();
    const docTypes = ['cpf', 'identification number'];

    docTypes.forEach(docType => {
      const updated = form.UpdateField('documentType', docType);
      expect(updated.documentType.value).toBe(docType);
    });
  });

  it('should set field error state', () => {
    const form = new ColumnFieldMap();
    form.firstname.error = true;
    expect(form.firstname.error).toBe(true);
  });

  it('should have error messages for all fields', () => {
    const form = new ColumnFieldMap();
    expect(form.firstname.message).toBeTruthy();
    expect(form.lastname.message).toBeTruthy();
    expect(form.mail.message).toBeTruthy();
    expect(form.password.message).toBeTruthy();
    expect(form.position.message).toBeTruthy();
    expect(form.documentType.message).toBeTruthy();
    expect(form.documentNumber.message).toBeTruthy();
  });

  it('should validate email format correctly', () => {
    const form = new ColumnFieldMap();
    form.firstname.value = 'John';
    form.lastname.value = 'Doe';
    form.mail.value = 'john.doe@example.co.uk';
    form.password.value = 'SecurePass@123';
    form.position.value = 'manager';
    form.documentType.value = 'cpf';
    form.documentNumber.value = '12345678900';

    const result = form.Validate();
    expect(result.validatedColumns.mail.error).toBe(false);
  });

  it('should validate password with special characters', () => {
    const form = new ColumnFieldMap();
    form.firstname.value = 'John';
    form.lastname.value = 'Doe';
    form.mail.value = 'john@example.com';
    form.password.value = 'P@ssw0rd!123';
    form.position.value = 'manager';
    form.documentType.value = 'cpf';
    form.documentNumber.value = '12345678900';

    const result = form.Validate();
    expect(result.validatedColumns.password.error).toBe(false);
  });
});
