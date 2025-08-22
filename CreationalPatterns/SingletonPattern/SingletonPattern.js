/**
 * A Singleton class that manages a single in-memory cache for key-value pairs.
 * Ensures only one cache instance exists, providing global access to store and retrieve data.
 * Useful in scenarios like caching database results or API responses to improve performance.
 * Uses lazy initialization.
 */
class CacheManager {
  // === Singleton Instance ===

  static #instance = null;

  /**
   * Gets the single instance of CacheManager, creating it if necessary.
   * @returns {CacheManager} The single CacheManager instance.
   */
  static getInstance() {
    if (!CacheManager.#instance) {
      CacheManager.#instance = new CacheManager();
    }
    return CacheManager.#instance;
  }

  // === Instance Fields ===

  #cache;

  /**
   * Private constructor to prevent direct instantiation.
   * Initializes an empty cache.
   */
  constructor() {
    if (CacheManager.#instance) {
      throw new Error('Use CacheManager.getInstance() to get the singleton instance.');
    }
    this.#cache = new Map();
  }

  /**
   * Stores a key-value pair in the cache.
   * Overwrites the value if the key already exists.
   * @param {string} key - The cache key (e.g., "user:123").
   * @param {*} value - The value to store (e.g., user data, API response).
   * @throws {Error} If key is null or undefined.
   */
  put(key, value) {
    if (key == null) {
      throw new Error('Cache key cannot be null or undefined');
    }
    this.#cache.set(key, value);
  }

  /**
   * Retrieves a value from the cache by key.
   * @param {string} key - The cache key to look up.
   * @returns {*} The value associated with the key, or undefined if not found.
   * @throws {Error} If key is null or undefined.
   */
  get(key) {
    if (key == null) {
      throw new Error('Cache key cannot be null or undefined');
    }
    return this.#cache.get(key);
  }

  /**
   * Clears all entries in the cache.
   */
  clear() {
    this.#cache.clear();
  }

  /**
   * Gets the current size of the cache.
   * @returns {number} The number of key-value pairs in the cache.
   */
  size() {
    return this.#cache.size;
  }

  /**
   * Generates a string representation of the cache contents.
   * @returns {string} A string listing all key-value pairs in the cache.
   */
  toString() {
    if (this.#cache.size === 0) {
      return 'Cache Contents:\n  (empty)';
    }
    let result = 'Cache Contents:\n';
    for (const [key, value] of this.#cache.entries()) {
      result += `  Key: ${key}, Value: ${value}\n`;
    }
    return result;
  }
}

CacheManager.instance = undefined;

// Demonstration
function main() {
  const cache1 = CacheManager.getInstance();

  // Store some data
  cache1.put('user:123', 'Alice Smith');
  cache1.put('config:theme', 'dark');
  console.log('Cache 1 Contents:');
  console.log(cache1.toString());

  const cache2 = CacheManager.getInstance();

  // Add more data and retrieve
  cache2.put('user:456', 'Bob Jones');
  console.log('\nAfter adding to Cache 2:');
  console.log('Cache 1 Contents (same instance):');
  console.log(cache1.toString()); // Reflects cache2's addition

  // Retrieve a value
  console.log('\nRetrieving user:123:', cache1.get('user:123'));

  // Verify same instance
  console.log('Is same instance?', cache1 === cache2); // true

  // Clear cache
  cache2.clear();
  console.log('\nAfter clearing Cache 2:');
  console.log('Cache 1 Contents (same instance):');
  console.log(cache1.toString()); // Empty
}

main();