using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace RandomCouponCode
{
    public static class JSONHelpers
    {
        public static string ListToJSON(object p_object)
        {
            return JsonConvert.SerializeObject(p_object);
        }
        public static T JSONToList<T>(string p_object)
        {
            return JsonConvert.DeserializeObject<T>(p_object);
        }
    }

    public static class StringHelpers
    {
        public static char FirstCharacter(string str)
        {
            return str[0];
        }

        // Karakterin string içerisinde kaç adet bulunduğunu bulan metot

        public static int FindCharCount(char chr, string str, int beginPos, int endPos)
        {
            // digits = digits.Remove(0, TextHelper.FindRep('0', digits, 0, digits.Length - 2));

            int pos;

            for (pos = beginPos; pos <= endPos; pos++)
            {
                if (str[pos] != chr)
                {
                    break;
                }
            }
            return pos - beginPos;
        }

        // Virgül (",")'e göre ayır ve string liste olarak döndür
        public static List<string> SplitByComma(string value)
        {
            return new List<string>(value.Split(','));
        }

        // Virgül (",")'e göre ayır ve int liste olarak döndür (String içerisinde 1,2,3,4 gibi rakamlar için çalışır)
        public static List<int> SplitByCommaToInt(string value)
        {
            return new List<int>(value.Split(',').Select(int.Parse));
        }

        // Türkçe karakteri İngilizce karaktere çevir
        public static string TurkishCharacterReplace(string Text)
        {
            return Text.Replace("ı", "i").Replace("İ", "I").
                        Replace("â", "a").
                        Replace("ç", "c").Replace("Ç", "C").
                        Replace("ğ", "g").Replace("Ğ", "G").
                        Replace("ö", "o").Replace("Ö", "O").
                        Replace("ş", "s").Replace("Ş", "S").
                        Replace("ü", "u").Replace("Ü", "U");
        }

        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }

        public static string GetFirst(this string source, int tail_length)
        {
            if (tail_length > source.Length)
                return source;
            return source.Substring(0 ,tail_length);
        }

        public static string MixString(string Value)
        {
            Random r = new Random();
            string newValue = "";
            int randomIndex = 0;
            int Length = Value.Length;
            for (int i = Length; i > 0; i--)
            {
                randomIndex = r.Next(0, Length);
                newValue += Value[randomIndex];
                Value = Value.Remove(randomIndex, 1);
                Length = Value.Length;
            }
            return newValue;
        }

        public static string _Encrypt(string value) // String Şifrele
        {
            string key = "1b48f5effrhreherh43353hrthrhrthrthrthrthkukk..86jrww5asdasdjh5hj3gb5ad20db7834acf";
            Byte[] inputArray = UTF8Encoding.UTF8.GetBytes(value);
            TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();
            TripleDes.Key = UTF8Encoding.UTF8.GetBytes(key);
            TripleDes.Mode = CipherMode.ECB;
            TripleDes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = TripleDes.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            TripleDes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string _Decrypt(string value) // String Şifre Çöz
        {
            string key = "1b48f5effrhreherh43353hrthrhrthrthrthrthkukk..86jrww5asdasdjh5hj3gb5ad20db7834acf";
            Byte[] inputArray = Convert.FromBase64String(value);
            TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();
            TripleDes.Key = UTF8Encoding.UTF8.GetBytes(key);
            TripleDes.Mode = CipherMode.ECB;
            TripleDes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = TripleDes.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            TripleDes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static bool EmailCheck(string emailAddress) // String Email mi Kontrolü
        {
            bool returnValue = false;
            string pattern = "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";
            Match emailAddressMatch = Regex.Match(emailAddress, pattern);
            if (emailAddressMatch.Success)
                returnValue = true;

            return returnValue;
        }

        public static int getMonthDays(int year, int month) // Bugüne göre Ay içindeki gün sayısını bulur (Şubat 28 gün gibi)
        {
            return DateTime.DaysInMonth(year, month);
        }

        public static bool IsUrl(string url) // string Url mi değil mi kontrolü
        {
            Uri outUri;
            if (Uri.TryCreate(url, UriKind.Absolute, out outUri) && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps)) // Url doğru ise devam et
                return true;

            return false;
        }
    }

    public static class HtmlHelpers
    {
        public static string Image(string Source, string Alt, string Title, int Height = 128, int Width = 128)
        {
            return String.Format("<img src='{0}' alt='{1}' title='{2}' height='{3}' width='{4}'/>", Source, Alt, Title, Height, Width);
        }

    }

    public static class FileHelpers
    {
        public static String Base64Encode(string path)
        {
            Byte[] bytes = File.ReadAllBytes(path);
            String file = Convert.ToBase64String(bytes);
            return file;
        }

        public static void Base64Decode(string path, string b64)
        {
            Byte[] bytes = Convert.FromBase64String(b64);
            File.WriteAllBytes(path, bytes);
        }
    }

    public static class DatatableHelpers
    {
        public static DataTable XmlNodeListToDataTable(XmlNodeList xnl)
        {

            DataTable dt = new DataTable();

            int TempColumn = 0;

            foreach (XmlNode node in xnl.Item(0).ChildNodes)
            {
                TempColumn++;

                DataColumn dc = new DataColumn(node.Name, System.Type.GetType("System.String"));

                if (dt.Columns.Contains(node.Name))
                {
                    dt.Columns.Add(dc.ColumnName = dc.ColumnName + TempColumn.ToString());
                }

                else
                {
                    dt.Columns.Add(dc);
                }
            }
            int ColumnsCount = dt.Columns.Count;
            for (int i = 0; i < xnl.Count; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < ColumnsCount; j++)
                {
                    dr[j] = xnl.Item(i).ChildNodes[j].InnerText;

                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static List<T> DatatableToList<T>(DataTable dt)
        {
            var fields = typeof(T).GetFields();

            List<T> lst = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                var ob = Activator.CreateInstance<T>();

                foreach (var fieldInfo in fields)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (fieldInfo.Name == dc.ColumnName)
                        {
                            object value = dr[dc.ColumnName];

                            fieldInfo.SetValue(ob, value);
                            break;
                        }
                    }
                }

                lst.Add(ob);
            }

            return lst;
        }

        public static void SwapObject<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }

    public static class NumberHelpers
    {
        public static bool IsNumber(object value)
        {
            bool isNum;
            long retNum;

            isNum = long.TryParse(Convert.ToString(value), System.Globalization.NumberStyles.Integer
                                   , System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
    }

}
