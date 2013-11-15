using System;

namespace com.VotoVisible.Utils
{
    /// <summary>
    /// Descripción breve de Conversion
    /// </summary>
    public class Conversion
    {
        public Conversion()
        {

        }

        public static int? Obj2Int(object value)
        {
            if (value == null || value == System.DBNull.Value)
                return null;

            int? ivalue = (int?)value;
            return ivalue;
        }

        public static string Obj2String(object value)
        {
            if (value == null || value == System.DBNull.Value)
                return null;
            return (string)value;
        }

        public static DateTime? Obj2DateTime(object value)
        {
            if (value == null || value == System.DBNull.Value)
                return null;
            return (DateTime)value;
        }

        public static DateTime? Str2DateTime(string value, string format)
        {
            DateTime fecha;
            if (!DateTime.TryParseExact(value, format
                    , System.Globalization.CultureInfo.InvariantCulture
                    , System.Globalization.DateTimeStyles.None
                    , out fecha))
                return null;

            return fecha;
        }

        //http://en.wikipedia.org/wiki/Base_36#C_implementation
        private const string Clist = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly char[] Clistarr = Clist.ToCharArray();

        public static long Base36Decode(string inputString)
        {
            long result = 0;
            var pow = 0;
            for (var i = inputString.Length - 1; i >= 0; i--)
            {
                var c = inputString[i];
                var pos = Clist.IndexOf(c);
                if (pos > -1)
                    result += pos * (long)Math.Pow(Clist.Length, pow);
                else
                    return -1;
                pow++;
            }
            return result;
        }

        public static string Base36Encode(ulong inputNumber)
        {
            var sb = new System.Text.StringBuilder();
            do
            {
                sb.Append(Clistarr[inputNumber % (ulong)Clist.Length]);
                inputNumber /= (ulong)Clist.Length;
            } while (inputNumber != 0);
            return Reverse(sb.ToString());
        }

        public static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}