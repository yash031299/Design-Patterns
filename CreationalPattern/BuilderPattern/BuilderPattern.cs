using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

/// <summary>
/// Builder for constructing an HttpRequestMessage object in a clean and fluent way.
/// </summary>
public class HttpRequestBuilder
{
    private HttpMethod method;
    private Uri uri;
    private HttpContent content;
    private readonly Dictionary<string, string> headers = new();

    /// <summary>
    /// Sets the HTTP method (GET, POST, etc.)
    /// </summary>
    public HttpRequestBuilder SetMethod(HttpMethod method)
    {
        this.method = method;
        return this;
    }

    /// <summary>
    /// Sets the URI for the request.
    /// </summary>
    public HttpRequestBuilder SetUri(string url)
    {
        this.uri = new Uri(url);
        return this;
    }

    /// <summary>
    /// Sets the content (body) of the request.
    /// </summary>
    public HttpRequestBuilder SetContent(string body, string mediaType = "application/json")
    {
        this.content = new StringContent(body, Encoding.UTF8, mediaType);
        return this;
    }

    /// <summary>
    /// Adds a header to the request.
    /// </summary>
    public HttpRequestBuilder AddHeader(string key, string value)
    {
        headers[key] = value;
        return this;
    }

    /// <summary>
    /// Builds the HttpRequestMessage instance.
    /// </summary>
    public HttpRequestMessage Build()
    {
        var request = new HttpRequestMessage(method, uri);
        if (content != null) request.Content = content;
        foreach (var header in headers)
            request.Headers.TryAddWithoutValidation(header.Key, header.Value);

        return request;
    }
}

// Usage
public class Program
{
    public static void Main()
    {
        var request = new HttpRequestBuilder()
            .SetMethod(HttpMethod.Post)
            .SetUri("https://api.example.com/users")
            .SetContent("{\"name\":\"Alice\"}")
            .AddHeader("Authorization", "Bearer token123")
            .Build();

        Console.WriteLine(request);
    }
}
