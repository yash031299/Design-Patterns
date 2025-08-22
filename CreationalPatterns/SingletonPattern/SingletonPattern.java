import java.util.HashMap;
import java.util.Map;

/**
 * A Singleton class that manages a single in-memory cache for key-value pairs.
 * Ensures only one cache instance exists, providing global access to store and retrieve data.
 * Useful in scenarios like caching database results or API responses to improve performance.
 * Uses lazy initialization with double-checked locking for thread-safety.
 */
class CacheManager {
    
    // === Singleton Instance ===
    
    /** The single instance of CacheManager, initialized lazily. */
    private static volatile CacheManager instance;
    
    // === Instance Fields ===
    
    /** The in-memory cache storing key-value pairs. */
    private final Map<String, Object> cache;
    
    /** Private lock object for thread-safe initialization. */
    private static final Object lock = new Object();
    
    /**
     * Private constructor to prevent direct instantiation.
     * Initializes an empty cache.
     */
    private CacheManager() {
        this.cache = new HashMap<>();
    }
    
    /**
     * Gets the single instance of CacheManager, creating it if necessary.
     * Uses double-checked locking for thread-safe lazy initialization.
     * @return The single CacheManager instance.
     */
    public static CacheManager getInstance() {
        // First check: Avoid locking if instance already exists
        if (instance == null) {
            synchronized (lock) {
                // Second check: Ensure another thread didn't create instance while waiting
                if (instance == null) {
                    instance = new CacheManager();
                }
            }
        }
        return instance;
    }
    
    /**
     * Stores a key-value pair in the cache.
     * Overwrites the value if the key already exists.
     * @param key The cache key (e.g., "user:123").
     * @param value The value to store (e.g., user data, API response).
     */
    public void put(String key, Object value) {
        if (key == null) {
            throw new IllegalArgumentException("Cache key cannot be null");
        }
        cache.put(key, value);
    }
    
    /**
     * Retrieves a value from the cache by key.
     * @param key The cache key to look up.
     * @return The value associated with the key, or null if not found.
     */
    public Object get(String key) {
        if (key == null) {
            throw new IllegalArgumentException("Cache key cannot be null");
        }
        return cache.get(key);
    }
    
    /**
     * Clears all entries in the cache.
     */
    public void clear() {
        cache.clear();
    }
    
    /**
     * Gets the current size of the cache.
     * @return The number of key-value pairs in the cache.
     */
    public int size() {
        return cache.size();
    }
    
    /**
     * Generates a string representation of the cache contents.
     * @return A string listing all key-value pairs in the cache.
     */
    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder("Cache Contents:\n");
        if (cache.isEmpty()) {
            sb.append("  (empty)");
        } else {
            for (Map.Entry<String, Object> entry : cache.entrySet()) {
                sb.append("  Key: ").append(entry.getKey())
                  .append(", Value: ").append(entry.getValue()).append("\n");
            }
        }
        return sb.toString();
    }
}

public class SingletonPattern {
    /**
     * Demonstrates usage of the CacheManager Singleton.
     * Shows how multiple components can access the same cache instance.
     * @param args Command-line arguments (not used).
     */
    public static void main(String[] args) {
        // Get the singleton instance
        CacheManager cache1 = CacheManager.getInstance();
        
        // Store some data
        cache1.put("user:123", "Alice Smith");
        cache1.put("config:theme", "dark");
        System.out.println("Cache 1 Contents:");
        System.out.println(cache1);
        
        // Get another reference to the same instance
        CacheManager cache2 = CacheManager.getInstance();
        
        // Add more data and retrieve
        cache2.put("user:456", "Bob Jones");
        System.out.println("\nAfter adding to Cache 2:");
        System.out.println("Cache 1 Contents (same instance):");
        System.out.println(cache1); // Reflects cache2's addition
        
        // Retrieve a value
        System.out.println("\nRetrieving user:123: " + cache1.get("user:123"));
        
        // Verify same instance
        System.out.println("Is same instance? " + (cache1 == cache2)); // true
        
        // Clear cache
        cache2.clear();
        System.out.println("\nAfter clearing Cache 2:");
        System.out.println("Cache 1 Contents (same instance):");
        System.out.println(cache1); // Empty
    }
}