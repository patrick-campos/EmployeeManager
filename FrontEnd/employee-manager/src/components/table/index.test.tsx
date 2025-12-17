import { Table } from './index';

jest.mock('./header', () => ({
  TableHeader: jest.fn(() => null),
}));

jest.mock('./body', () => ({
  TableBody: jest.fn(() => null),
}));

jest.mock('../ui/common', () => ({
  DivWithReference: jest.fn(({ children }) => children),
  PWithReference: jest.fn(({ children }) => children),
}));

jest.mock('../ui/table', () => ({
  TableElement: jest.fn(({ children }) => children),
}));

describe('Table Component', () => {
  const mockColumns = [
    { title: 'First Name', key: 'firstname', valueEmphasis: true },
    { title: 'Email', key: 'mail', valueEmphasis: false },
    { title: 'Position', key: 'position', valueEmphasis: true },
  ];

  const mockItems = [
    { firstname: 'Patrick', lastname: 'Campos', mail: 'patrick@example.com', position: 'Developer' },
    { firstname: 'Maria', lastname: 'Silva', mail: 'maria@example.com', position: 'Manager' },
  ];

  it('should accept Columns and Items props', () => {
    expect(() => Table({ Columns: mockColumns, Items: mockItems })).not.toThrow();
  });

  it('should render without crashing', () => {
    const component = Table({ Columns: mockColumns, Items: mockItems });
    expect(component).toBeDefined();
  });

  it('should handle empty Items array', () => {
    expect(() => Table({ Columns: mockColumns, Items: [] })).not.toThrow();
  });

  it('should handle single item', () => {
    const singleItem = [{ firstname: 'John', lastname: 'Doe', mail: 'john@example.com', position: 'Lead' }];
    expect(() => Table({ Columns: mockColumns, Items: singleItem })).not.toThrow();
  });

  it('should handle multiple items', () => {
    const manyItems = Array.from({ length: 10 }, (_, i) => ({
      firstname: `Employee${i}`,
      lastname: `Last${i}`,
      mail: `emp${i}@example.com`,
      position: 'Developer',
    }));
    expect(() => Table({ Columns: mockColumns, Items: manyItems })).not.toThrow();
  });

  it('should handle different column configurations', () => {
    const diffColumns = [
      { title: 'First Name', key: 'firstname', valueEmphasis: true },
      { title: 'Status', key: 'position', valueEmphasis: false },
    ];
    expect(() => Table({ Columns: diffColumns, Items: mockItems })).not.toThrow();
  });

  it('should render component with both Columns and Items', () => {
    const result = Table({ Columns: mockColumns, Items: mockItems });
    expect(result).toBeDefined();
  });

  it('should handle items with additional properties', () => {
    const itemsWithExtra = [
      { ...mockItems[0], department: 'Engineering', salary: 50000 },
      { ...mockItems[1], department: 'Management', salary: 60000 },
    ];
    expect(() => Table({ Columns: mockColumns, Items: itemsWithExtra })).not.toThrow();
  });

  it('should handle empty Columns array', () => {
    expect(() => Table({ Columns: [], Items: mockItems })).not.toThrow();
  });

  it('should accept valid Employee objects in Items', () => {
    const validEmployees = [
      { firstname: 'Test', lastname: 'User', mail: 'test@example.com', position: 'Engineer' },
    ];
    expect(() => Table({ Columns: mockColumns, Items: validEmployees })).not.toThrow();
  });

  it('should handle columns with required fields', () => {
    const requiredColumns = [
      { title: 'First', key: 'firstname', valueEmphasis: true },
      { title: 'Last', key: 'lastname', valueEmphasis: false },
      { title: 'Mail', key: 'mail', valueEmphasis: true },
    ];
    expect(() => Table({ Columns: requiredColumns, Items: mockItems })).not.toThrow();
  });

  it('should work with single column', () => {
    const singleColumn = [{ title: 'Name', key: 'firstname', valueEmphasis: true }];
    expect(() => Table({ Columns: singleColumn, Items: mockItems })).not.toThrow();
  });

  it('should work with many columns', () => {
    const manyColumns = Array.from({ length: 5 }, (_, i) => ({
      title: `Column ${i}`,
      key: `col${i}`,
      valueEmphasis: i % 2 === 0,
    }));
    expect(() => Table({ Columns: manyColumns, Items: mockItems })).not.toThrow();
  });

  it('should accept props without throwing errors', () => {
    expect(Table({ Columns: mockColumns, Items: mockItems })).toBeDefined();
  });
});
