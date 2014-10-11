using HighSpeedRail.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HighSpeedRail.Util
{
    public class Encryptor
    {
        /// <summary>
        /// Encrypt string by AES encryptor and convert to hex string, use UTF-8 encoding
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string EncryptAES_Hex(string input, string key = Constant.KEY, string iv = Constant.IV)
        {
            return EncryptAES_Hex(input, Encoding.UTF8, key, iv);
        }

        /// <summary>
        /// Encrypt string by AES encryptor and convert to hex string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string EncryptAES_Hex(string input, Encoding encoding, string key = Constant.KEY, string iv = Constant.IV, bool isUseEncryptKey = true)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;

            byte[] pwdBytes = isUseEncryptKey ? StringHelper.ConvertStringFromHex(key) : encoding.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            Array.Copy(pwdBytes, keyBytes, len);

            rijndaelCipher.Key = keyBytes;

            byte[] ivBytes1 = isUseEncryptKey ? StringHelper.ConvertStringFromHex(iv) : encoding.GetBytes(iv);
            byte[] keyBytes1 = new byte[16];

            int len1 = ivBytes1.Length;
            if (len1 > keyBytes1.Length) len1 = keyBytes1.Length;
            Array.Copy(ivBytes1, keyBytes1, len1);
            rijndaelCipher.IV = ivBytes1;

            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = encoding.GetBytes(input);
            byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
            return StringHelper.ConvertByteToHex(cipherBytes);
        }

        /// <summary>
        ///  Encrypt string by AES encryptor and convert to base64 string, use UTF8 encoding
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string EncryptAES_Base64(string input, string key = Constant.KEY, string iv = Constant.IV)
        {
            return EncryptAES_Base64(input, Encoding.UTF8, key, iv);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string EncryptAES_Base64(string input, Encoding encoding, string key = Constant.KEY, string iv = Constant.IV, bool isUseEncryptKey = true)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;

            byte[] pwdBytes = isUseEncryptKey ? StringHelper.ConvertStringFromHex(key) : encoding.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            Array.Copy(pwdBytes, keyBytes, len);

            rijndaelCipher.Key = keyBytes;

            byte[] ivBytes1 = isUseEncryptKey ? StringHelper.ConvertStringFromHex(iv) : encoding.GetBytes(iv);
            byte[] keyBytes1 = new byte[16];

            int len1 = ivBytes1.Length;
            if (len1 > keyBytes1.Length) len1 = keyBytes1.Length;
            Array.Copy(ivBytes1, keyBytes1, len1);
            rijndaelCipher.IV = ivBytes1;

            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = encoding.GetBytes(input);
            byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherBytes);
        }

        /// <summary>
        /// Decrypt HEX string by AES 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string DecryptAES_Hex(string input, string key = Constant.KEY, string iv = Constant.IV)
        {
            return DecryptAES_Hex(input, Encoding.UTF8, key, iv);
        }

        /// <summary>
        /// Decrypt HEX string by AES
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string DecryptAES_Hex(string input, Encoding encoding, string key = Constant.KEY, string iv = Constant.IV, bool isUseEncryptKey = true)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;

            byte[] encryptedByte = StringHelper.ConvertStringFromHex(input);
            byte[] pwdBytes = isUseEncryptKey ? StringHelper.ConvertStringFromHex(key) : encoding.GetBytes(key);
            byte[] keyBytes = new byte[16];

            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;

            byte[] ivBytes1 = isUseEncryptKey ? StringHelper.ConvertStringFromHex(iv) : encoding.GetBytes(iv);
            byte[] keyBytes1 = new byte[16];

            int len1 = ivBytes1.Length;
            if (len1 > keyBytes1.Length) len1 = keyBytes1.Length;
            Array.Copy(ivBytes1, keyBytes1, len1);
            rijndaelCipher.IV = keyBytes1;

            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();

            byte[] plainText = transform.TransformFinalBlock(encryptedByte, 0, encryptedByte.Length);
            return encoding.GetString(plainText);
        }

        /// <summary>
        /// Decrypt base64 string by AES
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string DecryptAES_Base64(string input, string key = Constant.KEY, string iv = Constant.IV)
        {
            return DecryptAES_Base64(input, Encoding.UTF8, key, iv);
        }

        /// <summary>
        /// Decrypt base64 string by AES
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string DecryptAES_Base64(string input, Encoding encoding, string key = Constant.KEY, string iv = Constant.IV, bool isUseEncryptKey = true)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;

            byte[] encryptedByte = Convert.FromBase64String(input);
            byte[] pwdBytes = isUseEncryptKey ? StringHelper.ConvertStringFromHex(key) : encoding.GetBytes(key);
            byte[] keyBytes = new byte[16];

            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;

            byte[] ivBytes1 = isUseEncryptKey ? StringHelper.ConvertStringFromHex(iv) : encoding.GetBytes(iv);
            byte[] keyBytes1 = new byte[16];

            int len1 = ivBytes1.Length;
            if (len1 > keyBytes1.Length) len1 = keyBytes1.Length;
            Array.Copy(ivBytes1, keyBytes1, len1);
            rijndaelCipher.IV = keyBytes1;

            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();

            byte[] plainText = transform.TransformFinalBlock(encryptedByte, 0, encryptedByte.Length);
            return encoding.GetString(plainText);
        }

        /// <summary>
        /// Encrypt string by DES encryptor, and convert byte array to hex string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string EncryptDES_Hex(string input, string key = Constant.KEY, string iv = Constant.IV)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(input);
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(iv);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary>
        /// Encrypt string by DES encryptor, and convert byte array to base64 string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string EncryptDES_Base64(string input, string key = Constant.KEY, string iv = Constant.IV)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(input);
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(iv);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// Decrypt Hex string by DES
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string DecryptDES_Hex(string input, string key = Constant.KEY, string iv = Constant.IV)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[input.Length / 2];
            for (int x = 0; x < input.Length / 2; x++)
            {
                int i = (Convert.ToInt32(input.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(iv);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }

        /// <summary>
        /// Decrypt Base64 string by DES
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string DecryptDES_Base64(string input, string key = Constant.KEY, string iv = Constant.IV)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Convert.FromBase64String(input);
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(iv);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// caculate md5 string by input string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}