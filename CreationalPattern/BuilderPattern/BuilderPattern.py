from typing import Dict, Optional


class HttpRequest:
    """
    Represents an HTTP request with URL, method, headers, query parameters, and body.
    Uses the Builder pattern to allow flexible request creation.
    """

    def __init__(self, builder: 'HttpRequest.Builder'):
        # Member variables representing parts of an HTTP request
        self._url: str = builder._url                      # Request URL
        self._method: str = builder._method                # HTTP method (GET, POST, etc.)
        self._headers: Dict[str, str] = builder._headers   # HTTP headers
        self._query_params: Dict[str, str] = builder._query_params  # URL query parameters
        self._body: Optional[str] = builder._body          # Request body (for POST, PUT, etc.)

    # Public getter methods to access fields

    @property
    def url(self) -> str:
        return self._url

    @property
    def method(self) -> str:
        return self._method

    @property
    def headers(self) -> Dict[str, str]:
        return self._headers

    @property
    def query_params(self) -> Dict[str, str]:
        return self._query_params

    @property
    def body(self) -> Optional[str]:
        return self._body

    class Builder:
        """
        Builder class for constructing HttpRequest objects in a flexible and readable way.
        """

        def __init__(self, url: str, method: str):
            """
            Constructor for Builder, requiring at minimum a URL and HTTP method.

            :param url:    the request URL
            :param method: the HTTP method (e.g., GET, POST)
            """
            if url is None:
                raise ValueError("URL is Required")
            elif method is None:
                raise ValueError("HTTP Method is Required")

            self._url: str = url
            self._method: str = method
            self._headers: Dict[str, str] = {}
            self._query_params: Dict[str, str] = {}
            self._body: Optional[str] = None

        def add_header(self, key: str, value: str) -> 'HttpRequest.Builder':
            """
            Adds an HTTP header to the request.

            :param key:   Header name
            :param value: Header value
            :return: Builder (for method chaining)
            """
            self._headers[key] = value
            return self

        def add_query_param(self, key: str, value: str) -> 'HttpRequest.Builder':
            """
            Adds a query parameter to the request URL.

            :param key:   Parameter name
            :param value: Parameter value
            :return: Builder (for method chaining)
            """
            self._query_params[key] = value
            return self

        def body(self, body: str) -> 'HttpRequest.Builder':
            """
            Adds a body to the request (usually used in POST/PUT requests).

            :param body: The request body content
            :return: Builder (for method chaining)
            """
            self._body = body
            return self

        def build(self) -> 'HttpRequest':
            """
            Builds and returns an HttpRequest instance.

            :return: HttpRequest object
            """
            return HttpRequest(self)

    def __str__(self) -> str:
        """
        Returns a human-readable string representation of the HttpRequest.
        Includes method, URL, query parameters, headers, and body.
        """
        parts = [f"{self.method} {self.url}"]

        # Append query parameters, if any
        if self.query_params:
            query = '&'.join(f"{k}={v}" for k, v in self.query_params.items())
            parts[0] += f"?{query}"

        # Append headers, if any
        if self.headers:
            parts.append("Headers:")
            for k, v in self.headers.items():
                parts.append(f"  {k}: {v}")

        # Append body, if present
        if self.body:
            parts.append("Body:")
            parts.append(f"  {self.body}")

        return '\n'.join(parts)


# Demonstrates usage of the HttpRequest class by building example requests.
def main():
    # Example 1: Simple GET request with a query parameter
    simple_get = HttpRequest.Builder("https://api.example.com/users", "GET") \
        .add_query_param("id", "123") \
        .build()

    print("Simple GET Request:")
    print(simple_get)

    # Example 2: Complex POST request with headers, query parameters, and a JSON body
    complex_post = HttpRequest.Builder("https://api.example.com/users", "POST") \
        .add_header("Content-Type", "application/json") \
        .add_header("Authorization", "Bearer abc123") \
        .add_query_param("version", "1.0") \
        .body('{"name": "Alice", "email": "alice@example.com"}') \
        .build()

    print("\nComplex POST Request:")
    print(complex_post)


if __name__ == "__main__":
    main()
