# A Singleton class that manages a single in-memory cache for key-value pairs.
# Ensures only one cache instance exists, providing global access to store and retrieve data.
# Useful in scenarios like caching database results or API responses to improve performance.
# Uses Ruby's built-in Singleton module for thread-safe lazy initialization.

require 'singleton'

class CacheManager
  include Singleton

  # Initializes an empty cache
  def initialize
    @cache = {}
    @mutex = Mutex.new  # For thread safety on cache operations
  end

  # Stores a key-value pair in the cache.
  # Overwrites the value if the key already exists.
  # @param key [String] The cache key (e.g., "user:123").
  # @param value [Object] The value to store (e.g., user data, API response).
  # @raise [ArgumentError] if key is nil
  def put(key, value)
    raise ArgumentError, 'Cache key cannot be nil' if key.nil?

    @mutex.synchronize do
      @cache[key] = value
    end
  end

  # Retrieves a value from the cache by key.
  # @param key [String] The cache key to look up.
  # @return [Object, nil] The value associated with the key, or nil if not found.
  # @raise [ArgumentError] if key is nil
  def get(key)
    raise ArgumentError, 'Cache key cannot be nil' if key.nil?

    @mutex.synchronize do
      @cache[key]
    end
  end

  # Clears all entries in the cache.
  def clear
    @mutex.synchronize do
      @cache.clear
    end
  end

  # Gets the current size of the cache.
  # @return [Integer] The number of key-value pairs in the cache.
  def size
    @mutex.synchronize do
      @cache.size
    end
  end

  # Generates a string representation of the cache contents.
  # @return [String] A string listing all key-value pairs in the cache.
  def to_s
    @mutex.synchronize do
      if @cache.empty?
        "Cache Contents:\n  (empty)"
      else
        result = "Cache Contents:\n"
        @cache.each do |key, value|
          result += "  Key: #{key}, Value: #{value}\n"
        end
        result
      end
    end
  end
end

# Demonstration
if __FILE__ == $0
  cache1 = CacheManager.instance

  # Store some data
  cache1.put("user:123", "Alice Smith")
  cache1.put("config:theme", "dark")
  puts "Cache 1 Contents:"
  puts cache1.to_s

  cache2 = CacheManager.instance

  # Add more data and retrieve
  cache2.put("user:456", "Bob Jones")
  puts "\nAfter adding to Cache 2:"
  puts "Cache 1 Contents (same instance):"
  puts cache1.to_s  # Reflects cache2's addition

  # Retrieve a value
  puts "\nRetrieving user:123: #{cache1.get("user:123")}"

  # Verify same instance
  puts "Is same instance? #{cache1.equal?(cache2)}"  # true

  # Clear cache
  cache2.clear
  puts "\nAfter clearing Cache 2:"
  puts "Cache 1 Contents (same instance):"
  puts cache1.to_s  # Empty
end
