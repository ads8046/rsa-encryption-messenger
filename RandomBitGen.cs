// Author: Atharva Shivankar <ads8046@rit.edu>
// COPADS Project 3: Secure Messaging
// Date: Nov 17 2022

namespace Messenger; 

using System.Numerics;
using System.Security.Cryptography;

public class RandomBitGen {

    private static RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        
    /// <summary>
    /// Generates a random byte array to initialize a number
    /// </summary>
    /// <param name="size"></param>
    /// <returns>A byte array of a random number </returns>
    public static byte[] GenerateRandomByteArr(int size) {
        var salt = new byte[size];
        _rng.GetBytes(salt);
        return salt;
    }

        
    /// <summary>
    /// Generates a random odd, positive number
    /// </summary>
    /// <param name="bytes">The number of bytes</param>
    /// <returns> A random BigInteger which is an odd and positive </returns>
    public static BigInteger GetRandomOdd(int bytes) {
        var randomBytes = GenerateRandomByteArr(bytes);
        var number = BigInteger.Abs(new BigInteger(randomBytes));

        while (number % 2 == 0) {
            randomBytes = GenerateRandomByteArr(bytes);
            number = BigInteger.Abs(new BigInteger(randomBytes));
        }
            
        return number;
    }

}