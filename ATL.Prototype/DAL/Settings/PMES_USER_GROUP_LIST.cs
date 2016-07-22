using DBUtility;
using Model.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Settings
{
    public class PMES_USER_GROUP_LIST
    {
        #region 查询

        public List<PMES_USER_GROUP_LIST_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PMES_USER_GROUP_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        PMES_USER_GROUP_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        USER_GROUP_NAME,
        USER_GROUP_DESC,
        VALID_FLAG
FROM PMES_USER_GROUP_LIST
", null);
        }

        public List<PMES_USER_GROUP_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PMES_USER_GROUP_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        PMES_USER_GROUP_ID,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE,
        USER_GROUP_NAME,
        USER_GROUP_DESC,
        VALID_FLAG
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            PMES_USER_GROUP_ID,
            FACTORY_ID,
            UPDATE_USER,
            UPDATE_DATE,
            USER_GROUP_NAME,
            USER_GROUP_DESC,
            VALID_FLAG
        FROM (  SELECT 
                    PMES_USER_GROUP_ID,
                    FACTORY_ID,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    USER_GROUP_NAME,
                    USER_GROUP_DESC,
                    VALID_FLAG
                FROM PMES_USER_GROUP_LIST ) T1,          
            (  SELECT COUNT (1) TOTAL FROM PMES_USER_GROUP_LIST ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PMES_USER_GROUP_LIST_Entity> GetDataById(string PMES_USER_GROUP_ID, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PMES_USER_GROUP_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PMES_USER_GROUP_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        USER_GROUP_NAME,
        USER_GROUP_DESC,
        VALID_FLAG
FROM PMES_USER_GROUP_LIST
WHERE PMES_USER_GROUP_ID=:PMES_USER_GROUP_ID 
    AND FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("PMES_USER_GROUP_ID",OracleType.VarChar,20,PMES_USER_GROUP_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<PMES_USER_GROUP_LIST_Entity> GetDataByFactoryId(string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PMES_USER_GROUP_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PMES_USER_GROUP_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        USER_GROUP_NAME,
        USER_GROUP_DESC,
        VALID_FLAG
FROM PMES_USER_GROUP_LIST
WHERE FACTORY_ID=:FACTORY_ID 
" + queryStr + " ORDER BY PMES_USER_GROUP_ID ", new[]{
           OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public bool GetDataValidateId(string PMES_USER_GROUP_ID, string FACTORY_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM PMES_USER_GROUP_LIST
WHERE PMES_USER_GROUP_ID=:PMES_USER_GROUP_ID 
    AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PMES_USER_GROUP_ID",OracleType.VarChar,20,PMES_USER_GROUP_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(PMES_USER_GROUP_LIST_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PMES_USER_GROUP_LIST
 WHERE PMES_USER_GROUP_ID=:PMES_USER_GROUP_ID AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("PMES_USER_GROUP_ID",OracleType.VarChar,20,entity.PMES_USER_GROUP_ID),
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
INSERT INTO PMES_USER_GROUP_LIST (
        PMES_USER_GROUP_ID,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE,
        USER_GROUP_NAME,
        USER_GROUP_DESC,
        VALID_FLAG
)
VALUES (
        :PMES_USER_GROUP_ID,
        :FACTORY_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :USER_GROUP_NAME,
        :USER_GROUP_DESC,
        :VALID_FLAG
)
", new[]{
            OracleHelper.MakeInParam("PMES_USER_GROUP_ID",OracleType.VarChar,20,entity.PMES_USER_GROUP_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("USER_GROUP_NAME",OracleType.VarChar,20,entity.USER_GROUP_NAME),
            OracleHelper.MakeInParam("USER_GROUP_DESC",OracleType.VarChar,25,entity.USER_GROUP_DESC),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PMES_USER_GROUP_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PMES_USER_GROUP_LIST SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    USER_GROUP_NAME=:USER_GROUP_NAME,
    USER_GROUP_DESC=:USER_GROUP_DESC,
    VALID_FLAG=:VALID_FLAG
 WHERE 
        PMES_USER_GROUP_ID=:PMES_USER_GROUP_ID AND  
        FACTORY_ID=:FACTORY_ID 
", new[]{
            OracleHelper.MakeInParam("PMES_USER_GROUP_ID",OracleType.VarChar,20,entity.PMES_USER_GROUP_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("USER_GROUP_NAME",OracleType.VarChar,20,entity.USER_GROUP_NAME),
            OracleHelper.MakeInParam("USER_GROUP_DESC",OracleType.VarChar,25,entity.USER_GROUP_DESC),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(PMES_USER_GROUP_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PMES_USER_GROUP_LIST 
WHERE
    PMES_USER_GROUP_ID=:PMES_USER_GROUP_ID AND 
    FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PMES_USER_GROUP_ID",OracleType.VarChar,20,entity.PMES_USER_GROUP_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
