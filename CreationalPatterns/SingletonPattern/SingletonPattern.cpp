#include <iostream>
#include <unordered_map>
#include <string>
#include <mutex>

/**
 * A Singleton class that manages a single in-memory cache for key-value pairs.
 * Ensures only one cache instance exists, providing global access to store and retrieve data.
 * Useful in scenarios like caching database results or API responses to improve performance.
 * Uses lazy initialization with thread-safe std::call_once.
 */
class CacheManager {
public:
    // Delete copy constructor and assignment operator to prevent copies
    CacheManager(const CacheManager&) = delete;
    CacheManager& operator=(const CacheManager&) = delete;

    /**
     * Gets the single instance of CacheManager, creating it if necessary.
     * Uses std::call_once for thread-safe lazy initialization.
     * @return Reference to the single CacheManager instance.
     */
    static CacheManager& getInstance() {
        std::call_once(initInstanceFlag, &CacheManager::initSingleton);
        return *instance;
    }

    /**
     * Stores a key-value pair in the cache.
     * Overwrites the value if the key already exists.
     * @param key The cache key (e.g., "user:123").
     * @param value The value to store (e.g., user data, API response).
     */
    void put(const std::string& key, const std::string& value) {
        if (key.empty()) {
            throw std::invalid_argument("Cache key cannot be empty");
        }
        std::lock_guard<std::mutex> lock(cacheMutex);
        cache[key] = value;
    }

    /**
     * Retrieves a value from the cache by key.
     * @param key The cache key to look up.
     * @return The value associated with the key, or empty string if not found.
     */
    std::string get(const std::string& key) {
        if (key.empty()) {
            throw std::invalid_argument("Cache key cannot be empty");
        }
        std::lock_guard<std::mutex> lock(cacheMutex);
        auto it = cache.find(key);
        if (it != cache.end()) {
            return it->second;
        }
        return ""; // or throw or use std::optional in C++17+
    }

    /**
     * Clears all entries in the cache.
     */
    void clear() {
        std::lock_guard<std::mutex> lock(cacheMutex);
        cache.clear();
    }

    /**
     * Gets the current size of the cache.
     * @return The number of key-value pairs in the cache.
     */
    size_t size() {
        std::lock_guard<std::mutex> lock(cacheMutex);
        return cache.size();
    }

    /**
     * Prints the cache contents.
     */
    void print() {
        std::lock_guard<std::mutex> lock(cacheMutex);
        std::cout << "Cache Contents:\n";
        if (cache.empty()) {
            std::cout << "  (empty)\n";
        } else {
            for (const auto& [key, value] : cache) {
                std::cout << "  Key: " << key << ", Value: " << value << "\n";
            }
        }
    }

private:
    CacheManager() = default; // Private constructor

    static void initSingleton() {
        instance = new CacheManager();
    }

    static CacheManager* instance;
    static std::once_flag initInstanceFlag;

    std::unordered_map<std::string, std::string> cache;
    std::mutex cacheMutex; // protects cache from concurrent access
};

// Static member initialization
CacheManager* CacheManager::instance = nullptr;
std::once_flag CacheManager::initInstanceFlag;


// Demonstration
int main() {
    CacheManager& cache1 = CacheManager::getInstance();

    // Store some data
    cache1.put("user:123", "Alice Smith");
    cache1.put("config:theme", "dark");
    std::cout << "Cache 1 Contents:\n";
    cache1.print();

    CacheManager& cache2 = CacheManager::getInstance();

    // Add more data
    cache2.put("user:456", "Bob Jones");
    std::cout << "\nAfter adding to Cache 2:\n";
    std::cout << "Cache 1 Contents (same instance):\n";
    cache1.print(); // Reflects cache2's addition

    // Retrieve a value
    std::cout << "\nRetrieving user:123: " << cache1.get("user:123") << "\n";

    // Verify same instance
    std::cout << "Is same instance? " << (&cache1 == &cache2 ? "true" : "false") << "\n";

    // Clear cache
    cache2.clear();
    std::cout << "\nAfter clearing Cache 2:\n";
    std::cout << "Cache 1 Contents (same instance):\n";
    cache1.print(); // Empty

    return 0;
}
