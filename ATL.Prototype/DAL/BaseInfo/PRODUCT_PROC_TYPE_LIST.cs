using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class PRODUCT_PROC_TYPE_LIST
    {
        #region 查询

        public List<PRODUCT_PROC_TYPE_LIST_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PRODUCT_PROC_TYPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PRODUCT_PROC_TYPE_ID,
        PRODUCT_TYPE_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        PRODUCT_PROC_TYPE_NAME,
        PRODUCT_PROC_TYPE_DESC,
        VALID_FLAG
FROM PRODUCT_PROC_TYPE_LIST
", null);
        }

        public List<PRODUCT_PROC_TYPE_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PRODUCT_PROC_TYPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  B.TOTAL,
        PRODUCT_PROC_TYPE_ID,
        PRODUCT_TYPE_ID,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE,
        PRODUCT_PROC_TYPE_NAME,
        PRODUCT_PROC_TYPE_DESC,
        VALID_FLAG
FROM (  SELECT ROWNUM AS ROWINDEX,
                PRODUCT_PROC_TYPE_ID,
                PRODUCT_TYPE_ID,
                FACTORY_ID,
                UPDATE_USER,
                TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                PRODUCT_PROC_TYPE_NAME,
                PRODUCT_PROC_TYPE_DESC,
                VALID_FLAG
        FROM PRODUCT_PROC_TYPE_LIST
           WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER ) A,
     (  SELECT COUNT (1) TOTAL FROM PRODUCT_PROC_TYPE_LIST ) B
WHERE A.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PRODUCT_PROC_TYPE_LIST_Entity> GetDataByProductTypeId(string PRODUCT_TYPE_ID, string FACTORY_ID)
        {
            return OracleHelper.SelectedToIList<PRODUCT_PROC_TYPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PRODUCT_PROC_TYPE_ID,
        PRODUCT_TYPE_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        PRODUCT_PROC_TYPE_NAME,
        PRODUCT_PROC_TYPE_DESC,
        VALID_FLAG
FROM PRODUCT_PROC_TYPE_LIST
WHERE PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND FACTORY_ID=:FACTORY_ID
    AND VALID_FLAG='1'
ORDER BY PRODUCT_PROC_TYPE_ID
", new[]{           
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        #endregion

        #region 新增

        public int PostAdd(PRODUCT_PROC_TYPE_LIST_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PRODUCT_PROC_TYPE_LIST
 WHERE PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
                    OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
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
INSERT INTO PRODUCT_PROC_TYPE_LIST (
        PRODUCT_PROC_TYPE_ID,
        PRODUCT_TYPE_ID,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE,
        PRODUCT_PROC_TYPE_NAME,
        PRODUCT_PROC_TYPE_DESC,
        VALID_FLAG
)
VALUES (
        :PRODUCT_PROC_TYPE_ID,
        :PRODUCT_TYPE_ID,
        :FACTORY_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :PRODUCT_PROC_TYPE_NAME,
        :PRODUCT_PROC_TYPE_DESC,
        :VALID_FLAG
)
", new[]{
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_NAME",OracleType.VarChar,20,entity.PRODUCT_PROC_TYPE_NAME),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_DESC",OracleType.VarChar,25,entity.PRODUCT_PROC_TYPE_DESC),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PRODUCT_PROC_TYPE_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PRODUCT_PROC_TYPE_LIST SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    PRODUCT_PROC_TYPE_NAME=:PRODUCT_PROC_TYPE_NAME,
    PRODUCT_PROC_TYPE_DESC=:PRODUCT_PROC_TYPE_DESC,
    VALID_FLAG=:VALID_FLAG
 WHERE PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_NAME",OracleType.VarChar,20,entity.PRODUCT_PROC_TYPE_NAME),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_DESC",OracleType.VarChar,25,entity.PRODUCT_PROC_TYPE_DESC),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(PRODUCT_PROC_TYPE_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PRODUCT_PROC_TYPE_LIST WHERE PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
