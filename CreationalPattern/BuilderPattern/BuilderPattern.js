/**
 * Represents an HTTP request with URL, method, headers, query parameters, and body.
 * Built using the Builder pattern for flexible and readable object creation.
 */
class HttpRequest {
  /**
   * Constructor is private-like (not directly used by client).
   * Accepts a builder object to initialize fields.
   */
  constructor(builder) {
    this.url = builder.url;                 // Request URL
    this.method = builder.method;           // HTTP method (GET, POST, etc.)
    this.headers = builder.headers;         // Headers as a key-value object
    this.queryParams = builder.queryParams; // Query parameters as key-value pairs
    this.body = builder.body;               // Optional body content
  }

  // Getters

  getUrl() {
    return this.url;
  }

  getMethod() {
    return this.method;
  }

  getHeaders() {
    return this.headers;
  }

  getQueryParams() {
    return this.queryParams;
  }

  getBody() {
    return this.body;
  }

  /**
   * Returns a human-readable string representation of the HTTP request.
   */
  toString() {
    let requestLine = `${this.method} ${this.url}`;

    // Add query parameters if present
    const queryKeys = Object.keys(this.queryParams);
    if (queryKeys.length > 0) {
      const queryString = queryKeys
        .map(key => `${key}=${this.queryParams[key]}`)
        .join("&");
      requestLine += `?${queryString}`;
    }

    let result = requestLine + "\n";

    // Add headers if any
    if (Object.keys(this.headers).length > 0) {
      result += "Headers:\n";
      for (const [key, value] of Object.entries(this.headers)) {
        result += `  ${key}: ${value}\n`;
      }
    }

    // Add body if present
    if (this.body) {
      result += `Body:\n  ${this.body}\n`;
    }

    return result;
  }

  /**
   * Static nested Builder class for constructing HttpRequest instances.
   */
  static Builder = class {
    /**
     * Initializes Builder with required fields: URL and HTTP method.
     * Throws error if required fields are missing.
     * @param {string} url 
     * @param {string} method 
     */
    constructor(url, method) {
      if (!url) throw new Error("URL is Required");
      if (!method) throw new Error("HTTP Method is Required");

      this.url = url;
      this.method = method;
      this.headers = {};         // Default empty headers object
      this.queryParams = {};     // Default empty query parameters
      this.body = null;          // Default body is null
    }

    /**
     * Adds a header to the request.
     * @param {string} key 
     * @param {string} value 
     * @returns {Builder} (for chaining)
     */
    addHeader(key, value) {
      this.headers[key] = value;
      return this;
    }

    /**
     * Adds a query parameter to the request.
     * @param {string} key 
     * @param {string} value 
     * @returns {Builder} (for chaining)
     */
    addQueryParam(key, value) {
      this.queryParams[key] = value;
      return this;
    }

    /**
     * Sets the request body.
     * @param {string} body 
     * @returns {Builder} (for chaining)
     */
    setBody(body) {
      this.body = body;
      return this;
    }

    /**
     * Builds and returns an instance of HttpRequest.
     * @returns {HttpRequest}
     */
    build() {
      return new HttpRequest(this);
    }
  }
}

// ------------------------------------------------------------------
// Demonstration of HttpRequest with Builder Pattern
// ------------------------------------------------------------------

/**
 * Example 1: Simple GET request with a query parameter
 */
const simpleGet = new HttpRequest.Builder("https://api.example.com/users", "GET")
  .addQueryParam("id", "123")
  .build();

console.log("Simple GET Request:");
console.log(simpleGet.toString());

/**
 * Example 2: Complex POST request with headers, query parameters, and JSON body
 */
const complexPost = new HttpRequest.Builder("https://api.example.com/users", "POST")
  .addHeader("Content-Type", "application/json")
  .addHeader("Authorization", "Bearer abc123")
  .addQueryParam("version", "1.0")
  .setBody(JSON.stringify({
    name: "Alice",
    email: "alice@example.com"
  }))
  .build();

console.log("\nComplex POST Request:");
console.log(complexPost.toString());
