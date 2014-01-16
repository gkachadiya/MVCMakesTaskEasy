using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SpotCollege.Common
{
    public class EncryptionHelper
    {
        #region [Encrypt Decrypt Cookie Method]
        private static string _rgbKeyString = "@*%#($^!#@";
        private static string _rgbIVString = "!^%@*$@)^$";
        private static string _slashCharacter = "/";
        private static string _slashReplaceCharacter = "_";

        private static byte[] SALT_BYTES = new byte[] { 98, 1, 28, 239, 64, 30, 162, 27, 156, 102, 223 };
        private static byte[] DerivePassword(string originalPassword, int passwordLength)
        {
            return new Rfc2898DeriveBytes(originalPassword, SALT_BYTES, 5).GetBytes(passwordLength);
        }

        public static string Encrypt(string decrypted)
        {

            byte[] data = Encoding.ASCII.GetBytes(decrypted);
            byte[] rgbIV = Encoding.ASCII.GetBytes(_rgbIVString);

            byte[] keyStream = DerivePassword(_rgbKeyString, 8);

            DESCryptoServiceProvider desCryptoServiceProvider = new DESCryptoServiceProvider();

            // Trasnformer!
            ICryptoTransform trans = desCryptoServiceProvider.CreateEncryptor(keyStream, rgbIV);

            byte[] result = trans.TransformFinalBlock(data, 0, data.Length);

            // Clean up.
            desCryptoServiceProvider.Clear();
            trans.Dispose();

            string encryptedString = System.Convert.ToBase64String(result);

            //Replacing "/" character with "_".

            return encryptedString.Replace(_slashCharacter, _slashReplaceCharacter);
        }

        public static string Decrypt(string encrypted)
        {
            try
            {
                byte[] data = null;
                byte[] rgbIV = null;

                try
                {
                    //Replacing "_" string with "/" character.
                    string replacedString = encrypted.Replace(_slashReplaceCharacter, _slashCharacter);

                    data = Convert.FromBase64String(replacedString);
                    //if (data.Length != 8) // By Puneet after discusion.
                    //    return "0";
                    rgbIV = Encoding.ASCII.GetBytes(_rgbIVString);
                }
                catch
                {
                    return "0";
                }
                DESCryptoServiceProvider desCryptoServiceProvider = new DESCryptoServiceProvider();

                // Trasnformer!
                byte[] keyStream = DerivePassword(_rgbKeyString, 8);

                ICryptoTransform trans = desCryptoServiceProvider.CreateDecryptor(keyStream, rgbIV);

                byte[] result = null;//= new byte[] { 0 };
                try
                {
                    result = trans.TransformFinalBlock(data, 0, data.Length);
                }
                catch { }
                finally
                {
                    // Clean up.
                    try
                    {
                        desCryptoServiceProvider.Clear();
                        trans.Dispose();
                    }
                    catch { }

                }
                if (result == null)
                    return "0";
                else
                    return Encoding.ASCII.GetString(result);
            }
            catch
            {
                return "0";
            }

        }
        #endregion
    }
}
