using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace ChatApp.Secutiry
{
    internal static class Default
    {
        public static string GetLocalIP()
        {
            IPHostEntry host;
            string temp = Dns.GetHostName();
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) return ip.ToString();
            }
            return "127.0.0.1";
        }

        //Math
        public static bool IsPrime(long n)
        {
            if (n < 2) return false;
            for (int i = 2; i <= Math.Sqrt(n); i++)
                if (n % i == 0) return false;
            return true;
        }

        public static long ModInverse(long e, long phi)
        {
            long t = 0, newt = 1;
            long r = phi, newr = e;

            while (newr != 0)
            {
                long quotient = r / newr;
                long temp = newt;
                newt = t - quotient * newt;
                t = temp;

                temp = newr;
                newr = r - quotient * newr;
                r = temp;
            }

            if (t < 0)
                t += phi;
            return t;
        }
        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long t = b;
                b = a % b;
                a = t;
            }
            return a;
        }
        public static long ModPow(long ibase, long exp, long mod)
        {
            long result = 1;
            ibase = ibase % mod;
            while (exp > 0)
            {
                if (exp % 2 == 1)
                    result = (result * ibase) % mod;
                exp = exp >> 1;
                ibase = (ibase * ibase) % mod;
            }
            return result;
        }

        public static long[] TrasferStringToLongArray(string data)
        {
            string[] array = data.Split(',');
            long[] result = new long[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = long.Parse(array[i]);
            }
            return result;
        }
        public static string TrasferLongArrayToString(long[] array)
        {
            string result= array[0].ToString();
            for(int i = 1; i < array.Length; i++)
            {
                result += ","+ array[i].ToString();
            }
            return result;
        }
    }
}
