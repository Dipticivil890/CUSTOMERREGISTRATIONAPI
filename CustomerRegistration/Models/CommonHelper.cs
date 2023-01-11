using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Models
{
    public class CommonHelper
    {
        public string Encrypt(string strToDecrypt)
        {
            string key = "C#S@$R%!";
            byte[] inputByptArr;
            byte[] keyBytes;
            string strToEncrypt;
            if (string.IsNullOrWhiteSpace(strToDecrypt))
            {
                strToEncrypt = null;
            }
            else
            {
                try
                {
                    inputByptArr = ASCIIEncoding.ASCII.GetBytes(strToDecrypt);
                    keyBytes = ASCIIEncoding.ASCII.GetBytes(key);

                    DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                    ICryptoTransform transform = provider.CreateEncryptor(keyBytes, keyBytes);
                    CryptoStreamMode mode = CryptoStreamMode.Write;

                    MemoryStream memStream = new MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream(memStream, transform, mode);
                    cryptoStream.Write(inputByptArr, 0, inputByptArr.Length);
                    cryptoStream.FlushFinalBlock();

                    byte[] encryptedInputByteArr = new byte[memStream.Length];
                    memStream.Position = 0;
                    memStream.Read(encryptedInputByteArr, 0, encryptedInputByteArr.Length);

                    strToEncrypt = Convert.ToBase64String(encryptedInputByteArr).Replace('/', '_').Replace('+', '-');
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return strToEncrypt;
        }
        public string Decrypt(string strToEncrypt)
        {
            string key = "C#S@$R%!";
            byte[] encryptedInputByteArr;
            byte[] keyBytes;
            string decryptedMsg;
            if (string.IsNullOrWhiteSpace(strToEncrypt))
            {
                decryptedMsg = null;
            }
            else
            {
                try
                {
                    //strToEncrypt = strToEncrypt.Trim();
                    strToEncrypt = strToEncrypt.Replace('-', '+').Replace('_', '/');
                    byte[] data = Encoding.UTF8.GetBytes(strToEncrypt);
                    encryptedInputByteArr = Convert.FromBase64String(strToEncrypt);
                    keyBytes = ASCIIEncoding.ASCII.GetBytes(key);

                    DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                    ICryptoTransform transform = provider.CreateDecryptor(keyBytes, keyBytes);
                    CryptoStreamMode mode = CryptoStreamMode.Write;

                    MemoryStream memStream = new MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream(memStream, transform, mode);
                    cryptoStream.Write(encryptedInputByteArr, 0, encryptedInputByteArr.Length);
                    cryptoStream.FlushFinalBlock();

                    byte[] decryptedInputByteArr = new byte[memStream.Length];
                    memStream.Position = 0;
                    memStream.Read(decryptedInputByteArr, 0, decryptedInputByteArr.Length);

                    decryptedMsg = ASCIIEncoding.ASCII.GetString(decryptedInputByteArr);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return decryptedMsg;
        }

    }
}
