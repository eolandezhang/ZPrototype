﻿using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class MATERIAL_PN_PARA_INFO
    {
        #region 查询

        public List<MATERIAL_PN_PARA_INFO_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<MATERIAL_PN_PARA_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        MATERIAL_PN_ID,
        PARAMETER_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        TARGET,
        USL,
        LSL,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        REMARK
FROM MATERIAL_PN_PARA_INFO
", null);
        }

        public List<MATERIAL_PN_PARA_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<MATERIAL_PN_PARA_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        MATERIAL_PN_ID,
        PARAMETER_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        TARGET,
        USL,
        LSL,
        UPDATE_USER,
        UPDATE_DATE,
        REMARK
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            MATERIAL_PN_ID,
            PARAMETER_ID,
            FACTORY_ID,
            PRODUCT_TYPE_ID,
            PRODUCT_PROC_TYPE_ID,
            TARGET,
            USL,
            LSL,
            UPDATE_USER,
            UPDATE_DATE,
            REMARK
        FROM (  SELECT 
                    MATERIAL_PN_ID,
                    PARAMETER_ID,
                    FACTORY_ID,
                    PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID,
                    TARGET,
                    USL,
                    LSL,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    REMARK
                FROM MATERIAL_PN_PARA_INFO ) T1,          
            (  SELECT COUNT (1) TOTAL FROM MATERIAL_PN_PARA_INFO ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<MATERIAL_PN_PARA_INFO_Entity> GetDataById(string MATERIAL_PN_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string PARAMETER_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<MATERIAL_PN_PARA_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        MATERIAL_PN_ID,
        PARAMETER_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        TARGET,
        USL,
        LSL,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        REMARK
FROM MATERIAL_PN_PARA_INFO
WHERE MATERIAL_PN_ID=:MATERIAL_PN_ID 
    AND FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND PARAMETER_ID=:PARAMETER_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,MATERIAL_PN_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,PARAMETER_ID)
            });
        }

        public List<MATERIAL_PN_PARA_INFO_Entity> GetDataByPN(string MATERIAL_PN_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<MATERIAL_PN_PARA_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.MATERIAL_PN_ID,
       A.PARAMETER_ID,
       A.FACTORY_ID,
       A.PRODUCT_TYPE_ID,
       A.PRODUCT_PROC_TYPE_ID,
       A.TARGET,
       A.USL,
       A.LSL,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       A.REMARK,
       PARAM.PARAM_NAME,
       PARAM.PARAM_DESC,
       PARAM.PARAM_TYPE_ID,
       PARAM.PARAM_IO,
       PARAM.SOURCE,
       PARAM.IS_SPEC_PARAM,
       PARAM.IS_FIRST_CHECK_PARAM,
       PARAM.IS_PROC_MON_PARAM,
       PARAM.IS_OUTPUT_PARAM,
       PARAM.IS_VERSION_CTRL,
       PARAM.MEASURE_METHOD,
       PARAM.IS_GROUP_PARAM,
       PARAM.PARAM_DATATYPE,
       PARAM.PARAM_UNIT,
       PARAM.TARGET,
       PARAM.USL,
       PARAM.LSL,
       PARAM.SAMPLING_FREQUENCY,
       PARAM.CONTROL_METHOD
  FROM MATERIAL_PN_PARA_INFO A, PARAMETER_LIST PARAM
 WHERE     A.MATERIAL_PN_ID = :MATERIAL_PN_ID
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.PRODUCT_TYPE_ID = :PRODUCT_TYPE_ID
       AND A.PRODUCT_PROC_TYPE_ID = :PRODUCT_PROC_TYPE_ID
       AND A.PARAMETER_ID = PARAM.PARAMETER_ID
       AND PARAM.FACTORY_ID = A.FACTORY_ID
       AND PARAM.PRODUCT_TYPE_ID = A.PRODUCT_TYPE_ID
       AND PARAM.PRODUCT_PROC_TYPE_ID = A.PRODUCT_PROC_TYPE_ID
" + queryStr, new[]{
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,MATERIAL_PN_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)
            });
        }

        public bool GetDataValidateId(string MATERIAL_PN_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string PARAMETER_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM MATERIAL_PN_PARA_INFO
WHERE MATERIAL_PN_ID=:MATERIAL_PN_ID 
    AND FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND PARAMETER_ID=:PARAMETER_ID
", new[]{
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,MATERIAL_PN_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,PARAMETER_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(MATERIAL_PN_PARA_INFO_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM MATERIAL_PN_PARA_INFO
 WHERE MATERIAL_PN_ID=:MATERIAL_PN_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND PARAMETER_ID=:PARAMETER_ID
",
                new[]{
                    OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,entity.MATERIAL_PN_ID),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
                    OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
                    OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
                    OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID)
                })
                ) > 0;
            if (exist)
            {
                return 0;
            }
            #endregion
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO MATERIAL_PN_PARA_INFO (
        MATERIAL_PN_ID,
        PARAMETER_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        TARGET,
        USL,
        LSL,
        UPDATE_USER,
        UPDATE_DATE,
        REMARK
)
VALUES (
        :MATERIAL_PN_ID,
        :PARAMETER_ID,
        :FACTORY_ID,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :TARGET,
        :USL,
        :LSL,
        :UPDATE_USER,
        :UPDATE_DATE,
        :REMARK
)
", new[]{
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,entity.MATERIAL_PN_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("TARGET",OracleType.VarChar,25,entity.TARGET),
            OracleHelper.MakeInParam("USL",OracleType.VarChar,25,entity.USL),
            OracleHelper.MakeInParam("LSL",OracleType.VarChar,25,entity.LSL),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("REMARK",OracleType.VarChar,15,entity.REMARK)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(MATERIAL_PN_PARA_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE MATERIAL_PN_PARA_INFO SET 
    TARGET=:TARGET,
    USL=:USL,
    LSL=:LSL,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    REMARK=:REMARK
 WHERE 
        MATERIAL_PN_ID=:MATERIAL_PN_ID AND  
        FACTORY_ID=:FACTORY_ID AND  
        PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND  
        PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND  
        PARAMETER_ID=:PARAMETER_ID 
", new[]{
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,entity.MATERIAL_PN_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("TARGET",OracleType.VarChar,25,entity.TARGET),
            OracleHelper.MakeInParam("USL",OracleType.VarChar,25,entity.USL),
            OracleHelper.MakeInParam("LSL",OracleType.VarChar,25,entity.LSL),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("REMARK",OracleType.VarChar,15,entity.REMARK)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(MATERIAL_PN_PARA_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM MATERIAL_PN_PARA_INFO 
WHERE
    MATERIAL_PN_ID=:MATERIAL_PN_ID AND 
    FACTORY_ID=:FACTORY_ID AND 
    PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND 
    PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID AND 
    PARAMETER_ID=:PARAMETER_ID
", new[]{
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,entity.MATERIAL_PN_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID)
            });
        }

        #endregion



    }
}
