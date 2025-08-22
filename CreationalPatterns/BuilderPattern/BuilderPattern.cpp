#include <iostream>
#include <string>
#include <map>
#include <vector>
#include <sstream>

/**
 * Represents an HTTP request with URL, method, headers, query parameters, and body.
 * Built using the Builder design pattern for clean and flexible creation.
 */
class HttpRequest {
private:
    // Member variables
    std::string url;
    std::string method;
    std::map<std::string, std::string> headers;
    std::map<std::string, std::string> queryParams;
    std::string body;

    // Private constructor, only accessible from Builder
    HttpRequest(const std::string& url,
                const std::string& method,
                const std::map<std::string, std::string>& headers,
                const std::map<std::string, std::string>& queryParams,
                const std::string& body)
        : url(url), method(method), headers(headers), queryParams(queryParams), body(body) {}

public:
    // Getters
    const std::string& getUrl() const { return url; }
    const std::string& getMethod() const { return method; }
    const std::map<std::string, std::string>& getHeaders() const { return headers; }
    const std::map<std::string, std::string>& getQueryParams() const { return queryParams; }
    const std::string& getBody() const { return body; }

    /**
     * Builder class for HttpRequest.
     */
    class Builder {
    private:
        std::string url;
        std::string method;
        std::map<std::string, std::string> headers;
        std::map<std::string, std::string> queryParams;
        std::string body;

    public:
        Builder(const std::string& url, const std::string& method) {
            if (url.empty()) {
                throw std::invalid_argument("URL is Required");
            }
            if (method.empty()) {
                throw std::invalid_argument("HTTP Method is Required");
            }
            this->url = url;
            this->method = method;
        }

        Builder& addHeader(const std::string& key, const std::string& value) {
            headers[key] = value;
            return *this;
        }

        Builder& addQueryParam(const std::string& key, const std::string& value) {
            queryParams[key] = value;
            return *this;
        }

        Builder& setBody(const std::string& bodyContent) {
            body = bodyContent;
            return *this;
        }

        HttpRequest build() const {
            return HttpRequest(url, method, headers, queryParams, body);
        }
    };

    /**
     * Returns a human-readable representation of the HTTP request.
     */
    std::string toString() const {
        std::ostringstream ss;
        ss << method << " " << url;

        // Add query parameters if present
        if (!queryParams.empty()) {
            ss << "?";
            bool first = true;
            for (const auto& param : queryParams) {
                if (!first) ss << "&";
                ss << param.first << "=" << param.second;
                first = false;
            }
        }

        ss << "\n";

        // Add headers
        if (!headers.empty()) {
            ss << "Headers:\n";
            for (const auto& header : headers) {
                ss << "  " << header.first << ": " << header.second << "\n";
            }
        }

        // Add body
        if (!body.empty()) {
            ss << "Body:\n  " << body << "\n";
        }

        return ss.str();
    }
};

/**
 * Demonstrates usage of the HttpRequest and Builder in C++
 */
int main() {
    try {
        // Example 1: Simple GET request
        HttpRequest simpleGet = HttpRequest::Builder("https://api.example.com/users", "GET")
                                    .addQueryParam("id", "123")
                                    .build();

        std::cout << "Simple GET Request:\n" << simpleGet.toString() << "\n";

        // Example 2: Complex POST request with headers and body
        HttpRequest complexPost = HttpRequest::Builder("https://api.example.com/users", "POST")
                                      .addHeader("Content-Type", "application/json")
                                      .addHeader("Authorization", "Bearer abc123")
                                      .addQueryParam("version", "1.0")
                                      .setBody("{\"name\": \"Alice\", \"email\": \"alice@example.com\"}")
                                      .build();

        std::cout << "\nComplex POST Request:\n" << complexPost.toString() << "\n";
    } catch (const std::exception& ex) {
        std::cerr << "Error: " << ex.what() << "\n";
    }

    return 0;
}
