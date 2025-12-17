import { cn } from './styleutil';

describe('styleutil - cn function', () => {
  it('should merge tailwind classes correctly', () => {
    const result = cn('px-2', 'py-1');
    expect(result).toContain('px-2');
    expect(result).toContain('py-1');
  });

  it('should handle conflicting tailwind classes with merge', () => {
    const result = cn('px-2', 'px-4');
    expect(result).toContain('px-4');
    expect(result).not.toContain('px-2');
  });

  it('should handle padding conflicts', () => {
    const result = cn('p-2', 'p-4');
    expect(result).toContain('p-4');
  });

  it('should handle display conflicts', () => {
    const result = cn('flex', 'block');
    expect(result).toContain('block');
  });

  it('should handle margin conflicts', () => {
    const result = cn('m-2', 'm-8');
    expect(result).toContain('m-8');
  });

  it('should remove falsy values', () => {
    const result = cn('px-2', false, 'py-1', null, undefined);
    expect(result).toContain('px-2');
    expect(result).toContain('py-1');
  });

  it('should handle empty string', () => {
    const result = cn('', 'px-2');
    expect(result).toContain('px-2');
  });

  it('should handle multiple classes with array', () => {
    const result = cn(['px-2', 'py-1']);
    expect(result).toContain('px-2');
    expect(result).toContain('py-1');
  });

  it('should handle mixed strings and arrays', () => {
    const result = cn('px-2', ['py-1', 'text-black']);
    expect(result).toContain('px-2');
    expect(result).toContain('py-1');
    expect(result).toContain('text-black');
  });

  it('should handle color conflicts', () => {
    const result = cn('text-red-500', 'text-blue-500');
    expect(result).toContain('text-blue-500');
  });

  it('should handle responsive classes', () => {
    const result = cn('md:px-4', 'lg:px-8');
    expect(result).toContain('md:px-4');
    expect(result).toContain('lg:px-8');
  });

  it('should handle hover states', () => {
    const result = cn('hover:bg-gray-100', 'hover:text-black');
    expect(result).toContain('hover:bg-gray-100');
    expect(result).toContain('hover:text-black');
  });

  it('should merge all classes without duplication', () => {
    const result = cn('text-lg', 'font-bold', 'text-center', 'bg-white');
    expect(result).toContain('text-lg');
    expect(result).toContain('font-bold');
    expect(result).toContain('text-center');
    expect(result).toContain('bg-white');
  });

  it('should handle complex class combinations', () => {
    const result = cn(
      'flex items-center justify-between',
      'px-4 py-2',
      'bg-white rounded-lg',
      'shadow-md'
    );
    expect(result).toBeTruthy();
    expect(result.length).toBeGreaterThan(0);
  });

  it('should handle dark mode classes', () => {
    const result = cn('dark:bg-gray-900', 'dark:text-white');
    expect(result).toContain('dark:bg-gray-900');
    expect(result).toContain('dark:text-white');
  });
});
