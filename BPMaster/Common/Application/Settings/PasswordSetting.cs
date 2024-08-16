using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Settings
{
    public class PasswordSetting
    {
        public int KeySize { get; set; } = 64;
        public int Iterations { get; set; } = 350000;
        public HashAlgorithmName HashAlgorithmName { get; set; } = HashAlgorithmName.SHA512;
    }
}
