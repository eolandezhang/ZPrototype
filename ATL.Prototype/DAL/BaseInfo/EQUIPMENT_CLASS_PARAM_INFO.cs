using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class EQUIPMENT_CLASS_PARAM_INFO
    {
        #region 查询

        public List<EQUIPMENT_CLASS_PARAM_INFO_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_CLASS_PARAM_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        EQUIPMENT_CLASS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PARAMETER_ID,
        DISP_ORDER_NO,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        IS_SC_PARAM
FROM EQUIPMENT_CLASS_PARAM_INFO
", null);
        }

        public List<EQUIPMENT_CLASS_PARAM_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_CLASS_PARAM_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        EQUIPMENT_CLASS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PARAMETER_ID,
        DISP_ORDER_NO,
        UPDATE_USER,
        UPDATE_DATE,
        IS_SC_PARAM
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            EQUIPMENT_CLASS_ID,
            FACTORY_ID,
            PRODUCT_TYPE_ID,
            PRODUCT_PROC_TYPE_ID,
            PARAMETER_ID,
            DISP_ORDER_NO,
            UPDATE_USER,
            UPDATE_DATE,
        IS_SC_PARAM
        FROM (  SELECT 
                    EQUIPMENT_CLASS_ID,
                    FACTORY_ID,
                    PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID,
                    PARAMETER_ID,
                    DISP_ORDER_NO,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    IS_SC_PARAM
                FROM EQUIPMENT_CLASS_PARAM_INFO ) T1,          
            (  SELECT COUNT (1) TOTAL FROM EQUIPMENT_CLASS_PARAM_INFO ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<EQUIPMENT_CLASS_PARAM_INFO_Entity> GetDataById(string EQUIPMENT_CLASS_ID, string PARAMETER_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_CLASS_PARAM_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        EQUIPMENT_CLASS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PARAMETER_ID,
        DISP_ORDER_NO,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        IS_SC_PARAM
FROM EQUIPMENT_CLASS_PARAM_INFO
WHERE EQUIPMENT_CLASS_ID=:EQUIPMENT_CLASS_ID 
    AND PARAMETER_ID=:PARAMETER_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,20,EQUIPMENT_CLASS_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,PARAMETER_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<EQUIPMENT_CLASS_PARAM_INFO_Entity> GetDataByClassId(string EQUIPMENT_CLASS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_CLASS_PARAM_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.EQUIPMENT_CLASS_ID,
       A.FACTORY_ID,
       A.PRODUCT_TYPE_ID,
       A.PRODUCT_PROC_TYPE_ID,
       A.PARAMETER_ID,
       A.DISP_ORDER_NO,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       PARAM.PARAM_TYPE_ID,
       PARAM.PARAM_DESC,
       A.IS_SC_PARAM,
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
  FROM EQUIPMENT_CLASS_PARAM_INFO A, PARAMETER_LIST PARAM
 WHERE     A.EQUIPMENT_CLASS_ID = :EQUIPMENT_CLASS_ID
       AND A.PRODUCT_TYPE_ID = :PRODUCT_TYPE_ID
       AND A.PRODUCT_PROC_TYPE_ID = :PRODUCT_PROC_TYPE_ID
       AND A.FACTORY_ID = :FACTORY_ID
       AND PARAM.FACTORY_ID = A.FACTORY_ID
       AND PARAM.PRODUCT_TYPE_ID = A.PRODUCT_TYPE_ID
       AND PARAM.PRODUCT_PROC_TYPE_ID = A.PRODUCT_PROC_TYPE_ID
       AND PARAM.PARAMETER_ID = A.PARAMETER_ID    
" + queryStr + " ORDER BY DISP_ORDER_NO", new[]{
            OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,20,EQUIPMENT_CLASS_ID),            
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }
                

        public bool GetDataValidateId(string EQUIPMENT_CLASS_ID, string PARAMETER_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM EQUIPMENT_CLASS_PARAM_INFO
WHERE EQUIPMENT_CLASS_ID=:EQUIPMENT_CLASS_ID 
    AND PARAMETER_ID=:PARAMETER_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,20,EQUIPMENT_CLASS_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,PARAMETER_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(EQUIPMENT_CLASS_PARAM_INFO_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM EQUIPMENT_CLASS_PARAM_INFO
 WHERE PARAMETER_ID=:PARAMETER_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND FACTORY_ID=:FACTORY_ID
",
                new[]{                    
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
INSERT INTO EQUIPMENT_CLASS_PARAM_INFO (
        EQUIPMENT_CLASS_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PARAMETER_ID,
        DISP_ORDER_NO,
        UPDATE_USER,
        UPDATE_DATE,
        IS_SC_PARAM
)
VALUES (
        :EQUIPMENT_CLASS_ID,
        :FACTORY_ID,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :PARAMETER_ID,
        :DISP_ORDER_NO,
        :UPDATE_USER,
        :UPDATE_DATE,
        :IS_SC_PARAM
)
", new[]{
            OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,20,entity.EQUIPMENT_CLASS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("DISP_ORDER_NO",OracleType.Number,0,entity.DISP_ORDER_NO),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("IS_SC_PARAM",OracleType.VarChar,1,entity.IS_SC_PARAM)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(EQUIPMENT_CLASS_PARAM_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE EQUIPMENT_CLASS_PARAM_INFO SET 
    DISP_ORDER_NO=:DISP_ORDER_NO,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    IS_SC_PARAM=:IS_SC_PARAM
 WHERE 
        EQUIPMENT_CLASS_ID=:EQUIPMENT_CLASS_ID AND  
        PARAMETER_ID=:PARAMETER_ID AND  
        PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND  
        PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND  
        FACTORY_ID=:FACTORY_ID 
", new[]{
            OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,20,entity.EQUIPMENT_CLASS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("DISP_ORDER_NO",OracleType.Number,0,entity.DISP_ORDER_NO),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("IS_SC_PARAM",OracleType.VarChar,1,entity.IS_SC_PARAM)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(EQUIPMENT_CLASS_PARAM_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM EQUIPMENT_CLASS_PARAM_INFO 
WHERE
    EQUIPMENT_CLASS_ID=:EQUIPMENT_CLASS_ID AND 
    PARAMETER_ID=:PARAMETER_ID AND 
    PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND 
    PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND 
    FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,20,entity.EQUIPMENT_CLASS_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
