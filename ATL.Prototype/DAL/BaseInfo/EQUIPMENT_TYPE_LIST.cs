using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class EQUIPMENT_TYPE_LIST
    {
        #region 查询

        public List<EQUIPMENT_TYPE_LIST_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_TYPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        EQUIPMENT_TYPE_ID,
        FACTORY_ID,
        EQUIPMENT_TYPE_NAME,
        EQUIPMENT_TYPE_DESC,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG
FROM EQUIPMENT_TYPE_LIST
", null);
        }

        public List<EQUIPMENT_TYPE_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_TYPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        EQUIPMENT_TYPE_ID,
        FACTORY_ID,
        EQUIPMENT_TYPE_NAME,
        EQUIPMENT_TYPE_DESC,
        UPDATE_USER,
        UPDATE_DATE,
        VALID_FLAG
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            EQUIPMENT_TYPE_ID,
            FACTORY_ID,
            EQUIPMENT_TYPE_NAME,
            EQUIPMENT_TYPE_DESC,
            UPDATE_USER,
            UPDATE_DATE,
            VALID_FLAG
        FROM (  SELECT 
                    EQUIPMENT_TYPE_ID,
                    FACTORY_ID,
                    EQUIPMENT_TYPE_NAME,
                    EQUIPMENT_TYPE_DESC,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    VALID_FLAG
                FROM EQUIPMENT_TYPE_LIST ) T1,          
            (  SELECT COUNT (1) TOTAL FROM EQUIPMENT_TYPE_LIST ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<EQUIPMENT_TYPE_LIST_Entity> GetDataById(string EQUIPMENT_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_TYPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        EQUIPMENT_TYPE_ID,
        FACTORY_ID,
        EQUIPMENT_TYPE_NAME,
        EQUIPMENT_TYPE_DESC,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG
FROM EQUIPMENT_TYPE_LIST
WHERE EQUIPMENT_TYPE_ID=:EQUIPMENT_TYPE_ID 
    AND FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("EQUIPMENT_TYPE_ID",OracleType.VarChar,20,EQUIPMENT_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<EQUIPMENT_TYPE_LIST_Entity> GetDataByFactoryId(string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_TYPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        EQUIPMENT_TYPE_ID,
        FACTORY_ID,
        EQUIPMENT_TYPE_NAME,
        EQUIPMENT_TYPE_DESC,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG
FROM EQUIPMENT_TYPE_LIST
WHERE FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        #endregion

        #region 新增

        public int PostAdd(EQUIPMENT_TYPE_LIST_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM EQUIPMENT_TYPE_LIST
 WHERE EQUIPMENT_TYPE_ID=:EQUIPMENT_TYPE_ID AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("EQUIPMENT_TYPE_ID",OracleType.VarChar,20,entity.EQUIPMENT_TYPE_ID),
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
INSERT INTO EQUIPMENT_TYPE_LIST (
        EQUIPMENT_TYPE_ID,
        FACTORY_ID,
        EQUIPMENT_TYPE_NAME,
        EQUIPMENT_TYPE_DESC,
        UPDATE_USER,
        UPDATE_DATE,
        VALID_FLAG
)
VALUES (
        :EQUIPMENT_TYPE_ID,
        :FACTORY_ID,
        :EQUIPMENT_TYPE_NAME,
        :EQUIPMENT_TYPE_DESC,
        :UPDATE_USER,
        :UPDATE_DATE,
        :VALID_FLAG
)
", new[]{
            OracleHelper.MakeInParam("EQUIPMENT_TYPE_ID",OracleType.VarChar,20,entity.EQUIPMENT_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("EQUIPMENT_TYPE_NAME",OracleType.VarChar,20,entity.EQUIPMENT_TYPE_NAME),
            OracleHelper.MakeInParam("EQUIPMENT_TYPE_DESC",OracleType.VarChar,25,entity.EQUIPMENT_TYPE_DESC),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(EQUIPMENT_TYPE_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE EQUIPMENT_TYPE_LIST SET 
    EQUIPMENT_TYPE_NAME=:EQUIPMENT_TYPE_NAME,
    EQUIPMENT_TYPE_DESC=:EQUIPMENT_TYPE_DESC,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    VALID_FLAG=:VALID_FLAG
 WHERE 
        EQUIPMENT_TYPE_ID=:EQUIPMENT_TYPE_ID AND  
        FACTORY_ID=:FACTORY_ID 
", new[]{
            OracleHelper.MakeInParam("EQUIPMENT_TYPE_ID",OracleType.VarChar,20,entity.EQUIPMENT_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("EQUIPMENT_TYPE_NAME",OracleType.VarChar,20,entity.EQUIPMENT_TYPE_NAME),
            OracleHelper.MakeInParam("EQUIPMENT_TYPE_DESC",OracleType.VarChar,25,entity.EQUIPMENT_TYPE_DESC),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(EQUIPMENT_TYPE_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM EQUIPMENT_TYPE_LIST 
WHERE
    EQUIPMENT_TYPE_ID=:EQUIPMENT_TYPE_ID AND 
    FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("EQUIPMENT_TYPE_ID",OracleType.VarChar,20,entity.EQUIPMENT_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
