using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace TenderSearch.Business.Extensions
{
    public static class AdoExtensions
    {
        public static List<T> CreateListFromTable<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = row.GetItem<T>();
                data.Add(item);
            }

            return data;
        }

        private static T GetItem<T>(this DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }

            return obj;
        }
    }
}
