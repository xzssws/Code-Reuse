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
        /// DataSet��ԭʼ��DataSet��Ƚϣ��Ƿ��޸��ˡ�
        /// </summary>
        /// <param name="dsInit">ԭʼDataSet</param>
        /// <param name="ds">Ҫ�Ƚϵ�DataSet</param>
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
        /// DataSet��ԭʼ��DataSet��Ƚϣ��Ƿ��޸��ˡ�
        /// true  �޸��ˣ�false  û�б��޸ġ�
        /// </summary>
        /// <param name="dsInit">ԭʼDataSet</param>
        /// <param name="ds">Ҫ�Ƚϵ�DataSet</param>
        /// <returns></returns>
        public static bool DataSetHasChanges(DataSet dsInit, DataSet ds)
        {
            if (string.IsNullOrEmpty(IfDataSetHasChanges(dsInit, ds)))
                return false;
            else
                return true;
        }
        /// <summary>
        /// �ж�DataTable�Ƿ�仯�ˣ���ԭʼ��DataTable��Ƚ�
        /// </summary>
        /// <param name="tblInit">ԭʼ��DataTable</param>
        /// <param name="tbl">Ҫ�Ƚϵ�DataTable</param>
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
            if (colCount != Count)//�ж����Ƿ���ͬ
            {
                vsRtn = "HasChanges";
                return vsRtn;
            }
            int rowCount = tblInit.Rows.Count;//�ж����Ƿ���ͬ
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
        ///  ����ɭ 
        /// </summary>
        /// <param name="tblInit">ԭʼ��DataTable</param>
        /// <param name="tbl">Ҫ�Ƚϵ�DataTable</param>
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
        /// ����һ���е���Ϣ����һ��DataRow��
        /// </summary>
        /// <param name="rowInit">Ҫ���Ƶ���</param>
        /// <param name="row">Ҫ���Ƶ�����</param>
        public static void CopyToDataRow(DataRow rowInit, DataRow row)
        {
            if ((rowInit != null) && (row != null))
            {
                if ((rowInit.RowState != DataRowState.Deleted) && (row.RowState != DataRowState.Deleted))
                {
                    DataTable tbl = row.Table;//Ҫ���Ƶ����еı���Ϣ
                    int colCount = rowInit.Table.Columns.Count;
                    int count = tbl.Columns.Count;
                    if (count != colCount) return;
                    string vsColName = "";
                    for (int colIndex = 0; colIndex < count; colIndex++)
                    {//2003/12/01rlx�����������и�ֵ
                        vsColName = tbl.Columns[colIndex].ColumnName;
                        row[vsColName] = rowInit[vsColName];
                    }
                }
            }
        }
    }
    
}
