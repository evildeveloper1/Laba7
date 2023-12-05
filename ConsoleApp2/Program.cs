using System;
using System.Collections.Generic;

class FunctionCache<TKey, TResult>
{
    private Dictionary<TKey, Tuple<TResult, DateTime>> cache = new Dictionary<TKey, Tuple<TResult, DateTime>>();
    private TimeSpan cacheDuration;

    public FunctionCache(TimeSpan cacheDuration)
    {
        this.cacheDuration = cacheDuration;
    }

    public TResult GetOrAdd(TKey key, Func<TKey, TResult> function)
    {
        if (cache.ContainsKey(key) && DateTime.Now - cache[key].Item2 < cacheDuration)
        {
            Console.WriteLine($"Result for key '{key}' found in cache.");
            return cache[key].Item1;
        }

        TResult result = function(key);
        cache[key] = Tuple.Create(result, DateTime.Now);
        Console.WriteLine($"Result for key '{key}' calculated and added to cache.");
        return result;
    }
}

class Program
{
    static void Main()
    {
        FunctionCache<int, string> cache = new FunctionCache<int, string>(TimeSpan.FromSeconds(5));

        Func<int, string> expensiveFunction = key =>
        {
            Console.WriteLine($"Executing expensive operation for key '{key}'");
            return $"Result for key {key}";
        };

        Console.WriteLine(cache.GetOrAdd(1, expensiveFunction));
        Console.WriteLine(cache.GetOrAdd(1, expensiveFunction));
        Console.WriteLine(cache.GetOrAdd(2, expensiveFunction)); 

        Console.ReadLine();
    }
}