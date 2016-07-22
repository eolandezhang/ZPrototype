using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class PARAMETER_LIST
    {

        #region 查询

        public List<PARAMETER_LIST_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PARAMETER_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PARAMETER_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        PARAM_NAME,
        PARAM_DESC,
        PARAM_TYPE_ID,
        PARAM_IO,
        SOURCE,
        IS_SPEC_PARAM,
        IS_FIRST_CHECK_PARAM,
        IS_PROC_MON_PARAM,
        IS_OUTPUT_PARAM,
        IS_VERSION_CTRL,
        MEASURE_METHOD,
        IS_GROUP_PARAM,
        PARAM_DATATYPE,
        PARAM_UNIT,
        TARGET,
        USL,
        LSL,
        VALID_FLAG,
        SAMPLING_FREQUENCY,
        CONTROL_METHOD
FROM PARAMETER_LIST
", null);
        }

        public List<PARAMETER_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PARAMETER_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  B.TOTAL,
        PARAMETER_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        PARAM_NAME,
        PARAM_DESC,
        PARAM_TYPE_ID,
        PARAM_IO,
        SOURCE,
        IS_SPEC_PARAM,
        IS_FIRST_CHECK_PARAM,
        IS_PROC_MON_PARAM,
        IS_OUTPUT_PARAM,
        IS_VERSION_CTRL,
        MEASURE_METHOD,
        IS_GROUP_PARAM,
        PARAM_DATATYPE,
        PARAM_UNIT,
        TARGET,
        USL,
        LSL,
        VALID_FLAG,
        SAMPLING_FREQUENCY,
        CONTROL_METHOD
FROM (  SELECT ROWNUM AS ROWINDEX,
                PARAMETER_ID,
                FACTORY_ID,
                PRODUCT_TYPE_ID,
                PRODUCT_PROC_TYPE_ID,
                UPDATE_USER,
                TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                PARAM_NAME,
                PARAM_DESC,
                PARAM_TYPE_ID,
                PARAM_IO,
                SOURCE,
                IS_SPEC_PARAM,
                IS_FIRST_CHECK_PARAM,
                IS_PROC_MON_PARAM,
                IS_OUTPUT_PARAM,
                IS_VERSION_CTRL,
                MEASURE_METHOD,
                IS_GROUP_PARAM,
                PARAM_DATATYPE,
                PARAM_UNIT,
                TARGET,
                USL,
                LSL,
                VALID_FLAG,
                SAMPLING_FREQUENCY,
                CONTROL_METHOD
        FROM PARAMETER_LIST
           WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER             
) A,
     (  SELECT COUNT (1) TOTAL FROM PARAMETER_LIST ) B
WHERE A.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PARAMETER_LIST_Entity> GetDataByType(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return OracleHelper.SelectedToIList<PARAMETER_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PARAMETER_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        PARAM_NAME,
        PARAM_DESC,
        PARAM_TYPE_ID,
        PARAM_IO,
        SOURCE,
        IS_SPEC_PARAM,
        IS_FIRST_CHECK_PARAM,
        IS_PROC_MON_PARAM,
        IS_OUTPUT_PARAM,
        IS_VERSION_CTRL,
        MEASURE_METHOD,
        IS_GROUP_PARAM,
        PARAM_DATATYPE,
        PARAM_UNIT,
        TARGET,
        USL,
        LSL,
        VALID_FLAG,
        SAMPLING_FREQUENCY,
        CONTROL_METHOD
FROM PARAMETER_LIST
WHERE  FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID   
ORDER BY PARAMETER_ID 
", new[]{            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)
            });
        }
        public List<PARAMETER_LIST_Entity> GetDataByPType(string PARAM_TYPE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PARAMETER_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PARAMETER_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        PARAM_NAME,
        PARAM_DESC,
        PARAM_TYPE_ID,
        PARAM_IO,
        SOURCE,
        IS_SPEC_PARAM,
        IS_FIRST_CHECK_PARAM,
        IS_PROC_MON_PARAM,
        IS_OUTPUT_PARAM,
        IS_VERSION_CTRL,
        MEASURE_METHOD,
        IS_GROUP_PARAM,
        PARAM_DATATYPE,
        PARAM_UNIT,
        TARGET,
        USL,
        LSL,
        VALID_FLAG,
        SAMPLING_FREQUENCY,
        CONTROL_METHOD
FROM PARAMETER_LIST
WHERE  FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
    AND PARAM_TYPE_ID=:PARAM_TYPE_ID
" + queryStr + "ORDER BY PARAMETER_ID", new[]{            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PARAM_TYPE_ID",OracleType.VarChar,15,PARAM_TYPE_ID)
            });
        }
        public List<PARAMETER_LIST_Entity> GetDataById(string PARAMETER_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return OracleHelper.SelectedToIList<PARAMETER_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PARAMETER_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        PARAM_NAME,
        PARAM_DESC,
        PARAM_TYPE_ID,
        PARAM_IO,
        SOURCE,
        IS_SPEC_PARAM,
        IS_FIRST_CHECK_PARAM,
        IS_PROC_MON_PARAM,
        IS_OUTPUT_PARAM,
        IS_VERSION_CTRL,
        MEASURE_METHOD,
        IS_GROUP_PARAM,
        PARAM_DATATYPE,
        PARAM_UNIT,
        TARGET,
        USL,
        LSL,
        VALID_FLAG,
        SAMPLING_FREQUENCY,
        CONTROL_METHOD
FROM PARAMETER_LIST
WHERE PARAMETER_ID=:PARAMETER_ID 
    AND FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,PARAMETER_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)
            });
        }
        #endregion

        #region 新增

        public int PostAdd(PARAMETER_LIST_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PARAMETER_LIST
 WHERE PARAMETER_ID=:PARAMETER_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
",
                new[]{
                    OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
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
INSERT INTO PARAMETER_LIST (
        PARAMETER_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        PARAM_NAME,
        PARAM_DESC,
        PARAM_TYPE_ID,
        PARAM_IO,
        SOURCE,
        IS_SPEC_PARAM,
        IS_FIRST_CHECK_PARAM,
        IS_PROC_MON_PARAM,
        IS_OUTPUT_PARAM,
        IS_VERSION_CTRL,
        MEASURE_METHOD,
        IS_GROUP_PARAM,
        PARAM_DATATYPE,
        PARAM_UNIT,
        TARGET,
        USL,
        LSL,
        VALID_FLAG,
        SAMPLING_FREQUENCY,
        CONTROL_METHOD
)
VALUES (
        :PARAMETER_ID,
        :FACTORY_ID,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :PARAM_NAME,
        :PARAM_DESC,
        :PARAM_TYPE_ID,
        :PARAM_IO,
        :SOURCE,
        :IS_SPEC_PARAM,
        :IS_FIRST_CHECK_PARAM,
        :IS_PROC_MON_PARAM,
        :IS_OUTPUT_PARAM,
        :IS_VERSION_CTRL,
        :MEASURE_METHOD,
        :IS_GROUP_PARAM,
        :PARAM_DATATYPE,
        :PARAM_UNIT,
        :TARGET,
        :USL,
        :LSL,
        :VALID_FLAG,
        :SAMPLING_FREQUENCY,
        :CONTROL_METHOD
)
", new[]{
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("PARAM_NAME",OracleType.VarChar,20,entity.PARAM_NAME),
            OracleHelper.MakeInParam("PARAM_DESC",OracleType.VarChar,30,entity.PARAM_DESC),
            OracleHelper.MakeInParam("PARAM_TYPE_ID",OracleType.VarChar,15,entity.PARAM_TYPE_ID),
            OracleHelper.MakeInParam("PARAM_IO",OracleType.VarChar,1,entity.PARAM_IO),
            OracleHelper.MakeInParam("SOURCE",OracleType.VarChar,1,entity.SOURCE),
            OracleHelper.MakeInParam("IS_SPEC_PARAM",OracleType.VarChar,1,entity.IS_SPEC_PARAM),
            OracleHelper.MakeInParam("IS_FIRST_CHECK_PARAM",OracleType.VarChar,1,entity.IS_FIRST_CHECK_PARAM),
            OracleHelper.MakeInParam("IS_PROC_MON_PARAM",OracleType.VarChar,1,entity.IS_PROC_MON_PARAM),
            OracleHelper.MakeInParam("IS_OUTPUT_PARAM",OracleType.VarChar,1,entity.IS_OUTPUT_PARAM),
            OracleHelper.MakeInParam("IS_VERSION_CTRL",OracleType.VarChar,1,entity.IS_VERSION_CTRL),
            OracleHelper.MakeInParam("MEASURE_METHOD",OracleType.VarChar,1,entity.MEASURE_METHOD),
            OracleHelper.MakeInParam("IS_GROUP_PARAM",OracleType.VarChar,1,entity.IS_GROUP_PARAM),
            OracleHelper.MakeInParam("PARAM_DATATYPE",OracleType.VarChar,8,entity.PARAM_DATATYPE),
            OracleHelper.MakeInParam("PARAM_UNIT",OracleType.VarChar,8,entity.PARAM_UNIT),
            OracleHelper.MakeInParam("TARGET",OracleType.VarChar,25,entity.TARGET),
            OracleHelper.MakeInParam("USL",OracleType.VarChar,25,entity.USL),
            OracleHelper.MakeInParam("LSL",OracleType.VarChar,25,entity.LSL),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("SAMPLING_FREQUENCY",OracleType.VarChar,50,entity.SAMPLING_FREQUENCY),
            OracleHelper.MakeInParam("CONTROL_METHOD",OracleType.VarChar,50,entity.CONTROL_METHOD)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PARAMETER_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PARAMETER_LIST SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    PARAM_NAME=:PARAM_NAME,
    PARAM_DESC=:PARAM_DESC,
    PARAM_TYPE_ID=:PARAM_TYPE_ID,
    PARAM_IO=:PARAM_IO,
    SOURCE=:SOURCE,
    IS_SPEC_PARAM=:IS_SPEC_PARAM,
    IS_FIRST_CHECK_PARAM=:IS_FIRST_CHECK_PARAM,
    IS_PROC_MON_PARAM=:IS_PROC_MON_PARAM,
    IS_OUTPUT_PARAM=:IS_OUTPUT_PARAM,
    IS_VERSION_CTRL=:IS_VERSION_CTRL,
    MEASURE_METHOD=:MEASURE_METHOD,
    IS_GROUP_PARAM=:IS_GROUP_PARAM,
    PARAM_DATATYPE=:PARAM_DATATYPE,
    PARAM_UNIT=:PARAM_UNIT,
    TARGET=:TARGET,
    USL=:USL,
    LSL=:LSL,
    VALID_FLAG=:VALID_FLAG,
    SAMPLING_FREQUENCY=:SAMPLING_FREQUENCY,
    CONTROL_METHOD=:CONTROL_METHOD
 WHERE PARAMETER_ID=:PARAMETER_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("PARAM_NAME",OracleType.VarChar,20,entity.PARAM_NAME),
            OracleHelper.MakeInParam("PARAM_DESC",OracleType.VarChar,30,entity.PARAM_DESC),
            OracleHelper.MakeInParam("PARAM_TYPE_ID",OracleType.VarChar,15,entity.PARAM_TYPE_ID),
            OracleHelper.MakeInParam("PARAM_IO",OracleType.VarChar,1,entity.PARAM_IO),
            OracleHelper.MakeInParam("SOURCE",OracleType.VarChar,1,entity.SOURCE),
            OracleHelper.MakeInParam("IS_SPEC_PARAM",OracleType.VarChar,1,entity.IS_SPEC_PARAM),
            OracleHelper.MakeInParam("IS_FIRST_CHECK_PARAM",OracleType.VarChar,1,entity.IS_FIRST_CHECK_PARAM),
            OracleHelper.MakeInParam("IS_PROC_MON_PARAM",OracleType.VarChar,1,entity.IS_PROC_MON_PARAM),
            OracleHelper.MakeInParam("IS_OUTPUT_PARAM",OracleType.VarChar,1,entity.IS_OUTPUT_PARAM),
            OracleHelper.MakeInParam("IS_VERSION_CTRL",OracleType.VarChar,1,entity.IS_VERSION_CTRL),
            OracleHelper.MakeInParam("MEASURE_METHOD",OracleType.VarChar,1,entity.MEASURE_METHOD),
            OracleHelper.MakeInParam("IS_GROUP_PARAM",OracleType.VarChar,1,entity.IS_GROUP_PARAM),
            OracleHelper.MakeInParam("PARAM_DATATYPE",OracleType.VarChar,8,entity.PARAM_DATATYPE),
            OracleHelper.MakeInParam("PARAM_UNIT",OracleType.VarChar,8,entity.PARAM_UNIT),
            OracleHelper.MakeInParam("TARGET",OracleType.VarChar,25,entity.TARGET),
            OracleHelper.MakeInParam("USL",OracleType.VarChar,25,entity.USL),
            OracleHelper.MakeInParam("LSL",OracleType.VarChar,25,entity.LSL),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("SAMPLING_FREQUENCY",OracleType.VarChar,50,entity.SAMPLING_FREQUENCY),
            OracleHelper.MakeInParam("CONTROL_METHOD",OracleType.VarChar,50,entity.CONTROL_METHOD)
            });
        }

        #endregion

        #region 删除
        //请移除多余的 "AND" 和 逗号
        public int PostDelete(PARAMETER_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PARAMETER_LIST WHERE PARAMETER_ID=:PARAMETER_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID)
            });
        }

        #endregion



    }
}
