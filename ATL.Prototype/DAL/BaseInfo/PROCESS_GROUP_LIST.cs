using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class PROCESS_GROUP_LIST
    {

        #region 查询
        public List<PROCESS_GROUP_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PROCESS_GROUP_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  B.TOTAL,
        PROCESS_GROUP_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        PROCESS_GROUP_NAME,
        PROCESS_GROUP_DESC,
        SEQUENCE_NO,
        VALID_FLAG
FROM (  SELECT ROWNUM AS ROWINDEX,
                PROCESS_GROUP_ID,
                FACTORY_ID,
                PRODUCT_TYPE_ID,
                PRODUCT_PROC_TYPE_ID,
                UPDATE_USER,
                TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                PROCESS_GROUP_NAME,
                PROCESS_GROUP_DESC,
                SEQUENCE_NO,
                VALID_FLAG
        FROM PROCESS_GROUP_LIST
           WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER ) A,
     (  SELECT COUNT (1) TOTAL FROM PROCESS_GROUP_LIST ) B
WHERE A.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE ORDER BY SEQUENCE_NO
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }
        public List<PROCESS_GROUP_LIST_Entity> GetData(string factoryId, string productTypeId, string produceProcTypeId, string queryStr)
        {
            return OracleHelper.SelectedToIList<PROCESS_GROUP_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PROCESS_GROUP_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        PROCESS_GROUP_NAME,
        PROCESS_GROUP_DESC,
        SEQUENCE_NO,
        VALID_FLAG
FROM PROCESS_GROUP_LIST
WHERE FACTORY_ID=:FACTORY_ID
  AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID
  AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
" + queryStr +" ORDER BY SEQUENCE_NO", new[]{ 
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,factoryId),
     OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,productTypeId),
     OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,produceProcTypeId)
 });
        }
        #endregion

        #region 新增

        public int PostAdd(PROCESS_GROUP_LIST_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PROCESS_GROUP_LIST
 WHERE PROCESS_GROUP_ID=:PROCESS_GROUP_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
",
                new[]{
                    OracleHelper.MakeInParam("PROCESS_GROUP_ID",OracleType.VarChar,20,entity.PROCESS_GROUP_ID),
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
INSERT INTO PROCESS_GROUP_LIST (
        PROCESS_GROUP_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        PROCESS_GROUP_NAME,
        PROCESS_GROUP_DESC,
        SEQUENCE_NO,
        VALID_FLAG
)
VALUES (
        :PROCESS_GROUP_ID,
        :FACTORY_ID,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :PROCESS_GROUP_NAME,
        :PROCESS_GROUP_DESC,
        :SEQUENCE_NO,
        :VALID_FLAG
)
", new[]{
            OracleHelper.MakeInParam("PROCESS_GROUP_ID",OracleType.VarChar,20,entity.PROCESS_GROUP_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("PROCESS_GROUP_NAME",OracleType.VarChar,25,entity.PROCESS_GROUP_NAME),
            OracleHelper.MakeInParam("PROCESS_GROUP_DESC",OracleType.VarChar,30,entity.PROCESS_GROUP_DESC),
            OracleHelper.MakeInParam("SEQUENCE_NO",OracleType.Number,0,entity.SEQUENCE_NO),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PROCESS_GROUP_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PROCESS_GROUP_LIST SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    PROCESS_GROUP_NAME=:PROCESS_GROUP_NAME,
    PROCESS_GROUP_DESC=:PROCESS_GROUP_DESC,
    SEQUENCE_NO=:SEQUENCE_NO,
    VALID_FLAG=:VALID_FLAG
 WHERE PROCESS_GROUP_ID=:PROCESS_GROUP_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("PROCESS_GROUP_ID",OracleType.VarChar,20,entity.PROCESS_GROUP_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("PROCESS_GROUP_NAME",OracleType.VarChar,25,entity.PROCESS_GROUP_NAME),
            OracleHelper.MakeInParam("PROCESS_GROUP_DESC",OracleType.VarChar,30,entity.PROCESS_GROUP_DESC),
            OracleHelper.MakeInParam("SEQUENCE_NO",OracleType.Number,0,entity.SEQUENCE_NO),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
            });
        }

        #endregion

        #region 删除
        //请移除多余的 "AND" 和 逗号
        public int PostDelete(PROCESS_GROUP_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PROCESS_GROUP_LIST WHERE PROCESS_GROUP_ID=:PROCESS_GROUP_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("PROCESS_GROUP_ID",OracleType.VarChar,20,entity.PROCESS_GROUP_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID)
            });
        }

        #endregion



    }
}
