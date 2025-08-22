import threading

class CacheManager:
    """
    A Singleton class that manages a single in-memory cache for key-value pairs.
    Ensures only one cache instance exists, providing global access to store and retrieve data.
    Useful in scenarios like caching database results or API responses to improve performance.
    Uses lazy initialization with double-checked locking for thread-safety.
    """

    # === Singleton Instance ===

    _instance = None
    _lock = threading.Lock()  # Lock object for thread-safe initialization

    def __new__(cls):
        # Double-checked locking for thread-safe singleton creation
        if cls._instance is None:
            with cls._lock:
                if cls._instance is None:
                    cls._instance = super(CacheManager, cls).__new__(cls)
                    cls._instance._init_cache()
        return cls._instance

    def _init_cache(self):
        """
        Initialize the cache dictionary.
        Called only once during instance creation.
        """
        self._cache = {}

    # === Instance Methods ===

    def put(self, key: str, value):
        """
        Stores a key-value pair in the cache.
        Overwrites the value if the key already exists.

        Args:
            key (str): The cache key (e.g., "user:123").
            value: The value to store (e.g., user data, API response).

        Raises:
            ValueError: If key is None.
        """
        if key is None:
            raise ValueError("Cache key cannot be None")
        self._cache[key] = value

    def get(self, key: str):
        """
        Retrieves a value from the cache by key.

        Args:
            key (str): The cache key to look up.

        Returns:
            The value associated with the key, or None if not found.

        Raises:
            ValueError: If key is None.
        """
        if key is None:
            raise ValueError("Cache key cannot be None")
        return self._cache.get(key)

    def clear(self):
        """
        Clears all entries in the cache.
        """
        self._cache.clear()

    def size(self) -> int:
        """
        Gets the current size of the cache.

        Returns:
            int: The number of key-value pairs in the cache.
        """
        return len(self._cache)

    def __str__(self) -> str:
        """
        Generates a string representation of the cache contents.

        Returns:
            str: A string listing all key-value pairs in the cache.
        """
        if not self._cache:
            return "Cache Contents:\n  (empty)"
        lines = ["Cache Contents:"]
        for key, value in self._cache.items():
            lines.append(f"  Key: {key}, Value: {value}")
        return "\n".join(lines)


def main():
    """
    Demonstrates usage of the CacheManager Singleton.
    Shows how multiple components can access the same cache instance.
    """
    # Get the singleton instance
    cache1 = CacheManager()

    # Store some data
    cache1.put("user:123", "Alice Smith")
    cache1.put("config:theme", "dark")
    print("Cache 1 Contents:")
    print(cache1)

    # Get another reference to the same instance
    cache2 = CacheManager()

    # Add more data and retrieve
    cache2.put("user:456", "Bob Jones")
    print("\nAfter adding to Cache 2:")
    print("Cache 1 Contents (same instance):")
    print(cache1)  # Reflects cache2's addition

    # Retrieve a value
    print("\nRetrieving user:123:", cache1.get("user:123"))

    # Verify same instance
    print("Is same instance?", cache1 is cache2)  # True

    # Clear cache
    cache2.clear()
    print("\nAfter clearing Cache 2:")
    print("Cache 1 Contents (same instance):")
    print(cache1)  # Empty


if __name__ == "__main__":
    main()
