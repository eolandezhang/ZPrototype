using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class RECIPE_TYPE_LIST
    {
        #region 查询

        public List<RECIPE_TYPE_LIST_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<RECIPE_TYPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        RECIPE_TYPE_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG,
        RECIPE_TYPE_NAME,
        RECIPE_TYPE_DESC
FROM RECIPE_TYPE_LIST
", null);
        }

        public List<RECIPE_TYPE_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<RECIPE_TYPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        RECIPE_TYPE_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        VALID_FLAG,
        RECIPE_TYPE_NAME,
        RECIPE_TYPE_DESC
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            RECIPE_TYPE_ID,
            FACTORY_ID,
            PRODUCT_TYPE_ID,
            PRODUCT_PROC_TYPE_ID,
            UPDATE_USER,
            UPDATE_DATE,
            VALID_FLAG,
            RECIPE_TYPE_NAME,
            RECIPE_TYPE_DESC
        FROM (  SELECT 
                    RECIPE_TYPE_ID,
                    FACTORY_ID,
                    PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    VALID_FLAG,
                    RECIPE_TYPE_NAME,
                    RECIPE_TYPE_DESC
                FROM RECIPE_TYPE_LIST ) T1,          
            (  SELECT COUNT (1) TOTAL FROM RECIPE_TYPE_LIST ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<RECIPE_TYPE_LIST_Entity> GetDataById(string RECIPE_TYPE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<RECIPE_TYPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        RECIPE_TYPE_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG,
        RECIPE_TYPE_NAME,
        RECIPE_TYPE_DESC
FROM RECIPE_TYPE_LIST
WHERE RECIPE_TYPE_ID=:RECIPE_TYPE_ID 
    AND FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("RECIPE_TYPE_ID",OracleType.VarChar,15,RECIPE_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)
            });
        }

        public List<RECIPE_TYPE_LIST_Entity> GetDataByFactoryId( string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<RECIPE_TYPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        RECIPE_TYPE_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG,
        RECIPE_TYPE_NAME,
        RECIPE_TYPE_DESC
FROM RECIPE_TYPE_LIST
WHERE FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
" + queryStr, new[]{           
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)
            });
        }

        public bool GetDataValidateId(string RECIPE_TYPE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM RECIPE_TYPE_LIST
WHERE RECIPE_TYPE_ID=:RECIPE_TYPE_ID 
    AND FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("RECIPE_TYPE_ID",OracleType.VarChar,15,RECIPE_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(RECIPE_TYPE_LIST_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM RECIPE_TYPE_LIST
 WHERE RECIPE_TYPE_ID=:RECIPE_TYPE_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
",
                new[]{
                    OracleHelper.MakeInParam("RECIPE_TYPE_ID",OracleType.VarChar,15,entity.RECIPE_TYPE_ID),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
                    OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
                    OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID)
                })
                ) > 0;
            if (exist)
            {
                return 0;
            }
            #endregion
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO RECIPE_TYPE_LIST (
        RECIPE_TYPE_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        VALID_FLAG,
        RECIPE_TYPE_NAME,
        RECIPE_TYPE_DESC
)
VALUES (
        :RECIPE_TYPE_ID,
        :FACTORY_ID,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :VALID_FLAG,
        :RECIPE_TYPE_NAME,
        :RECIPE_TYPE_DESC
)
", new[]{
            OracleHelper.MakeInParam("RECIPE_TYPE_ID",OracleType.VarChar,15,entity.RECIPE_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("RECIPE_TYPE_NAME",OracleType.VarChar,15,entity.RECIPE_TYPE_NAME),
            OracleHelper.MakeInParam("RECIPE_TYPE_DESC",OracleType.VarChar,50,entity.RECIPE_TYPE_DESC)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(RECIPE_TYPE_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE RECIPE_TYPE_LIST SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    VALID_FLAG=:VALID_FLAG,
    RECIPE_TYPE_NAME=:RECIPE_TYPE_NAME,
    RECIPE_TYPE_DESC=:RECIPE_TYPE_DESC
 WHERE 
        RECIPE_TYPE_ID=:RECIPE_TYPE_ID AND  
        FACTORY_ID=:FACTORY_ID AND  
        PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND  
        PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
", new[]{
            OracleHelper.MakeInParam("RECIPE_TYPE_ID",OracleType.VarChar,15,entity.RECIPE_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("RECIPE_TYPE_NAME",OracleType.VarChar,15,entity.RECIPE_TYPE_NAME),
            OracleHelper.MakeInParam("RECIPE_TYPE_DESC",OracleType.VarChar,50,entity.RECIPE_TYPE_DESC)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(RECIPE_TYPE_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM RECIPE_TYPE_LIST 
WHERE
    RECIPE_TYPE_ID=:RECIPE_TYPE_ID AND 
    FACTORY_ID=:FACTORY_ID AND 
    PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND 
    PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("RECIPE_TYPE_ID",OracleType.VarChar,15,entity.RECIPE_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID)
            });
        }

        #endregion



    }
}
