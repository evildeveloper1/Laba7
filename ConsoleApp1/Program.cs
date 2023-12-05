using System;
using System.Collections.Generic;

class Repository<T>
{
    private List<T> elements = new List<T>();

    public void Add(T element)
    {
        elements.Add(element);
    }

    public List<T> Find(Criteria<T> criteria)
    {
        List<T> result = new List<T>();

        foreach (T element in elements)
        {
            if (criteria(element))
            {
                result.Add(element);
            }
        }

        return result;
    }
}

public delegate bool Criteria<T>(T element);

class Program
{
    static void Main()
    {
        Repository<int> intRepository = new Repository<int>();
        intRepository.Add(5);
        intRepository.Add(10);
        intRepository.Add(15);

        Criteria<int> criteria = x => x > 8;
        List<int> result = intRepository.Find(criteria);

        Console.WriteLine("Elements greater than 8:");
        foreach (int element in result)
        {
            Console.WriteLine(element);
        }

        Repository<string> stringRepository = new Repository<string>();
        stringRepository.Add("apple");
        stringRepository.Add("banana");
        stringRepository.Add("orange");

        Criteria<string> stringCriteria = s => s.Length > 5;
        List<string> stringResult = stringRepository.Find(stringCriteria);

        Console.WriteLine("\nStrings with length greater than 5:");
        foreach (string element in stringResult)
        {
            Console.WriteLine(element);
        }

        Console.ReadLine();
    }
}
