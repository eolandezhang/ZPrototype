using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class PROCESS_MATERIAL_INFO
    {

        #region 查询

        public List<PROCESS_MATERIAL_INFO_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PROCESS_MATERIAL_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        MATERIAL_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG
FROM PROCESS_MATERIAL_INFO
", null);
        }

        public List<PROCESS_MATERIAL_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PROCESS_MATERIAL_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  TOTAL, 
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        MATERIAL_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        VALID_FLAG
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            PROCESS_ID,
            FACTORY_ID,
            PRODUCT_TYPE_ID,
            PRODUCT_PROC_TYPE_ID,
            MATERIAL_TYPE_ID,
            UPDATE_USER,
            UPDATE_DATE,
            VALID_FLAG
        FROM (  SELECT 
                    PROCESS_ID,
                    FACTORY_ID,
                    PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID,
                    MATERIAL_TYPE_ID,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    VALID_FLAG
                FROM PROCESS_MATERIAL_INFO ) T1,          
            (  SELECT COUNT (1) TOTAL FROM PROCESS_MATERIAL_INFO ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PROCESS_MATERIAL_INFO_Entity> GetDataByProcessId(string PROCESS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PROCESS_MATERIAL_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        A.PROCESS_ID,
        A.FACTORY_ID,
        A.PRODUCT_TYPE_ID,
        A.PRODUCT_PROC_TYPE_ID,
        A.MATERIAL_TYPE_ID,
        A.UPDATE_USER,
        TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        A.VALID_FLAG,
        B.MATERIAL_TYPE_NAME,
        B.MATERIAL_TYPE_DESC
FROM PROCESS_MATERIAL_INFO A ,MATERIAL_TYPE_LIST B
WHERE PROCESS_ID=:PROCESS_ID         
    AND A.PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND A.PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND A.FACTORY_ID=:FACTORY_ID 
    AND B.FACTORY_ID=A.FACTORY_ID
    AND B.PRODUCT_TYPE_ID=A.PRODUCT_TYPE_ID
    AND B.PRODUCT_PROC_TYPE_ID=A.PRODUCT_PROC_TYPE_ID
    AND B.MATERIAL_TYPE_ID=A.MATERIAL_TYPE_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID), 
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<PROCESS_MATERIAL_INFO_Entity> GetDataByProcessIdAndCategoryId(string PROCESS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string MATERIAL_CATEGORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PROCESS_MATERIAL_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        A.PROCESS_ID,
        A.FACTORY_ID,
        A.PRODUCT_TYPE_ID,
        A.PRODUCT_PROC_TYPE_ID,
        A.MATERIAL_TYPE_ID,
        A.UPDATE_USER,
        TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        A.VALID_FLAG,
        B.MATERIAL_TYPE_NAME,
        B.MATERIAL_TYPE_DESC
FROM PROCESS_MATERIAL_INFO A ,MATERIAL_TYPE_LIST B
WHERE PROCESS_ID=:PROCESS_ID         
    AND A.PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND A.PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND A.FACTORY_ID=:FACTORY_ID 
    AND B.FACTORY_ID=A.FACTORY_ID
    AND B.PRODUCT_TYPE_ID=A.PRODUCT_TYPE_ID
    AND B.PRODUCT_PROC_TYPE_ID=A.PRODUCT_PROC_TYPE_ID
    AND B.MATERIAL_TYPE_ID=A.MATERIAL_TYPE_ID 
    AND B.MATERIAL_CATEGORY_ID=:MATERIAL_CATEGORY_ID
" + queryStr, new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID), 
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("MATERIAL_CATEGORY_ID",OracleType.VarChar,20,MATERIAL_CATEGORY_ID)
            });
        }

        public List<PROCESS_MATERIAL_INFO_Entity> GetDataQuery(string PROCESS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string MATERIAL_CATEGORY_ID, string MATERIAL_TYPE_ID, string MATERIAL_TYPE_NAME, string MATERIAL_TYPE_DESC, string queryStr)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            List<OracleParameter> paramList = new List<OracleParameter>{
             OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID), 
             OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
             OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
             OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)             
            };
            
            if (string.IsNullOrEmpty(MATERIAL_TYPE_NAME))
            {
                str1 = "";
            }
            else
            {
                str1 = " AND UPPER(B.MATERIAL_TYPE_NAME) LIKE '%'||:MATERIAL_TYPE_NAME||'%' ";
                paramList.Add(OracleHelper.MakeInParam("MATERIAL_TYPE_NAME", OracleType.VarChar, MATERIAL_TYPE_NAME.Trim().ToUpper()));
            }
            if (string.IsNullOrEmpty(MATERIAL_TYPE_DESC))
            {
                str2 = "";
            }
            else
            {
                str2 = " AND UPPER(B.MATERIAL_TYPE_DESC) LIKE '%'||:MATERIAL_TYPE_DESC||'%' ";
                paramList.Add(OracleHelper.MakeInParam("MATERIAL_TYPE_DESC", OracleType.VarChar, MATERIAL_TYPE_DESC.Trim().ToUpper()));
            }
            if (string.IsNullOrEmpty(MATERIAL_CATEGORY_ID))
            {
                str3 = "";
            }
            else
            {
                str3 = " AND UPPER(B.MATERIAL_CATEGORY_ID) LIKE '%'||:MATERIAL_CATEGORY_ID||'%' ";
                paramList.Add(OracleHelper.MakeInParam("MATERIAL_CATEGORY_ID", OracleType.VarChar, MATERIAL_CATEGORY_ID.Trim().ToUpper()));
            }

            if (string.IsNullOrEmpty(MATERIAL_TYPE_ID))
            {
                str4 = "";
            }
            else
            {
                str4 = " AND UPPER(B.MATERIAL_TYPE_ID) LIKE '%'||:MATERIAL_TYPE_ID||'%' ";
                paramList.Add(OracleHelper.MakeInParam("MATERIAL_TYPE_ID", OracleType.VarChar, MATERIAL_TYPE_ID.Trim().ToUpper()));
            }
            OracleParameter[] param = param=paramList.ToArray();
            return OracleHelper.SelectedToIList<PROCESS_MATERIAL_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        A.PROCESS_ID,
        A.FACTORY_ID,
        A.PRODUCT_TYPE_ID,
        A.PRODUCT_PROC_TYPE_ID,
        A.MATERIAL_TYPE_ID,
        A.UPDATE_USER,
        TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        A.VALID_FLAG,
        B.MATERIAL_TYPE_NAME,
        B.MATERIAL_TYPE_DESC
FROM PROCESS_MATERIAL_INFO A ,MATERIAL_TYPE_LIST B
WHERE PROCESS_ID=:PROCESS_ID         
    AND A.PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND A.PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND A.FACTORY_ID=:FACTORY_ID 
    AND B.FACTORY_ID=A.FACTORY_ID
    AND B.PRODUCT_TYPE_ID=A.PRODUCT_TYPE_ID
    AND B.PRODUCT_PROC_TYPE_ID=A.PRODUCT_PROC_TYPE_ID
    AND B.MATERIAL_TYPE_ID=A.MATERIAL_TYPE_ID "
    + str1 + str2 + str3
    + queryStr, param);
        }

        public bool GetDataValidateId(string PROCESS_ID, string MATERIAL_TYPE_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM PROCESS_MATERIAL_INFO
WHERE PROCESS_ID=:PROCESS_ID 
    AND MATERIAL_TYPE_ID=:MATERIAL_TYPE_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID),
            OracleHelper.MakeInParam("MATERIAL_TYPE_ID",OracleType.VarChar,15,MATERIAL_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(PROCESS_MATERIAL_INFO_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PROCESS_MATERIAL_INFO
 WHERE PROCESS_ID=:PROCESS_ID AND MATERIAL_TYPE_ID=:MATERIAL_TYPE_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
                    OracleHelper.MakeInParam("MATERIAL_TYPE_ID",OracleType.VarChar,15,entity.MATERIAL_TYPE_ID),
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
INSERT INTO PROCESS_MATERIAL_INFO (
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        MATERIAL_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        VALID_FLAG
)
VALUES (
        :PROCESS_ID,
        :FACTORY_ID,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :MATERIAL_TYPE_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :VALID_FLAG
)
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("MATERIAL_TYPE_ID",OracleType.VarChar,15,entity.MATERIAL_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PROCESS_MATERIAL_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PROCESS_MATERIAL_INFO SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    VALID_FLAG=:VALID_FLAG
 WHERE 
        PROCESS_ID=:PROCESS_ID AND  
        MATERIAL_TYPE_ID=:MATERIAL_TYPE_ID AND  
        PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND  
        PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND  
        FACTORY_ID=:FACTORY_ID 
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("MATERIAL_TYPE_ID",OracleType.VarChar,15,entity.MATERIAL_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
            });
        }

        #endregion

        #region 删除
        //请移除多余的 "AND" 和 逗号
        public int PostDelete(PROCESS_MATERIAL_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PROCESS_MATERIAL_INFO WHERE PROCESS_ID=:PROCESS_ID AND MATERIAL_TYPE_ID=:MATERIAL_TYPE_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("MATERIAL_TYPE_ID",OracleType.VarChar,15,entity.MATERIAL_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
