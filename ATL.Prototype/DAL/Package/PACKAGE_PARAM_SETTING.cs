using DBUtility;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Package
{
    public class PACKAGE_PARAM_SETTING
    {
        #region 查询

        public List<PACKAGE_PARAM_SETTING_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SETTING_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        FACTORY_ID,
        VERSION_NO,
        PARAMETER_ID,
        PARAM_TYPE_ID,
        PROCESS_ID,
        PROC_TASK_ID,       
        DISP_ORDER_IN_SC,
        PARAM_IO,
        IS_GROUP_PARAM,
        IS_FIRST_CHECK_PARAM,
        IS_PROC_MON_PARAM,
        IS_OUTPUT_PARAM,
        PARAM_UNIT,
        PARAM_DATATYPE,
        TARGET,
        USL,
        LSL,
        ILLUSTRATION_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        SAMPLING_FREQUENCY,
        CONTROL_METHOD,
        IS_SC_PARAM
FROM PACKAGE_PARAM_SETTING
", null);
        }

        public List<PACKAGE_PARAM_SETTING_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SETTING_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  TOTAL, 
        PACKAGE_NO,
        GROUP_NO,
        FACTORY_ID,
        VERSION_NO,
        PARAMETER_ID,
        PARAM_TYPE_ID,
        PROCESS_ID,
        PROC_TASK_ID,
        DISP_ORDER_IN_SC,
        PARAM_IO,
        IS_GROUP_PARAM,
        IS_FIRST_CHECK_PARAM,
        IS_PROC_MON_PARAM,
        IS_OUTPUT_PARAM,
        PARAM_UNIT,
        PARAM_DATATYPE,
        TARGET,
        USL,
        LSL,
        ILLUSTRATION_ID,
        UPDATE_USER,
        UPDATE_DATE,
        SAMPLING_FREQUENCY,
        CONTROL_METHOD,
        IS_SC_PARAM
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            PACKAGE_NO,
            GROUP_NO,
            FACTORY_ID,
            VERSION_NO,
            PARAMETER_ID,
            PARAM_TYPE_ID,
            PROCESS_ID,
            PROC_TASK_ID,
            DISP_ORDER_IN_SC,
            PARAM_IO,
            IS_GROUP_PARAM,
            IS_FIRST_CHECK_PARAM,
            IS_PROC_MON_PARAM,
            IS_OUTPUT_PARAM,
            PARAM_UNIT,
            PARAM_DATATYPE,
            TARGET,
            USL,
            LSL,
            ILLUSTRATION_ID,
            UPDATE_USER,
            UPDATE_DATE,
            SAMPLING_FREQUENCY,
            CONTROL_METHOD,
            IS_SC_PARAM
        FROM (  SELECT 
                    PACKAGE_NO,
                    GROUP_NO,
                    FACTORY_ID,
                    VERSION_NO,
                    PARAMETER_ID,
                    PARAM_TYPE_ID,
                    PROCESS_ID,
                    PROC_TASK_ID,
                    DISP_ORDER_IN_SC,
                    PARAM_IO,
                    IS_GROUP_PARAM,
                    IS_FIRST_CHECK_PARAM,
                    IS_PROC_MON_PARAM,
                    IS_OUTPUT_PARAM,
                    PARAM_UNIT,
                    PARAM_DATATYPE,
                    TARGET,
                    USL,
                    LSL,
                    ILLUSTRATION_ID,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    SAMPLING_FREQUENCY,
                    CONTROL_METHOD,
                    IS_SC_PARAM
                FROM PACKAGE_PARAM_SETTING ) T1,          
            (  SELECT COUNT (1) TOTAL FROM PACKAGE_PARAM_SETTING ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByPackageId(string PACKAGE_NO, string FACTORY_ID, string VERSION_NO, string queryStr)
        {
            var result = OracleHelper.SelectedToIList<PACKAGE_PARAM_SETTING_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        FACTORY_ID,
        VERSION_NO,
        PARAMETER_ID,
        PARAM_TYPE_ID,
        PROCESS_ID,
        PROC_TASK_ID,
        DISP_ORDER_IN_SC,
        PARAM_IO,
        IS_GROUP_PARAM,
        IS_FIRST_CHECK_PARAM,
        IS_PROC_MON_PARAM,
        IS_OUTPUT_PARAM,
        PARAM_UNIT,
        PARAM_DATATYPE,
        TARGET,
        USL,
        LSL,
        ILLUSTRATION_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        SAMPLING_FREQUENCY,
        CONTROL_METHOD,
        IS_SC_PARAM
FROM PACKAGE_PARAM_SETTING
WHERE PACKAGE_NO=:PACKAGE_NO    
    AND FACTORY_ID=:FACTORY_ID 
    AND VERSION_NO=:VERSION_NO     
" + queryStr, new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO)
            });
            return result;
        }

        public List<PACKAGE_PARAM_SETTING_Entity> GetDataById(string PACKAGE_NO, string GROUP_NO, string FACTORY_ID, string VERSION_NO, string PARAMETER_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SETTING_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        FACTORY_ID,
        VERSION_NO,
        PARAMETER_ID,
        PARAM_TYPE_ID,
        PROCESS_ID,
        PROC_TASK_ID,
        DISP_ORDER_IN_SC,
        PARAM_IO,
        IS_GROUP_PARAM,
        IS_FIRST_CHECK_PARAM,
        IS_PROC_MON_PARAM,
        IS_OUTPUT_PARAM,
        PARAM_UNIT,
        PARAM_DATATYPE,
        TARGET,
        USL,
        LSL,
        ILLUSTRATION_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        SAMPLING_FREQUENCY,
        CONTROL_METHOD,
        IS_SC_PARAM
FROM PACKAGE_PARAM_SETTING
WHERE PACKAGE_NO=:PACKAGE_NO 
    AND GROUP_NO=:GROUP_NO 
    AND FACTORY_ID=:FACTORY_ID 
    AND VERSION_NO=:VERSION_NO 
    AND PARAMETER_ID=:PARAMETER_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,PARAMETER_ID)
            });
        }

        //工序-参数
        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByProcessIdAndGroupNo(string PACKAGE_NO, string GROUP_NO, string FACTORY_ID, string VERSION_NO, string PROCESS_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SETTING_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PACKAGE_NO,
         A.GROUP_NO,
         A.FACTORY_ID,
         A.VERSION_NO,
         A.PARAMETER_ID,
         A.PARAM_TYPE_ID,
         A.PROCESS_ID,
         A.PROC_TASK_ID,        
         A.DISP_ORDER_IN_SC,
         A.PARAM_IO,
         A.IS_GROUP_PARAM,
         A.IS_FIRST_CHECK_PARAM,
         A.IS_PROC_MON_PARAM,
         A.IS_OUTPUT_PARAM,
         A.PARAM_UNIT,
         A.PARAM_DATATYPE,
         A.TARGET,
         A.USL,
         A.LSL,
         A.ILLUSTRATION_ID,
         A.UPDATE_USER,
         TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
         A.SAMPLING_FREQUENCY,
         A.CONTROL_METHOD,
         A.IS_SC_PARAM,
         C.PARAM_ORDER_NO,         
         PARAM.PARAM_NAME,
         PARAM.PARAM_DESC,
         PARAM.PARAM_TYPE_ID
    FROM PACKAGE_PARAM_SETTING A,
         PACKAGE_BASE_INFO B,
         PROCESS_PARAM_INFO C,
         PARAMETER_LIST PARAM
   WHERE     A.PACKAGE_NO = :PACKAGE_NO
         AND A.GROUP_NO = :GROUP_NO
         AND A.FACTORY_ID = :FACTORY_ID
         AND A.VERSION_NO = :VERSION_NO
         AND B.PACKAGE_NO = :PACKAGE_NO
         AND B.VERSION_NO = :VERSION_NO
         AND B.FACTORY_ID = :FACTORY_ID
         AND C.PRODUCT_TYPE_ID = B.PRODUCT_TYPE_ID
         AND C.PRODUCT_PROC_TYPE_ID = B.PRODUCT_PROC_TYPE_ID
         AND C.FACTORY_ID = B.FACTORY_ID
         AND PARAM.PARAMETER_ID = C.PARAMETER_ID
         AND PARAM.FACTORY_ID = C.FACTORY_ID
         AND PARAM.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID
         AND PARAM.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID
         AND A.PARAMETER_ID = PARAM.PARAMETER_ID
         AND C.PROCESS_ID = :PROCESS_ID 
         AND (PARAM.PARAM_TYPE_ID='PRODUCT' OR PARAM.PARAM_TYPE_ID='PROCESS') 
         AND IS_ILLUSTRATION_PARAM!=1 
" + queryStr +
@" ORDER BY C.PARAM_ORDER_NO  
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,25,PROCESS_ID)
            });
        }

        //设备类型-参数
        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByEquipmentClass(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string GROUP_NO, string EQUIPMENT_CLASS_ID, string queryStr)
        {

            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SETTING_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.FACTORY_ID,
       A.VERSION_NO,
       A.PARAMETER_ID,
       A.PARAM_TYPE_ID,
       A.PROCESS_ID,
       A.PROC_TASK_ID,
       A.DISP_ORDER_IN_SC,
       A.PARAM_IO,
       A.IS_GROUP_PARAM,
       A.IS_FIRST_CHECK_PARAM,
       A.IS_PROC_MON_PARAM,
       A.IS_OUTPUT_PARAM,
       A.PARAM_UNIT,
       A.PARAM_DATATYPE,
       A.TARGET,
       A.USL,
       A.LSL,
       A.ILLUSTRATION_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       A.SAMPLING_FREQUENCY,
       A.CONTROL_METHOD,
       A.IS_SC_PARAM,      
       E.DISP_ORDER_NO,
       PARAM.PARAM_NAME,
       PARAM.PARAM_DESC,
       PARAM.PARAM_TYPE_ID
  FROM PACKAGE_PARAM_SETTING A,
       PACKAGE_BASE_INFO B,
       PARAMETER_LIST PARAM,      
       EQUIPMENT_CLASS_PARAM_INFO E       
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.GROUP_NO = :GROUP_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.VERSION_NO = :VERSION_NO
       AND B.PACKAGE_NO = :PACKAGE_NO
       AND B.VERSION_NO = :VERSION_NO
       AND B.FACTORY_ID = :FACTORY_ID     
       AND PARAM.PARAMETER_ID=A.PARAMETER_ID
       AND PARAM.FACTORY_ID=:FACTORY_ID
       AND PARAM.PRODUCT_TYPE_ID=B.PRODUCT_TYPE_ID
       AND PARAM.PRODUCT_PROC_TYPE_ID=B.PRODUCT_PROC_TYPE_ID       
       AND E.EQUIPMENT_CLASS_ID=:EQUIPMENT_CLASS_ID
       AND E.FACTORY_ID=:FACTORY_ID
       AND E.PRODUCT_TYPE_ID=B.PRODUCT_TYPE_ID
       AND E.PRODUCT_PROC_TYPE_ID=B.PRODUCT_PROC_TYPE_ID
       AND E.PARAMETER_ID=A.PARAMETER_ID " + queryStr +
@" ORDER BY E.DISP_ORDER_NO  
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),            
            OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,25,EQUIPMENT_CLASS_ID)
            });
        }

        //设备PN-参数
        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByEquipmentInfo(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string GROUP_NO, string EQUIPMENT_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SETTING_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.FACTORY_ID,
       A.VERSION_NO,
       A.PARAMETER_ID,
       A.PARAM_TYPE_ID,
       A.PROCESS_ID,
       A.PROC_TASK_ID,
       A.DISP_ORDER_IN_SC,
       A.PARAM_IO,
       A.IS_GROUP_PARAM,
       A.IS_FIRST_CHECK_PARAM,
       A.IS_PROC_MON_PARAM,
       A.IS_OUTPUT_PARAM,
       A.PARAM_UNIT,
       A.PARAM_DATATYPE,
       A.TARGET,
       A.USL,
       A.LSL,
       A.ILLUSTRATION_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       A.SAMPLING_FREQUENCY,
       A.CONTROL_METHOD,
       A.IS_SC_PARAM,      
       E.DISP_ORDER_NO,
       PARAM.PARAM_NAME,
       PARAM.PARAM_DESC,
       PARAM.PARAM_TYPE_ID
  FROM PACKAGE_PARAM_SETTING A,
       PACKAGE_BASE_INFO B,
       PARAMETER_LIST PARAM,      
       EQUIPMENT_PARAM_INFO E       
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.GROUP_NO = :GROUP_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.VERSION_NO = :VERSION_NO
       AND B.PACKAGE_NO = :PACKAGE_NO
       AND B.VERSION_NO = :VERSION_NO
       AND B.FACTORY_ID = :FACTORY_ID     
       AND PARAM.PARAMETER_ID=A.PARAMETER_ID
       AND PARAM.FACTORY_ID=:FACTORY_ID
       AND PARAM.PRODUCT_TYPE_ID=B.PRODUCT_TYPE_ID
       AND PARAM.PRODUCT_PROC_TYPE_ID=B.PRODUCT_PROC_TYPE_ID       
       AND E.EQUIPMENT_ID=:EQUIPMENT_ID
       AND E.FACTORY_ID=:FACTORY_ID
       AND E.PRODUCT_TYPE_ID=B.PRODUCT_TYPE_ID
       AND E.PRODUCT_PROC_TYPE_ID=B.PRODUCT_PROC_TYPE_ID
       AND E.PARAMETER_ID=A.PARAMETER_ID  " + queryStr +
@" ORDER BY E.IS_SC_PARAM  
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),            
            OracleHelper.MakeInParam("EQUIPMENT_ID",OracleType.VarChar,25,EQUIPMENT_ID)
            });
        }

        //附图-参数
        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByIllustrationId(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string GROUP_NO, string ILLUSTRATION_ID, string queryStr)
        {

            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SETTING_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.FACTORY_ID,
       A.VERSION_NO,
       A.PARAMETER_ID,
       A.PARAM_TYPE_ID,
       A.PROCESS_ID,
       A.PROC_TASK_ID,
       A.DISP_ORDER_IN_SC,
       A.PARAM_IO,
       A.IS_GROUP_PARAM,
       A.IS_FIRST_CHECK_PARAM,
       A.IS_PROC_MON_PARAM,
       A.IS_OUTPUT_PARAM,
       A.PARAM_UNIT,
       A.PARAM_DATATYPE,
       A.TARGET,
       A.USL,
       A.LSL,
       A.ILLUSTRATION_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       A.SAMPLING_FREQUENCY,
       A.CONTROL_METHOD,
       A.IS_SC_PARAM,
       E.PARAM_ORDER_NO,
       PARAM.PARAM_NAME,
       PARAM.PARAM_DESC,
       PARAM.PARAM_TYPE_ID
  FROM PACKAGE_PARAM_SETTING A,
       PACKAGE_BASE_INFO B,
       PARAMETER_LIST PARAM,
       ILLUSTRATION_PARAM_INFO E
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.GROUP_NO = :GROUP_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.VERSION_NO = :VERSION_NO
       AND B.PACKAGE_NO = :PACKAGE_NO
       AND B.VERSION_NO = :VERSION_NO
       AND B.FACTORY_ID = :FACTORY_ID
       AND PARAM.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.FACTORY_ID = :FACTORY_ID
       AND PARAM.PRODUCT_TYPE_ID = B.PRODUCT_TYPE_ID
       AND PARAM.PRODUCT_PROC_TYPE_ID = B.PRODUCT_PROC_TYPE_ID
       AND E.ILLUSTRATION_ID = :ILLUSTRATION_ID
       AND E.FACTORY_ID = :FACTORY_ID
       AND E.PRODUCT_TYPE_ID = B.PRODUCT_TYPE_ID
       AND E.PRODUCT_PROC_TYPE_ID = B.PRODUCT_PROC_TYPE_ID
       AND E.PARAMETER_ID = A.PARAMETER_ID" + queryStr +
@" ORDER BY E.PARAM_ORDER_NO  
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),            
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,15,ILLUSTRATION_ID)
            });
        }

        //物料类型-参数
        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByMaterialTypeId(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string GROUP_NO, string MATERIAL_TYPE_ID, string queryStr)
        {

            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SETTING_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.FACTORY_ID,
       A.VERSION_NO,
       A.PARAMETER_ID,
       A.PARAM_TYPE_ID,
       A.PROCESS_ID,
       A.PROC_TASK_ID,
       A.DISP_ORDER_IN_SC,
       A.PARAM_IO,
       A.IS_GROUP_PARAM,
       A.IS_FIRST_CHECK_PARAM,
       A.IS_PROC_MON_PARAM,
       A.IS_OUTPUT_PARAM,
       A.PARAM_UNIT,
       A.PARAM_DATATYPE,
       A.TARGET,
       A.USL,
       A.LSL,
       A.ILLUSTRATION_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       A.SAMPLING_FREQUENCY,
       A.CONTROL_METHOD,
       A.IS_SC_PARAM,
       PARAM.PARAM_NAME,
       PARAM.PARAM_DESC,
       PARAM.PARAM_TYPE_ID
  FROM PACKAGE_PARAM_SETTING A,
       PACKAGE_BASE_INFO B,
       PARAMETER_LIST PARAM,
       MATERIAL_PARA_INFO E
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.GROUP_NO = :GROUP_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.VERSION_NO = :VERSION_NO
       AND B.PACKAGE_NO = :PACKAGE_NO
       AND B.VERSION_NO = :VERSION_NO
       AND B.FACTORY_ID = :FACTORY_ID
       AND PARAM.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.FACTORY_ID = :FACTORY_ID
       AND PARAM.PRODUCT_TYPE_ID = B.PRODUCT_TYPE_ID
       AND PARAM.PRODUCT_PROC_TYPE_ID = B.PRODUCT_PROC_TYPE_ID
       AND E.MATERIAL_TYPE_ID = :MATERIAL_TYPE_ID
       AND E.FACTORY_ID = :FACTORY_ID
       AND E.PRODUCT_TYPE_ID = B.PRODUCT_TYPE_ID
       AND E.PRODUCT_PROC_TYPE_ID = B.PRODUCT_PROC_TYPE_ID
       AND E.PARAMETER_ID = A.PARAMETER_ID
" + queryStr, new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),            
            OracleHelper.MakeInParam("MATERIAL_TYPE_ID",OracleType.VarChar,20,MATERIAL_TYPE_ID)
            });
        }

        //物料编号-参数
        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByMaterialPN(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string GROUP_NO, string MATERIAL_PN_ID, string queryStr)
        {

            return OracleHelper.SelectedToIList<PACKAGE_PARAM_SETTING_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.FACTORY_ID,
       A.VERSION_NO,
       A.PARAMETER_ID,
       A.PARAM_TYPE_ID,
       A.PROCESS_ID,
       A.PROC_TASK_ID,
       A.DISP_ORDER_IN_SC,
       A.PARAM_IO,
       A.IS_GROUP_PARAM,
       A.IS_FIRST_CHECK_PARAM,
       A.IS_PROC_MON_PARAM,
       A.IS_OUTPUT_PARAM,
       A.PARAM_UNIT,
       A.PARAM_DATATYPE,
       A.TARGET,
       A.USL,
       A.LSL,
       A.ILLUSTRATION_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       A.SAMPLING_FREQUENCY,
       A.CONTROL_METHOD,
       A.IS_SC_PARAM,
       PARAM.PARAM_NAME,
       PARAM.PARAM_DESC,
       PARAM.PARAM_TYPE_ID
  FROM PACKAGE_PARAM_SETTING A,
       PACKAGE_BASE_INFO B,
       PARAMETER_LIST PARAM,
       MATERIAL_PN_PARA_INFO E
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.GROUP_NO = :GROUP_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.VERSION_NO = :VERSION_NO
       AND B.PACKAGE_NO = :PACKAGE_NO
       AND B.VERSION_NO = :VERSION_NO
       AND B.FACTORY_ID = :FACTORY_ID
       AND PARAM.PARAMETER_ID = A.PARAMETER_ID
       AND PARAM.FACTORY_ID = :FACTORY_ID
       AND PARAM.PRODUCT_TYPE_ID = B.PRODUCT_TYPE_ID
       AND PARAM.PRODUCT_PROC_TYPE_ID = B.PRODUCT_PROC_TYPE_ID
       AND E.MATERIAL_PN_ID = :MATERIAL_PN_ID
       AND E.FACTORY_ID = :FACTORY_ID
       AND E.PRODUCT_TYPE_ID = B.PRODUCT_TYPE_ID
       AND E.PRODUCT_PROC_TYPE_ID = B.PRODUCT_PROC_TYPE_ID
       AND E.PARAMETER_ID = A.PARAMETER_ID
" + queryStr, new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),            
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,MATERIAL_PN_ID)
            });
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_PARAM_SETTING_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PACKAGE_PARAM_SETTING
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND FACTORY_ID=:FACTORY_ID AND VERSION_NO=:VERSION_NO AND PARAMETER_ID=:PARAMETER_ID
",
                new[]{
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
                    OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
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
INSERT INTO PACKAGE_PARAM_SETTING (
        PACKAGE_NO,
        GROUP_NO,
        FACTORY_ID,
        VERSION_NO,
        PARAMETER_ID,
        PARAM_TYPE_ID,
        PROCESS_ID,
        PROC_TASK_ID,        
        DISP_ORDER_IN_SC,
        PARAM_IO,
        IS_GROUP_PARAM,
        IS_FIRST_CHECK_PARAM,
        IS_PROC_MON_PARAM,
        IS_OUTPUT_PARAM,
        PARAM_UNIT,
        PARAM_DATATYPE,
        TARGET,
        USL,
        LSL,
        ILLUSTRATION_ID,
        UPDATE_USER,
        UPDATE_DATE,
        SAMPLING_FREQUENCY,
        CONTROL_METHOD,
        IS_SC_PARAM
)
VALUES (
        :PACKAGE_NO,
        :GROUP_NO,
        :FACTORY_ID,
        :VERSION_NO,
        :PARAMETER_ID,
        :PARAM_TYPE_ID,
        :PROCESS_ID,
        :PROC_TASK_ID,        
        :DISP_ORDER_IN_SC,
        :PARAM_IO,
        :IS_GROUP_PARAM,
        :IS_FIRST_CHECK_PARAM,
        :IS_PROC_MON_PARAM,
        :IS_OUTPUT_PARAM,
        :PARAM_UNIT,
        :PARAM_DATATYPE,
        :TARGET,
        :USL,
        :LSL,
        :ILLUSTRATION_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :SAMPLING_FREQUENCY,
        :CONTROL_METHOD,
        :IS_SC_PARAM
)
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("PARAM_TYPE_ID",OracleType.VarChar,15,entity.PARAM_TYPE_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,25,entity.PROCESS_ID),
            OracleHelper.MakeInParam("PROC_TASK_ID",OracleType.VarChar,15,entity.PROC_TASK_ID),           
            OracleHelper.MakeInParam("DISP_ORDER_IN_SC",OracleType.Number,0,entity.DISP_ORDER_IN_SC),
            OracleHelper.MakeInParam("PARAM_IO",OracleType.VarChar,1,entity.PARAM_IO),
            OracleHelper.MakeInParam("IS_GROUP_PARAM",OracleType.VarChar,1,entity.IS_GROUP_PARAM),
            OracleHelper.MakeInParam("IS_FIRST_CHECK_PARAM",OracleType.VarChar,1,entity.IS_FIRST_CHECK_PARAM),
            OracleHelper.MakeInParam("IS_PROC_MON_PARAM",OracleType.VarChar,1,entity.IS_PROC_MON_PARAM),
            OracleHelper.MakeInParam("IS_OUTPUT_PARAM",OracleType.VarChar,1,entity.IS_OUTPUT_PARAM),
            OracleHelper.MakeInParam("PARAM_UNIT",OracleType.VarChar,8,entity.PARAM_UNIT),
            OracleHelper.MakeInParam("PARAM_DATATYPE",OracleType.VarChar,8,entity.PARAM_DATATYPE),
            OracleHelper.MakeInParam("TARGET",OracleType.VarChar,25,entity.TARGET),
            OracleHelper.MakeInParam("USL",OracleType.VarChar,25,entity.USL),
            OracleHelper.MakeInParam("LSL",OracleType.VarChar,25,entity.LSL),
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,15,entity.ILLUSTRATION_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("SAMPLING_FREQUENCY",OracleType.VarChar,50,entity.SAMPLING_FREQUENCY),
            OracleHelper.MakeInParam("CONTROL_METHOD",OracleType.VarChar,50,entity.CONTROL_METHOD),
            OracleHelper.MakeInParam("IS_SC_PARAM",OracleType.VarChar,1,entity.IS_SC_PARAM)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PARAM_SETTING_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_PARAM_SETTING SET 
    PARAM_TYPE_ID=:PARAM_TYPE_ID,
    PROCESS_ID=:PROCESS_ID,
    PROC_TASK_ID=:PROC_TASK_ID,    
    DISP_ORDER_IN_SC=:DISP_ORDER_IN_SC,
    PARAM_IO=:PARAM_IO,
    IS_GROUP_PARAM=:IS_GROUP_PARAM,
    IS_FIRST_CHECK_PARAM=:IS_FIRST_CHECK_PARAM,
    IS_PROC_MON_PARAM=:IS_PROC_MON_PARAM,
    IS_OUTPUT_PARAM=:IS_OUTPUT_PARAM,
    PARAM_UNIT=:PARAM_UNIT,
    PARAM_DATATYPE=:PARAM_DATATYPE,
    TARGET=:TARGET,
    USL=:USL,
    LSL=:LSL,
    ILLUSTRATION_ID=:ILLUSTRATION_ID,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    SAMPLING_FREQUENCY=:SAMPLING_FREQUENCY,
    CONTROL_METHOD=:CONTROL_METHOD,
    IS_SC_PARAM=:IS_SC_PARAM
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND FACTORY_ID=:FACTORY_ID AND VERSION_NO=:VERSION_NO AND PARAMETER_ID=:PARAMETER_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("PARAM_TYPE_ID",OracleType.VarChar,15,entity.PARAM_TYPE_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,25,entity.PROCESS_ID),
            OracleHelper.MakeInParam("PROC_TASK_ID",OracleType.VarChar,15,entity.PROC_TASK_ID),           
            OracleHelper.MakeInParam("DISP_ORDER_IN_SC",OracleType.Number,0,entity.DISP_ORDER_IN_SC),
            OracleHelper.MakeInParam("PARAM_IO",OracleType.VarChar,1,entity.PARAM_IO),
            OracleHelper.MakeInParam("IS_GROUP_PARAM",OracleType.VarChar,1,entity.IS_GROUP_PARAM),
            OracleHelper.MakeInParam("IS_FIRST_CHECK_PARAM",OracleType.VarChar,1,entity.IS_FIRST_CHECK_PARAM),
            OracleHelper.MakeInParam("IS_PROC_MON_PARAM",OracleType.VarChar,1,entity.IS_PROC_MON_PARAM),
            OracleHelper.MakeInParam("IS_OUTPUT_PARAM",OracleType.VarChar,1,entity.IS_OUTPUT_PARAM),
            OracleHelper.MakeInParam("PARAM_UNIT",OracleType.VarChar,8,entity.PARAM_UNIT),
            OracleHelper.MakeInParam("PARAM_DATATYPE",OracleType.VarChar,8,entity.PARAM_DATATYPE),
            OracleHelper.MakeInParam("TARGET",OracleType.VarChar,25,entity.TARGET),
            OracleHelper.MakeInParam("USL",OracleType.VarChar,25,entity.USL),
            OracleHelper.MakeInParam("LSL",OracleType.VarChar,25,entity.LSL),
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,15,entity.ILLUSTRATION_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("SAMPLING_FREQUENCY",OracleType.VarChar,50,entity.SAMPLING_FREQUENCY),
            OracleHelper.MakeInParam("CONTROL_METHOD",OracleType.VarChar,50,entity.CONTROL_METHOD),
            OracleHelper.MakeInParam("IS_SC_PARAM",OracleType.VarChar,1,entity.IS_SC_PARAM)
            });
        }

        public int PostEdit_FAI(PACKAGE_PARAM_SETTING_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_PARAM_SETTING SET
    IS_FIRST_CHECK_PARAM=:IS_FIRST_CHECK_PARAM
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND FACTORY_ID=:FACTORY_ID AND VERSION_NO=:VERSION_NO AND PARAMETER_ID=:PARAMETER_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),            
            OracleHelper.MakeInParam("IS_FIRST_CHECK_PARAM",OracleType.VarChar,1,entity.IS_FIRST_CHECK_PARAM)
            });
        }

        public int PostEdit_PMI(PACKAGE_PARAM_SETTING_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_PARAM_SETTING SET    
    IS_PROC_MON_PARAM=:IS_PROC_MON_PARAM
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND FACTORY_ID=:FACTORY_ID AND VERSION_NO=:VERSION_NO AND PARAMETER_ID=:PARAMETER_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("IS_PROC_MON_PARAM",OracleType.VarChar,1,entity.IS_PROC_MON_PARAM)      
            });
        }

        public int PostEdit_OI(PACKAGE_PARAM_SETTING_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_PARAM_SETTING SET    
    IS_OUTPUT_PARAM=:IS_OUTPUT_PARAM    
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND FACTORY_ID=:FACTORY_ID AND VERSION_NO=:VERSION_NO AND PARAMETER_ID=:PARAMETER_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID),
            OracleHelper.MakeInParam("IS_OUTPUT_PARAM",OracleType.VarChar,1,entity.IS_OUTPUT_PARAM)            
            });
        }

        #endregion

        #region 删除
        public int PostDelete(PACKAGE_PARAM_SETTING_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_PARAM_SETTING WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND FACTORY_ID=:FACTORY_ID AND VERSION_NO=:VERSION_NO AND PARAMETER_ID=:PARAMETER_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PARAMETER_ID",OracleType.VarChar,25,entity.PARAMETER_ID)
            });
        }
        public int DeleteByPackageId(PACKAGE_PARAM_SETTING_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_PARAM_SETTING 
    WHERE PACKAGE_NO=:PACKAGE_NO        
        AND FACTORY_ID=:FACTORY_ID 
        AND VERSION_NO=:VERSION_NO        
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO)
            });
        }

        public int DeleteByPackageIdAndGroupNo(PACKAGE_PARAM_SETTING_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_PARAM_SETTING 
    WHERE PACKAGE_NO=:PACKAGE_NO      
        AND GROUP_NO=:GROUP_NO  
        AND FACTORY_ID=:FACTORY_ID 
        AND VERSION_NO=:VERSION_NO        
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),   
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO)
            });
        }

        public int DeleteByProcessIDAndGroupNo(PACKAGE_PARAM_SETTING_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_PARAM_SETTING
      WHERE     PACKAGE_NO = :PACKAGE_NO
            AND FACTORY_ID = :FACTORY_ID
            AND VERSION_NO = :VERSION_NO
            AND PROCESS_ID = :PROCESS_ID
            AND GROUP_NO=:GROUP_NO
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),       
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID)
            });
        }

        #endregion



    }
}
