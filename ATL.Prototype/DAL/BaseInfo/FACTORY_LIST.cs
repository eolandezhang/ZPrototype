using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class FACTORY_LIST
    {

        #region 查询

        public List<FACTORY_LIST_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<FACTORY_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        FACTORY_NAME,
        FACTORY_DESC
FROM FACTORY_LIST
", null);
        }

        public List<FACTORY_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<FACTORY_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  B.TOTAL,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE,
        FACTORY_NAME,
        FACTORY_DESC
FROM (  SELECT ROWNUM AS ROWINDEX,
                FACTORY_ID,
                UPDATE_USER,
                TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                FACTORY_NAME,
                FACTORY_DESC
        FROM FACTORY_LIST
           WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER ) A,
     (  SELECT COUNT (1) TOTAL FROM FACTORY_LIST ) B
WHERE A.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        #endregion

        #region 新增

        public int PostAdd(FACTORY_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO FACTORY_LIST (
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE,
        FACTORY_NAME,
        FACTORY_DESC
)
VALUES (
        :FACTORY_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :FACTORY_NAME,
        :FACTORY_DESC
)
", new[]{
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("FACTORY_NAME",OracleType.VarChar,15,entity.FACTORY_NAME),
            OracleHelper.MakeInParam("FACTORY_DESC",OracleType.VarChar,20,entity.FACTORY_DESC)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(FACTORY_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE FACTORY_LIST SET 
    FACTORY_ID=:FACTORY_ID,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    FACTORY_NAME=:FACTORY_NAME,
    FACTORY_DESC=:FACTORY_DESC
 WHERE FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("FACTORY_NAME",OracleType.VarChar,15,entity.FACTORY_NAME),
            OracleHelper.MakeInParam("FACTORY_DESC",OracleType.VarChar,20,entity.FACTORY_DESC)
        });
        }

        #endregion

        #region 删除
        //请移除多余的 "AND" 和 逗号
        public int PostDelete(FACTORY_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM FACTORY_LIST WHERE FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
