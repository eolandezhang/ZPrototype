using DBUtility;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Package
{
    public class PACKAGE_BASE_INFO
    {

        #region 查询

        public List<PACKAGE_BASE_INFO_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PACKAGE_BASE_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        FACTORY_ID,
        VERSION_NO,
        GROUPS,
        GROUP_NO_LIST,
        GROUPS_PURPOSE,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PACKAGE_TYPE_ID,
        TO_CHAR (EFFECT_DATE, 'MM/DD/YYYY') EFFECT_DATE,
        BATTERY_MODEL,
        BATTERY_TYPE,
        BATTERY_LAYERS,
        BATTERY_QTY,
        BATTERY_PARTNO,
        PROJECT_CODE,
        CUSTOMER_CODE,
        PURPOSE,
        ORDER_TYPE,
        SO_NO,
        IS_URGENT,
        TO_CHAR (OUTPUT_TARGET_DATE, 'MM/DD/YYYY') OUTPUT_TARGET_DATE,
        REASON_FORURGENT,
        PREPARED_BY,
        TO_CHAR (PREPARED_DATE, 'MM/DD/YYYY HH24:MI:SS') PREPARED_DATE,
        APPROVE_FLOW_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        GROUP_QTY_LIST,
        VALID_FLAG,
        DELETE_FLAG,
        STATUS,
        PRODUCT_CHANGE_HL,
        PROCESS_CHANGE_HL,
        MATERIAL_CHANGE_HL,
        OTHER_CHANGE_HL
FROM PACKAGE_BASE_INFO
", null);
        }

        public List<PACKAGE_BASE_INFO_Entity> GetData(decimal pageSize, decimal pageNumber, string factoryId, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_BASE_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT TOTAL,     
       ROWINDEX, 
       PACKAGE_NO,
       FACTORY_ID,
       VERSION_NO,
       GROUPS,
       GROUP_NO_LIST,
       GROUPS_PURPOSE,
       PRODUCT_TYPE_ID,
       PRODUCT_PROC_TYPE_ID,
       PACKAGE_TYPE_ID,
       EFFECT_DATE,
       BATTERY_MODEL,
       BATTERY_TYPE,
       BATTERY_LAYERS,
       BATTERY_QTY,
       BATTERY_PARTNO,
       PROJECT_CODE,
       CUSTOMER_CODE,
       PURPOSE,
       ORDER_TYPE,
       SO_NO,
       IS_URGENT,
       OUTPUT_TARGET_DATE,
       REASON_FORURGENT,
       PREPARED_BY,
       PREPARED_DATE,
       APPROVE_FLOW_ID,
       UPDATE_USER,
       UPDATE_DATE,
       GROUP_QTY_LIST,
       VALID_FLAG,
       DELETE_FLAG,
       STATUS,
       PRODUCT_CHANGE_HL,
       PROCESS_CHANGE_HL,
       MATERIAL_CHANGE_HL,
       OTHER_CHANGE_HL
FROM (SELECT  
       T3.TOTAL TOTAL,
       ROWNUM AS ROWINDEX,
       PACKAGE_NO,
       FACTORY_ID,
       VERSION_NO,
       GROUPS,
       GROUP_NO_LIST,
       GROUPS_PURPOSE,
       PRODUCT_TYPE_ID,
       PRODUCT_PROC_TYPE_ID,
       PACKAGE_TYPE_ID,
       EFFECT_DATE,
       BATTERY_MODEL,
       BATTERY_TYPE,
       BATTERY_LAYERS,
       BATTERY_QTY,
       BATTERY_PARTNO,
       PROJECT_CODE,
       CUSTOMER_CODE,
       PURPOSE,
       ORDER_TYPE,
       SO_NO,
       IS_URGENT,
       OUTPUT_TARGET_DATE,
       REASON_FORURGENT,
       PREPARED_BY,
       PREPARED_DATE,
       APPROVE_FLOW_ID,
       UPDATE_USER,
       UPDATE_DATE,
       GROUP_QTY_LIST,
       VALID_FLAG,
       DELETE_FLAG,
       STATUS,
       PRODUCT_CHANGE_HL,
       PROCESS_CHANGE_HL,
       MATERIAL_CHANGE_HL,
       OTHER_CHANGE_HL
  FROM (  SELECT 
                 PACKAGE_NO,
                 FACTORY_ID,
                 VERSION_NO,
                 GROUPS,
                 GROUP_NO_LIST,
                 GROUPS_PURPOSE,
                 PRODUCT_TYPE_ID,
                 PRODUCT_PROC_TYPE_ID,
                 PACKAGE_TYPE_ID,
                 TO_CHAR (EFFECT_DATE, 'MM/DD/YYYY') EFFECT_DATE,
                 BATTERY_MODEL,
                 BATTERY_TYPE,
                 BATTERY_LAYERS,
                 BATTERY_QTY,
                 BATTERY_PARTNO,
                 PROJECT_CODE,
                 CUSTOMER_CODE,
                 PURPOSE,
                 ORDER_TYPE,
                 SO_NO,
                 IS_URGENT,
                 TO_CHAR (OUTPUT_TARGET_DATE, 'MM/DD/YYYY') OUTPUT_TARGET_DATE,
                 REASON_FORURGENT,
                 PREPARED_BY,
                 TO_CHAR (PREPARED_DATE, 'MM/DD/YYYY HH24:MI:SS') PREPARED_DATE,
                 APPROVE_FLOW_ID,
                 UPDATE_USER,
                 TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                 GROUP_QTY_LIST,
                 VALID_FLAG,
                 DELETE_FLAG,
                 STATUS,
                 PRODUCT_CHANGE_HL,
                 PROCESS_CHANGE_HL,
                 MATERIAL_CHANGE_HL,
        OTHER_CHANGE_HL
            FROM PACKAGE_BASE_INFO
           WHERE FACTORY_ID = :FACTORY_ID "
                + queryStr +
                @"  ORDER BY PACKAGE_NO, VERSION_NO) T1,
        (SELECT COUNT (1) TOTAL FROM PACKAGE_BASE_INFO) T3
 WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
  WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber),
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,factoryId)
 });
        }

        public List<PACKAGE_BASE_INFO_Entity> GetDataByPackageNo(string factoryId, string packageNo)
        {
            return OracleHelper.SelectedToIList<PACKAGE_BASE_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        FACTORY_ID,
        VERSION_NO,
        GROUPS,
        GROUP_NO_LIST,
        GROUPS_PURPOSE,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PACKAGE_TYPE_ID,
        TO_CHAR (EFFECT_DATE, 'MM/DD/YYYY') EFFECT_DATE,
        BATTERY_MODEL,
        BATTERY_TYPE,
        BATTERY_LAYERS,
        BATTERY_QTY,
        BATTERY_PARTNO,
        PROJECT_CODE,
        CUSTOMER_CODE,
        PURPOSE,
        ORDER_TYPE,
        SO_NO,
        IS_URGENT,
        TO_CHAR (OUTPUT_TARGET_DATE, 'MM/DD/YYYY') OUTPUT_TARGET_DATE,
        REASON_FORURGENT,
        PREPARED_BY,
        TO_CHAR (PREPARED_DATE, 'MM/DD/YYYY HH24:MI:SS') PREPARED_DATE,
        APPROVE_FLOW_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        GROUP_QTY_LIST,
        VALID_FLAG,
        DELETE_FLAG,
        STATUS,
        PRODUCT_CHANGE_HL,
        PROCESS_CHANGE_HL,
        MATERIAL_CHANGE_HL,
        OTHER_CHANGE_HL
FROM PACKAGE_BASE_INFO
WHERE UPPER(PACKAGE_NO)=:PACKAGE_NO
    AND FACTORY_ID=:FACTORY_ID
ORDER BY VERSION_NO
", new[]{
     OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,packageNo.ToUpper()),
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,factoryId)
 });
        }

        public List<PACKAGE_BASE_INFO_Entity> GetDataByFactoryId(string factoryId)
        {
            return OracleHelper.SelectedToIList<PACKAGE_BASE_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT DISTINCT PACKAGE_NO        
FROM PACKAGE_BASE_INFO
WHERE FACTORY_ID=:FACTORY_ID    
    AND DELETE_FLAG=0
", new[]{    
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,factoryId)
 });
        }

        public List<PACKAGE_BASE_INFO_Entity> GetDataById(string PACKAGE_NO, string FACTORY_ID, string VERSION_NO, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_BASE_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        FACTORY_ID,
        VERSION_NO,
        GROUPS,
        GROUP_NO_LIST,
        GROUPS_PURPOSE,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PACKAGE_TYPE_ID,
        TO_CHAR (EFFECT_DATE, 'MM/DD/YYYY') EFFECT_DATE,
        BATTERY_MODEL,
        BATTERY_TYPE,
        BATTERY_LAYERS,
        BATTERY_QTY,
        BATTERY_PARTNO,
        PROJECT_CODE,
        CUSTOMER_CODE,
        PURPOSE,
        ORDER_TYPE,
        SO_NO,
        IS_URGENT,
        TO_CHAR (OUTPUT_TARGET_DATE, 'MM/DD/YYYY') OUTPUT_TARGET_DATE,
        REASON_FORURGENT,
        PREPARED_BY,
        TO_CHAR (PREPARED_DATE, 'MM/DD/YYYY HH24:MI:SS') PREPARED_DATE,
        APPROVE_FLOW_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        GROUP_QTY_LIST,
        VALID_FLAG,
        DELETE_FLAG,
        STATUS,
        PRODUCT_CHANGE_HL,
        PROCESS_CHANGE_HL,
        MATERIAL_CHANGE_HL,
        OTHER_CHANGE_HL
FROM PACKAGE_BASE_INFO
WHERE PACKAGE_NO=:PACKAGE_NO 
    AND FACTORY_ID=:FACTORY_ID 
    AND VERSION_NO=:VERSION_NO 
" + queryStr, new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,PACKAGE_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO)
            });
        }


        #endregion

        #region 新增

        public int PostAdd(PACKAGE_BASE_INFO_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PACKAGE_BASE_INFO
 WHERE UPPER(PACKAGE_NO)=:PACKAGE_NO AND FACTORY_ID=:FACTORY_ID AND UPPER(VERSION_NO)=:VERSION_NO
",
                new[]{
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO.Trim().ToUpper()),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO.Trim().ToUpper())
                })
                ) > 0;
            if (exist)
            {
                return 0;
            }
            #endregion
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO PACKAGE_BASE_INFO (
        PACKAGE_NO,
        FACTORY_ID,
        VERSION_NO,
        GROUPS_PURPOSE,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        PACKAGE_TYPE_ID,
        EFFECT_DATE,
        BATTERY_MODEL,
        BATTERY_TYPE,
        BATTERY_LAYERS,
        BATTERY_QTY,
        BATTERY_PARTNO,
        PROJECT_CODE,
        CUSTOMER_CODE,
        PURPOSE,
        ORDER_TYPE,
        SO_NO,
        IS_URGENT,
        OUTPUT_TARGET_DATE,
        REASON_FORURGENT,
        PREPARED_BY,
        PREPARED_DATE,
        APPROVE_FLOW_ID,
        UPDATE_USER,
        UPDATE_DATE,        
        VALID_FLAG,
        DELETE_FLAG,
        STATUS,
        PRODUCT_CHANGE_HL,
        PROCESS_CHANGE_HL,
        MATERIAL_CHANGE_HL,
        OTHER_CHANGE_HL
)
VALUES (
        :PACKAGE_NO,
        :FACTORY_ID,
        :VERSION_NO,        
        :GROUPS_PURPOSE,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :PACKAGE_TYPE_ID,
        :EFFECT_DATE,
        :BATTERY_MODEL,
        :BATTERY_TYPE,
        :BATTERY_LAYERS,
        :BATTERY_QTY,
        :BATTERY_PARTNO,
        :PROJECT_CODE,
        :CUSTOMER_CODE,
        :PURPOSE,
        :ORDER_TYPE,
        :SO_NO,
        :IS_URGENT,
        :OUTPUT_TARGET_DATE,
        :REASON_FORURGENT,
        :PREPARED_BY,
        :PREPARED_DATE,
        :APPROVE_FLOW_ID,
        :UPDATE_USER,
        :UPDATE_DATE,        
        :VALID_FLAG,
        :DELETE_FLAG,
        :STATUS,
        :PRODUCT_CHANGE_HL,
        :PROCESS_CHANGE_HL,
        :MATERIAL_CHANGE_HL,
        :OTHER_CHANGE_HL
)
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO.Trim().ToUpper()),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO.Trim().ToUpper()),
            OracleHelper.MakeInParam("GROUPS_PURPOSE",OracleType.VarChar,100,entity.GROUPS_PURPOSE),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,20,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,20,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PACKAGE_TYPE_ID",OracleType.VarChar,5,entity.PACKAGE_TYPE_ID),
            OracleHelper.MakeInParam("EFFECT_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.EFFECT_DATE)?DateTime.Now: DateTime.ParseExact(entity.EFFECT_DATE,"MM/dd/yyyy",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("BATTERY_MODEL",OracleType.VarChar,8,entity.BATTERY_MODEL),
            OracleHelper.MakeInParam("BATTERY_TYPE",OracleType.VarChar,10,entity.BATTERY_TYPE),
            OracleHelper.MakeInParam("BATTERY_LAYERS",OracleType.Number,0,entity.BATTERY_LAYERS),
            OracleHelper.MakeInParam("BATTERY_QTY",OracleType.Number,0,entity.BATTERY_QTY),
            OracleHelper.MakeInParam("BATTERY_PARTNO",OracleType.VarChar,20,entity.BATTERY_PARTNO),
            OracleHelper.MakeInParam("PROJECT_CODE",OracleType.VarChar,20,entity.PROJECT_CODE),
            OracleHelper.MakeInParam("CUSTOMER_CODE",OracleType.VarChar,15,entity.CUSTOMER_CODE),
            OracleHelper.MakeInParam("PURPOSE",OracleType.VarChar,50,entity.PURPOSE),
            OracleHelper.MakeInParam("ORDER_TYPE",OracleType.VarChar,15,entity.ORDER_TYPE),
            OracleHelper.MakeInParam("SO_NO",OracleType.VarChar,20,entity.SO_NO),
            OracleHelper.MakeInParam("IS_URGENT",OracleType.VarChar,1,entity.IS_URGENT),
            OracleHelper.MakeInParam("OUTPUT_TARGET_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.OUTPUT_TARGET_DATE)?DateTime.Now: DateTime.ParseExact(entity.OUTPUT_TARGET_DATE,"MM/dd/yyyy",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("REASON_FORURGENT",OracleType.VarChar,30,entity.REASON_FORURGENT),
            OracleHelper.MakeInParam("PREPARED_BY",OracleType.VarChar,10,entity.PREPARED_BY),
            OracleHelper.MakeInParam("PREPARED_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.PREPARED_DATE)?DateTime.Now: DateTime.ParseExact(entity.PREPARED_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("APPROVE_FLOW_ID",OracleType.VarChar,20,entity.APPROVE_FLOW_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),            
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("DELETE_FLAG",OracleType.VarChar,1,entity.DELETE_FLAG),
            OracleHelper.MakeInParam("STATUS",OracleType.VarChar,5,entity.STATUS),
            OracleHelper.MakeInParam("PRODUCT_CHANGE_HL",OracleType.VarChar,150,entity.PRODUCT_CHANGE_HL),
            OracleHelper.MakeInParam("PROCESS_CHANGE_HL",OracleType.VarChar,150,entity.PROCESS_CHANGE_HL),
            OracleHelper.MakeInParam("MATERIAL_CHANGE_HL",OracleType.VarChar,150,entity.MATERIAL_CHANGE_HL),
            OracleHelper.MakeInParam("OTHER_CHANGE_HL",OracleType.VarChar,150,entity.OTHER_CHANGE_HL)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_BASE_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_BASE_INFO SET     
    GROUPS_PURPOSE=:GROUPS_PURPOSE,
    PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID,
    PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID,
    PACKAGE_TYPE_ID=:PACKAGE_TYPE_ID,
    EFFECT_DATE=:EFFECT_DATE,
    BATTERY_MODEL=:BATTERY_MODEL,
    BATTERY_TYPE=:BATTERY_TYPE,
    BATTERY_LAYERS=:BATTERY_LAYERS,
    BATTERY_QTY=:BATTERY_QTY,
    BATTERY_PARTNO=:BATTERY_PARTNO,
    PROJECT_CODE=:PROJECT_CODE,
    CUSTOMER_CODE=:CUSTOMER_CODE,
    PURPOSE=:PURPOSE,
    ORDER_TYPE=:ORDER_TYPE,
    SO_NO=:SO_NO,
    IS_URGENT=:IS_URGENT,
    OUTPUT_TARGET_DATE=:OUTPUT_TARGET_DATE,
    REASON_FORURGENT=:REASON_FORURGENT,
    PREPARED_BY=:PREPARED_BY,
    PREPARED_DATE=:PREPARED_DATE,
    APPROVE_FLOW_ID=:APPROVE_FLOW_ID,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,   
    VALID_FLAG=:VALID_FLAG,
    DELETE_FLAG=:DELETE_FLAG,
    STATUS=:STATUS,
    PRODUCT_CHANGE_HL=:PRODUCT_CHANGE_HL,
    PROCESS_CHANGE_HL=:PROCESS_CHANGE_HL,
    MATERIAL_CHANGE_HL=:MATERIAL_CHANGE_HL,
    OTHER_CHANGE_HL=:OTHER_CHANGE_HL
 WHERE PACKAGE_NO=:PACKAGE_NO AND FACTORY_ID=:FACTORY_ID AND VERSION_NO=:VERSION_NO
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),            
            OracleHelper.MakeInParam("GROUPS_PURPOSE",OracleType.VarChar,100,entity.GROUPS_PURPOSE),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,20,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,20,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PACKAGE_TYPE_ID",OracleType.VarChar,5,entity.PACKAGE_TYPE_ID),
            OracleHelper.MakeInParam("EFFECT_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.EFFECT_DATE)?DateTime.Now: DateTime.ParseExact(entity.EFFECT_DATE,"MM/dd/yyyy",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("BATTERY_MODEL",OracleType.VarChar,8,entity.BATTERY_MODEL),
            OracleHelper.MakeInParam("BATTERY_TYPE",OracleType.VarChar,10,entity.BATTERY_TYPE),
            OracleHelper.MakeInParam("BATTERY_LAYERS",OracleType.Number,0,entity.BATTERY_LAYERS),
            OracleHelper.MakeInParam("BATTERY_QTY",OracleType.Number,0,entity.BATTERY_QTY),
            OracleHelper.MakeInParam("BATTERY_PARTNO",OracleType.VarChar,20,entity.BATTERY_PARTNO),
            OracleHelper.MakeInParam("PROJECT_CODE",OracleType.VarChar,20,entity.PROJECT_CODE),
            OracleHelper.MakeInParam("CUSTOMER_CODE",OracleType.VarChar,15,entity.CUSTOMER_CODE),
            OracleHelper.MakeInParam("PURPOSE",OracleType.VarChar,50,entity.PURPOSE),
            OracleHelper.MakeInParam("ORDER_TYPE",OracleType.VarChar,15,entity.ORDER_TYPE),
            OracleHelper.MakeInParam("SO_NO",OracleType.VarChar,20,entity.SO_NO),
            OracleHelper.MakeInParam("IS_URGENT",OracleType.VarChar,1,entity.IS_URGENT),
            OracleHelper.MakeInParam("OUTPUT_TARGET_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.OUTPUT_TARGET_DATE)?DateTime.Now: DateTime.ParseExact(entity.OUTPUT_TARGET_DATE,"MM/dd/yyyy",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("REASON_FORURGENT",OracleType.VarChar,30,entity.REASON_FORURGENT),
            OracleHelper.MakeInParam("PREPARED_BY",OracleType.VarChar,10,entity.PREPARED_BY),
            OracleHelper.MakeInParam("PREPARED_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.PREPARED_DATE)?DateTime.Now: DateTime.ParseExact(entity.PREPARED_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("APPROVE_FLOW_ID",OracleType.VarChar,20,entity.APPROVE_FLOW_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),            
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("DELETE_FLAG",OracleType.VarChar,1,entity.DELETE_FLAG),
            OracleHelper.MakeInParam("STATUS",OracleType.VarChar,5,entity.STATUS),
            OracleHelper.MakeInParam("PRODUCT_CHANGE_HL",OracleType.VarChar,150,entity.PRODUCT_CHANGE_HL),
            OracleHelper.MakeInParam("PROCESS_CHANGE_HL",OracleType.VarChar,150,entity.PROCESS_CHANGE_HL),
            OracleHelper.MakeInParam("MATERIAL_CHANGE_HL",OracleType.VarChar,150,entity.MATERIAL_CHANGE_HL),
            OracleHelper.MakeInParam("OTHER_CHANGE_HL",OracleType.VarChar,150,entity.OTHER_CHANGE_HL)
            });
        }

        public int UpdateGroupInfo(int GROUPS, string GROUP_NO_LIST, string GROUP_QTY_LIST, string PACKAGE_NO, string FACTORY_ID, string VERSION_NO)
        {
            return OracleHelper.ExecuteNonQuery(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
UPDATE PACKAGE_BASE_INFO
   SET GROUPS = :GROUPS,
       GROUP_NO_LIST = :GROUP_NO_LIST,
       GROUP_QTY_LIST = :GROUP_QTY_LIST
 WHERE     PACKAGE_NO = :PACKAGE_NO
       AND FACTORY_ID = :FACTORY_ID
       AND VERSION_NO = :VERSION_NO
",
                new[] { 
                    OracleHelper.MakeInParam("GROUPS", OracleType.Number, GROUPS),
                    OracleHelper.MakeInParam("GROUP_NO_LIST",OracleType.VarChar,60,GROUP_NO_LIST),
                    OracleHelper.MakeInParam("GROUP_QTY_LIST",OracleType.VarChar,250,GROUP_QTY_LIST),
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,PACKAGE_NO),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO)
                });
        }

        public int UpdateValidFlag(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            OracleHelper.ExecuteNonQuery(
               PubConstant.ConnectionString,
               CommandType.Text,
               @"
UPDATE PACKAGE_BASE_INFO
   SET VALID_FLAG = :VALID_FLAG      
 WHERE     PACKAGE_NO = :PACKAGE_NO
       AND FACTORY_ID = :FACTORY_ID
       AND VERSION_NO != :VERSION_NO
",
               new[] {                     
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,PACKAGE_NO),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
                    OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,"0")
                });
            OracleHelper.ExecuteNonQuery(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
UPDATE PACKAGE_BASE_INFO
   SET VALID_FLAG = :VALID_FLAG,      
       EFFECT_DATE= SYSDATE
 WHERE     PACKAGE_NO = :PACKAGE_NO
       AND FACTORY_ID = :FACTORY_ID
       AND VERSION_NO = :VERSION_NO
",
                new[] {                     
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,PACKAGE_NO),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
                    OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,"1")
                });

            return 1;
        }

        public int UpdateBatteryQty(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, decimal BATTERY_QTY)
        {
            return OracleHelper.ExecuteNonQuery(
               PubConstant.ConnectionString,
               CommandType.Text,
               @"
UPDATE PACKAGE_BASE_INFO
   SET BATTERY_QTY = :BATTERY_QTY      
 WHERE     PACKAGE_NO = :PACKAGE_NO
       AND FACTORY_ID = :FACTORY_ID
       AND VERSION_NO = :VERSION_NO
",
               new[] {                     
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,PACKAGE_NO),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
                    OracleHelper.MakeInParam("BATTERY_QTY",OracleType.Number,0,BATTERY_QTY)
                });
        }

        //更新status审批状态
        public int UpdateStatus(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string status)
        {
            return OracleHelper.ExecuteNonQuery(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
UPDATE PACKAGE_BASE_INFO
   SET STATUS = :STATUS       
 WHERE     PACKAGE_NO = :PACKAGE_NO
       AND FACTORY_ID = :FACTORY_ID
       AND VERSION_NO = :VERSION_NO
",
                new[] {                     
                    OracleHelper.MakeInParam("STATUS",OracleType.VarChar,5,status),
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,PACKAGE_NO),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO)
                });
        }

        #endregion

        #region 删除
        //请移除多余的 "AND" 和 逗号
        public int PostDelete(PACKAGE_BASE_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_BASE_INFO WHERE PACKAGE_NO=:PACKAGE_NO AND FACTORY_ID=:FACTORY_ID AND VERSION_NO=:VERSION_NO
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO)
            });
        }

        #endregion



    }
}
