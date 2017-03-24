using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace Genersoft.ZJGL.Servers.PublicCom
{
    /// <summary>
    /// ���� Save 
    /// ���� Insert
    /// ɾ�� Delete
    /// ������ɾ���ɵĺͲ����µ����
    /// </summary>
    public class SqlBuilder
    {

        public SqlBuilder()
        {

        }
        private ArrayList excluedColumns = null;
        /// <summary>
        /// ���ɱ���һ�����ݵ�SQL��
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="tableName">����</param>
        /// <param name="excludeCols">���ų�����</param>	
        /// <returns>�����SQL</returns>
        private string GenerateSaveSql(DataRow dr, string tableName, string primaryKeys, string excludeCols)
        {
            try
            {

                //������ɾ���������ķ�ʽ

                string delSql = GenerateDeleteSql(dr, tableName, primaryKeys);  //����Delete SQL				
                string insertSql = GenerateInsertSql(dr, tableName, primaryKeys, excludeCols);  //����Insert SQL��				
              
                string resultSql = delSql + " ; " + insertSql;
                return resultSql;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        /// <summary>
        /// ���ɱ���һ�����ݵ�SQL��
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="tableName">����</param>
        /// <param name="excludeCols">���ų�����</param>	
        /// <returns>�����SQL</returns>
        private string GenerateSql(DataRow dr, string tableName, string primaryKeys, string excludeCols)
        {
            string resultSql = string.Empty;
            try
            {
                //�жϸü�¼�Ƿ���ڣ�����update����insert  ���޸�
                if (resultSql=="")
                {
                    resultSql  = GenerateInsertSql(dr, tableName, primaryKeys, excludeCols);  //����Insert SQL��		
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
        /// �ж���һ���Ƿ���һ������Ҫ�������
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns>True����Ҫ���档False��Ҫ����</returns>
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
        /// �Դ���Ĳ���Ҫ������ַ������в�֣�����һ����������
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
        /// ���ɱ���������ݵ�SQL��
        /// </summary>
        /// <param name="dr">DataSet</param>
        /// <param name="tableName">����</param>
        /// <param name="excludeCols">���ų�����</param>	
        /// <returns>�����SQL</returns>
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
                throw new Exception("����SQL�����쳣��");
            }

            if (resultSql.Length != 0)
            {
                resultSql = "begin " + resultSql + " end;";
            }
            return resultSql;
        }

        /// <summary>
        /// ���ɲ���һ�����ݵ�Sql 
        /// </summary>
        /// <param name="dr">DataRow </param>
        /// <param name="tableName">TableName</param>
        /// <param name="primaryKeys">PrimaryKey</param>
        /// <param name="excludeCols">���ų����У����������</param>
        /// <returns></returns>
        public string GenerateInsertSql(DataRow dr, string tableName, string primaryKeys, string excludeCols)
        {
            excluedColumns = GetExcludeCols(excludeCols);

            string insertSql = "insert into " + tableName + "  (";
            foreach (DataColumn dc in dr.Table.Columns) //������������ÿһ��
            {

                if (IfExcluded(dc.ColumnName))   //��Ҫ���������
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
            //�����и�ֵ
            foreach (DataColumn dc in dr.Table.Columns)
            {
                if (IfExcluded(dc.ColumnName))   //��Ҫ���������
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
        /// ����ɾ��һ�����ݵ�SQL
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="tableName">����</param>
        /// <param name="primaryKeys">����</param>
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
                throw new Exception("����SQL�����쳣��");
            }
        }

        /// <summary>
        /// ���ɸ���һ�����ݵ�Sql 
        /// </summary>
        /// <param name="dr">DataRow </param>
        /// <param name="tableName">TableName</param>
        /// <param name="primaryKeys">PrimaryKey</param>
        /// <param name="excludeCols">���ų����У����������</param>
        /// <returns></returns>
        public string GenerateUpdateSql(DataRow dr, string tableName, string primaryKeys, string excludeCols)
        {
            excluedColumns = GetExcludeCols(excludeCols);
            string updateSql = "update " + tableName + " set ";
            foreach (DataColumn dc in dr.Table.Columns) //������������ÿһ��
            {
                if (IfExcluded(dc.ColumnName))   //��Ҫ���������
                {
                    continue;
                }
                else
                {
                    updateSql += dc.ColumnName + "=";
                    //�����и�ֵ
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
        /// ���ɱ���������ݵ�SQL��
        /// </summary>
        /// <param name="dr">DataSet</param>
        /// <param name="tableName">����</param>
        /// <param name="excludeCols">���ų�����</param>	
        /// <returns>�����SQL</returns>
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
                throw new Exception("����SQL�����쳣��");
            }

            if (resultSql.Length != 0)
            {
                resultSql = "begin " + resultSql + " end;";
            }
            return resultSql;
        }


    }

}
