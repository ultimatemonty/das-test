using System;
using System.Collections;
using System.Collections.Generic;

namespace Secret
{
    // You are given a function 'secret()' that accepts a single integer parameter and returns an integer.
    // In your favorite programming language, write a command-line program that takes
    //   one command-line argument (a number) and determines if the secret() function
    //   is additive [secret(x+y) = secret(x) + secret(y)], for all combinations x and y, where x and y are all prime numbers less than the number passed via the command-line argument.
    // Describe how to run your examples.
    class Program
    {
        static void Main()
        {   
            var limit = _getLimit(0);
            Console.WriteLine();
            Console.WriteLine("Getting all primes <= " + limit.ToString());
            Console.WriteLine();
            var primes = _getPrimes(limit);
            Console.ForegroundColor = ConsoleColor.Green;
            foreach(int prime in primes)
            {   
                Console.WriteLine(prime);
            }
            Console.ResetColor();
            Console.ReadLine();
        }

        // simple function that will return whatever is passed in making it inherently additive
        static int secret(int number)
        {
            return number;
        }

        // Recursively call this until the user gets the input correct
        static int _getLimit(int count)
        {
            count++;
            Console.WriteLine("Enter a positive integer");
            var value = Console.ReadLine();

            int limit;
            if (!int.TryParse(value, out limit) || limit <= 0)
            {                
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;

                if (count >= 3 && count < 5)
                {
                    Console.WriteLine("Seriously? Value must be a POSITIVE INTEGER - I.E. A WHOLE NUMBER GREATER THAN 0");
                } 
                else if (count == 5)
                {
                    Console.WriteLine("Obviously you're effing with me. So I'm using 100 and you can go jump in a lake.");
                    limit = 100;
                    return limit;
                } 
                else 
                {
                    Console.WriteLine("Value must be a positive integer");
                }

                Console.ResetColor();
                Console.WriteLine();
                return _getLimit(count);
            }
            
            return limit;
        }

        // Implementation of the Sieve of Eratosthenes courtesy of http://rosettacode.org/wiki/Sieve_of_Eratosthenes#C.23
        private static List<int> _getPrimes(int limit)
        {
            var primes = new List<int>() { 2 };
            var maxSquareRoot = Math.Sqrt(limit);
            var eliminated = new BitArray(limit + 1);

            for (int i = 3; i <= limit; i += 2)
            {
                if (!eliminated[i])
                {
                    primes.Add(i);
                    if(i < maxSquareRoot)
                    {
                        for (int j = i * i; j <= limit; j += 2 * i)
                        {
                            eliminated[j] = true;
                        }
                    }
                }
            }

            return primes;
        }
    }
}
