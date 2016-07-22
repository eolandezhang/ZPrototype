using DBUtility;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Package
{
    public class PACKAGE_TYPE_LIST
    {
        #region 查询

        public List<PACKAGE_TYPE_LIST_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PACKAGE_TYPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_TYPE_ID,
        FACTORY_ID,
        PACKAGE_TYPE_DESC,
        PACKAGE_CODE,
        VALID_FLAG,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        GROUPS_LIMIT
FROM PACKAGE_TYPE_LIST
", null);
        }

        public List<PACKAGE_TYPE_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PACKAGE_TYPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  B.TOTAL,
        PACKAGE_TYPE_ID,
        FACTORY_ID,
        PACKAGE_TYPE_DESC,
        PACKAGE_CODE,
        VALID_FLAG,
        UPDATE_USER,
        UPDATE_DATE,
        GROUPS_LIMIT
FROM (  SELECT ROWNUM AS ROWINDEX,
                PACKAGE_TYPE_ID,
                FACTORY_ID,
                PACKAGE_TYPE_DESC,
                PACKAGE_CODE,
                VALID_FLAG,
                UPDATE_USER,
                TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                GROUPS_LIMIT
        FROM PACKAGE_TYPE_LIST
           WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER ) A,
     (  SELECT COUNT (1) TOTAL FROM PACKAGE_TYPE_LIST ) B
WHERE A.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PACKAGE_TYPE_LIST_Entity> GetDataByFactoryId(string FACTORY_ID)
        {
            return OracleHelper.SelectedToIList<PACKAGE_TYPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_TYPE_ID,
        FACTORY_ID,
        PACKAGE_TYPE_DESC,
        PACKAGE_CODE,
        VALID_FLAG,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        GROUPS_LIMIT
FROM PACKAGE_TYPE_LIST
WHERE FACTORY_ID=:FACTORY_ID
", new[]{       
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_TYPE_LIST_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PACKAGE_TYPE_LIST
 WHERE PACKAGE_TYPE_ID=:PACKAGE_TYPE_ID AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("PACKAGE_TYPE_ID",OracleType.VarChar,5,entity.PACKAGE_TYPE_ID),
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
INSERT INTO PACKAGE_TYPE_LIST (
        PACKAGE_TYPE_ID,
        FACTORY_ID,
        PACKAGE_TYPE_DESC,
        PACKAGE_CODE,
        VALID_FLAG,
        UPDATE_USER,
        UPDATE_DATE,
        GROUPS_LIMIT
)
VALUES (
        :PACKAGE_TYPE_ID,
        :FACTORY_ID,
        :PACKAGE_TYPE_DESC,
        :PACKAGE_CODE,
        :VALID_FLAG,
        :UPDATE_USER,
        :UPDATE_DATE,
        :GROUPS_LIMIT
)
", new[]{
            OracleHelper.MakeInParam("PACKAGE_TYPE_ID",OracleType.VarChar,5,entity.PACKAGE_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PACKAGE_TYPE_DESC",OracleType.VarChar,25,entity.PACKAGE_TYPE_DESC),
            OracleHelper.MakeInParam("PACKAGE_CODE",OracleType.VarChar,2,entity.PACKAGE_CODE),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("GROUPS_LIMIT",OracleType.Number,0,entity.GROUPS_LIMIT)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_TYPE_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_TYPE_LIST SET 
    PACKAGE_TYPE_DESC=:PACKAGE_TYPE_DESC,
    PACKAGE_CODE=:PACKAGE_CODE,
    VALID_FLAG=:VALID_FLAG,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    GROUPS_LIMIT=:GROUPS_LIMIT
 WHERE PACKAGE_TYPE_ID=:PACKAGE_TYPE_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_TYPE_ID",OracleType.VarChar,5,entity.PACKAGE_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PACKAGE_TYPE_DESC",OracleType.VarChar,25,entity.PACKAGE_TYPE_DESC),
            OracleHelper.MakeInParam("PACKAGE_CODE",OracleType.VarChar,2,entity.PACKAGE_CODE),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("GROUPS_LIMIT",OracleType.Number,0,entity.GROUPS_LIMIT)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(PACKAGE_TYPE_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_TYPE_LIST WHERE PACKAGE_TYPE_ID=:PACKAGE_TYPE_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_TYPE_ID",OracleType.VarChar,5,entity.PACKAGE_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
