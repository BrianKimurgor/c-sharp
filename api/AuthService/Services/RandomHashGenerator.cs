using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Services
{
    public class RandomHashGenerator
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] secretKey = new byte[32]; // 32 bytes = 256 bits
            rng.GetBytes(secretKey);
            return Convert.ToBase64String(secretKey);
        }
    }
}