using System;
using System.Collections.Generic;
using System.Text;

/**
 * Represents an HTTP request with URL, method, headers, query parameters, and body.
 * Uses the Builder pattern to allow flexible request creation.
 */
class HttpRequest
{
    // Member variables representing parts of an HTTP request
    private readonly string url;                          // Request URL
    private readonly string method;                       // HTTP method (GET, POST, etc.)
    private readonly Dictionary<string, string> headers;  // HTTP headers
    private readonly Dictionary<string, string> queryParams; // URL query parameters
    private readonly string body;                         // Request body (for POST, PUT, etc.)

    /**
     * Private constructor. Instances can only be created via the Builder.
     */
    private HttpRequest(Builder builder)
    {
        this.url = builder.Url;
        this.method = builder.Method;
        this.headers = builder.Headers;
        this.queryParams = builder.QueryParams;
        this.body = builder.Body;
    }

    // Public getter methods to access fields

    public string Url => this.url;

    public string Method => this.method;

    public Dictionary<string, string> Headers => this.headers;

    public Dictionary<string, string> QueryParams => this.queryParams;

    public string Body => this.body;

    /**
     * Builder class for constructing HttpRequest objects in a flexible and readable way.
     */
    public class Builder
    {
        public string Url { get; }
        public string Method { get; }

        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();
        public Dictionary<string, string> QueryParams { get; } = new Dictionary<string, string>();
        public string Body { get; private set; } = null;

        /**
         * Constructor for Builder, requiring at minimum a URL and HTTP method.
         *
         * @param url    the request URL
         * @param method the HTTP method (e.g., GET, POST)
         */
        public Builder(string url, string method)
        {
            if (url == null)
            {
                throw new ArgumentException("URL is Required");
            }
            else if (method == null)
            {
                throw new ArgumentException("HTTP Method is Required");
            }

            this.Url = url;
            this.Method = method;
        }

        /**
         * Adds an HTTP header to the request.
         *
         * @param key   Header name
         * @param value Header value
         * @return Builder (for method chaining)
         */
        public Builder AddHeader(string key, string value)
        {
            this.Headers[key] = value;
            return this;
        }

        /**
         * Adds a query parameter to the request URL.
         *
         * @param key   Parameter name
         * @param value Parameter value
         * @return Builder (for method chaining)
         */
        public Builder AddQueryParam(string key, string value)
        {
            this.QueryParams[key] = value;
            return this;
        }

        /**
         * Adds a body to the request (usually used in POST/PUT requests).
         *
         * @param body The request body content
         * @return Builder (for method chaining)
         */
        public Builder SetBody(string body)
        {
            this.Body = body;
            return this;
        }

        /**
         * Builds and returns an HttpRequest instance.
         *
         * @return HttpRequest object
         */
        public HttpRequest Build()
        {
            return new HttpRequest(this);
        }
    }

    /**
     * Returns a human-readable string representation of the HttpRequest.
     * Includes method, URL, query parameters, headers, and body.
     */
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(Method).Append(" ").Append(Url);

        // Append query parameters, if any
        if (QueryParams.Count > 0)
        {
            sb.Append("?");
            List<string> paramsList = new List<string>();
            foreach (var param in QueryParams)
            {
                paramsList.Add($"{param.Key}={param.Value}");
            }
            sb.Append(string.Join("&", paramsList));
        }

        sb.AppendLine();

        // Append headers, if any
        if (Headers.Count > 0)
        {
            sb.AppendLine("Headers:");
            foreach (var header in Headers)
            {
                sb.AppendLine($"  {header.Key}: {header.Value}");
            }
        }

        // Append body, if present
        if (Body != null)
        {
            sb.AppendLine("Body:");
            sb.AppendLine($"  {Body}");
        }

        return sb.ToString();
    }
}


/**
 * Demonstrates usage of the HttpRequest class by building example requests.
 */
public class BuilderPattern
{
    public static void Main(string[] args)
    {
        // Example 1: Simple GET request with a query parameter
        HttpRequest simpleGet = new HttpRequest.Builder("https://api.example.com/users", "GET")
            .AddQueryParam("id", "123")
            .Build();

        Console.WriteLine("Simple GET Request:");
        Console.WriteLine(simpleGet);

        // Example 2: Complex POST request with headers, query parameters, and a JSON body
        HttpRequest complexPost = new HttpRequest.Builder("https://api.example.com/users", "POST")
            .AddHeader("Content-Type", "application/json")
            .AddHeader("Authorization", "Bearer abc123")
            .AddQueryParam("version", "1.0")
            .SetBody("{\"name\": \"Alice\", \"email\": \"alice@example.com\"}")
            .Build();

        Console.WriteLine("\nComplex POST Request:");
        Console.WriteLine(complexPost);
    }
}
