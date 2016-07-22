using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class PROCESS_PARAM_INFO
    {
        #region 查询

        public List<PROCESS_PARAM_INFO_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PROCESS_PARAM_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PARAMETER_ID,
        IS_ILLUSTRATION_PARAM,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        IS_SC_PARAM,
        PARAM_ORDER_NO,
        DISP_ORDER_IN_SC
FROM PROCESS_PARAM_INFO
", null);
        }

        public List<PROCESS_PARAM_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PROCESS_PARAM_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  TOTAL, 
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PARAMETER_ID,
        IS_ILLUSTRATION_PARAM,
        UPDATE_USER,
        UPDATE_DATE,
        IS_SC_PARAM,
        PARAM_ORDER_NO,
        DISP_ORDER_IN_SC
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            PROCESS_ID,
            FACTORY_ID,
            PRODUCT_TYPE_ID,
            PRODUCT_PROC_TYPE_ID,
            PARAMETER_ID,
            IS_ILLUSTRATION_PARAM,
            UPDATE_USER,
            UPDATE_DATE,
            IS_SC_PARAM,
            PARAM_ORDER_NO,
            DISP_ORDER_IN_SC
        FROM (  SELECT 
                    PROCESS_ID,
                    FACTORY_ID,
                    PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID,
                    PARAMETER_ID,
                    IS_ILLUSTRATION_PARAM,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    IS_SC_PARAM,
                    PARAM_ORDER_NO,
                    DISP_ORDER_IN_SC
                FROM PROCESS_PARAM_INFO ) T1,          
            (  SELECT COUNT (1) TOTAL FROM PROCESS_PARAM_INFO ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PROCESS_PARAM_INFO_Entity> GetData(string PROCESS_ID, string PARAMETER_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID)
        {
            return OracleHelper.SelectedToIList<PROCESS_PARAM_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PARAMETER_ID,
        IS_ILLUSTRATION_PARAM,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        IS_SC_PARAM,
        PARAM_ORDER_NO,
        DISP_ORDER_IN_SC
FROM PROCESS_PARAM_INFO
WHERE PROCESS_ID=:PROCESS_ID 
    AND PARAMETER_ID=:PARAMETER_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,PARAMETER_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<PROCESS_PARAM_INFO_Entity> GetDataByProcessId(string PROCESS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID)
        {
            return OracleHelper.SelectedToIList<PROCESS_PARAM_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PROCESS_ID,
       A.FACTORY_ID,
       A.PRODUCT_TYPE_ID,
       A.PRODUCT_PROC_TYPE_ID,
       A.PARAMETER_ID,
       A.IS_ILLUSTRATION_PARAM,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       A.IS_SC_PARAM,
       A.PARAM_ORDER_NO,
       A.DISP_ORDER_IN_SC,
       B.PARAM_DESC,
       B.IS_FIRST_CHECK_PARAM,
       B.IS_PROC_MON_PARAM,
       B.IS_OUTPUT_PARAM,
       B.PARAM_UNIT,
       B.TARGET,
       B.USL,
       B.LSL,
       B.PARAM_IO,
       B.PARAM_TYPE_ID,
       B.SAMPLING_FREQUENCY,
       B.CONTROL_METHOD,
       B.IS_GROUP_PARAM,       
       B.PARAM_DATATYPE
  FROM PROCESS_PARAM_INFO A, PARAMETER_LIST B
 WHERE     A.PROCESS_ID = :PROCESS_ID
       AND A.PRODUCT_TYPE_ID = :PRODUCT_TYPE_ID
       AND A.PRODUCT_PROC_TYPE_ID = :PRODUCT_PROC_TYPE_ID
       AND A.FACTORY_ID = :FACTORY_ID
       AND B.PARAMETER_ID = A.PARAMETER_ID
       AND B.FACTORY_ID = A.FACTORY_ID
       AND B.PRODUCT_TYPE_ID = A.PRODUCT_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID),            
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<PROCESS_PARAM_INFO_Entity> GetDataByProcessIdQuery(string PROCESS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID,string queryStr)
        {
            return OracleHelper.SelectedToIList<PROCESS_PARAM_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PROCESS_ID,
       A.FACTORY_ID,
       A.PRODUCT_TYPE_ID,
       A.PRODUCT_PROC_TYPE_ID,
       A.PARAMETER_ID,
       A.IS_ILLUSTRATION_PARAM,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       A.IS_SC_PARAM,
       A.PARAM_ORDER_NO,
       A.DISP_ORDER_IN_SC,
       PARAM.PARAM_DESC,
       PARAM.IS_FIRST_CHECK_PARAM,
       PARAM.IS_PROC_MON_PARAM,
       PARAM.IS_OUTPUT_PARAM,
       PARAM.PARAM_UNIT,
       PARAM.TARGET,
       PARAM.USL,
       PARAM.LSL,
       PARAM.PARAM_IO,
       PARAM.PARAM_TYPE_ID,
       PARAM.SAMPLING_FREQUENCY,
       PARAM.CONTROL_METHOD,
       PARAM.IS_GROUP_PARAM,
       PARAM.PARAM_DATATYPE
  FROM PROCESS_PARAM_INFO A, PARAMETER_LIST PARAM
 WHERE     A.PROCESS_ID = :PROCESS_ID
       AND A.PRODUCT_TYPE_ID = :PRODUCT_TYPE_ID
       AND A.PRODUCT_PROC_TYPE_ID = :PRODUCT_PROC_TYPE_ID
       AND A.FACTORY_ID = :FACTORY_ID
       AND PARAM.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.FACTORY_ID = A.FACTORY_ID
       AND PARAM.PRODUCT_TYPE_ID = A.PRODUCT_TYPE_ID
       AND PARAM.PRODUCT_PROC_TYPE_ID = A.PRODUCT_PROC_TYPE_ID
" + queryStr, new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID),            
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public bool GetDataValidateId(string PROCESS_ID, string PARAMETER_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM PROCESS_PARAM_INFO
WHERE PROCESS_ID=:PROCESS_ID 
    AND PARAMETER_ID=:PARAMETER_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,PARAMETER_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(PROCESS_PARAM_INFO_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PROCESS_PARAM_INFO
 WHERE PROCESS_ID=:PROCESS_ID AND PARAMETER_ID=:PARAMETER_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
                    OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
                    OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
                    OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
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
INSERT INTO PROCESS_PARAM_INFO (
        PROCESS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PARAMETER_ID,
        IS_ILLUSTRATION_PARAM,
        UPDATE_USER,
        UPDATE_DATE,
        IS_SC_PARAM,
        PARAM_ORDER_NO,
        DISP_ORDER_IN_SC
)
VALUES (
        :PROCESS_ID,
        :FACTORY_ID,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :PARAMETER_ID,
        :IS_ILLUSTRATION_PARAM,
        :UPDATE_USER,
        :UPDATE_DATE,
        :IS_SC_PARAM,
        :PARAM_ORDER_NO,
        :DISP_ORDER_IN_SC
)
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("IS_ILLUSTRATION_PARAM",OracleType.VarChar,1,entity.IS_ILLUSTRATION_PARAM),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("IS_SC_PARAM",OracleType.VarChar,1,entity.IS_SC_PARAM),
            OracleHelper.MakeInParam("PARAM_ORDER_NO",OracleType.Number,0,entity.PARAM_ORDER_NO),
            OracleHelper.MakeInParam("DISP_ORDER_IN_SC",OracleType.Number,0,entity.DISP_ORDER_IN_SC)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PROCESS_PARAM_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PROCESS_PARAM_INFO SET 
    IS_ILLUSTRATION_PARAM=:IS_ILLUSTRATION_PARAM,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    IS_SC_PARAM=:IS_SC_PARAM,
    PARAM_ORDER_NO=:PARAM_ORDER_NO,
    DISP_ORDER_IN_SC=:DISP_ORDER_IN_SC
 WHERE PROCESS_ID=:PROCESS_ID AND PARAMETER_ID=:PARAMETER_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("IS_ILLUSTRATION_PARAM",OracleType.VarChar,1,entity.IS_ILLUSTRATION_PARAM),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("IS_SC_PARAM",OracleType.VarChar,1,entity.IS_SC_PARAM),
            OracleHelper.MakeInParam("PARAM_ORDER_NO",OracleType.Number,0,entity.PARAM_ORDER_NO),
            OracleHelper.MakeInParam("DISP_ORDER_IN_SC",OracleType.Number,0,entity.DISP_ORDER_IN_SC)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(PROCESS_PARAM_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PROCESS_PARAM_INFO WHERE PROCESS_ID=:PROCESS_ID AND PARAMETER_ID=:PARAMETER_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
