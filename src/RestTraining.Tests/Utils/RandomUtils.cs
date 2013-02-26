using System;

namespace RestTraining.Api.Tests.Utils
{
    public static class RandomUtils
    {
        public static readonly Random Rng = new Random((int)DateTime.Now.Ticks);
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

        public static T RandomEnumItem<T>()
        {
            var allValues = (T[])Enum.GetValues(typeof(T));
            var value = allValues[Rng.Next(allValues.Length)];
            return value;
        }

        public static int RandomInt(int max)
        {
            return Rng.Next(max);
        }
    }
}
