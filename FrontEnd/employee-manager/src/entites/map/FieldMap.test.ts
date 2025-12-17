import { FieldMap, ColumnFieldMap } from './FieldMap';

describe('FieldMap', () => {
  it('should create FieldMap with message and default values', () => {
    const fieldMap = new FieldMap('Test message');
    expect(fieldMap.message).toBe('Test message');
    expect(fieldMap.value).toBe('');
    expect(fieldMap.error).toBe(false);
  });

  it('should allow setting value', () => {
    const fieldMap = new FieldMap('Test');
    fieldMap.value = 'test value';
    expect(fieldMap.value).toBe('test value');
  });

  it('should allow setting error state', () => {
    const fieldMap = new FieldMap('Test');
    fieldMap.error = true;
    expect(fieldMap.error).toBe(true);
  });
});

describe('ColumnFieldMap', () => {
  it('should initialize all fields with default messages', () => {
    const columnMap = new ColumnFieldMap();
    expect(columnMap.firstname.message).toBe('First Name is required');
    expect(columnMap.lastname.message).toBe('Last Name is required');
    expect(columnMap.mail.message).toBe('Mail is required');
    expect(columnMap.password.message).toContain('12 characters');
    expect(columnMap.position.message).toBe('Position is required');
    expect(columnMap.documentType.message).toBe('Document Type is required');
    expect(columnMap.documentNumber.message).toBe('Document Number is required');
  });

  it('should validate empty fields as invalid', () => {
    const columnMap = new ColumnFieldMap();
    const result = columnMap.Validate();
    expect(result.isValid).toBe(false);
    expect(result.validatedColumns.firstname.error).toBe(true);
    expect(result.validatedColumns.lastname.error).toBe(true);
  });

  it('should validate valid email format', () => {
    const columnMap = new ColumnFieldMap();
    columnMap.firstname.value = 'John';
    columnMap.lastname.value = 'Doe';
    columnMap.mail.value = 'john@example.com';
    columnMap.password.value = 'SecurePass@123';
    columnMap.position.value = 'manager';
    columnMap.documentType.value = 'cpf';
    columnMap.documentNumber.value = '12345678901';

    const result = columnMap.Validate();
    expect(result.validatedColumns.mail.error).toBe(false);
  });

  it('should invalidate invalid email format', () => {
    const columnMap = new ColumnFieldMap();
    columnMap.firstname.value = 'John';
    columnMap.lastname.value = 'Doe';
    columnMap.mail.value = 'invalid-email';
    columnMap.password.value = 'SecurePass@123';
    columnMap.position.value = 'manager';
    columnMap.documentType.value = 'cpf';
    columnMap.documentNumber.value = '12345678901';

    const result = columnMap.Validate();
    expect(result.validatedColumns.mail.error).toBe(true);
  });

  it('should validate strong password', () => {
    const columnMap = new ColumnFieldMap();
    columnMap.firstname.value = 'John';
    columnMap.lastname.value = 'Doe';
    columnMap.mail.value = 'john@example.com';
    columnMap.password.value = 'StrongPass@123';
    columnMap.position.value = 'manager';
    columnMap.documentType.value = 'cpf';
    columnMap.documentNumber.value = '12345678901';

    const result = columnMap.Validate();
    expect(result.validatedColumns.password.error).toBe(false);
  });

  it('should invalidate weak password', () => {
    const columnMap = new ColumnFieldMap();
    columnMap.firstname.value = 'John';
    columnMap.lastname.value = 'Doe';
    columnMap.mail.value = 'john@example.com';
    columnMap.password.value = 'weak';
    columnMap.position.value = 'manager';
    columnMap.documentType.value = 'cpf';
    columnMap.documentNumber.value = '12345678901';

    const result = columnMap.Validate();
    expect(result.validatedColumns.password.error).toBe(true);
  });

  it('should set error state for specific field', () => {
    const columnMap = new ColumnFieldMap();
    columnMap.SetErrorState('firstname', true);
    expect(columnMap.firstname.error).toBe(true);
    expect(columnMap.lastname.error).toBe(false);
  });

  it('should update field value', () => {
    const columnMap = new ColumnFieldMap();
    const updated = columnMap.UpdateField('firstname', 'Patrick');
    expect(updated.firstname.value).toBe('Patrick');
    expect(columnMap.firstname.value).toBe('');
  });

  it('should not mutate original when updating field', () => {
    const columnMap = new ColumnFieldMap();
    columnMap.firstname.value = 'John';
    const updated = columnMap.UpdateField('firstname', 'Patrick');
    expect(columnMap.firstname.value).toBe('John');
    expect(updated.firstname.value).toBe('Patrick');
  });

  it('should validate complete valid form', () => {
    const columnMap = new ColumnFieldMap();
    columnMap.firstname.value = 'Patrick';
    columnMap.lastname.value = 'Campos';
    columnMap.mail.value = 'patrick@example.com';
    columnMap.password.value = 'ValidPass@123';
    columnMap.position.value = 'normal';
    columnMap.documentType.value = 'cpf';
    columnMap.documentNumber.value = '12345678901';

    const result = columnMap.Validate();
    expect(result.isValid).toBe(true);
  });

  it('should handle whitespace-only values as invalid', () => {
    const columnMap = new ColumnFieldMap();
    columnMap.firstname.value = '   ';
    columnMap.lastname.value = 'Campos';
    columnMap.mail.value = 'patrick@example.com';
    columnMap.password.value = 'ValidPass@123';
    columnMap.position.value = 'normal';
    columnMap.documentType.value = 'cpf';
    columnMap.documentNumber.value = '12345678901';

    const result = columnMap.Validate();
    expect(result.validatedColumns.firstname.error).toBe(true);
    expect(result.isValid).toBe(false);
  });

  it('should validate multiple email formats', () => {
    const validEmails = [
      'user@example.com',
      'test.name@company.org',
      'email+tag@domain.io'
    ];

    validEmails.forEach(email => {
      const columnMap = new ColumnFieldMap();
      columnMap.firstname.value = 'John';
      columnMap.lastname.value = 'Doe';
      columnMap.mail.value = email;
      columnMap.password.value = 'ValidPass@123';
      columnMap.position.value = 'manager';
      columnMap.documentType.value = 'cpf';
      columnMap.documentNumber.value = '12345678901';

      const result = columnMap.Validate();
      expect(result.validatedColumns.mail.error).toBe(false);
    });
  });
});
