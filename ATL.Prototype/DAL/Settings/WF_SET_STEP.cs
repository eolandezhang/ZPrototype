using DBUtility;
using Model.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Settings
{
    public class WF_SET_STEP
    {
        #region 查询

        public List<WF_SET_STEP_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<WF_SET_STEP_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        WF_SET_STEP_ID,
        WF_SET_NUM,
        ORDER_NUM,
        AGREE_STEP_ID,
        DISAGREE_STEP_ID,
        PMES_USER_GROUP_ID,
        STEP_FLAG,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG,
        FACTORY_ID,
        WF_SET_STEP_NAME
FROM WF_SET_STEP
", null);
        }

        public List<WF_SET_STEP_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<WF_SET_STEP_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        WF_SET_STEP_ID,
        WF_SET_NUM,
        ORDER_NUM,
        AGREE_STEP_ID,
        DISAGREE_STEP_ID,
        PMES_USER_GROUP_ID,
        STEP_FLAG,
        UPDATE_USER,
        UPDATE_DATE,
        VALID_FLAG,
        FACTORY_ID,WF_SET_STEP_NAME
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            WF_SET_STEP_ID,
            WF_SET_NUM,
            ORDER_NUM,
            AGREE_STEP_ID,
            DISAGREE_STEP_ID,
            PMES_USER_GROUP_ID,
            STEP_FLAG,
            UPDATE_USER,
            UPDATE_DATE,
            VALID_FLAG,
            FACTORY_ID,WF_SET_STEP_NAME
        FROM (  SELECT 
                    WF_SET_STEP_ID,
                    WF_SET_NUM,
                    ORDER_NUM,
                    AGREE_STEP_ID,
                    DISAGREE_STEP_ID,
                    PMES_USER_GROUP_ID,
                    STEP_FLAG,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    VALID_FLAG,
                    FACTORY_ID,WF_SET_STEP_NAME
                FROM WF_SET_STEP ) T1,          
            (  SELECT COUNT (1) TOTAL FROM WF_SET_STEP ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<WF_SET_STEP_Entity> GetDataById(string WF_SET_STEP_ID, string WF_SET_NUM, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<WF_SET_STEP_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        WF_SET_STEP_ID,
        WF_SET_NUM,
        ORDER_NUM,
        AGREE_STEP_ID,
        DISAGREE_STEP_ID,
        PMES_USER_GROUP_ID,
        STEP_FLAG,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG,
        FACTORY_ID,WF_SET_STEP_NAME
FROM WF_SET_STEP
WHERE WF_SET_STEP_ID=:WF_SET_STEP_ID 
    AND WF_SET_NUM=:WF_SET_NUM 
    AND FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("WF_SET_STEP_ID",OracleType.VarChar,20,WF_SET_STEP_ID),
            OracleHelper.MakeInParam("WF_SET_NUM",OracleType.VarChar,20,WF_SET_NUM),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }
        public List<WF_SET_STEP_Entity> GetDataBySetId(string WF_SET_NUM, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<WF_SET_STEP_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        WF_SET_STEP_ID,
        WF_SET_NUM,
        ORDER_NUM,
        AGREE_STEP_ID,
        DISAGREE_STEP_ID,
        PMES_USER_GROUP_ID,
        STEP_FLAG,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG,
        FACTORY_ID,
        WF_SET_STEP_NAME
FROM WF_SET_STEP
WHERE WF_SET_NUM=:WF_SET_NUM 
    AND FACTORY_ID=:FACTORY_ID 
" + queryStr + " ORDER BY ORDER_NUM", new[]{
            OracleHelper.MakeInParam("WF_SET_NUM",OracleType.VarChar,20,WF_SET_NUM),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }
        public bool GetDataValidateId(string WF_SET_STEP_ID, string WF_SET_NUM, string FACTORY_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM WF_SET_STEP
WHERE WF_SET_STEP_ID=:WF_SET_STEP_ID 
    AND WF_SET_NUM=:WF_SET_NUM 
    AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("WF_SET_STEP_ID",OracleType.VarChar,20,WF_SET_STEP_ID),
            OracleHelper.MakeInParam("WF_SET_NUM",OracleType.VarChar,20,WF_SET_NUM),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(WF_SET_STEP_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM WF_SET_STEP
 WHERE WF_SET_STEP_ID=:WF_SET_STEP_ID AND WF_SET_NUM=:WF_SET_NUM AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("WF_SET_STEP_ID",OracleType.VarChar,20,entity.WF_SET_STEP_ID),
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
INSERT INTO WF_SET_STEP (
        WF_SET_STEP_ID,
        WF_SET_NUM,
        ORDER_NUM,
        AGREE_STEP_ID,
        DISAGREE_STEP_ID,
        PMES_USER_GROUP_ID,
        STEP_FLAG,
        UPDATE_USER,
        UPDATE_DATE,
        VALID_FLAG,
        FACTORY_ID,WF_SET_STEP_NAME
)
VALUES (
        :WF_SET_STEP_ID,
        :WF_SET_NUM,
        :ORDER_NUM,
        :AGREE_STEP_ID,
        :DISAGREE_STEP_ID,
        :PMES_USER_GROUP_ID,
        :STEP_FLAG,
        :UPDATE_USER,
        :UPDATE_DATE,
        :VALID_FLAG,
        :FACTORY_ID,:WF_SET_STEP_NAME
)
", new[]{
            OracleHelper.MakeInParam("WF_SET_STEP_ID",OracleType.VarChar,20,entity.WF_SET_STEP_ID),
            OracleHelper.MakeInParam("WF_SET_NUM",OracleType.VarChar,20,entity.WF_SET_NUM),
            OracleHelper.MakeInParam("ORDER_NUM",OracleType.Number,0,entity.ORDER_NUM),
            OracleHelper.MakeInParam("AGREE_STEP_ID",OracleType.VarChar,20,entity.AGREE_STEP_ID),
            OracleHelper.MakeInParam("DISAGREE_STEP_ID",OracleType.VarChar,20,entity.DISAGREE_STEP_ID),
            OracleHelper.MakeInParam("PMES_USER_GROUP_ID",OracleType.VarChar,20,entity.PMES_USER_GROUP_ID),
            OracleHelper.MakeInParam("STEP_FLAG",OracleType.VarChar,10,entity.STEP_FLAG),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("WF_SET_STEP_NAME",OracleType.VarChar,20,entity.WF_SET_STEP_NAME)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(WF_SET_STEP_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE WF_SET_STEP SET 
    ORDER_NUM=:ORDER_NUM,
    AGREE_STEP_ID=:AGREE_STEP_ID,
    DISAGREE_STEP_ID=:DISAGREE_STEP_ID,
    PMES_USER_GROUP_ID=:PMES_USER_GROUP_ID,
    STEP_FLAG=:STEP_FLAG,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    VALID_FLAG=:VALID_FLAG,WF_SET_STEP_NAME=:WF_SET_STEP_NAME
 WHERE 
        WF_SET_STEP_ID=:WF_SET_STEP_ID AND  
        WF_SET_NUM=:WF_SET_NUM AND  
        FACTORY_ID=:FACTORY_ID 
", new[]{
            OracleHelper.MakeInParam("WF_SET_STEP_ID",OracleType.VarChar,20,entity.WF_SET_STEP_ID),
            OracleHelper.MakeInParam("WF_SET_NUM",OracleType.VarChar,20,entity.WF_SET_NUM),
            OracleHelper.MakeInParam("ORDER_NUM",OracleType.Number,0,entity.ORDER_NUM),
            OracleHelper.MakeInParam("AGREE_STEP_ID",OracleType.VarChar,20,entity.AGREE_STEP_ID),
            OracleHelper.MakeInParam("DISAGREE_STEP_ID",OracleType.VarChar,20,entity.DISAGREE_STEP_ID),
            OracleHelper.MakeInParam("PMES_USER_GROUP_ID",OracleType.VarChar,20,entity.PMES_USER_GROUP_ID),
            OracleHelper.MakeInParam("STEP_FLAG",OracleType.VarChar,10,entity.STEP_FLAG),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("WF_SET_STEP_NAME",OracleType.VarChar,20,entity.WF_SET_STEP_NAME)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(WF_SET_STEP_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM WF_SET_STEP 
WHERE
    WF_SET_STEP_ID=:WF_SET_STEP_ID AND 
    WF_SET_NUM=:WF_SET_NUM AND 
    FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("WF_SET_STEP_ID",OracleType.VarChar,20,entity.WF_SET_STEP_ID),
            OracleHelper.MakeInParam("WF_SET_NUM",OracleType.VarChar,20,entity.WF_SET_NUM),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
