using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Helpers
{
    public class PasswordGeneratorHelper
    {
        private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Lower = "abcdefghijklmnopqrstuvwxyz";
        private const string Digits = "0123456789";
        private const string Special = "!@#$%^&*()-_=+[]{};:,.<>?";

        public static string Generate(int length = 8)
        {

            var allChars = Upper + Lower + Digits + Special;
            var randomChars = new char[length];

            randomChars[0] = GetRandomChar(Upper);
            randomChars[1] = GetRandomChar(Lower);
            randomChars[2] = GetRandomChar(Digits);
            randomChars[3] = GetRandomChar(Special);

            for (int i = 4; i < length; i++)
            {
                randomChars[i] = GetRandomChar(allChars);
            }

            return Shuffle(randomChars);
        }

        private static char GetRandomChar(string charset)
        {
            var index = RandomNumberGenerator.GetInt32(charset.Length);
            return charset[index];
        }

        private static string Shuffle(char[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = RandomNumberGenerator.GetInt32(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
            return new string(array);
        }
    }
}
