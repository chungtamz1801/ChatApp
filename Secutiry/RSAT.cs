using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp.Secutiry
{
    internal static class RSAT
    {
        public static void GenerateKeys(long p, long q, out long n,out long e,out long d)
        {
            if (!Default.IsPrime(p) || !Default.IsPrime(q))
            {
                throw new Exception();
            }

            n = p * q;
            long phi = (p - 1) * (q - 1);

            e = 3;
            while (Default.GCD(e, phi) != 1)
                e += 2;

            d = Default.ModInverse(e, phi);
        }
        //Co the gop lai thanh 1 ham
        public static string Encrypt(byte[] message, long e, long n)
        {

            string result;
            long[] temp = new long[message.Length];
            for (int i = 0; i < message.Length; ++i)
            {
                temp[i] = Default.ModPow(Convert.ToInt64(message[i]), e, n);
            }
            result = Default.TrasferLongArrayToString(temp);
            return result;
        }
        public static byte[] Decrypt(string cipher, long d, long n)
        {
            long[] temp = Default.TrasferStringToLongArray(cipher);
            byte[] result = new byte[temp.Length];
            for (int i = 0; i < temp.Length; ++i)
            {
                try
                {
                    result[i] = Convert.ToByte(Default.ModPow(temp[i], d, n));
                }
                catch 
                {
                    MessageBox.Show(temp[i].ToString());
                }        
            }
            return result;
            /*string result = "";
            for (int i = 0; i < cipher.Count; ++i)
            {
                result += (char)Default.ModPow(cipher[i], d, n);
            }
            return result;*/
        }
    }
}
