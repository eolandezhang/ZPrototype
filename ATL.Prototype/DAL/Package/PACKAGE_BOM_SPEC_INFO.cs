using DBUtility;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Package
{
    public class PACKAGE_BOM_SPEC_INFO
    {
        #region 查询

        public List<PACKAGE_BOM_SPEC_INFO_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PACKAGE_BOM_SPEC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        PROCESS_ID,
        FACTORY_ID,
        VERSION_NO,
        P_PART_ID,
        C_PART_ID,
        P_PART_QTY,
        C_PART_QTY,
        IS_VIRTUAL_PART,
        IS_IQC_MATERIAL,
        IS_SUBSTITUTE,
        TO_CHAR (SYNC_DATE, 'MM/DD/YYYY HH24:MI:SS') SYNC_DATE,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_BOM_SPEC_INFO
", null);
        }

        public List<PACKAGE_BOM_SPEC_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PACKAGE_BOM_SPEC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  B.TOTAL,
        PACKAGE_NO,
        GROUP_NO,
        PROCESS_ID,
        FACTORY_ID,
        VERSION_NO,
        P_PART_ID,
        C_PART_ID,
        P_PART_QTY,
        C_PART_QTY,
        IS_VIRTUAL_PART,
        IS_IQC_MATERIAL,
        IS_SUBSTITUTE,
        SYNC_DATE,
        UPDATE_USER,
        UPDATE_DATE
FROM (  SELECT ROWNUM AS ROWINDEX,
                PACKAGE_NO,
                GROUP_NO,
                PROCESS_ID,
                FACTORY_ID,
                VERSION_NO,
                P_PART_ID,
                C_PART_ID,
                P_PART_QTY,
                C_PART_QTY,
                IS_VIRTUAL_PART,
                IS_IQC_MATERIAL,
                IS_SUBSTITUTE,
                TO_CHAR (SYNC_DATE, 'MM/DD/YYYY HH24:MI:SS') SYNC_DATE,
                UPDATE_USER,
                TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
        FROM PACKAGE_BOM_SPEC_INFO
           WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER ) A,
     (  SELECT COUNT (1) TOTAL FROM PACKAGE_BOM_SPEC_INFO ) B
WHERE A.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PACKAGE_BOM_SPEC_INFO_Entity> GetDataByProcessIdAndGroupNo(string PACKAGE_NO, string GROUP_NO, string FACTORY_ID, string PROCESS_ID, string VERSION_NO)
        {
            return OracleHelper.SelectedToIList<PACKAGE_BOM_SPEC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        PROCESS_ID,
        FACTORY_ID,
        VERSION_NO,
        P_PART_ID,
        C_PART_ID,
        P_PART_QTY,
        C_PART_QTY,
        IS_VIRTUAL_PART,
        IS_IQC_MATERIAL,
        IS_SUBSTITUTE,
        TO_CHAR (SYNC_DATE, 'MM/DD/YYYY HH24:MI:SS') SYNC_DATE,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_BOM_SPEC_INFO
WHERE PACKAGE_NO=:PACKAGE_NO 
    AND GROUP_NO=:GROUP_NO 
    AND FACTORY_ID=:FACTORY_ID 
    AND PROCESS_ID=:PROCESS_ID 
    AND VERSION_NO=:VERSION_NO    
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,1,GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,10,PROCESS_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO)            
            });
        }

        public List<PACKAGE_BOM_SPEC_INFO_Entity> GetDataByPackageId(string PACKAGE_NO, string FACTORY_ID, string VERSION_NO,  string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_BOM_SPEC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        PROCESS_ID,
        FACTORY_ID,
        VERSION_NO,
        P_PART_ID,
        C_PART_ID,
        P_PART_QTY,
        C_PART_QTY,
        IS_VIRTUAL_PART,
        IS_IQC_MATERIAL,
        IS_SUBSTITUTE,
        TO_CHAR (SYNC_DATE, 'MM/DD/YYYY HH24:MI:SS') SYNC_DATE,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_BOM_SPEC_INFO
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

        public int PostAdd(PACKAGE_BOM_SPEC_INFO_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PACKAGE_BOM_SPEC_INFO
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND FACTORY_ID=:FACTORY_ID AND PROCESS_ID=:PROCESS_ID AND VERSION_NO=:VERSION_NO AND P_PART_ID=:P_PART_ID AND C_PART_ID=:C_PART_ID
",
                new[]{
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
                    OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,1,entity.GROUP_NO),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
                    OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,10,entity.PROCESS_ID),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
                    OracleHelper.MakeInParam("P_PART_ID",OracleType.VarChar,20,entity.P_PART_ID),
                    OracleHelper.MakeInParam("C_PART_ID",OracleType.VarChar,20,entity.C_PART_ID)
                })
                ) > 0;
            if (exist)
            {
                return 0;
            }
            #endregion
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO PACKAGE_BOM_SPEC_INFO (
        PACKAGE_NO,
        GROUP_NO,
        PROCESS_ID,
        FACTORY_ID,
        VERSION_NO,
        P_PART_ID,
        C_PART_ID,
        P_PART_QTY,
        C_PART_QTY,
        IS_VIRTUAL_PART,
        IS_IQC_MATERIAL,
        IS_SUBSTITUTE,
        SYNC_DATE,
        UPDATE_USER,
        UPDATE_DATE
)
VALUES (
        :PACKAGE_NO,
        :GROUP_NO,
        :PROCESS_ID,
        :FACTORY_ID,
        :VERSION_NO,
        :P_PART_ID,
        :C_PART_ID,
        :P_PART_QTY,
        :C_PART_QTY,
        :IS_VIRTUAL_PART,
        :IS_IQC_MATERIAL,
        :IS_SUBSTITUTE,
        :SYNC_DATE,
        :UPDATE_USER,
        :UPDATE_DATE
)
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,1,entity.GROUP_NO),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,10,entity.PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("P_PART_ID",OracleType.VarChar,20,entity.P_PART_ID),
            OracleHelper.MakeInParam("C_PART_ID",OracleType.VarChar,20,entity.C_PART_ID),
            OracleHelper.MakeInParam("P_PART_QTY",OracleType.Number,0,entity.P_PART_QTY),
            OracleHelper.MakeInParam("C_PART_QTY",OracleType.Number,0,entity.C_PART_QTY),
            OracleHelper.MakeInParam("IS_VIRTUAL_PART",OracleType.VarChar,1,entity.IS_VIRTUAL_PART),
            OracleHelper.MakeInParam("IS_IQC_MATERIAL",OracleType.VarChar,1,entity.IS_IQC_MATERIAL),
            OracleHelper.MakeInParam("IS_SUBSTITUTE",OracleType.VarChar,1,entity.IS_SUBSTITUTE),
            OracleHelper.MakeInParam("SYNC_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.SYNC_DATE)?DateTime.Now: DateTime.ParseExact(entity.SYNC_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_BOM_SPEC_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_BOM_SPEC_INFO SET 
    P_PART_QTY=:P_PART_QTY,
    C_PART_QTY=:C_PART_QTY,
    IS_VIRTUAL_PART=:IS_VIRTUAL_PART,
    IS_IQC_MATERIAL=:IS_IQC_MATERIAL,
    IS_SUBSTITUTE=:IS_SUBSTITUTE,
    SYNC_DATE=:SYNC_DATE,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND FACTORY_ID=:FACTORY_ID AND PROCESS_ID=:PROCESS_ID AND VERSION_NO=:VERSION_NO AND P_PART_ID=:P_PART_ID AND C_PART_ID=:C_PART_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,1,entity.GROUP_NO),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,10,entity.PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("P_PART_ID",OracleType.VarChar,20,entity.P_PART_ID),
            OracleHelper.MakeInParam("C_PART_ID",OracleType.VarChar,20,entity.C_PART_ID),
            OracleHelper.MakeInParam("P_PART_QTY",OracleType.Number,0,entity.P_PART_QTY),
            OracleHelper.MakeInParam("C_PART_QTY",OracleType.Number,0,entity.C_PART_QTY),
            OracleHelper.MakeInParam("IS_VIRTUAL_PART",OracleType.VarChar,1,entity.IS_VIRTUAL_PART),
            OracleHelper.MakeInParam("IS_IQC_MATERIAL",OracleType.VarChar,1,entity.IS_IQC_MATERIAL),
            OracleHelper.MakeInParam("IS_SUBSTITUTE",OracleType.VarChar,1,entity.IS_SUBSTITUTE),
            OracleHelper.MakeInParam("SYNC_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.SYNC_DATE)?DateTime.Now: DateTime.ParseExact(entity.SYNC_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
            });
        }

        #endregion

        #region 删除
        public int PostDelete(PACKAGE_BOM_SPEC_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_BOM_SPEC_INFO WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND FACTORY_ID=:FACTORY_ID AND PROCESS_ID=:PROCESS_ID AND VERSION_NO=:VERSION_NO AND P_PART_ID=:P_PART_ID AND C_PART_ID=:C_PART_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,1,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,10,entity.PROCESS_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("P_PART_ID",OracleType.VarChar,20,entity.P_PART_ID),
            OracleHelper.MakeInParam("C_PART_ID",OracleType.VarChar,20,entity.C_PART_ID)
            });
        }
        public int DeleteByPackageId(PACKAGE_BOM_SPEC_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_BOM_SPEC_INFO 
    WHERE PACKAGE_NO=:PACKAGE_NO       
        AND FACTORY_ID=:FACTORY_ID         
        AND VERSION_NO=:VERSION_NO         
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),            
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO)
            });
        }
        public int DeleteByPackageIdAndGroupNo(PACKAGE_BOM_SPEC_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_BOM_SPEC_INFO 
    WHERE PACKAGE_NO=:PACKAGE_NO   
        AND GROUP_NO=:GROUP_NO    
        AND FACTORY_ID=:FACTORY_ID         
        AND VERSION_NO=:VERSION_NO         
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),    
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,1,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),            
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO)
            });
        }
        public int DeleteByProcessIDAndGroupNo(PACKAGE_BOM_SPEC_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_BOM_SPEC_INFO
      WHERE     PACKAGE_NO = :PACKAGE_NO
            AND GROUP_NO = :GROUP_NO
            AND FACTORY_ID = :FACTORY_ID
            AND PROCESS_ID = :PROCESS_ID
            AND VERSION_NO = :VERSION_NO
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,1,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,10,entity.PROCESS_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO)
            });
        }
        #endregion



    }
}
