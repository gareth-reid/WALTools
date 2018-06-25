using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace WALTools.Extension
{
    public static class StringExtension
    {
        public static string ToMyString(this object sValue)
        {
            try
            {
                if (!String.IsNullOrEmpty(sValue.ToString()))
                {
                    return sValue.ToString();
                }
                return String.Empty;
            }
            catch
            {
                return String.Empty;
            }
        }

        public static DateTime? ToDateTime(this string currentDate)
        {
            try
            {
                DateTime dValue;
                if (DateTime.TryParse(currentDate, out dValue))
                {
                    return dValue;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int WordCount(this object sValue)
        {
            try
            {
                var stringValue = sValue.ToString();
                if(!String.IsNullOrEmpty(stringValue))
                {
                    stringValue = stringValue.Trim();
                    var splitIntoWords = stringValue.Split(' ');
                    if (splitIntoWords.ContainsElements() && String.IsNullOrEmpty(splitIntoWords[0]))
                    {
                        //was an empty string
                        return 0;
                    }
                    return splitIntoWords.Count();
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Same as split but handles the null check
        /// </summary>
        /// <param name="str"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static string[] TheSplit(this object str, char splitChar)
        {
            try
            {
                return str.ToString().Split(splitChar);
            }
            catch
            {
                return new string[0];
            }
        }

        public static bool IsValidEmailAddress(this object s)
        {
            try
            {
                return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$").IsMatch(s.ToString());
            }
            catch
            {
                return false;
            }
            
        }

        public static string RemoveWhiteSpace(this object s)
        {
            try
            {
                var returnValue = s.ToString().Replace(" ", "");
                returnValue = returnValue.Trim();
                return returnValue;
            }
            catch
            {
                return "";
            }
        }

        //IsNumber
        public static bool IsNumber(this object num)
        {
            try
            {
                int iValue = 0;
                if(Int32.TryParse(num.ToString(), out iValue))
                {
                    return true;
                }
                double dValue = 0;
                if (Double.TryParse(num.ToString(), out dValue))
                {
                    return true;
                }
                float fValue = 0;
                if (Single.TryParse(num.ToString(), out fValue))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// hasg to MD5
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static MD5CryptoServiceProvider s_md5 = null;
        public static string ToMD5(this string s)
        {
            if (s_md5 == null) //creating only when needed
                s_md5 = new MD5CryptoServiceProvider();
            Byte[] newdata = Encoding.Default.GetBytes(s);
            Byte[] encrypted = s_md5.ComputeHash(newdata);
            return BitConverter.ToString(encrypted).Replace("-", "").ToLower();
        }
        
        /// <summary>
        /// Replace all line endings with html break
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string NlToBreak(this string s)
        {
            try
            {
                return s.Replace("\r\n", "<br />").Replace("\n", "<br />");
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string Encrypt(this string message, string passphrase)
        {
            byte[] results;
            var uTF8 = new UTF8Encoding();

            using (var hashProvider = new MD5CryptoServiceProvider())
            {
                byte[] tDESKey = hashProvider.ComputeHash(uTF8.GetBytes(passphrase));

                using (var tDESAlgorithm = new TripleDESCryptoServiceProvider())
                {

                    tDESAlgorithm.Key = tDESKey;
                    tDESAlgorithm.Mode = CipherMode.ECB;
                    tDESAlgorithm.Padding = PaddingMode.PKCS7;

                    byte[] dataToEncrypt = uTF8.GetBytes(message);
                    
                    ICryptoTransform Encryptor = tDESAlgorithm.CreateEncryptor();
                    results = Encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
                }
            }

            return Convert.ToBase64String(results);
        }

        public static string Decrypt(this string message, string passphrase)
        {
            if (message == String.Empty)
                return String.Empty;

            byte[] results;
            var uTF8 = new UTF8Encoding();

            using (var hashProvider = new MD5CryptoServiceProvider())
            {
                byte[] TDESKey = hashProvider.ComputeHash(uTF8.GetBytes(passphrase));

                using (var tDESAlgorithm = new TripleDESCryptoServiceProvider())
                {

                    tDESAlgorithm.Key = TDESKey;
                    tDESAlgorithm.Mode = CipherMode.ECB;
                    tDESAlgorithm.Padding = PaddingMode.PKCS7;

                    byte[] dataToDecrypt = Convert.FromBase64String(message);

                    try
                    {
                        ICryptoTransform decryptor = tDESAlgorithm.CreateDecryptor();
                        results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
                    }
                    catch
                    {
                        results = new byte[0];
                    }
                }
            }

            return uTF8.GetString(results);
        }
    }
}
