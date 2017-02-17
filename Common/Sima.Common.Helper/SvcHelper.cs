using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sima.Common.Helper
{
    public static class SvcHelper
    {
        public static List<T> ConvertToList<T>(this IEnumerable<object[]> records) where T : class, new()
        {
            try
            {
                var list = new List<T>();

                foreach (var item in records)
                {
                    var obj = Activator.CreateInstance<T>();
                    var properties = obj.GetType().GetProperties();
                    for (var i = 0; i < properties.Length; i++)
                    {
                        try
                        {
                            var propertyInfo = obj.GetType().GetProperty(properties[i].Name);
                            var t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                            var safeValue = (item[i] == null) ? null : Convert.ChangeType(item[i], t);
                            propertyInfo.SetValue(obj, safeValue, null);
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static string EncryptPassword(string sPassword)
        {
            var md5Hasher = new MD5CryptoServiceProvider();
            var hashedDataBytes = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(sPassword));
            var sEncryptPass = Convert.ToBase64String(hashedDataBytes);
            return sEncryptPass;
        }

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                var list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    var obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            var propertyInfo = obj.GetType().GetProperty(prop.Name);
                            var t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                            var safeValue = (row[prop.Name] == null) ? null : Convert.ChangeType(row[prop.Name], t);
                            propertyInfo.SetValue(obj, safeValue, null);
                        }
                        catch
                        {
                            //continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }


        //public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        //{
        //    if (enumerable == null)
        //    {
        //        return true;
        //    }
        //    /* If this is a list, use the Count property for efficiency. 
        //     * The Count property is O(1) while IEnumerable.Count() is O(N). */
        //    var collection = enumerable as ICollection<T>;
        //    if (collection != null)
        //    {
        //        return collection.Count < 1;
        //    }
        //    return !enumerable.Any();
        //}

        public static string GetUniqueKey(int maxSize)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(maxSize);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString().ToUpper();
        }

        public static string GetNewPasword(int maxSize)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890".ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(maxSize);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public static string GetConfirmCode(int len)
        {
            var chars = "1234567890".ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[len];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(len);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public static List<object[]> DataTableToListObjectArray(this DataTable table)
        {
            try
            {
                var list = new List<object[]>();
                for (var m = 0; m < table.Rows.Count; m++) list.Add(table.Rows[m].ItemArray);
                return list;
            }
            catch
            {
                return null;
            }
        }

        public static Dictionary<string, string> ToDictionary<T>(this T obj)
        {
            return obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => Convert.ToString(prop.GetValue(obj, null)));
        }
        public static T ToObject<T>(this Dictionary<string, string> dict)
        {
            Type type = typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                type.GetProperty(kv.Key).SetValue(obj, kv.Value);
            }
            return (T)obj;
        }


        public static double ToUnixTimestamp(this DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
           new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }
    }
}
