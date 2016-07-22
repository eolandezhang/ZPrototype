using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class APPROVE_FLOW_LIST
    {
        #region 查询

        public List<APPROVE_FLOW_LIST_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<APPROVE_FLOW_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        APPROVE_FLOW_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG,
        APPROVE_FLOW_DESC,
        OWNER_APPROVE1,
        OWNER_APPROVE2,
        PROTO_APPROVE1,
        PROTO_APPROVE2
FROM APPROVE_FLOW_LIST
", null);
        }

        public List<APPROVE_FLOW_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<APPROVE_FLOW_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  TOTAL, 
        APPROVE_FLOW_ID,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE,
        VALID_FLAG,
        APPROVE_FLOW_DESC,
        OWNER_APPROVE1,
        OWNER_APPROVE2,
        PROTO_APPROVE1,
        PROTO_APPROVE2
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            APPROVE_FLOW_ID,
            FACTORY_ID,
            UPDATE_USER,
            UPDATE_DATE,
            VALID_FLAG,
            APPROVE_FLOW_DESC,
            OWNER_APPROVE1,
            OWNER_APPROVE2,
            PROTO_APPROVE1,
            PROTO_APPROVE2
        FROM (  SELECT 
                    APPROVE_FLOW_ID,
                    FACTORY_ID,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    VALID_FLAG,
                    APPROVE_FLOW_DESC,
                    OWNER_APPROVE1,
                    OWNER_APPROVE2,
                    PROTO_APPROVE1,
                    PROTO_APPROVE2
                FROM APPROVE_FLOW_LIST ) T1,          
            (  SELECT COUNT (1) TOTAL FROM APPROVE_FLOW_LIST ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<APPROVE_FLOW_LIST_Entity> GetDataByFactoryId(string FACTORY_ID)
        {
            return OracleHelper.SelectedToIList<APPROVE_FLOW_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        APPROVE_FLOW_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        VALID_FLAG,
        APPROVE_FLOW_DESC,
        OWNER_APPROVE1,
        OWNER_APPROVE2,
        PROTO_APPROVE1,
        PROTO_APPROVE2
FROM APPROVE_FLOW_LIST
WHERE FACTORY_ID=:FACTORY_ID AND VALID_FLAG=1
", new[]{           
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        #endregion

        #region 新增

        public int PostAdd(APPROVE_FLOW_LIST_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM APPROVE_FLOW_LIST
 WHERE APPROVE_FLOW_ID=:APPROVE_FLOW_ID AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("APPROVE_FLOW_ID",OracleType.VarChar,10,entity.APPROVE_FLOW_ID),
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
INSERT INTO APPROVE_FLOW_LIST (
        APPROVE_FLOW_ID,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE,
        VALID_FLAG,
        APPROVE_FLOW_DESC,
        OWNER_APPROVE1,
        OWNER_APPROVE2,
        PROTO_APPROVE1,
        PROTO_APPROVE2
)
VALUES (
        :APPROVE_FLOW_ID,
        :FACTORY_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :VALID_FLAG,
        :APPROVE_FLOW_DESC,
        :OWNER_APPROVE1,
        :OWNER_APPROVE2,
        :PROTO_APPROVE1,
        :PROTO_APPROVE2
)
", new[]{
            OracleHelper.MakeInParam("APPROVE_FLOW_ID",OracleType.VarChar,10,entity.APPROVE_FLOW_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("APPROVE_FLOW_DESC",OracleType.VarChar,25,entity.APPROVE_FLOW_DESC),
            OracleHelper.MakeInParam("OWNER_APPROVE1",OracleType.VarChar,10,entity.OWNER_APPROVE1),
            OracleHelper.MakeInParam("OWNER_APPROVE2",OracleType.VarChar,10,entity.OWNER_APPROVE2),
            OracleHelper.MakeInParam("PROTO_APPROVE1",OracleType.VarChar,10,entity.PROTO_APPROVE1),
            OracleHelper.MakeInParam("PROTO_APPROVE2",OracleType.VarChar,10,entity.PROTO_APPROVE2)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(APPROVE_FLOW_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE APPROVE_FLOW_LIST SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    VALID_FLAG=:VALID_FLAG,
    APPROVE_FLOW_DESC=:APPROVE_FLOW_DESC,
    OWNER_APPROVE1=:OWNER_APPROVE1,
    OWNER_APPROVE2=:OWNER_APPROVE2,
    PROTO_APPROVE1=:PROTO_APPROVE1,
    PROTO_APPROVE2=:PROTO_APPROVE2
 WHERE APPROVE_FLOW_ID=:APPROVE_FLOW_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("APPROVE_FLOW_ID",OracleType.VarChar,10,entity.APPROVE_FLOW_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("APPROVE_FLOW_DESC",OracleType.VarChar,25,entity.APPROVE_FLOW_DESC),
            OracleHelper.MakeInParam("OWNER_APPROVE1",OracleType.VarChar,10,entity.OWNER_APPROVE1),
            OracleHelper.MakeInParam("OWNER_APPROVE2",OracleType.VarChar,10,entity.OWNER_APPROVE2),
            OracleHelper.MakeInParam("PROTO_APPROVE1",OracleType.VarChar,10,entity.PROTO_APPROVE1),
            OracleHelper.MakeInParam("PROTO_APPROVE2",OracleType.VarChar,10,entity.PROTO_APPROVE2)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(APPROVE_FLOW_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM APPROVE_FLOW_LIST WHERE APPROVE_FLOW_ID=:APPROVE_FLOW_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("APPROVE_FLOW_ID",OracleType.VarChar,10,entity.APPROVE_FLOW_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
