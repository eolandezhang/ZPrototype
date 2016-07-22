using DBUtility;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Package
{
    public class PACKAGE_PROC_EQUIP_INFO
    {
        #region 查询

        public List<PACKAGE_PROC_EQUIP_INFO_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PACKAGE_PROC_EQUIP_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        PROCESS_ID,        
        EQUIPMENT_ID,
        FACTORY_ID,
        REMARK,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_PROC_EQUIP_INFO
", null);
        }

        public List<PACKAGE_PROC_EQUIP_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PROC_EQUIP_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  B.TOTAL,
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        PROCESS_ID,        
        EQUIPMENT_ID,
        FACTORY_ID,
        REMARK,
        UPDATE_USER,
        UPDATE_DATE
FROM (  SELECT ROWNUM AS ROWINDEX,
                PACKAGE_NO,
                GROUP_NO,
                VERSION_NO,
                PROCESS_ID,                
                EQUIPMENT_ID,
                FACTORY_ID,
                REMARK,
                UPDATE_USER,
                TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
        FROM PACKAGE_PROC_EQUIP_INFO
           WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER ) A,
     (  SELECT COUNT (1) TOTAL FROM PACKAGE_PROC_EQUIP_INFO ) B
WHERE A.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PACKAGE_PROC_EQUIP_INFO_Entity> GetDataByProcessIdAndGroupNo(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string PROCESS_ID, string FACTORY_ID)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PROC_EQUIP_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.VERSION_NO,
       A.PROCESS_ID,
       A.EQUIPMENT_ID,
       A.FACTORY_ID,
       A.REMARK,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       B.EQUIPMENT_TYPE_ID
  FROM PACKAGE_PROC_EQUIP_INFO A, EQUIPMENT_PROC_INFO B
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.GROUP_NO = :GROUP_NO
       AND A.VERSION_NO = :VERSION_NO
       AND A.PROCESS_ID = :PROCESS_ID
       AND A.FACTORY_ID = :FACTORY_ID
       AND B.PROCESS_ID = A.PROCESS_ID
       AND B.EQUIPMENT_ID = A.EQUIPMENT_ID
       AND B.FACTORY_ID = A.FACTORY_ID       
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),            
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<PACKAGE_PROC_EQUIP_INFO_Entity> GetDataByProcessIdAndGroupNoAndTypeId(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string EQUIPMENT_TYPE_ID, string PROCESS_ID, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PROC_EQUIP_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.VERSION_NO,
       A.PROCESS_ID,
       A.EQUIPMENT_ID,
       A.FACTORY_ID,
       A.REMARK,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       B.EQUIPMENT_TYPE_ID
  FROM PACKAGE_PROC_EQUIP_INFO A, EQUIPMENT_PROC_INFO B
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.GROUP_NO = :GROUP_NO
       AND A.VERSION_NO = :VERSION_NO
       AND A.PROCESS_ID = :PROCESS_ID
       AND A.FACTORY_ID = :FACTORY_ID
       AND B.PROCESS_ID = A.PROCESS_ID
       AND B.EQUIPMENT_ID = A.EQUIPMENT_ID
       AND B.FACTORY_ID = A.FACTORY_ID
       AND B.EQUIPMENT_TYPE_ID = :EQUIPMENT_TYPE_ID
" + queryStr, new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),           
            OracleHelper.MakeInParam("EQUIPMENT_TYPE_ID",OracleType.VarChar,15,EQUIPMENT_TYPE_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<PACKAGE_PROC_EQUIP_INFO_Entity> GetDataById(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string EQUIPMENT_ID, string PROCESS_ID, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PROC_EQUIP_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        PROCESS_ID,
        EQUIPMENT_TYPE_ID,
        EQUIPMENT_ID,
        FACTORY_ID,
        REMARK,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_PROC_EQUIP_INFO
WHERE PACKAGE_NO=:PACKAGE_NO 
    AND GROUP_NO=:GROUP_NO 
    AND VERSION_NO=:VERSION_NO 
    AND EQUIPMENT_ID=:EQUIPMENT_ID     
    AND PROCESS_ID=:PROCESS_ID 
    AND FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("EQUIPMENT_ID",OracleType.VarChar,20,EQUIPMENT_ID),            
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<PACKAGE_PROC_EQUIP_INFO_Entity> GetDataByPackageId(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PROC_EQUIP_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        PROCESS_ID,        
        EQUIPMENT_ID,
        FACTORY_ID,
        REMARK,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_PROC_EQUIP_INFO
WHERE PACKAGE_NO=:PACKAGE_NO     
    AND VERSION_NO=:VERSION_NO  
    AND FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),            
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }
        #endregion

        #region 新增

        public int PostAdd(PACKAGE_PROC_EQUIP_INFO_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PACKAGE_PROC_EQUIP_INFO
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND VERSION_NO=:VERSION_NO AND EQUIPMENT_ID=:EQUIPMENT_ID  AND PROCESS_ID=:PROCESS_ID AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
                    OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
                    OracleHelper.MakeInParam("EQUIPMENT_ID",OracleType.VarChar,20,entity.EQUIPMENT_ID),                    
                    OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
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
INSERT INTO PACKAGE_PROC_EQUIP_INFO (
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        PROCESS_ID,        
        EQUIPMENT_ID,
        FACTORY_ID,
        REMARK,
        UPDATE_USER,
        UPDATE_DATE
)
VALUES (
        :PACKAGE_NO,
        :GROUP_NO,
        :VERSION_NO,
        :PROCESS_ID,        
        :EQUIPMENT_ID,
        :FACTORY_ID,
        :REMARK,
        :UPDATE_USER,
        :UPDATE_DATE
)
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),            
            OracleHelper.MakeInParam("EQUIPMENT_ID",OracleType.VarChar,20,entity.EQUIPMENT_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("REMARK",OracleType.VarChar,25,entity.REMARK),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PROC_EQUIP_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_PROC_EQUIP_INFO SET 
    REMARK=:REMARK,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND VERSION_NO=:VERSION_NO AND EQUIPMENT_ID=:EQUIPMENT_ID AND PROCESS_ID=:PROCESS_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),            
            OracleHelper.MakeInParam("EQUIPMENT_ID",OracleType.VarChar,20,entity.EQUIPMENT_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("REMARK",OracleType.VarChar,25,entity.REMARK),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
            });
        }

        #endregion

        #region 删除
        public int PostDelete(PACKAGE_PROC_EQUIP_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_PROC_EQUIP_INFO WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND VERSION_NO=:VERSION_NO AND EQUIPMENT_ID=:EQUIPMENT_ID AND PROCESS_ID=:PROCESS_ID AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("EQUIPMENT_ID",OracleType.VarChar,20,entity.EQUIPMENT_ID),            
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }
        public int DeleteByPackageId(PACKAGE_PROC_EQUIP_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_PROC_EQUIP_INFO 
    WHERE PACKAGE_NO=:PACKAGE_NO         
        AND VERSION_NO=:VERSION_NO
        AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),            
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }
        public int DeleteByPackageIdAndGroupNo(PACKAGE_PROC_EQUIP_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_PROC_EQUIP_INFO 
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
        public int DeleteByProcessIDAndGroupNo(PACKAGE_PROC_EQUIP_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_PROC_EQUIP_INFO
      WHERE     PACKAGE_NO = :PACKAGE_NO
            AND GROUP_NO = :GROUP_NO
            AND VERSION_NO = :VERSION_NO
            AND PROCESS_ID = :PROCESS_ID
            AND FACTORY_ID = :FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }
        #endregion



    }
}
