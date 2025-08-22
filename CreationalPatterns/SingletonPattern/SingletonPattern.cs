using System;
using System.Collections.Concurrent;

namespace SingletonPattern
{
    /// <summary>
    /// A Singleton class that manages a single in-memory cache for key-value pairs.
    /// Ensures only one cache instance exists, providing global access to store and retrieve data.
    /// Useful in scenarios like caching database results or API responses to improve performance.
    /// Uses lazy initialization with thread-safety.
    /// </summary>
    public sealed class CacheManager
    {
        // === Singleton Instance ===
        
        // Lazy<T> ensures thread-safe lazy initialization
        private static readonly Lazy<CacheManager> lazyInstance = new Lazy<CacheManager>(() => new CacheManager());

        /// <summary>
        /// Gets the single instance of CacheManager.
        /// </summary>
        public static CacheManager Instance => lazyInstance.Value;

        // === Instance Fields ===

        // Thread-safe dictionary for storing cache entries
        private readonly ConcurrentDictionary<string, object> cache;

        /// <summary>
        /// Private constructor to prevent external instantiation.
        /// Initializes the cache.
        /// </summary>
        private CacheManager()
        {
            cache = new ConcurrentDictionary<string, object>();
        }

        /// <summary>
        /// Stores a key-value pair in the cache.
        /// Overwrites the value if the key already exists.
        /// </summary>
        /// <param name="key">The cache key (e.g., "user:123").</param>
        /// <param name="value">The value to store (e.g., user data, API response).</param>
        /// <exception cref="ArgumentNullException">Thrown if key is null.</exception>
        public void Put(string key, object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Cache key cannot be null");

            cache[key] = value;
        }

        /// <summary>
        /// Retrieves a value from the cache by key.
        /// </summary>
        /// <param name="key">The cache key to look up.</param>
        /// <returns>The value associated with the key, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown if key is null.</exception>
        public object Get(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Cache key cannot be null");

            cache.TryGetValue(key, out var value);
            return value;
        }

        /// <summary>
        /// Clears all entries in the cache.
        /// </summary>
        public void Clear()
        {
            cache.Clear();
        }

        /// <summary>
        /// Gets the current size of the cache.
        /// </summary>
        /// <returns>The number of key-value pairs in the cache.</returns>
        public int Size()
        {
            return cache.Count;
        }

        /// <summary>
        /// Generates a string representation of the cache contents.
        /// </summary>
        /// <returns>A string listing all key-value pairs in the cache.</returns>
        public override string ToString()
        {
            if (cache.IsEmpty)
                return "Cache Contents:\n  (empty)";

            var result = "Cache Contents:\n";
            foreach (var kvp in cache)
            {
                result += $"  Key: {kvp.Key}, Value: {kvp.Value}\n";
            }
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Get the singleton instance
            var cache1 = CacheManager.Instance;

            // Store some data
            cache1.Put("user:123", "Alice Smith");
            cache1.Put("config:theme", "dark");
            Console.WriteLine("Cache 1 Contents:");
            Console.WriteLine(cache1);

            // Get another reference to the same instance
            var cache2 = CacheManager.Instance;

            // Add more data and retrieve
            cache2.Put("user:456", "Bob Jones");
            Console.WriteLine("\nAfter adding to Cache 2:");
            Console.WriteLine("Cache 1 Contents (same instance):");
            Console.WriteLine(cache1); // Reflects cache2's addition

            // Retrieve a value
            Console.WriteLine("\nRetrieving user:123: " + cache1.Get("user:123"));

            // Verify same instance
            Console.WriteLine("Is same instance? " + (ReferenceEquals(cache1, cache2))); // true

            // Clear cache
            cache2.Clear();
            Console.WriteLine("\nAfter clearing Cache 2:");
            Console.WriteLine("Cache 1 Contents (same instance):");
            Console.WriteLine(cache1); // Empty
        }
    }
}
