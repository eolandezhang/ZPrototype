using DBUtility;
using Model.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Settings
{
    public class PMES_TASK_LIST
    {
        #region 查询

        public List<PMES_TASK_LIST_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PMES_TASK_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        PMES_TASK_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        TASK_NAME,
        TASK_DESC,
        PROGRAM_NAME,
        MODULE_NAME,
        FUNCTIONS,
        VALID_FLAG,
        MENU_NAME,
        MENU_LAYER,
        PARENT_MENU
FROM PMES_TASK_LIST
", null);
        }

        public List<PMES_TASK_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PMES_TASK_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        PMES_TASK_ID,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE,
        TASK_NAME,
        TASK_DESC,
        PROGRAM_NAME,
        MODULE_NAME,
        FUNCTIONS,
        VALID_FLAG,
        MENU_NAME,
        MENU_LAYER,
        PARENT_MENU
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            PMES_TASK_ID,
            FACTORY_ID,
            UPDATE_USER,
            UPDATE_DATE,
            TASK_NAME,
            TASK_DESC,
            PROGRAM_NAME,
            MODULE_NAME,
            FUNCTIONS,
            VALID_FLAG,
            MENU_NAME,
            MENU_LAYER,
            PARENT_MENU
        FROM (  SELECT 
                    PMES_TASK_ID,
                    FACTORY_ID,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    TASK_NAME,
                    TASK_DESC,
                    PROGRAM_NAME,
                    MODULE_NAME,
                    FUNCTIONS,
                    VALID_FLAG,
                    MENU_NAME,
                    MENU_LAYER,
                    PARENT_MENU
                FROM PMES_TASK_LIST ) T1,          
            (  SELECT COUNT (1) TOTAL FROM PMES_TASK_LIST ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PMES_TASK_LIST_Entity> GetDataById(string PMES_TASK_ID, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PMES_TASK_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PMES_TASK_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        TASK_NAME,
        TASK_DESC,
        PROGRAM_NAME,
        MODULE_NAME,
        FUNCTIONS,
        VALID_FLAG,
        MENU_NAME,
        MENU_LAYER,
        PARENT_MENU
FROM PMES_TASK_LIST
WHERE PMES_TASK_ID=:PMES_TASK_ID 
    AND FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("PMES_TASK_ID",OracleType.VarChar,20,PMES_TASK_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<PMES_TASK_LIST_Entity> GetDataByFactoryId(string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PMES_TASK_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PMES_TASK_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        TASK_NAME,
        TASK_DESC,
        PROGRAM_NAME,
        MODULE_NAME,
        FUNCTIONS,
        VALID_FLAG,
        MENU_NAME,
        MENU_LAYER,
        PARENT_MENU
FROM PMES_TASK_LIST
WHERE  FACTORY_ID=:FACTORY_ID 
ORDER BY PMES_TASK_ID
" + queryStr, new[]{           
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public bool GetDataValidateId(string PMES_TASK_ID, string FACTORY_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM PMES_TASK_LIST
WHERE PMES_TASK_ID=:PMES_TASK_ID 
    AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PMES_TASK_ID",OracleType.VarChar,20,PMES_TASK_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(PMES_TASK_LIST_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PMES_TASK_LIST
 WHERE PMES_TASK_ID=:PMES_TASK_ID AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("PMES_TASK_ID",OracleType.VarChar,20,entity.PMES_TASK_ID),
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
INSERT INTO PMES_TASK_LIST (
        PMES_TASK_ID,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE,
        TASK_NAME,
        TASK_DESC,
        PROGRAM_NAME,
        MODULE_NAME,
        FUNCTIONS,
        VALID_FLAG,
        MENU_NAME,
        MENU_LAYER,
        PARENT_MENU
)
VALUES (
        :PMES_TASK_ID,
        :FACTORY_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :TASK_NAME,
        :TASK_DESC,
        :PROGRAM_NAME,
        :MODULE_NAME,
        :FUNCTIONS,
        :VALID_FLAG,
        :MENU_NAME,
        :MENU_LAYER,
        :PARENT_MENU
)
", new[]{
            OracleHelper.MakeInParam("PMES_TASK_ID",OracleType.VarChar,20,entity.PMES_TASK_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("TASK_NAME",OracleType.VarChar,20,entity.TASK_NAME),
            OracleHelper.MakeInParam("TASK_DESC",OracleType.VarChar,25,entity.TASK_DESC),
            OracleHelper.MakeInParam("PROGRAM_NAME",OracleType.VarChar,25,entity.PROGRAM_NAME),
            OracleHelper.MakeInParam("MODULE_NAME",OracleType.VarChar,25,entity.MODULE_NAME),
            OracleHelper.MakeInParam("FUNCTIONS",OracleType.VarChar,50,entity.FUNCTIONS),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("MENU_NAME",OracleType.VarChar,25,entity.MENU_NAME),
            OracleHelper.MakeInParam("MENU_LAYER",OracleType.Number,0,entity.MENU_LAYER),
            OracleHelper.MakeInParam("PARENT_MENU",OracleType.VarChar,25,entity.PARENT_MENU)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PMES_TASK_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PMES_TASK_LIST SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    TASK_NAME=:TASK_NAME,
    TASK_DESC=:TASK_DESC,
    PROGRAM_NAME=:PROGRAM_NAME,
    MODULE_NAME=:MODULE_NAME,
    FUNCTIONS=:FUNCTIONS,
    VALID_FLAG=:VALID_FLAG,
    MENU_NAME=:MENU_NAME,
    MENU_LAYER=:MENU_LAYER,
    PARENT_MENU=:PARENT_MENU
 WHERE 
        PMES_TASK_ID=:PMES_TASK_ID AND  
        FACTORY_ID=:FACTORY_ID 
", new[]{
            OracleHelper.MakeInParam("PMES_TASK_ID",OracleType.VarChar,20,entity.PMES_TASK_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("TASK_NAME",OracleType.VarChar,20,entity.TASK_NAME),
            OracleHelper.MakeInParam("TASK_DESC",OracleType.VarChar,25,entity.TASK_DESC),
            OracleHelper.MakeInParam("PROGRAM_NAME",OracleType.VarChar,25,entity.PROGRAM_NAME),
            OracleHelper.MakeInParam("MODULE_NAME",OracleType.VarChar,25,entity.MODULE_NAME),
            OracleHelper.MakeInParam("FUNCTIONS",OracleType.VarChar,50,entity.FUNCTIONS),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("MENU_NAME",OracleType.VarChar,25,entity.MENU_NAME),
            OracleHelper.MakeInParam("MENU_LAYER",OracleType.Number,0,entity.MENU_LAYER),
            OracleHelper.MakeInParam("PARENT_MENU",OracleType.VarChar,25,entity.PARENT_MENU)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(PMES_TASK_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PMES_TASK_LIST 
WHERE
    PMES_TASK_ID=:PMES_TASK_ID AND 
    FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PMES_TASK_ID",OracleType.VarChar,20,entity.PMES_TASK_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
