using System.Security.Cryptography;

namespace SaLes__APIs.Serviecs.Security
{

    public delegate string HashPasswordDelegate(string password);
    public delegate bool VerifyHashedPasswordDelegate(string hashedPassword, string password);
    public class PasswordCryptographerNew
    {
        private static bool _enableRfc2898 = true;
        private static bool _supportLegacySha512 = false;
        private static HashPasswordDelegate _hashPasswordDelegate = HashPassword;
        private static VerifyHashedPasswordDelegate _verifyHashedPasswordDelegate = VerifyHashedPassword;
        const int saltLength = 6;
        const string delim = "*";
        static string SaltPassword(string password, string salt)
        {
            SHA512 hashAlgorithm = SHA512.Create();
            return Convert.ToBase64String(hashAlgorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(salt + password)));
        }
        static string HashPassword(string password)
        {
            if (EnableRfc2898)
            {
                //return Rfc2898PasswordCryptographer.HashPassword(password);
            }
            if (string.IsNullOrEmpty(password))
                return password;
            byte[] randomSalt = new byte[saltLength];
            new RNGCryptoServiceProvider().GetBytes(randomSalt);
            string salt = Convert.ToBase64String(randomSalt);
            return salt + delim + SaltPassword(password, salt);
        }
        static bool VerifyHashedPassword(string saltedPassword, string password)
        {
            if (EnableRfc2898)
            {
                bool result = false;
                //result = Rfc2898PasswordCryptographer.VerifyHashedPassword(saltedPassword, password);
                if (!result && SupportLegacySha512)
                {
                    try
                    {
                        result = AreEqualSHA512(saltedPassword, password);
                    }
                    catch
                    {
                    }
                }
                return result;
            }
            return AreEqualSHA512(saltedPassword, password);
        }
        static bool AreEqualSHA512(string saltedPassword, string password)
        {
            if (string.IsNullOrEmpty(saltedPassword))
                return string.IsNullOrEmpty(password);
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }
            int delimPos = saltedPassword.IndexOf(delim);
            if (delimPos <= 0)
            {
                return saltedPassword.Equals(password);
            }
            else
            {
                string calculatedSaltedPassword = SaltPassword(password, saltedPassword.Substring(0, delimPos));
                string expectedSaltedPassword = saltedPassword.Substring(delimPos + delim.Length);
                if (expectedSaltedPassword.Equals(calculatedSaltedPassword))
                {
                    return true;
                }
                return expectedSaltedPassword.Equals(SaltPassword(password, "System.Byte[]"));
            }
        }
        public static bool EnableRfc2898
        {
            get { return PasswordCryptographerNew._enableRfc2898; }
            set { PasswordCryptographerNew._enableRfc2898 = value; }
        }
        public static bool SupportLegacySha512
        {
            get { return PasswordCryptographerNew._supportLegacySha512; }
            set { PasswordCryptographerNew._supportLegacySha512 = value; }
        }
        public static VerifyHashedPasswordDelegate VerifyHashedPasswordDelegate
        {
            get { return PasswordCryptographerNew._verifyHashedPasswordDelegate; }
            set { PasswordCryptographerNew._verifyHashedPasswordDelegate = value; }
        }
        public static HashPasswordDelegate HashPasswordDelegate
        {
            get { return PasswordCryptographerNew._hashPasswordDelegate; }
            set { PasswordCryptographerNew._hashPasswordDelegate = value; }
        }
        public virtual string GenerateSaltedPassword(string password)
        {
            return HashPasswordDelegate(password);
        }
        public virtual bool AreEqual(string saltedPassword, string password)
        {
            return VerifyHashedPasswordDelegate(saltedPassword, password);
        }
    }
}
