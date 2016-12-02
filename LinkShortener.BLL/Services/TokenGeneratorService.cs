using System;
using LinkShortener.BLL.Contracts;

namespace LinkShortener.BLL.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        public string GenerateToken(int length)
        {
            var guidString = Guid.NewGuid().ToString().Replace("=", string.Empty).Replace("+", string.Empty);
            var startIndex = new Random().Next(guidString.Length - length - 1);

            return guidString.Substring(startIndex, length);
        }
    }
}
