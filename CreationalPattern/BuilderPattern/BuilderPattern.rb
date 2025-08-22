# Simple builder pattern for constructing HTTP-style requests

class HttpRequest
  attr_reader :method, :url, :headers, :params, :body

  def initialize(method:, url:, headers: {}, params: {}, body: nil)
    @method = method
    @url = url
    @headers = headers
    @params = params
    @body = body
  end

  def to_s
    param_string = params.map { |k, v| "#{k}=#{v}" }.join("&")
    full_url = param_string.empty? ? url : "#{url}?#{param_string}"
    <<~REQ
    #{method} #{full_url}
    Headers:
    #{headers.map { |k, v| "  #{k}: #{v}" }.join("\n")}
    Body:
      #{body}
    REQ
  end
end

# Builder class
class HttpRequestBuilder
  def initialize(method, url)
    @method = method
    @url = url
    @headers = {}
    @params = {}
    @body = nil
  end

  def add_header(key, value)
    @headers[key] = value
    self
  end

  def add_param(key, value)
    @params[key] = value
    self
  end

  def set_body(body)
    @body = body
    self
  end

  def build
    HttpRequest.new(method: @method, url: @url, headers: @headers, params: @params, body: @body)
  end
end

# Usage
request = HttpRequestBuilder.new("POST", "https://api.example.com/users")
            .add_header("Authorization", "Bearer abc123")
            .add_param("version", "1.0")
            .set_body("{\"name\":\"Alice\"}")
            .build

puts request
