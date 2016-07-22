using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class PROCESS_MATERIAL_PN_INFO
    {

        #region 查询

        public List<PROCESS_MATERIAL_PN_INFO_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PROCESS_MATERIAL_PN_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        MATERIAL_PN_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PROCESS_MATERIAL_PN_INFO
", null);
        }

        public List<PROCESS_MATERIAL_PN_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PROCESS_MATERIAL_PN_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  B.TOTAL,
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        MATERIAL_PN_ID,
        UPDATE_USER,
        UPDATE_DATE
FROM (  SELECT ROWNUM AS ROWINDEX,
                PROCESS_ID,
                FACTORY_ID,
                PRODUCT_TYPE_ID,
                PRODUCT_PROC_TYPE_ID,
                MATERIAL_PN_ID,
                UPDATE_USER,
                TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
        FROM PROCESS_MATERIAL_PN_INFO
           WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER ) A,
     (  SELECT COUNT (1) TOTAL FROM PROCESS_MATERIAL_PN_INFO ) B
WHERE A.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PROCESS_MATERIAL_PN_INFO_Entity> GetDataByProcessId(string PROCESS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PROCESS_MATERIAL_PN_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        MATERIAL_PN_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PROCESS_MATERIAL_PN_INFO
WHERE PROCESS_ID=:PROCESS_ID     
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID),           
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<PROCESS_MATERIAL_PN_INFO_Entity> GetDataQuery(string PROCESS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string MATERIAL_CATEGORY_ID, string MATERIAL_TYPE_ID, string MATERIAL_PN_ID, string MATERIAL_PN_NAME, string MATERIAL_PN_DESC, string queryStr)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            List<OracleParameter> paramList = new List<OracleParameter>{
             OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID), 
             OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
             OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
             OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)             
            };

            if (string.IsNullOrEmpty(MATERIAL_CATEGORY_ID))
            {
                str1 = "";
            }
            else
            {
                str1 = " AND B.MATERIAL_CATEGORY_ID LIKE '%'||:MATERIAL_CATEGORY_ID||'%' ";
                paramList.Add(OracleHelper.MakeInParam("MATERIAL_CATEGORY_ID", OracleType.VarChar, MATERIAL_CATEGORY_ID.Trim().ToUpper()));
            }
            if (string.IsNullOrEmpty(MATERIAL_TYPE_ID))
            {
                str2 = "";
            }
            else
            {
                str2 = " AND C.MATERIAL_TYPE_ID LIKE '%'||:MATERIAL_TYPE_ID||'%' ";
                paramList.Add(OracleHelper.MakeInParam("MATERIAL_TYPE_ID", OracleType.VarChar, MATERIAL_TYPE_ID.Trim().ToUpper()));
            }
            if (string.IsNullOrEmpty(MATERIAL_PN_ID))
            {
                str3 = "";
            }
            else
            {
                str3 = " AND C.MATERIAL_PN_ID LIKE '%'||:MATERIAL_PN_ID||'%' ";
                paramList.Add(OracleHelper.MakeInParam("MATERIAL_PN_ID", OracleType.VarChar, MATERIAL_PN_ID.Trim().ToUpper()));
            }

            if (string.IsNullOrEmpty(MATERIAL_PN_NAME))
            {
                str4 = "";
            }
            else
            {
                str4 = " AND C.MATERIAL_PN_NAME LIKE '%'||:MATERIAL_PN_NAME||'%' ";
                paramList.Add(OracleHelper.MakeInParam("MATERIAL_PN_NAME", OracleType.VarChar, MATERIAL_PN_NAME.Trim().ToUpper()));
            }

            if (string.IsNullOrEmpty(MATERIAL_PN_DESC))
            {
                str5 = "";
            }
            else
            {
                str5 = " AND C.MATERIAL_PN_DESC LIKE '%'||:MATERIAL_PN_DESC||'%'  ";
                paramList.Add(OracleHelper.MakeInParam("MATERIAL_PN_DESC", OracleType.VarChar, MATERIAL_PN_DESC.Trim().ToUpper()));
            }
            OracleParameter[] param = param = paramList.ToArray();
            return OracleHelper.SelectedToIList<PROCESS_MATERIAL_PN_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        A.PROCESS_ID,
        A.FACTORY_ID,
        A.PRODUCT_TYPE_ID,
        A.PRODUCT_PROC_TYPE_ID,
        A.MATERIAL_PN_ID,
        A.UPDATE_USER,
        TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        C.MATERIAL_TYPE_ID,        
        C.MATERIAL_PN_NAME,
        C.MATERIAL_PN_DESC
FROM PROCESS_MATERIAL_PN_INFO A,MATERIAL_TYPE_LIST B,MATERIAL_PN_LIST C
WHERE A.PROCESS_ID=:PROCESS_ID     
    AND A.PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND A.PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND A.FACTORY_ID=:FACTORY_ID
    AND B.MATERIAL_TYPE_ID=C.MATERIAL_TYPE_ID
    AND B.FACTORY_ID=A.FACTORY_ID
    AND B.PRODUCT_TYPE_ID=A.PRODUCT_TYPE_ID
    AND B.PRODUCT_PROC_TYPE_ID=A.PRODUCT_PROC_TYPE_ID
    AND C.FACTORY_ID=A.FACTORY_ID 
    AND C.PRODUCT_TYPE_ID=A.PRODUCT_TYPE_ID
    AND C.PRODUCT_PROC_TYPE_ID=A.PRODUCT_PROC_TYPE_ID    
    AND C.MATERIAL_PN_ID=A.MATERIAL_PN_ID "
                + str1 + str2 + str3 + str4 + str5 + queryStr, param);
        }

        public List<PROCESS_MATERIAL_PN_INFO_Entity> GetDataSearchById(string PROCESS_ID, string MATERIAL_PN_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PROCESS_MATERIAL_PN_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        MATERIAL_PN_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PROCESS_MATERIAL_PN_INFO
WHERE PROCESS_ID=:PROCESS_ID     
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND FACTORY_ID=:FACTORY_ID 
" + " AND UPPER(MATERIAL_PN_ID) LIKE '%" + MATERIAL_PN_ID.ToUpper() + "%'" + queryStr, new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID),           
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        #endregion

        #region 新增

        public int PostAdd(PROCESS_MATERIAL_PN_INFO_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PROCESS_MATERIAL_PN_INFO
 WHERE PROCESS_ID=:PROCESS_ID AND MATERIAL_PN_ID=:MATERIAL_PN_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
                    OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,entity.MATERIAL_PN_ID),
                    OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
                    OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
                })
                ) > 0;
            if (exist)
            {
                return 0;
            }
            #endregion
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO PROCESS_MATERIAL_PN_INFO (
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        MATERIAL_PN_ID,
        UPDATE_USER,
        UPDATE_DATE
)
VALUES (
        :PROCESS_ID,
        :FACTORY_ID,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :MATERIAL_PN_ID,
        :UPDATE_USER,
        :UPDATE_DATE
)
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,entity.MATERIAL_PN_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PROCESS_MATERIAL_PN_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PROCESS_MATERIAL_PN_INFO SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE
 WHERE PROCESS_ID=:PROCESS_ID AND MATERIAL_PN_ID=:MATERIAL_PN_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,entity.MATERIAL_PN_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
            });
        }

        #endregion

        #region 删除
        //请移除多余的 "AND" 和 逗号
        public int PostDelete(PROCESS_MATERIAL_PN_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PROCESS_MATERIAL_PN_INFO WHERE PROCESS_ID=:PROCESS_ID AND MATERIAL_PN_ID=:MATERIAL_PN_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,entity.MATERIAL_PN_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
