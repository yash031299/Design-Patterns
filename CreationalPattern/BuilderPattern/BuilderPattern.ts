/**
 * Interface for builder pattern — defines structure for HttpRequest.
 */
interface IHttpRequest {
  method: string;
  url: string;
  headers: Record<string, string>;
  queryParams: Record<string, string>;
  body: string | null;
}

/**
 * HttpRequest class constructed via the HttpRequestBuilder.
 */
class HttpRequest implements IHttpRequest {
  public method: string;
  public url: string;
  public headers: Record<string, string>;
  public queryParams: Record<string, string>;
  public body: string | null;

  constructor(builder: HttpRequestBuilder) {
    this.method = builder.method;
    this.url = builder.url;
    this.headers = builder.headers;
    this.queryParams = builder.queryParams;
    this.body = builder.body;
  }

  /**
   * Returns a formatted string representation of the request.
   */
  public toString(): string {
    let fullUrl = this.url;
    const queryKeys = Object.keys(this.queryParams);

    if (queryKeys.length > 0) {
      const queryString = queryKeys
        .map(key => `${key}=${this.queryParams[key]}`)
        .join("&");
      fullUrl += `?${queryString}`;
    }

    const headersFormatted = Object.entries(this.headers)
      .map(([k, v]) => `  ${k}: ${v}`)
      .join("\n");

    return `${this.method} ${fullUrl}
Headers:
${headersFormatted}
Body:
  ${this.body ?? ""}`;
  }
}

/**
 * Builder class for creating HttpRequest instances.
 */
class HttpRequestBuilder {
  public method: string;
  public url: string;
  public headers: Record<string, string> = {};
  public queryParams: Record<string, string> = {};
  public body: string | null = null;

  /**
   * Initializes with mandatory fields.
   * @param method HTTP method (GET, POST, etc.)
   * @param url Request URL
   */
  constructor(method: string, url: string) {
    if (!method) throw new Error("HTTP method is required.");
    if (!url) throw new Error("URL is required.");
    this.method = method;
    this.url = url;
  }

  /**
   * Adds an HTTP header.
   */
  public addHeader(key: string, value: string): this {
    this.headers[key] = value;
    return this;
  }

  /**
   * Adds a query parameter to the request.
   */
  public addQueryParam(key: string, value: string): this {
    this.queryParams[key] = value;
    return this;
  }

  /**
   * Sets the request body.
   */
  public setBody(body: string): this {
    this.body = body;
    return this;
  }

  /**
   * Builds the HttpRequest instance.
   */
  public build(): HttpRequest {
    return new HttpRequest(this);
  }
}

// -------------------------------------------
// Example Usage
// -------------------------------------------

// Simple GET Request
const getRequest = new HttpRequestBuilder("GET", "https://api.example.com/users")
  .addQueryParam("id", "123")
  .build();

console.log("Simple GET Request:");
console.log(getRequest.toString());

// Complex POST Request
const postRequest = new HttpRequestBuilder("POST", "https://api.example.com/users")
  .addHeader("Content-Type", "application/json")
  .addHeader("Authorization", "Bearer abc123")
  .addQueryParam("version", "1.0")
  .setBody(JSON.stringify({ name: "Alice", email: "alice@example.com" }))
  .build();

console.log("\nComplex POST Request:");
console.log(postRequest.toString());
