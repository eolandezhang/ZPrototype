using DBUtility;
using Model.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Settings
{
    public class WF_SET
    {
        #region 查询

        public List<WF_SET_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<WF_SET_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        WF_SET_NUM,
        WF_SET_NAME,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG,
        FACTORY_ID
FROM WF_SET
", null);
        }

        public List<WF_SET_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<WF_SET_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        WF_SET_NUM,
        WF_SET_NAME,
        UPDATE_USER,
        UPDATE_DATE,
        VALID_FLAG,
        FACTORY_ID
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            WF_SET_NUM,
            WF_SET_NAME,
            UPDATE_USER,
            UPDATE_DATE,
            VALID_FLAG,
            FACTORY_ID
        FROM (  SELECT 
                    WF_SET_NUM,
                    WF_SET_NAME,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    VALID_FLAG,
                    FACTORY_ID
                FROM WF_SET ) T1,          
            (  SELECT COUNT (1) TOTAL FROM WF_SET ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<WF_SET_Entity> GetDataById(string WF_SET_NUM, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<WF_SET_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        WF_SET_NUM,
        WF_SET_NAME,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG,
        FACTORY_ID
FROM WF_SET
WHERE WF_SET_NUM=:WF_SET_NUM 
    AND FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("WF_SET_NUM",OracleType.VarChar,20,WF_SET_NUM),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }
        public List<WF_SET_Entity> GetDataByFactoryId(string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<WF_SET_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        WF_SET_NUM,
        WF_SET_NAME,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG,
        FACTORY_ID
FROM WF_SET
WHERE FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }
        public bool GetDataValidateId(string WF_SET_NUM, string FACTORY_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM WF_SET
WHERE WF_SET_NUM=:WF_SET_NUM 
    AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("WF_SET_NUM",OracleType.VarChar,20,WF_SET_NUM),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(WF_SET_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM WF_SET
 WHERE WF_SET_NUM=:WF_SET_NUM AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("WF_SET_NUM",OracleType.VarChar,20,entity.WF_SET_NUM),
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
INSERT INTO WF_SET (
        WF_SET_NUM,
        WF_SET_NAME,
        UPDATE_USER,
        UPDATE_DATE,
        VALID_FLAG,
        FACTORY_ID
)
VALUES (
        :WF_SET_NUM,
        :WF_SET_NAME,
        :UPDATE_USER,
        :UPDATE_DATE,
        :VALID_FLAG,
        :FACTORY_ID
)
", new[]{
            OracleHelper.MakeInParam("WF_SET_NUM",OracleType.VarChar,20,entity.WF_SET_NUM),
            OracleHelper.MakeInParam("WF_SET_NAME",OracleType.VarChar,30,entity.WF_SET_NAME),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(WF_SET_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE WF_SET SET 
    WF_SET_NAME=:WF_SET_NAME,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    VALID_FLAG=:VALID_FLAG
 WHERE 
        WF_SET_NUM=:WF_SET_NUM AND  
        FACTORY_ID=:FACTORY_ID 
", new[]{
            OracleHelper.MakeInParam("WF_SET_NUM",OracleType.VarChar,20,entity.WF_SET_NUM),
            OracleHelper.MakeInParam("WF_SET_NAME",OracleType.VarChar,30,entity.WF_SET_NAME),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(WF_SET_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM WF_SET 
WHERE
    WF_SET_NUM=:WF_SET_NUM AND 
    FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("WF_SET_NUM",OracleType.VarChar,20,entity.WF_SET_NUM),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
