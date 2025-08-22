# frozen_string_literal: true

##
# Represents an HTTP request with URL, method, headers, query parameters, and body.
# Uses the Builder pattern to allow flexible request creation.
class HttpRequest
  # Member variables representing parts of an HTTP request
  attr_reader :url, :method, :headers, :query_params, :body

  ##
  # Private constructor. Instances can only be created via the Builder.
  def initialize(builder)
    @url = builder.url                         # Request URL
    @method = builder.method                   # HTTP method (GET, POST, etc.)
    @headers = builder.headers                 # HTTP headers
    @query_params = builder.query_params       # URL query parameters
    @body = builder.body                       # Request body (for POST, PUT, etc.)
  end

  ##
  # Builder class for constructing HttpRequest objects in a flexible and readable way.
  class Builder
    attr_reader :url, :method, :headers, :query_params, :body

    ##
    # Constructor for Builder, requiring at minimum a URL and HTTP method.
    #
    # @param url    the request URL
    # @param method the HTTP method (e.g., GET, POST)
    def initialize(url, method)
      raise ArgumentError, 'URL is Required' if url.nil?
      raise ArgumentError, 'HTTP Method is Required' if method.nil?

      @url = url
      @method = method
      @headers = {}
      @query_params = {}
      @body = nil
    end

    ##
    # Adds an HTTP header to the request.
    #
    # @param key   Header name
    # @param value Header value
    # @return Builder (for method chaining)
    def add_header(key, value)
      @headers[key] = value
      self
    end

    ##
    # Adds a query parameter to the request URL.
    #
    # @param key   Parameter name
    # @param value Parameter value
    # @return Builder (for method chaining)
    def add_query_param(key, value)
      @query_params[key] = value
      self
    end

    ##
    # Adds a body to the request (usually used in POST/PUT requests).
    #
    # @param body The request body content
    # @return Builder (for method chaining)
    def set_body(body)
      @body = body
      self
    end

    ##
    # Builds and returns an HttpRequest instance.
    #
    # @return HttpRequest object
    def build
      HttpRequest.new(self)
    end
  end

  ##
  # Returns a human-readable string representation of the HttpRequest.
  # Includes method, URL, query parameters, headers, and body.
  def to_s
    sb = +"#{@method} #{@url}"

    # Append query parameters, if any
    unless @query_params.empty?
      query = @query_params.map { |k, v| "#{k}=#{v}" }.join('&')
      sb << "?#{query}"
    end

    sb << "\n"

    # Append headers, if any
    unless @headers.empty?
      sb << "Headers:\n"
      @headers.each { |k, v| sb << "  #{k}: #{v}\n" }
    end

    # Append body, if present
    if @body
      sb << "Body:\n  #{@body}\n"
    end

    sb
  end
end

##
# Demonstrates usage of the HttpRequest class by building example requests.
class BuilderPattern
  def self.main
    # Example 1: Simple GET request with a query parameter
    simple_get = HttpRequest::Builder.new('https://api.example.com/users', 'GET')
                                     .add_query_param('id', '123')
                                     .build

    puts 'Simple GET Request:'
    puts simple_get

    # Example 2: Complex POST request with headers, query parameters, and a JSON body
    complex_post = HttpRequest::Builder.new('https://api.example.com/users', 'POST')
                                       .add_header('Content-Type', 'application/json')
                                       .add_header('Authorization', 'Bearer abc123')
                                       .add_query_param('version', '1.0')
                                       .set_body('{"name": "Alice", "email": "alice@example.com"}')
                                       .build

    puts "\nComplex POST Request:"
    puts complex_post
  end
end

# Run the demonstration
BuilderPattern.main
