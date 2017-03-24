using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Genersoft.ZJGL.Client.PublicCom
{
    public class DataHandle
    {
        public DataHandle()
        { }

        /// <summary>
        /// DataSet和原始的DataSet相比较，是否被修改了。
        /// </summary>
        /// <param name="dsInit">原始DataSet</param>
        /// <param name="ds">要比较的DataSet</param>
        /// <returns></returns>
        public static string IfDataSetHasChanges(DataSet dsInit, DataSet ds)
        {
            string vsRtn = "";

            if ((ds == null) || (dsInit == null))
            {              
                return vsRtn;
            }
            int countInit = dsInit.Tables.Count;
            int count = ds.Tables.Count;
            if (countInit != count)
            {
                vsRtn = "HasChanges";
                return vsRtn;
            }
            int tblIndex = 0;
            DataTable tblInit = null;
            foreach (DataTable tbl in ds.Tables)
            {
                tblInit = dsInit.Tables[tblIndex];
                vsRtn = IfDataTableHasChanges(tblInit, tbl);
                if (vsRtn != "")
                    break;
                tblIndex++;
            }
            return vsRtn;
        }
        /// <summary>
        /// DataSet和原始的DataSet相比较，是否被修改了。
        /// true  修改了，false  没有被修改。
        /// </summary>
        /// <param name="dsInit">原始DataSet</param>
        /// <param name="ds">要比较的DataSet</param>
        /// <returns></returns>
        public static bool DataSetHasChanges(DataSet dsInit, DataSet ds)
        {
            if (string.IsNullOrEmpty(IfDataSetHasChanges(dsInit, ds)))
                return false;
            else
                return true;
        }
        /// <summary>
        /// 判断DataTable是否变化了，和原始的DataTable相比较
        /// </summary>
        /// <param name="tblInit">原始的DataTable</param>
        /// <param name="tbl">要比较的DataTable</param>
        /// <returns></returns>
        public static string IfDataTableHasChanges(DataTable tblInit, DataTable tbl)
        {
            string vsRtn = "";
            if (((tbl == null) && (tblInit != null)) || ((tbl != null) && (tblInit == null)))
            {
                vsRtn = "HasChanges";
                return vsRtn;
            }
            int colCount = tblInit.Columns.Count;
            int Count = tbl.Columns.Count;
            if (colCount != Count)//判断了是否相同
            {
                vsRtn = "HasChanges";
                return vsRtn;
            }
            int rowCount = tblInit.Rows.Count;//判断行是否相同
            Count = tbl.Rows.Count;
            if (rowCount != Count)
            {
                vsRtn = "HasChanges";
                return vsRtn;
            }
            int rowIndex = 0;
            DataRow rowInit = null;
            foreach (DataRow row in tbl.Rows)
            {
                rowInit = tblInit.Rows[rowIndex];
                if (row.RowState == DataRowState.Deleted)
                {
                    vsRtn = "HasChanges";
                    break;
                }
                if (vsRtn != "")
                    break;
                for (int colIndex = 0; colIndex < colCount; colIndex++)
                {
                    if (tblInit.Columns[colIndex].DataType.Name.ToUpper() == "DOUBLE" || tblInit.Columns[colIndex].DataType.Name.ToUpper() == "FLOAT" || tblInit.Columns[colIndex].DataType.Name.ToUpper() == "DECIMAL")
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(rowInit[colIndex])) || string.IsNullOrEmpty(Convert.ToString(row[colIndex])))
                        {
                            if (Convert.ToString(rowInit[colIndex]) != Convert.ToString(row[colIndex]))
                            {
                                vsRtn = "HasChanges";
                                break;
                            }
                        }
                        else
                        {
                            if (Math.Round(Convert.ToDouble(rowInit[colIndex]), 5) != Math.Round(Convert.ToDouble(row[colIndex]), 5))
                            {
                                vsRtn = "HasChanges";
                                break;
                            }
                        }
                    }
                    else
                    if (Convert.ToString(rowInit[colIndex]) != Convert.ToString(row[colIndex]))
                    {
                        vsRtn = "HasChanges";
                        break;
                    }
                }
                rowIndex++;
            }
            return vsRtn;
        }

        /// <summary>
        ///  王豪森 
        /// </summary>
        /// <param name="tblInit">原始的DataTable</param>
        /// <param name="tbl">要比较的DataTable</param>
        /// <returns></returns>
        public static bool IfDataRowHasChanges(DataRow drInit, DataRow drdata)
        {
            try
            {
                bool  flag = true ;
                if (drInit.ItemArray.Length !=drdata.ItemArray.Length)
                {
                    flag =  true;
                }
                for (int i = 0; i < drInit.ItemArray.Length ; i++)
                {
                    if (drInit.ItemArray[i].ToString().Trim() != drdata.ItemArray[i].ToString().Trim() )
                    {
                        flag = true;
                        break;
                    }

                }
                flag = false ;
                return flag;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {

            }

        }

        /// <summary>
        /// 复制一行中的信息到另一个DataRow中
        /// </summary>
        /// <param name="rowInit">要复制的行</param>
        /// <param name="row">要复制到的行</param>
        public static void CopyToDataRow(DataRow rowInit, DataRow row)
        {
            if ((rowInit != null) && (row != null))
            {
                if ((rowInit.RowState != DataRowState.Deleted) && (row.RowState != DataRowState.Deleted))
                {
                    DataTable tbl = row.Table;//要复制到的行的表信息
                    int colCount = rowInit.Table.Columns.Count;
                    int count = tbl.Columns.Count;
                    if (count != colCount) return;
                    string vsColName = "";
                    for (int colIndex = 0; colIndex < count; colIndex++)
                    {//2003/12/01rlx根据列名进行赋值
                        vsColName = tbl.Columns[colIndex].ColumnName;
                        row[vsColName] = rowInit[vsColName];
                    }
                }
            }
        }
    }
    
}
