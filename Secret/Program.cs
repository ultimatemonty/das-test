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
        static int falseCount = 0;

        static void Main()
        {   
            var limit = _getLimit(0);
            
            var primes = _getPrimes(limit);

            _testPrimes(primes);

            // print the results
            Console.WriteLine();
            Console.WriteLine("Like a Geraldo Rivera paternity test THE RESULT IS IN...");
            Console.WriteLine();
            Console.Write("Secret ");

            if (falseCount > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("IS NOT");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("IS");
            }

            Console.ResetColor();
            Console.Write(" additive for all combinations of primes under {0}", limit);

            // prevent app from automagically exiting so we can see our glorious output
            Console.ReadLine();
        }

        // simple function that will return whatever is passed in making it inherently additive
        static int secret(int number)
        {
            return number;
        }
        
        static int _getLimit(int count)
        {
            Console.ResetColor();

            count++;
            Console.WriteLine("Enter a positive integer");
            var value = Console.ReadLine();

            int limit;

            // Recursively call this function until the user gets the input correct or hits one of our "Effing with us" limits
            if (!int.TryParse(value, out limit) || limit <= 0)
            {   
                Console.ForegroundColor = ConsoleColor.Red;

                if (count >= 3 && count < 5)
                {
                    Console.WriteLine("Seriously? Value must be a POSITIVE INTEGER - I.E. A WHOLE NUMBER GREATER THAN 0");
                } 
                else if (count == 5)
                {
                    Console.WriteLine("Obviously you're effing with me. So I'm using 40 and you can go jump in a lake.");
                    limit = 100;
                    return limit;
                } 
                else 
                {
                    Console.WriteLine("Value must be a positive integer");
                }
                
                Console.WriteLine();
                return _getLimit(count);
            }
            
            return limit;
        }

        // Implementation of the Sieve of Eratosthenes courtesy of http://rosettacode.org/wiki/Sieve_of_Eratosthenes#C.23
        private static List<int> _getPrimes(int limit)
        {
            Console.ResetColor();
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

            Console.WriteLine();
            Console.WriteLine("Found {0} prime numbers <= {1}", primes.Count, limit);
            Console.WriteLine();

            return primes;
        }

        // Writes the list of primes to the console
        private static void _listPrimes(List<int> primes)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (int prime in primes)
            {
                Console.WriteLine(prime);
            }
        }

        // Tests the primes per the specified conditions
        private static void _testPrimes(List<int> primes)
        {
            Console.ResetColor();
            int x, y, startIndex;
            for(int i = 0; i < primes.Count; i++)
            {
                x = primes[i];

                // since we know the combinations will regress linearly we simply keep track of the primary index 
                //      and start the secondary loop at that index
                // in this way we skip combinations that have already been tested
                startIndex = i;
                
                for (int j = startIndex; j < primes.Count; j++)
                {
                    y = primes[j];

                    Console.Write("{0},{1}: ", x, y);
                    bool isAdditive = _isAdditive(x, y);
                    
                    if (isAdditive)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        falseCount++;
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine(isAdditive);
                    Console.ResetColor();
                }
            }
        }

        // Test if secret(x+y) == secret(x) + secret(y)
        private static bool _isAdditive(int x, int y)
        {
            if (secret(x+y) == (secret(x) + secret(y))) {
                return true;
            }

            return false;
        }
    }
}
