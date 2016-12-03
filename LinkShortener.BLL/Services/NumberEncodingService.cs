using System.Linq;
using LinkShortener.BLL.Contracts;

namespace LinkShortener.BLL.Services
{
    public class NumberEncodingService : INumberEncodingService
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly int _base = Alphabet.Length;

        public string Encode(int value)
        {
            if (value == 0)
                return Alphabet[0].ToString();

            var s = string.Empty;

            while (value > 0)
            {
                s += Alphabet[value % _base];
                value = value / _base;
            }

            return string.Join(string.Empty, s.Reverse());
        }

        public int Decode(string str)
        {
            var i = 0;
            foreach (var c in str)
            {
                i = (i * _base) + Alphabet.IndexOf(c);
            }
            return i;
        }
    }
}
