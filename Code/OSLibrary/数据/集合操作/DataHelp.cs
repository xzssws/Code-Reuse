using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections;

namespace OSLibrary.数据操作
{
    /// <summary>
    /// 数据帮助类
    /// </summary>
    class DataHelp
    {
        /// <summary>
        /// 数据集转HashTable集合
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<Hashtable> GetList(DataTable dt)
        {
            List<Hashtable> mList = new List<Hashtable>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i <= count - 1; i++)
                {
                    Hashtable ht = new Hashtable();
                    foreach (DataColumn col in dt.Columns)
                    {
                        ht.Add(col.ColumnName, dt.Rows[i][col.ColumnName]);
                    }
                    mList.Add(ht);
                }
            }
            return mList;
        }


        /// <summary>
        /// 数据集转实体集合
        /// </summary>
        /// <typeparam name="T">实体类< peparam>
        /// <param name="ds">数据集</param>
        /// <param name="tableIndex">表索引</param>
        /// <returns>实体集合</returns>
        public List<T> DataSetToList<T>(DataSet ds, int tableIndex)
        {
            #region 判断参数是否有误

            if (ds == null || ds.Tables.Count < 0) return null;
            if (tableIndex > ds.Tables.Count - 1) return null;
            if (tableIndex < 0) tableIndex = 0;

            #endregion

            #region 临时载体
            //获取要转换的表
            DataTable dt = ds.Tables[tableIndex];
            // 初始化集合
            List<T> result = new List<T>();
            #endregion

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                //创建一个泛型类型的实例
                T t = (T)Activator.CreateInstance(typeof(T));
                //获取泛型类型所有属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历泛型类型的所有属性
                foreach (PropertyInfo pi in propertys)
                {
                    //遍历DataTable表的每一行对泛型类型实体的属性进行赋值
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        //如果属性名等于数据表名
                        if (pi.Name.Equals(dt.Columns[i].ColumnName))
                        {
                            //设置属性值如果该列值为DBNULL改为NULL 如果存在值则直接赋值过去
                            pi.SetValue(t, dt.Rows[j][i] != DBNull.Value ? dt.Rows[j][i] : null, null);
                        }
                    }
                }
                //向集合中添加改对象
                result.Add(t);
            }
            return result;
        }
        /// <summary>
        /// 数据表转实体集合
        /// </summary>
        /// <typeparam name="T">与数据表对应的实体< peparam>
        /// <param name="datatable">数据表</param>
        /// <returns>转换后的集合</returns>
        public List<T> DataTableToList<T>(DataTable datatable)
        {
            #region 临时载体
            //获取要转换的表
            DataTable dt = datatable;
            // 初始化集合
            List<T> result = new List<T>();
            #endregion

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                //创建一个泛型类型的实例
                T t = (T)Activator.CreateInstance(typeof(T));
                //获取泛型类型所有属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历泛型类型的所有属性
                foreach (PropertyInfo pi in propertys)
                {
                    //遍历DataTable表的每一行对泛型类型实体的属性进行赋值
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        //如果属性名等于数据表名
                        if (pi.Name.Equals(dt.Columns[i].ColumnName))
                        {
                            //设置属性值如果该列值为DBNULL改为NULL 如果存在值则直接赋值过去
                            pi.SetValue(t, dt.Rows[j][i] != DBNull.Value ? dt.Rows[j][i] : null, null);
                        }
                    }
                }
                //向集合中添加改对象
                result.Add(t);
            }
            return result;
        }
        //应用
        public void GO()
        {
            //数据集转实体集合
            DataSetToList<Object>(new DataSet(), 0);
            //数据表转实体集合 
            DataTableToList<object>(new DataTable());
        }
         /// <summary>
      /// DataTable行转列
      /// </summary>
      /// <param name="dtable">需要转换的表</param>
      /// <param name="head">转换表表头对应旧表字段（小写）</param>
      /// <returns></returns>
      public static DataTable DataTableRowtoCon(DataTable dtable, string head)
      {
       DataTable dt = new DataTable();
       dt.Columns.Add("NumberID");
       for (int i = 0; i < dtable.Rows.Count; i++)
       {//设置表头
        dt.Columns.Add(dtable.Rows[i][head].ToString());
       }
       for (int k = 0; k < dtable.Columns.Count; k++)
       {
        string temcol = dtable.Columns[k].ToString();
        if (dtable.Columns[k].ToString().ToLower() != head)//过滤掉设置表头的列
        {
         DataRow new_dr = dt.NewRow();
         new_dr[0] = dtable.Columns[k].ToString();
         for (int j = 0; j < dtable.Rows.Count; j++)
         {
          string temp = dtable.Rows[j][k].ToString();
          new_dr[j + 1] = (Object)temp;
         }
         dt.Rows.Add(new_dr);
        }
       }
       return dt;
      }


    }
}