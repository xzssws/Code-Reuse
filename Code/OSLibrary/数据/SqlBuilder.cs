using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace Genersoft.ZJGL.Servers.PublicCom
{
    /// <summary>
    /// 保存 Save 
    /// 插入 Insert
    /// 删除 Delete
    /// 保存由删除旧的和插入新的组成
    /// </summary>
    public class SqlBuilder
    {

        public SqlBuilder()
        {

        }
        private ArrayList excluedColumns = null;
        /// <summary>
        /// 生成保存一行数据的SQL。
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="tableName">表名</param>
        /// <param name="excludeCols">被排除的列</param>	
        /// <returns>保存的SQL</returns>
        private string GenerateSaveSql(DataRow dr, string tableName, string primaryKeys, string excludeCols)
        {
            try
            {

                //采用先删除，后插入的方式

                string delSql = GenerateDeleteSql(dr, tableName, primaryKeys);  //生成Delete SQL				
                string insertSql = GenerateInsertSql(dr, tableName, primaryKeys, excludeCols);  //生成Insert SQL；				
              
                string resultSql = delSql + " ; " + insertSql;
                return resultSql;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        /// <summary>
        /// 生成保存一行数据的SQL。
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="tableName">表名</param>
        /// <param name="excludeCols">被排除的列</param>	
        /// <returns>保存的SQL</returns>
        private string GenerateSql(DataRow dr, string tableName, string primaryKeys, string excludeCols)
        {
            string resultSql = string.Empty;
            try
            {
                //判断该记录是否存在，存在update否则insert  待修改
                if (resultSql=="")
                {
                    resultSql  = GenerateInsertSql(dr, tableName, primaryKeys, excludeCols);  //生成Insert SQL；		
                }
                else
                {
                    resultSql = GenerateUpdateSql(dr, tableName, primaryKeys, excludeCols);
                }                		
                
                return resultSql;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }


        /// <summary>
        /// 判断这一列是否是一个不需要保存的列
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns>True：不要保存。False：要保存</returns>
        private bool IfExcluded(string columnName)
        {           
            if (excluedColumns.IndexOf(columnName.ToUpper()) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 对传入的不需要保存的字符串进行拆分，放在一个数组里面
        /// </summary>
        /// <param name="excludeCols"></param>
        /// <returns></returns>
        private ArrayList GetExcludeCols(string excludeCols)
        {
            ArrayList list = new ArrayList();           
            string[] cols = excludeCols.ToUpper().Split(',');
            for (int i = 0; i < cols.Length; i++)
            {
                list.Add(cols[i]);
            }
            return list;
        }
        /// <summary>
        /// 生成保存多行数据的SQL。
        /// </summary>
        /// <param name="dr">DataSet</param>
        /// <param name="tableName">表名</param>
        /// <param name="excludeCols">被排除的列</param>	
        /// <returns>插入的SQL</returns>
        public string GenerateSaveSql(DataSet ds, string tableName, string primaryKeys, string excludeCols)
        {

            string resultSql = string.Empty;
            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    resultSql += GenerateSaveSql(dr, tableName, primaryKeys, excludeCols) + ";";
                }

            }
            catch (Exception)
            {
                throw new Exception("构造SQL出现异常！");
            }

            if (resultSql.Length != 0)
            {
                resultSql = "begin " + resultSql + " end;";
            }
            return resultSql;
        }

        /// <summary>
        /// 生成插入一条数据的Sql 
        /// </summary>
        /// <param name="dr">DataRow </param>
        /// <param name="tableName">TableName</param>
        /// <param name="primaryKeys">PrimaryKey</param>
        /// <param name="excludeCols">被排出的列，不保存的列</param>
        /// <returns></returns>
        public string GenerateInsertSql(DataRow dr, string tableName, string primaryKeys, string excludeCols)
        {
            excluedColumns = GetExcludeCols(excludeCols);

            string insertSql = "insert into " + tableName + "  (";
            foreach (DataColumn dc in dr.Table.Columns) //对于这个表里的每一列
            {

                if (IfExcluded(dc.ColumnName))   //不要保存则继续
                {
                    continue;
                }
                else
                {
                    insertSql += dc.ColumnName + ",";
                }

            }
            insertSql = insertSql.Substring(0, insertSql.LastIndexOf(","));
            insertSql += ") values (";
            //给各列赋值
            foreach (DataColumn dc in dr.Table.Columns)
            {
                if (IfExcluded(dc.ColumnName))   //不要保存则继续
                {
                    continue;
                }
                else
                {
                    if (dc.DataType != typeof(System.Decimal))
                    {
                        insertSql += "'" + dr[dc.ColumnName].ToString() + "',";
                    }
                    else
                    {
                        if (dr[dc.ColumnName].ToString().Length != 0)
                        {
                            insertSql += Convert.ToDecimal(dr[dc.ColumnName]) + ",";
                        }
                        else
                        {
                            insertSql += 0.0 + ",";
                        }
                    }
                }

            }
            insertSql = insertSql.Substring(0, insertSql.LastIndexOf(","));
            insertSql += ") ";
            return insertSql;
        }

        /// <summary>
        /// 生成删除一条数据的SQL
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKeys">主键</param>
        /// <returns></returns>
        public string GenerateDeleteSql(DataRow dr, string tableName, string primaryKeys)
        {
            string resultSql = "delete from " + tableName + " where ";
            string whereSql = string.Empty;
            try
            {
                string[] keys = primaryKeys.Split(',');
                for (int i = 0; i < keys.Length; i++)
                {
                    whereSql += "AND " + keys[i] + " ='" + dr[keys[i]].ToString() + "'";
                }
                whereSql = whereSql.Substring(3);
                resultSql += whereSql;
                return resultSql;
            }
            catch (Exception)
            {
                throw new Exception("构造SQL出现异常！");
            }
        }

        /// <summary>
        /// 生成更新一条数据的Sql 
        /// </summary>
        /// <param name="dr">DataRow </param>
        /// <param name="tableName">TableName</param>
        /// <param name="primaryKeys">PrimaryKey</param>
        /// <param name="excludeCols">被排出的列，不保存的列</param>
        /// <returns></returns>
        public string GenerateUpdateSql(DataRow dr, string tableName, string primaryKeys, string excludeCols)
        {
            excluedColumns = GetExcludeCols(excludeCols);
            string updateSql = "update " + tableName + " set ";
            foreach (DataColumn dc in dr.Table.Columns) //对于这个表里的每一列
            {
                if (IfExcluded(dc.ColumnName))   //不要保存则继续
                {
                    continue;
                }
                else
                {
                    updateSql += dc.ColumnName + "=";
                    //给各列赋值
                    if (dc.DataType != typeof(System.Decimal))
                    {
                        updateSql += "'" + dr[dc.ColumnName].ToString() + "',";
                    }
                    else
                    {
                        if (dr[dc.ColumnName].ToString().Length != 0)
                        {
                            updateSql += Convert.ToDecimal(dr[dc.ColumnName]) + ",";
                        }
                        else
                        {
                            updateSql += 0.0 + ",";
                        }
                    }
                }
            }
            updateSql = updateSql.Substring(0, updateSql.LastIndexOf(","));
            updateSql += " where ";
            string whereSql = string.Empty;
            string[] keys = primaryKeys.Split(',');
            for (int i = 0; i < keys.Length; i++)
            {
                whereSql += "AND " + keys[i] + " ='" + dr[keys[i]].ToString() + "'";
            }
            whereSql = whereSql.Substring(3);
            updateSql += whereSql;
            return updateSql;
        }


        /// <summary>
        /// 生成保存多行数据的SQL。
        /// </summary>
        /// <param name="dr">DataSet</param>
        /// <param name="tableName">表名</param>
        /// <param name="excludeCols">被排除的列</param>	
        /// <returns>插入的SQL</returns>
        public string GenerateSql(DataSet ds, string tableName, string primaryKeys, string excludeCols)
        {

            string resultSql = string.Empty;
            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    resultSql += GenerateSql(dr, tableName, primaryKeys, excludeCols) + ";";
                }

            }
            catch (Exception)
            {
                throw new Exception("构造SQL出现异常！");
            }

            if (resultSql.Length != 0)
            {
                resultSql = "begin " + resultSql + " end;";
            }
            return resultSql;
        }


    }

}
