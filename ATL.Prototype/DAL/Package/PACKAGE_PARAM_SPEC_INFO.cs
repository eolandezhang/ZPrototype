using DBUtility;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Package
{
    public class PACKAGE_PARAM_SPEC_INFO
    {
        #region 查询

        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SPEC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        FACTORY_ID,
        PARAMETER_ID,
        SPEC_TYPE,
        PARAM_UNIT,
        TARGET,
        USL,
        LSL,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_PARAM_SPEC_INFO
", null);
        }

        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetData(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string PARAMETER_ID, string FACTORY_ID, string SPEC_TYPE)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SPEC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        FACTORY_ID,
        PARAMETER_ID,
        SPEC_TYPE,
        PARAM_UNIT,
        TARGET,
        USL,
        LSL,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_PARAM_SPEC_INFO
WHERE PACKAGE_NO=:PACKAGE_NO 
    AND GROUP_NO=:GROUP_NO 
    AND VERSION_NO=:VERSION_NO 
    AND PARAMETER_ID=:PARAMETER_ID 
    AND FACTORY_ID=:FACTORY_ID 
    AND SPEC_TYPE=:SPEC_TYPE
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,PARAMETER_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("SPEC_TYPE",OracleType.VarChar,15,SPEC_TYPE)
            });
        }

        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetDataByPackageId(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SPEC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        FACTORY_ID,
        PARAMETER_ID,
        SPEC_TYPE,
        PARAM_UNIT,
        TARGET,
        USL,
        LSL,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_PARAM_SPEC_INFO
WHERE PACKAGE_NO=:PACKAGE_NO    
    AND VERSION_NO=:VERSION_NO    
    AND FACTORY_ID=:FACTORY_ID    
" + queryStr, new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),            
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),         
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SPEC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  B.TOTAL,
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        FACTORY_ID,
        PARAMETER_ID,
        SPEC_TYPE,
        PARAM_UNIT,
        TARGET,
        USL,
        LSL,
        UPDATE_USER,
        UPDATE_DATE
FROM (  SELECT ROWNUM AS ROWINDEX,
                PACKAGE_NO,
                GROUP_NO,
                VERSION_NO,
                FACTORY_ID,
                PARAMETER_ID,
                SPEC_TYPE,
                PARAM_UNIT,
                TARGET,
                USL,
                LSL,
                UPDATE_USER,
                TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
        FROM PACKAGE_PARAM_SPEC_INFO
           WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER ) A,
     (  SELECT COUNT (1) TOTAL FROM PACKAGE_PARAM_SPEC_INFO ) B
WHERE A.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetDataByParamId(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string PARAMETER_ID, string FACTORY_ID)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SPEC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.VERSION_NO,
       A.FACTORY_ID,
       A.PARAMETER_ID,
       A.SPEC_TYPE,
       A.PARAM_UNIT,
       A.TARGET,
       A.USL,
       A.LSL,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       C.PARAM_DESC
  FROM PACKAGE_PARAM_SPEC_INFO A, PACKAGE_BASE_INFO B, PARAMETER_LIST C
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.GROUP_NO = :GROUP_NO
       AND A.VERSION_NO = :VERSION_NO
       AND A.PARAMETER_ID = :PARAMETER_ID
       AND A.FACTORY_ID = :FACTORY_ID
       AND B.FACTORY_ID = A.FACTORY_ID
       AND B.PACKAGE_NO = A.PACKAGE_NO
       AND B.VERSION_NO = A.VERSION_NO
       AND C.FACTORY_ID = B.FACTORY_ID
       AND C.PRODUCT_TYPE_ID = B.PRODUCT_TYPE_ID
       AND C.PRODUCT_PROC_TYPE_ID = B.PRODUCT_PROC_TYPE_ID
       AND C.PARAMETER_ID = A.PARAMETER_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,PARAMETER_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetDataByProcessIDAndGroupNo(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SPEC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT B.PACKAGE_NO,
       B.GROUP_NO,
       B.VERSION_NO,
       B.FACTORY_ID,
       B.PARAMETER_ID,
       B.SPEC_TYPE
  FROM PACKAGE_PARAM_SETTING A, PACKAGE_PARAM_SPEC_INFO B
 WHERE     B.PACKAGE_NO = A.PACKAGE_NO
       AND B.GROUP_NO = A.GROUP_NO
       AND B.VERSION_NO = A.VERSION_NO
       AND B.FACTORY_ID = A.FACTORY_ID
       AND B.PARAMETER_ID = A.PARAMETER_ID
       AND A.PACKAGE_NO = :PACKAGE_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.VERSION_NO = :VERSION_NO
       AND A.PROCESS_ID = :PROCESS_ID
       AND A.GROUP_NO=:GROUP_NO
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),   
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),     
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID)
            });
        }

        //工序-参数
        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetDataByProcessIDAndGroupNoWithSetting(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SPEC_INFO_Entity>(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.FACTORY_ID,
       A.VERSION_NO,
       A.PARAMETER_ID,
       PARAM.PARAM_NAME,
       PARAM.PARAM_DESC,
       A.PARAM_TYPE_ID,
       A.PROCESS_ID,
       B.SPEC_TYPE,
       B.PARAM_UNIT,
       B.TARGET,
       B.USL,
       B.LSL,
       A.PROC_TASK_ID,
       A.DISP_ORDER_IN_SC,
       A.PARAM_IO,
       A.IS_GROUP_PARAM,
       A.IS_FIRST_CHECK_PARAM,
       A.IS_PROC_MON_PARAM,
       A.IS_OUTPUT_PARAM,
       A.PARAM_DATATYPE,
       A.ILLUSTRATION_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,       
       A.SAMPLING_FREQUENCY,
       A.CONTROL_METHOD,
       A.IS_SC_PARAM
  FROM PACKAGE_PARAM_SETTING A,
       PACKAGE_PARAM_SPEC_INFO B,
       PACKAGE_BASE_INFO C,
       PROCESS_PARAM_INFO D,
       PARAMETER_LIST PARAM
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.VERSION_NO = :VERSION_NO
       AND A.GROUP_NO = :GROUP_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND B.PACKAGE_NO = A.PACKAGE_NO
       AND B.VERSION_NO = A.VERSION_NO
       AND B.FACTORY_ID = A.FACTORY_ID
       AND B.GROUP_NO = A.GROUP_NO
       AND B.PARAMETER_ID = A.PARAMETER_ID
       AND C.PACKAGE_NO = A.PACKAGE_NO
       AND C.VERSION_NO = A.VERSION_NO
       AND C.FACTORY_ID = A.FACTORY_ID
       AND D.PROCESS_ID = :PROCESS_ID
       AND D.FACTORY_ID = A.FACTORY_ID
       AND D.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID
       AND D.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID
       AND D.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.FACTORY_ID = A.FACTORY_ID
       AND PARAM.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID
       AND PARAM.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID
       AND (PARAM.PARAM_TYPE_ID='PRODUCT' OR PARAM.PARAM_TYPE_ID='PROCESS') 
       AND IS_ILLUSTRATION_PARAM!=1
" + queryStr + " ORDER BY A.DISP_ORDER_IN_SC, B.SPEC_TYPE DESC", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),   
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),     
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID)
            }
                );

        }

        //物料类型-参数
        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetDataByMaterialTypeId(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string FACTORY_ID, string MATERIAL_TYPE_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SPEC_INFO_Entity>(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.FACTORY_ID,
       A.VERSION_NO,
       A.PARAMETER_ID,
       PARAM.PARAM_NAME,
       PARAM.PARAM_DESC,
       A.PARAM_TYPE_ID,
       A.PROCESS_ID,
       B.SPEC_TYPE,
       B.PARAM_UNIT,
       B.TARGET,
       B.USL,
       B.LSL,
       A.PROC_TASK_ID,
       A.DISP_ORDER_IN_SC,
       A.PARAM_IO,
       A.IS_GROUP_PARAM,
       A.IS_FIRST_CHECK_PARAM,
       A.IS_PROC_MON_PARAM,
       A.IS_OUTPUT_PARAM,
       A.PARAM_DATATYPE,
       A.ILLUSTRATION_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,       
       A.SAMPLING_FREQUENCY,
       A.CONTROL_METHOD,
       A.IS_SC_PARAM
  FROM PACKAGE_PARAM_SETTING A,
       PACKAGE_PARAM_SPEC_INFO B,
       PACKAGE_BASE_INFO C,
       MATERIAL_PARA_INFO D,
       PARAMETER_LIST PARAM
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.VERSION_NO = :VERSION_NO
       AND A.GROUP_NO = :GROUP_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND B.PACKAGE_NO = A.PACKAGE_NO
       AND B.VERSION_NO = A.VERSION_NO
       AND B.FACTORY_ID = A.FACTORY_ID
       AND B.GROUP_NO = A.GROUP_NO
       AND B.PARAMETER_ID = A.PARAMETER_ID
       AND C.PACKAGE_NO = A.PACKAGE_NO
       AND C.VERSION_NO = A.VERSION_NO
       AND C.FACTORY_ID = A.FACTORY_ID
       AND D.MATERIAL_TYPE_ID = :MATERIAL_TYPE_ID
       AND D.FACTORY_ID = A.FACTORY_ID
       AND D.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID
       AND D.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID
       AND D.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.FACTORY_ID = A.FACTORY_ID
       AND PARAM.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID
       AND PARAM.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID
       AND (PARAM.PARAM_TYPE_ID = 'MATERIAL')
" + queryStr + " ORDER BY A.DISP_ORDER_IN_SC, B.SPEC_TYPE DESC", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),   
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),     
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("MATERIAL_TYPE_ID",OracleType.VarChar,20,MATERIAL_TYPE_ID)
            }
                );

        }

        //物料编号-参数
        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetDataByMaterialPNId(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string FACTORY_ID, string MATERIAL_PN_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SPEC_INFO_Entity>(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.FACTORY_ID,
       A.VERSION_NO,
       A.PARAMETER_ID,
       PARAM.PARAM_NAME,
       PARAM.PARAM_DESC,
       A.PARAM_TYPE_ID,
       A.PROCESS_ID,
       B.SPEC_TYPE,
       B.PARAM_UNIT,
       B.TARGET,
       B.USL,
       B.LSL,
       A.PROC_TASK_ID,
       A.DISP_ORDER_IN_SC,
       A.PARAM_IO,
       A.IS_GROUP_PARAM,
       A.IS_FIRST_CHECK_PARAM,
       A.IS_PROC_MON_PARAM,
       A.IS_OUTPUT_PARAM,
       A.PARAM_DATATYPE,
       A.ILLUSTRATION_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,       
       A.SAMPLING_FREQUENCY,
       A.CONTROL_METHOD,
       A.IS_SC_PARAM
  FROM PACKAGE_PARAM_SETTING A,
       PACKAGE_PARAM_SPEC_INFO B,
       PACKAGE_BASE_INFO C,
       MATERIAL_PN_PARA_INFO D,
       PARAMETER_LIST PARAM
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.VERSION_NO = :VERSION_NO
       AND A.GROUP_NO = :GROUP_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND B.PACKAGE_NO = A.PACKAGE_NO
       AND B.VERSION_NO = A.VERSION_NO
       AND B.FACTORY_ID = A.FACTORY_ID
       AND B.GROUP_NO = A.GROUP_NO
       AND B.PARAMETER_ID = A.PARAMETER_ID
       AND C.PACKAGE_NO = A.PACKAGE_NO
       AND C.VERSION_NO = A.VERSION_NO
       AND C.FACTORY_ID = A.FACTORY_ID
       AND D.MATERIAL_PN_ID = :MATERIAL_PN_ID
       AND D.FACTORY_ID = A.FACTORY_ID
       AND D.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID
       AND D.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID
       AND D.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.FACTORY_ID = A.FACTORY_ID
       AND PARAM.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID
       AND PARAM.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID
       AND (PARAM.PARAM_TYPE_ID = 'MATERIAL')
" + queryStr + " ORDER BY A.DISP_ORDER_IN_SC, B.SPEC_TYPE DESC", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),   
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),     
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,15,MATERIAL_PN_ID)
            }
                );

        }

        //设备类型-参数
        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetDataByEquipmentClass(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string FACTORY_ID, string EQUIPMENT_CLASS_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SPEC_INFO_Entity>(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.FACTORY_ID,
       A.VERSION_NO,
       A.PARAMETER_ID,
       PARAM.PARAM_NAME,
       PARAM.PARAM_DESC,
       A.PARAM_TYPE_ID,
       A.PROCESS_ID,
       B.SPEC_TYPE,
       B.PARAM_UNIT,
       B.TARGET,
       B.USL,
       B.LSL,
       A.PROC_TASK_ID,
       A.DISP_ORDER_IN_SC,
       A.PARAM_IO,
       A.IS_GROUP_PARAM,
       A.IS_FIRST_CHECK_PARAM,
       A.IS_PROC_MON_PARAM,
       A.IS_OUTPUT_PARAM,
       A.PARAM_DATATYPE,
       A.ILLUSTRATION_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,       
       A.SAMPLING_FREQUENCY,
       A.CONTROL_METHOD,
       A.IS_SC_PARAM
  FROM PACKAGE_PARAM_SETTING A,
       PACKAGE_PARAM_SPEC_INFO B,
       PACKAGE_BASE_INFO C,
       EQUIPMENT_CLASS_PARAM_INFO D,
       PARAMETER_LIST PARAM
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.VERSION_NO = :VERSION_NO
       AND A.GROUP_NO = :GROUP_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND B.PACKAGE_NO = A.PACKAGE_NO
       AND B.VERSION_NO = A.VERSION_NO
       AND B.FACTORY_ID = A.FACTORY_ID
       AND B.GROUP_NO = A.GROUP_NO
       AND B.PARAMETER_ID = A.PARAMETER_ID
       AND C.PACKAGE_NO = A.PACKAGE_NO
       AND C.VERSION_NO = A.VERSION_NO
       AND C.FACTORY_ID = A.FACTORY_ID
       AND D.EQUIPMENT_CLASS_ID = :EQUIPMENT_CLASS_ID
       AND D.FACTORY_ID = A.FACTORY_ID
       AND D.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID
       AND D.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID
       AND D.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.FACTORY_ID = A.FACTORY_ID
       AND PARAM.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID
       AND PARAM.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID
       AND (   PARAM.PARAM_TYPE_ID = 'MC'
            OR PARAM.PARAM_TYPE_ID = 'TESTER'
            OR PARAM.PARAM_TYPE_ID = 'FIXTURE')
" + queryStr + " ORDER BY A.DISP_ORDER_IN_SC, B.SPEC_TYPE DESC", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),   
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),     
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,20,EQUIPMENT_CLASS_ID)
            }
                );

        }

        //设备编号-参数
        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetDataByEquipmentInfo(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string FACTORY_ID, string EQUIPMENT_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SPEC_INFO_Entity>(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.FACTORY_ID,
       A.VERSION_NO,
       A.PARAMETER_ID,
       PARAM.PARAM_NAME,
       PARAM.PARAM_DESC,
       A.PARAM_TYPE_ID,
       A.PROCESS_ID,
       B.SPEC_TYPE,
       B.PARAM_UNIT,
       B.TARGET,
       B.USL,
       B.LSL,
       A.PROC_TASK_ID,
       A.DISP_ORDER_IN_SC,
       A.PARAM_IO,
       A.IS_GROUP_PARAM,
       A.IS_FIRST_CHECK_PARAM,
       A.IS_PROC_MON_PARAM,
       A.IS_OUTPUT_PARAM,
       A.PARAM_DATATYPE,
       A.ILLUSTRATION_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,       
       A.SAMPLING_FREQUENCY,
       A.CONTROL_METHOD,
       A.IS_SC_PARAM
  FROM PACKAGE_PARAM_SETTING A,
       PACKAGE_PARAM_SPEC_INFO B,
       PACKAGE_BASE_INFO C,
       EQUIPMENT_PARAM_INFO D,
       PARAMETER_LIST PARAM
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.VERSION_NO = :VERSION_NO
       AND A.GROUP_NO = :GROUP_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND B.PACKAGE_NO = A.PACKAGE_NO
       AND B.VERSION_NO = A.VERSION_NO
       AND B.FACTORY_ID = A.FACTORY_ID
       AND B.GROUP_NO = A.GROUP_NO
       AND B.PARAMETER_ID = A.PARAMETER_ID
       AND C.PACKAGE_NO = A.PACKAGE_NO
       AND C.VERSION_NO = A.VERSION_NO
       AND C.FACTORY_ID = A.FACTORY_ID
       AND D.EQUIPMENT_ID = :EQUIPMENT_ID
       AND D.FACTORY_ID = A.FACTORY_ID
       AND D.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID
       AND D.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID
       AND D.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.FACTORY_ID = A.FACTORY_ID
       AND PARAM.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID
       AND PARAM.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID
       AND (   PARAM.PARAM_TYPE_ID = 'MC'
            OR PARAM.PARAM_TYPE_ID = 'TESTER'
            OR PARAM.PARAM_TYPE_ID = 'FIXTURE')
" + queryStr + " ORDER BY A.DISP_ORDER_IN_SC, B.SPEC_TYPE DESC", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),   
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),     
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("EQUIPMENT_ID",OracleType.VarChar,20,EQUIPMENT_ID)
            }
                );

        }

//设备编号-参数
        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetDataByIllustrationId(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string FACTORY_ID, string ILLUSTRATION_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SPEC_INFO_Entity>(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.FACTORY_ID,
       A.VERSION_NO,
       A.PARAMETER_ID,
       PARAM.PARAM_NAME,
       PARAM.PARAM_DESC,
       A.PARAM_TYPE_ID,
       A.PROCESS_ID,
       B.SPEC_TYPE,
       B.PARAM_UNIT,
       B.TARGET,
       B.USL,
       B.LSL,
       A.PROC_TASK_ID,
       A.DISP_ORDER_IN_SC,
       A.PARAM_IO,
       A.IS_GROUP_PARAM,
       A.IS_FIRST_CHECK_PARAM,
       A.IS_PROC_MON_PARAM,
       A.IS_OUTPUT_PARAM,
       A.PARAM_DATATYPE,
       A.ILLUSTRATION_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,       
       A.SAMPLING_FREQUENCY,
       A.CONTROL_METHOD,
       A.IS_SC_PARAM
  FROM PACKAGE_PARAM_SETTING A,
       PACKAGE_PARAM_SPEC_INFO B,
       PACKAGE_BASE_INFO C,
       ILLUSTRATION_PARAM_INFO D,
       PARAMETER_LIST PARAM
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.VERSION_NO = :VERSION_NO
       AND A.GROUP_NO = :GROUP_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND B.PACKAGE_NO = A.PACKAGE_NO
       AND B.VERSION_NO = A.VERSION_NO
       AND B.FACTORY_ID = A.FACTORY_ID
       AND B.GROUP_NO = A.GROUP_NO
       AND B.PARAMETER_ID = A.PARAMETER_ID
       AND C.PACKAGE_NO = A.PACKAGE_NO
       AND C.VERSION_NO = A.VERSION_NO
       AND C.FACTORY_ID = A.FACTORY_ID
       AND D.ILLUSTRATION_ID = :ILLUSTRATION_ID
       AND D.FACTORY_ID = A.FACTORY_ID
       AND D.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID
       AND D.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID
       AND D.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.FACTORY_ID = A.FACTORY_ID
       AND PARAM.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID
       AND PARAM.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID
       AND (PARAM.PARAM_TYPE_ID='PRODUCT' OR PARAM.PARAM_TYPE_ID='PROCESS') 
" + queryStr + " ORDER BY A.DISP_ORDER_IN_SC,B.SPEC_TYPE DESC", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),   
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),     
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,15,ILLUSTRATION_ID)
            }
                );

        }
        #endregion

        #region 新增

        public int PostAdd(PACKAGE_PARAM_SPEC_INFO_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PACKAGE_PARAM_SPEC_INFO
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND VERSION_NO=:VERSION_NO AND PARAMETER_ID=:PARAMETER_ID AND FACTORY_ID=:FACTORY_ID AND SPEC_TYPE=:SPEC_TYPE
",
                new[]{
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
                    OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
                    OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
                    OracleHelper.MakeInParam("SPEC_TYPE",OracleType.VarChar,15,entity.SPEC_TYPE)
                })
                ) > 0;
            if (exist)
            {
                return 0;
            }
            #endregion
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO PACKAGE_PARAM_SPEC_INFO (
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        FACTORY_ID,
        PARAMETER_ID,
        SPEC_TYPE,
        PARAM_UNIT,
        TARGET,
        USL,
        LSL,
        UPDATE_USER,
        UPDATE_DATE
)
VALUES (
        :PACKAGE_NO,
        :GROUP_NO,
        :VERSION_NO,
        :FACTORY_ID,
        :PARAMETER_ID,
        :SPEC_TYPE,
        :PARAM_UNIT,
        :TARGET,
        :USL,
        :LSL,
        :UPDATE_USER,
        :UPDATE_DATE
)
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("SPEC_TYPE",OracleType.VarChar,15,entity.SPEC_TYPE),
            OracleHelper.MakeInParam("PARAM_UNIT",OracleType.VarChar,8,entity.PARAM_UNIT),
            OracleHelper.MakeInParam("TARGET",OracleType.VarChar,25,entity.TARGET),
            OracleHelper.MakeInParam("USL",OracleType.VarChar,25,entity.USL),
            OracleHelper.MakeInParam("LSL",OracleType.VarChar,25,entity.LSL),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PARAM_SPEC_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_PARAM_SPEC_INFO SET 
    PARAM_UNIT=:PARAM_UNIT,
    TARGET=:TARGET,
    USL=:USL,
    LSL=:LSL,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND VERSION_NO=:VERSION_NO AND PARAMETER_ID=:PARAMETER_ID AND FACTORY_ID=:FACTORY_ID AND SPEC_TYPE=:SPEC_TYPE
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("SPEC_TYPE",OracleType.VarChar,15,entity.SPEC_TYPE),
            OracleHelper.MakeInParam("PARAM_UNIT",OracleType.VarChar,8,entity.PARAM_UNIT),
            OracleHelper.MakeInParam("TARGET",OracleType.VarChar,25,entity.TARGET),
            OracleHelper.MakeInParam("USL",OracleType.VarChar,25,entity.USL),
            OracleHelper.MakeInParam("LSL",OracleType.VarChar,25,entity.LSL),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
            });
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_PARAM_SPEC_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_PARAM_SPEC_INFO WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND VERSION_NO=:VERSION_NO AND PARAMETER_ID=:PARAMETER_ID AND FACTORY_ID=:FACTORY_ID AND SPEC_TYPE=:SPEC_TYPE
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("SPEC_TYPE",OracleType.VarChar,15,entity.SPEC_TYPE)
            });
        }

        public int DeleteByPackageId(PACKAGE_PARAM_SPEC_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_PARAM_SPEC_INFO 
    WHERE PACKAGE_NO=:PACKAGE_NO         
        AND VERSION_NO=:VERSION_NO 
        AND FACTORY_ID=:FACTORY_ID        
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),            
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)            
            });
        }

        public int DeleteByPackageIdAndGroupNo(PACKAGE_PARAM_SPEC_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_PARAM_SPEC_INFO 
    WHERE PACKAGE_NO=:PACKAGE_NO      
        AND GROUP_NO=:GROUP_NO   
        AND VERSION_NO=:VERSION_NO 
        AND FACTORY_ID=:FACTORY_ID        
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),  
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)            
            });
        }

        public int PostDeleteByParamId(PACKAGE_PARAM_SPEC_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_PARAM_SPEC_INFO WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND VERSION_NO=:VERSION_NO AND PARAMETER_ID=:PARAMETER_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)            
            });
        }

        #endregion
    }
}
