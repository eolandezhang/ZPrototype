using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBUtility
{
    /// <summary>
    /// 封闭一个不具有返回类型的委托
    /// </summary>
    /// <typeparam name="T">IDataDreader</typeparam>
    /// <param name="IDataDreader">IDataDreader</param>
    public delegate void Func<T>(T IDataDreader) where T : IDataReader;

    public delegate T Load<T>(IDataRecord dataRecord);

    /// <summary>
    /// 数据类型转换
    /// </summary>
    public static class DataReaderConvert
    {
        public static T BuildEntity<T>(IDataRecord record) where T : new()
        {
            DynamicBuilder<T> result = DynamicBuilder<T>.CreateBuilder(record);
            return result.Build(record);
        }

        /// <summary>
        /// DataReader中,取得某一列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static T Field<T>(this IDataRecord record, string fieldName)
        {
            T fieldValue = default(T);
            for (int i = 0; i < record.FieldCount; i++)
            {
                if (string.Equals(record.GetName(i), fieldName, StringComparison.OrdinalIgnoreCase))
                {
                    if (record[i] != DBNull.Value)
                    {
                        fieldValue = (T)record[fieldName];
                    }
                }
            }
            return fieldValue;
        }

        /// <summary>
        /// DataReader中的列名和值,存到list中
        /// </summary>
        /// <param name="record">IDataReader</param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> ToTable(this IDataReader record)
        {
            List<Dictionary<string, object>> Result = new List<Dictionary<string, object>>();
            while (record.Read())
            {
                Dictionary<string, object> column = new Dictionary<string, object>();
                for (int i = 0; i < record.FieldCount; i++)
                {
                    if (!column.ContainsKey(record.GetName(i)))
                        column.Add(record.GetName(i), record.GetValue(i));
                    else
                    {
                        throw new Exception("查询语句中出现重复的列!");
                    }
                }
                Result.Add(column);
            }
            return Result;
        }

        public static IEnumerable<T> GetEnumerator<T>(this IDataReader reader, Func<IDataRecord, T> generator)
        {
            while (reader.Read())
                yield return generator(reader);
        }

        public static IEnumerable<T> GetEnumeratorEntity<T>(this IDataReader reader) where T : new()
        {
            while (reader.Read())
                yield return BuildEntity<T>(reader);
        }

        /// <summary>
        /// 发射快速数据填充,注意:有发射器缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public static List<T> GetIListEntity<T>(this IDataReader reader, Load<T> invoker) where T : new()
        {
            List<T> result = new List<T>();
            while (reader.Read())
            {
                result.Add(invoker(reader));
            }
            return result;
        }
    }
}
