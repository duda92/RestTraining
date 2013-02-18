using System;

namespace RestTraining.Api.Tests.Utils
{
    public static class RandomStringUtils
    {
        private static readonly Random Rng = new Random((int)DateTime.Now.Ticks);
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string RandomString(int size)
        {
            var buffer = new char[size];

            for (var i = 0; i < size; i++)
            {
                buffer[i] = Chars[Rng.Next(Chars.Length)];
            }
            return new string(buffer);
        }
    }
}
