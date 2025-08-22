import java.util.Map;
import java.util.HashMap;
import java.util.List;
import java.util.ArrayList;

/**
 * Represents an HTTP request with URL, method, headers, query parameters, and body.
 * Uses the Builder pattern to allow flexible request creation.
 */
class HttpRequest {

    // Member variables representing parts of an HTTP request
    private final String url;                         // Request URL
    private final String method;                      // HTTP method (GET, POST, etc.)
    private final Map<String, String> headers;        // HTTP headers
    private final Map<String, String> queryParams;    // URL query parameters
    private final String body;                        // Request body (for POST, PUT, etc.)

    /**
     * Private constructor. Instances can only be created via the Builder.
     */
    private HttpRequest(Builder builder) {
        this.url = builder.url;
        this.method = builder.method;
        this.headers = builder.headers;
        this.queryParams = builder.queryParams;
        this.body = builder.body;
    }

    // Public getter methods to access fields

    public String getUrl() {
        return this.url;
    }

    public String getMethod() {
        return this.method;
    }

    public Map<String, String> getHeaders() {
        return this.headers;
    }

    public Map<String, String> getQueryParams() {
        return this.queryParams;
    }

    public String getBody() {
        return this.body;
    }

    /**
     * Builder class for constructing HttpRequest objects in a flexible and readable way.
     */
    public static class Builder {
        private String url;
        private String method;
        private Map<String, String> headers = new HashMap<>();
        private Map<String, String> queryParams = new HashMap<>();
        private String body = null;

        /**
         * Constructor for Builder, requiring at minimum a URL and HTTP method.
         *
         * @param url    the request URL
         * @param method the HTTP method (e.g., GET, POST)
         */
        public Builder(String url, String method) {
            if (url == null) {
                throw new IllegalArgumentException("URL is Required");
            } else if (method == null) {
                throw new IllegalArgumentException("HTTP Method is Required");
            }
            this.url = url;
            this.method = method;
        }

        /**
         * Adds an HTTP header to the request.
         *
         * @param key   Header name
         * @param value Header value
         * @return Builder (for method chaining)
         */
        public Builder addHeader(String key, String value) {
            this.headers.put(key, value);
            return this;
        }

        /**
         * Adds a query parameter to the request URL.
         *
         * @param key   Parameter name
         * @param value Parameter value
         * @return Builder (for method chaining)
         */
        public Builder addQueryParam(String key, String value) {
            this.queryParams.put(key, value);
            return this;
        }

        /**
         * Adds a body to the request (usually used in POST/PUT requests).
         *
         * @param body The request body content
         * @return Builder (for method chaining)
         */
        public Builder body(String body) {
            this.body = body;
            return this;
        }

        /**
         * Builds and returns an HttpRequest instance.
         *
         * @return HttpRequest object
         */
        public HttpRequest build() {
            return new HttpRequest(this);
        }
    }

    /**
     * Returns a human-readable string representation of the HttpRequest.
     * Includes method, URL, query parameters, headers, and body.
     */
    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder();
        sb.append(method).append(" ").append(url);

        // Append query parameters, if any
        if (!queryParams.isEmpty()) {
            sb.append("?");
            List<String> params = new ArrayList<>();
            for (Map.Entry<String, String> param : queryParams.entrySet()) {
                params.add(param.getKey() + "=" + param.getValue());
            }
            sb.append(String.join("&", params));
        }

        sb.append("\n");

        // Append headers, if any
        if (!headers.isEmpty()) {
            sb.append("Headers:\n");
            for (Map.Entry<String, String> header : headers.entrySet()) {
                sb.append("  ").append(header.getKey()).append(": ").append(header.getValue()).append("\n");
            }
        }

        // Append body, if present
        if (body != null) {
            sb.append("Body:\n  ").append(body).append("\n");
        }

        return sb.toString();
    }
}

/**
 * Demonstrates usage of the HttpRequest class by building example requests.
 */
public class BuilderPattern {
    public static void main(String[] args) {

        // Example 1: Simple GET request with a query parameter
        HttpRequest simpleGet = new HttpRequest.Builder("https://api.example.com/users", "GET")
                .addQueryParam("id", "123")
                .build();

        System.out.println("Simple GET Request:");
        System.out.println(simpleGet);
        

        // Example 2: Complex POST request with headers, query parameters, and a JSON body
        HttpRequest complexPost = new HttpRequest.Builder("https://api.example.com/users", "POST")
                .addHeader("Content-Type", "application/json")
                .addHeader("Authorization", "Bearer abc123")
                .addQueryParam("version", "1.0")
                .body("{\"name\": \"Alice\", \"email\": \"alice@example.com\"}")
                .build();

        System.out.println("\nComplex POST Request:");
        System.out.println(complexPost);
    }
}
