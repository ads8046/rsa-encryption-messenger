using System.Numerics;
using System.Security.Cryptography;
// Author: Atharva Shivankar <ads8046@rit.edu>
// COPADS Project 3: Secure Messaging
// Date: Nov 17 2022

namespace Messenger; 

public static class PrimeTester {
        
        /// <summary>
        /// Tests if a number is probably a prime number or a composite number.
        /// </summary>
        /// <param name="value">The number to be tested for primality </param>
        /// <param name="k">Number of rounds of tests to perform (default k is 10) </param>
        /// <returns></returns>
        public static string IsProbablyPrime(BigInteger value, int k=10) {
            if(value == 2 || value == 3 || value == 5 || value == 7 || value == 11 || value == 13)
                return "probably prime";
            
            if (value < 2 || 
                value % 2 == 0 ||
                value % 3 == 0 || 
                value % 5 == 0 || 
                value % 7 == 0 ||
                value % 11 == 0 ||
                value % 8 == 0 || 
                value % 13 == 0 || 
                value % 89 == 0 || 
                value % 151 == 0) {
                return "composite";
            }

            BigInteger d = value - 1;
            int s = 0;

            while(d % 2 == 0) {
                d /= 2;
                s += 1;
            }

            // There is no built-in method for generating random BigInteger values.
            // Instead, random BigIntegers are constructed from randomly generated
            // byte arrays of the same length as the source.
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[value.ToByteArray().LongLength];
            BigInteger a;

            for(int i = 0; i < k; i++) {
                do {
                    // This may raise an exception in Mono 2.10.8 and earlier.
                    // http://bugzilla.xamarin.com/show_bug.cgi?id=2761
                    rng.GetBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while(a < 2 || a >= value - 2);

                BigInteger x = BigInteger.ModPow(a, d, value);
                if(x == 1 || x == value - 1)
                    continue;

                for(int r = 1; r < s; r++) {
                    x = BigInteger.ModPow(x, 2, value);
                    if(x == 1)
                        return "composite";
                    if(x == value - 1)
                        break;
                }

                if(x != value - 1)
                    return "composite";
            }
            return "probably prime";
        }
        
    }