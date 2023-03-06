// Author: Atharva Shivankar <ads8046@rit.edu>
// COPADS Project 3: Secure Messaging
// Date: Nov 17 2022

using System.Numerics;
namespace Messenger; 


/// <summary>
/// A class comprising of useful helper functions.
/// </summary>
static class Helper {
    
    /// <summary>
    /// A helper function to find the modInverse of two BigIntegers
    /// </summary>
    /// <param name="a"></param>
    /// <param name="n"></param>
    /// <returns>modInverse (BigInteger)</returns>
    public static BigInteger ModInverse(BigInteger a, BigInteger n) {
        BigInteger i = n, v = 0, d = 1;
        while (a>0) {
            BigInteger t = i/a, x = a;
            a = i % x;
            i = x;
            x = d;
            d = v - t*x;
            v = x; }
        v %= n;
        if (v<0) v = (v+n)%n;
        return v;
    }
    
    
    /// <summary>
    /// A custom "parallel" while loop implemented to parallelize tasks to improve efficiency.
    /// </summary>
    /// <param name="parallelOptions"></param>
    /// <param name="condition"></param>
    /// <param name="body"></param>
    private static void ParallelWhile(
        ParallelOptions parallelOptions,
        Func<bool> condition,
        Action<ParallelLoopState> body)
    {
        ArgumentNullException.ThrowIfNull(parallelOptions);
        ArgumentNullException.ThrowIfNull(condition);
        ArgumentNullException.ThrowIfNull(body);

        var workersCount = parallelOptions.MaxDegreeOfParallelism switch
        {
            -1 => Int32.MaxValue,
            _ => parallelOptions.MaxDegreeOfParallelism
        };

        Parallel.For(0, workersCount, parallelOptions, (_, state) => {
            while (!state.ShouldExitCurrentIteration)
            {
                if (!condition()) { state.Stop(); break; }
                body(state);
            }
        });
    }
    
    
    /// <summary>
    /// A method to generate and return large prime numbers
    /// </summary>
    /// <returns>A prime number (Big Integer)</returns>
    public static BigInteger GetPrime(string result, BigInteger number, int bytes) {

        BigInteger primeAnswer = 0;
        ParallelOptions options = new() { MaxDegreeOfParallelism = Environment.ProcessorCount };
        ParallelWhile(options, () => true, state => {
            lock (result) {
                if (result == "probably prime") {
                    primeAnswer = number;
                    state.Stop();
                }
            }
            
            number = RandomBitGen.GetRandomOdd(bytes);
            result = PrimeTester.IsProbablyPrime(number);
        });
        return primeAnswer;
    }
    
    
    /// <summary>
    /// Prints help for the user in the commandline if incorrect arguments were passed
    /// to the program. 
    /// </summary>
    public static void PrintHelp() {
        Console.WriteLine("dotnet run <option> <other arguments>" + "\n" +
                          "   option: keyGen, sendKey, getKey, sendMsg, getMsg" + "\n" + 
                          "   other arguments: email or/and message depending on the operation" + "\n");
    }
}