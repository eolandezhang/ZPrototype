using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class ILLUSTRATION_PARAM_INFO
    {

        #region 查询

        public List<ILLUSTRATION_PARAM_INFO_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<ILLUSTRATION_PARAM_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        ILLUSTRATION_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PARAMETER_ID,
        PARAM_ORDER_NO,
        TARGET,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        USL,
        LSL
FROM ILLUSTRATION_PARAM_INFO
", null);
        }

        public List<ILLUSTRATION_PARAM_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<ILLUSTRATION_PARAM_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  TOTAL, 
        ILLUSTRATION_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PARAMETER_ID,
        PARAM_ORDER_NO,
        TARGET,
        UPDATE_USER,
        UPDATE_DATE,
        USL,
        LSL
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            ILLUSTRATION_ID,
            FACTORY_ID,
            PRODUCT_TYPE_ID,
            PRODUCT_PROC_TYPE_ID,
            PARAMETER_ID,
            PARAM_ORDER_NO,
            TARGET,
            UPDATE_USER,
            UPDATE_DATE,
            USL,
            LSL
        FROM (  SELECT 
                    ILLUSTRATION_ID,
                    FACTORY_ID,
                    PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID,
                    PARAMETER_ID,
                    PARAM_ORDER_NO,
                    TARGET,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    USL,
                    LSL
                FROM ILLUSTRATION_PARAM_INFO ) T1,          
            (  SELECT COUNT (1) TOTAL FROM ILLUSTRATION_PARAM_INFO ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<ILLUSTRATION_PARAM_INFO_Entity> GetDataByImgId(string ILLUSTRATION_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID,string queryStr)
        {
            return OracleHelper.SelectedToIList<ILLUSTRATION_PARAM_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.ILLUSTRATION_ID,
       A.FACTORY_ID,
       A.PRODUCT_TYPE_ID,
       A.PRODUCT_PROC_TYPE_ID,
       A.PARAMETER_ID,
       A.PARAM_ORDER_NO,
       A.TARGET,
       A.USL,
       A.LSL,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,       
       PARAM.PARAM_DESC,
       PARAM.PARAM_TYPE_ID,
       PARAM.IS_FIRST_CHECK_PARAM,
       PARAM.IS_PROC_MON_PARAM,
       PARAM.IS_OUTPUT_PARAM,
       PARAM.PARAM_UNIT,       
       PARAM.PARAM_IO,       
       PARAM.SAMPLING_FREQUENCY,
       PARAM.CONTROL_METHOD,
       PARAM.IS_GROUP_PARAM,
       PARAM.PARAM_DATATYPE
  FROM ILLUSTRATION_PARAM_INFO A, PARAMETER_LIST PARAM
 WHERE     A.ILLUSTRATION_ID = :ILLUSTRATION_ID
       AND A.PRODUCT_TYPE_ID = :PRODUCT_TYPE_ID
       AND A.PRODUCT_PROC_TYPE_ID = :PRODUCT_PROC_TYPE_ID
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.PARAMETER_ID = PARAM.PARAMETER_ID
ORDER BY A.PARAM_ORDER_NO
 " + queryStr, new[]{
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,15,ILLUSTRATION_ID),            
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<ILLUSTRATION_PARAM_INFO_Entity> GetDataById(string ILLUSTRATION_ID, string PARAMETER_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<ILLUSTRATION_PARAM_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        ILLUSTRATION_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PARAMETER_ID,
        PARAM_ORDER_NO,
        TARGET,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        USL,
        LSL
FROM ILLUSTRATION_PARAM_INFO
WHERE ILLUSTRATION_ID=:ILLUSTRATION_ID 
    AND PARAMETER_ID=:PARAMETER_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,15,ILLUSTRATION_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,PARAMETER_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        #endregion

        #region 新增

        public int PostAdd(ILLUSTRATION_PARAM_INFO_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM ILLUSTRATION_PARAM_INFO
 WHERE ILLUSTRATION_ID=:ILLUSTRATION_ID AND PARAMETER_ID=:PARAMETER_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,15,entity.ILLUSTRATION_ID),
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
INSERT INTO ILLUSTRATION_PARAM_INFO (
        ILLUSTRATION_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PARAMETER_ID,
        PARAM_ORDER_NO,
        TARGET,
        UPDATE_USER,
        UPDATE_DATE,        
        USL,
        LSL
)
VALUES (
        :ILLUSTRATION_ID,
        :FACTORY_ID,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :PARAMETER_ID,
        :PARAM_ORDER_NO,
        :TARGET,
        :UPDATE_USER,
        :UPDATE_DATE,        
        :USL,
        :LSL
)
", new[]{
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,15,entity.ILLUSTRATION_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("PARAM_ORDER_NO",OracleType.Number,0,entity.PARAM_ORDER_NO),
            OracleHelper.MakeInParam("TARGET",OracleType.VarChar,25,entity.TARGET),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("USL",OracleType.VarChar,25,entity.USL),
            OracleHelper.MakeInParam("LSL",OracleType.VarChar,25,entity.LSL)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(ILLUSTRATION_PARAM_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE ILLUSTRATION_PARAM_INFO SET 
    PARAM_ORDER_NO=:PARAM_ORDER_NO,
    TARGET=:TARGET,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    USL=:USL,
    LSL=:LSL
 WHERE ILLUSTRATION_ID=:ILLUSTRATION_ID AND PARAMETER_ID=:PARAMETER_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,15,entity.ILLUSTRATION_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("PARAM_ORDER_NO",OracleType.Number,0,entity.PARAM_ORDER_NO),
            OracleHelper.MakeInParam("TARGET",OracleType.VarChar,25,entity.TARGET),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("USL",OracleType.VarChar,25,entity.USL),
            OracleHelper.MakeInParam("LSL",OracleType.VarChar,25,entity.LSL)
            });
        }

        #endregion

        #region 删除
        //请移除多余的 "AND" 和 逗号
        public int PostDelete(ILLUSTRATION_PARAM_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM ILLUSTRATION_PARAM_INFO WHERE ILLUSTRATION_ID=:ILLUSTRATION_ID AND PARAMETER_ID=:PARAMETER_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,15,entity.ILLUSTRATION_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
