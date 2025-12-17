const API_BASE_URL = import.meta.env.VITE_API_URL || '/api';

interface RequestOptions {
  method?: 'GET' | 'POST' | 'PUT' | 'DELETE' | 'PATCH';
  body?: unknown;
  headers?: Record<string, string>;
}

class ApiService {
  private baseUrl: string;

  constructor(baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  private async request<T>(endpoint: string, options: RequestOptions = {}): Promise<T> {
    const { method = 'GET', body, headers = {} } = options;

    const config: RequestInit = {
      method,
      headers: {
        'Content-Type': 'application/json',
        ...headers,
      },
    };

    if (body) {
      config.body = JSON.stringify(body);
    }

    const response = await fetch(`${this.baseUrl}${endpoint}`, config);

    if (!response.ok) {
      return await this.handleErrorResponse<T>(response);
    }

    return await this.handleSuccessResponse<T>(response);
  }

  // Parse response body as JSON when possible, otherwise return raw text, or undefined when empty
  private async parseBody(response: Response): Promise<unknown> {
    try {
      const text = await response.text();
      if (!text || text.trim().length === 0) return undefined;
      try {
        return JSON.parse(text);
      } catch {
        return text;
      }
    } catch {
      return undefined;
    }
  }


  private async handleSuccessResponse<T>(response: Response): Promise<T> {
    if (response.status === 204) return {} as T;
    const parsed = await this.parseBody(response);
    return parsed as T;
  }
  private async handleErrorResponse<T>(response: Response): Promise<T> {
    const parsed = await this.parseBody(response);
    throw new ApiError(response.status, response.statusText, parsed);
  }

  async get<T>(endpoint: string): Promise<T> {
    return this.request<T>(endpoint, { method: 'GET' });
  }

  async post<T>(endpoint: string, data: unknown): Promise<T> {
    return this.request<T>(endpoint, { method: 'POST', body: data });
  }

  async patch<T>(endpoint: string, data: unknown): Promise<T> {
    return this.request<T>(endpoint, { method: 'PATCH', body: data });
  }

  async delete<T>(endpoint: string): Promise<T> {
    return this.request<T>(endpoint, { method: 'DELETE' });
  }
}

export const api = new ApiService(API_BASE_URL);
export default ApiService;

// Error wrapper that carries response status and parsed body (if any)
export class ApiError extends Error {
  status: number;
  statusText: string;
  body?: unknown;

  constructor(status: number, statusText: string, body?: unknown) {
    super(`API Error: ${status} ${statusText}`);
    this.status = status;
    this.statusText = statusText;
    this.body = body;
    Object.setPrototypeOf(this, ApiError.prototype);
  }
}
