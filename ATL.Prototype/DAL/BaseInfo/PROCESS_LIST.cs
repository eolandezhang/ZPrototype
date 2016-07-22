using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class PROCESS_LIST
    {

        #region 查询
        public List<PROCESS_LIST_Entity> GetDataByFactoryIdAndTypeId(string factoryId, string productTypeId, string produceProcTypeId)
        {
            return OracleHelper.SelectedToIList<PROCESS_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        VALID_FLAG,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        PROCESS_NAME,
        PROCESS_DESC,
        WORKSTATION_ID,
        PROCESS_GROUP_ID,
        SEQUENCE_NO,
        ORDER_IN_GROUP,
        IS_MULTI_TASK,
        PREVIOUS_PROCESS_ID,
        NEXT_PROCESS_ID
FROM PROCESS_LIST
WHERE FACTORY_ID=:FACTORY_ID
  AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID
  AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
  AND VALID_FLAG = 1
  ORDER BY SEQUENCE_NO", new[]{ 
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,factoryId),
     OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,productTypeId),
     OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,produceProcTypeId)
 });
        }
        public List<PROCESS_LIST_Entity> GetData(string factoryId, string productTypeId, string produceProcTypeId, string processGroupId, string queryStr)
        {
            return OracleHelper.SelectedToIList<PROCESS_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        A.PROCESS_ID,
        A.FACTORY_ID,
        A.PRODUCT_TYPE_ID,
        A.PRODUCT_PROC_TYPE_ID,
        A.VALID_FLAG,
        A.UPDATE_USER,
        TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        A.PROCESS_NAME,
        A.PROCESS_DESC,
        A.WORKSTATION_ID,
        A.PROCESS_GROUP_ID,
        A.SEQUENCE_NO,
        A.ORDER_IN_GROUP,
        A.IS_MULTI_TASK,
        A.PREVIOUS_PROCESS_ID,
        A.NEXT_PROCESS_ID,
        B.PROCESS_DESC PREVIOUS_PROCESS_DESC,
        C.PROCESS_DESC NEXT_PROCESS_DESC
FROM PROCESS_LIST A,PROCESS_LIST B,PROCESS_LIST C
WHERE A.FACTORY_ID=:FACTORY_ID
  AND A.PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID
  AND A.PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
  AND A.PROCESS_GROUP_ID=:PROCESS_GROUP_ID
  AND A.PREVIOUS_PROCESS_ID=B.PROCESS_ID(+)
  AND A.NEXT_PROCESS_ID=C.PROCESS_ID(+) 
" + queryStr + " ORDER BY A.SEQUENCE_NO", new[]{ 
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,factoryId),
     OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,productTypeId),
     OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,produceProcTypeId),
     OracleHelper.MakeInParam("PROCESS_GROUP_ID",OracleType.VarChar,20,processGroupId)
 });
        }

        public List<PROCESS_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PROCESS_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  B.TOTAL,
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        VALID_FLAG,
        UPDATE_USER,
        UPDATE_DATE,
        PROCESS_NAME,
        PROCESS_DESC,
        WORKSTATION_ID,
        PROCESS_GROUP_ID,
        SEQUENCE_NO,
        ORDER_IN_GROUP,
        IS_MULTI_TASK,
        PREVIOUS_PROCESS_ID,
        NEXT_PROCESS_ID
FROM (  SELECT ROWNUM AS ROWINDEX,
                PROCESS_ID,
                FACTORY_ID,
                PRODUCT_TYPE_ID,
                PRODUCT_PROC_TYPE_ID,
                VALID_FLAG,
                UPDATE_USER,
                TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                PROCESS_NAME,
                PROCESS_DESC,
                WORKSTATION_ID,
                PROCESS_GROUP_ID,
                SEQUENCE_NO,
                ORDER_IN_GROUP,
                IS_MULTI_TASK,
                PREVIOUS_PROCESS_ID,
                NEXT_PROCESS_ID
        FROM PROCESS_LIST
           WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER ) A,
     (  SELECT COUNT (1) TOTAL FROM PROCESS_LIST ) B
WHERE A.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
ORDER BY SEQUENCE_NO
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }
        public List<PROCESS_LIST_Entity> GetDataById(string PROCESS_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return OracleHelper.SelectedToIList<PROCESS_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        VALID_FLAG,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        PROCESS_NAME,
        PROCESS_DESC,
        WORKSTATION_ID,
        PROCESS_GROUP_ID,
        SEQUENCE_NO,
        ORDER_IN_GROUP,
        IS_MULTI_TASK,
        PREVIOUS_PROCESS_ID,
        NEXT_PROCESS_ID
FROM PROCESS_LIST
WHERE PROCESS_ID=:PROCESS_ID 
    AND FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,20,PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)
            });
        }

        #endregion

        #region 新增

        public int PostAdd(PROCESS_LIST_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PROCESS_LIST
 WHERE PROCESS_ID=:PROCESS_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
",
                new[]{
                    OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,20,entity.PROCESS_ID),
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
INSERT INTO PROCESS_LIST (
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        VALID_FLAG,
        UPDATE_USER,
        UPDATE_DATE,
        PROCESS_NAME,
        PROCESS_DESC,
        WORKSTATION_ID,
        PROCESS_GROUP_ID,
        SEQUENCE_NO,
        ORDER_IN_GROUP,
        IS_MULTI_TASK,
        PREVIOUS_PROCESS_ID,
        NEXT_PROCESS_ID
)
VALUES (
        :PROCESS_ID,
        :FACTORY_ID,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :VALID_FLAG,
        :UPDATE_USER,
        :UPDATE_DATE,
        :PROCESS_NAME,
        :PROCESS_DESC,
        :WORKSTATION_ID,
        :PROCESS_GROUP_ID,
        :SEQUENCE_NO,
        :ORDER_IN_GROUP,
        :IS_MULTI_TASK,
        :PREVIOUS_PROCESS_ID,
        :NEXT_PROCESS_ID
)
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,20,entity.PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("PROCESS_NAME",OracleType.VarChar,20,entity.PROCESS_NAME),
            OracleHelper.MakeInParam("PROCESS_DESC",OracleType.VarChar,25,entity.PROCESS_DESC),
            OracleHelper.MakeInParam("WORKSTATION_ID",OracleType.VarChar,20,entity.WORKSTATION_ID),
            OracleHelper.MakeInParam("PROCESS_GROUP_ID",OracleType.VarChar,20,entity.PROCESS_GROUP_ID),
            OracleHelper.MakeInParam("SEQUENCE_NO",OracleType.Number,0,entity.SEQUENCE_NO),
            OracleHelper.MakeInParam("ORDER_IN_GROUP",OracleType.Number,0,entity.ORDER_IN_GROUP),
            OracleHelper.MakeInParam("IS_MULTI_TASK",OracleType.VarChar,1,entity.IS_MULTI_TASK),
            OracleHelper.MakeInParam("PREVIOUS_PROCESS_ID",OracleType.VarChar,15,entity.PREVIOUS_PROCESS_ID),
            OracleHelper.MakeInParam("NEXT_PROCESS_ID",OracleType.VarChar,15,entity.NEXT_PROCESS_ID)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PROCESS_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PROCESS_LIST SET 
    VALID_FLAG=:VALID_FLAG,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    PROCESS_NAME=:PROCESS_NAME,
    PROCESS_DESC=:PROCESS_DESC,
    WORKSTATION_ID=:WORKSTATION_ID,
    PROCESS_GROUP_ID=:PROCESS_GROUP_ID,
    SEQUENCE_NO=:SEQUENCE_NO,
    ORDER_IN_GROUP=:ORDER_IN_GROUP,
    IS_MULTI_TASK=:IS_MULTI_TASK,
    PREVIOUS_PROCESS_ID=:PREVIOUS_PROCESS_ID,
    NEXT_PROCESS_ID=:NEXT_PROCESS_ID
 WHERE PROCESS_ID=:PROCESS_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,20,entity.PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("PROCESS_NAME",OracleType.VarChar,20,entity.PROCESS_NAME),
            OracleHelper.MakeInParam("PROCESS_DESC",OracleType.VarChar,25,entity.PROCESS_DESC),
            OracleHelper.MakeInParam("WORKSTATION_ID",OracleType.VarChar,20,entity.WORKSTATION_ID),
            OracleHelper.MakeInParam("PROCESS_GROUP_ID",OracleType.VarChar,20,entity.PROCESS_GROUP_ID),
            OracleHelper.MakeInParam("SEQUENCE_NO",OracleType.Number,0,entity.SEQUENCE_NO),
            OracleHelper.MakeInParam("ORDER_IN_GROUP",OracleType.Number,0,entity.ORDER_IN_GROUP),
            OracleHelper.MakeInParam("IS_MULTI_TASK",OracleType.VarChar,1,entity.IS_MULTI_TASK),
            OracleHelper.MakeInParam("PREVIOUS_PROCESS_ID",OracleType.VarChar,15,entity.PREVIOUS_PROCESS_ID),
            OracleHelper.MakeInParam("NEXT_PROCESS_ID",OracleType.VarChar,15,entity.NEXT_PROCESS_ID)
            });
        }

        #endregion

        #region 删除
        //请移除多余的 "AND" 和 逗号
        public int PostDelete(PROCESS_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PROCESS_LIST WHERE PROCESS_ID=:PROCESS_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,20,entity.PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID)
            });
        }

        #endregion



    }
}
