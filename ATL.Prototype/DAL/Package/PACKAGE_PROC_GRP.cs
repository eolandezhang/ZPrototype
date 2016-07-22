using DBUtility;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Package
{
    public class PACKAGE_PROC_GRP
    {
        #region 查询

        public List<PACKAGE_PROC_GRP_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PACKAGE_PROC_GRP_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        PROC_GRP_ID,
        PACKAGE_NO,
        VERSION_NO,
        FACTORY_ID,
        PROCESS_ID,
        PROC_GRP_DESC,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_PROC_GRP
", null);
        }

        public List<PACKAGE_PROC_GRP_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PROC_GRP_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        PROC_GRP_ID,
        PACKAGE_NO,
        VERSION_NO,
        FACTORY_ID,
        PROCESS_ID,
        PROC_GRP_DESC,
        UPDATE_USER,
        UPDATE_DATE
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            PROC_GRP_ID,
            PACKAGE_NO,
            VERSION_NO,
            FACTORY_ID,
            PROCESS_ID,
            PROC_GRP_DESC,
            UPDATE_USER,
            UPDATE_DATE
        FROM (  SELECT 
                    PROC_GRP_ID,
                    PACKAGE_NO,
                    VERSION_NO,
                    FACTORY_ID,
                    PROCESS_ID,
                    PROC_GRP_DESC,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
                FROM PACKAGE_PROC_GRP ) T1,          
            (  SELECT COUNT (1) TOTAL FROM PACKAGE_PROC_GRP ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PACKAGE_PROC_GRP_Entity> GetDataById(string PROC_GRP_ID, string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PROC_GRP_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PROC_GRP_ID,
        PACKAGE_NO,
        VERSION_NO,
        FACTORY_ID,
        PROCESS_ID,
        PROC_GRP_DESC,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_PROC_GRP
WHERE PROC_GRP_ID=:PROC_GRP_ID 
    AND PACKAGE_NO=:PACKAGE_NO 
    AND VERSION_NO=:VERSION_NO 
    AND FACTORY_ID=:FACTORY_ID 
    AND PROCESS_ID=:PROCESS_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("PROC_GRP_ID",OracleType.VarChar,30,PROC_GRP_ID),
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID)
            });
        }

        public List<PACKAGE_PROC_GRP_Entity> GetDataByProcessId(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PROC_GRP_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PROC_GRP_ID,
        PACKAGE_NO,
        VERSION_NO,
        FACTORY_ID,
        PROCESS_ID,
        PROC_GRP_DESC,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_PROC_GRP
WHERE  PACKAGE_NO=:PACKAGE_NO 
    AND VERSION_NO=:VERSION_NO 
    AND FACTORY_ID=:FACTORY_ID 
    AND PROCESS_ID=:PROCESS_ID 
" + queryStr, new[]{            
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID)
            });
        }

        public List<PACKAGE_PROC_GRP_Entity> GetDataByPackageId(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_PROC_GRP_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PROC_GRP_ID,
        PACKAGE_NO,
        VERSION_NO,
        FACTORY_ID,
        PROCESS_ID,
        PROC_GRP_DESC,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_PROC_GRP
WHERE  PACKAGE_NO=:PACKAGE_NO 
    AND VERSION_NO=:VERSION_NO 
    AND FACTORY_ID=:FACTORY_ID   
" + queryStr, new[]{           
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),           
            });
        }

        public bool GetDataValidateId(string PROC_GRP_ID, string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM PACKAGE_PROC_GRP
WHERE PROC_GRP_ID=:PROC_GRP_ID 
    AND PACKAGE_NO=:PACKAGE_NO 
    AND VERSION_NO=:VERSION_NO 
    AND FACTORY_ID=:FACTORY_ID 
    AND PROCESS_ID=:PROCESS_ID
", new[]{
            OracleHelper.MakeInParam("PROC_GRP_ID",OracleType.VarChar,30,PROC_GRP_ID),
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_PROC_GRP_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PACKAGE_PROC_GRP
 WHERE PROC_GRP_ID=:PROC_GRP_ID AND PACKAGE_NO=:PACKAGE_NO AND VERSION_NO=:VERSION_NO AND FACTORY_ID=:FACTORY_ID AND PROCESS_ID=:PROCESS_ID
",
                new[]{
                    OracleHelper.MakeInParam("PROC_GRP_ID",OracleType.VarChar,30,entity.PROC_GRP_ID),
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
                    OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID)
                })
                ) > 0;
            if (exist)
            {
                return 0;
            }
            #endregion
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO PACKAGE_PROC_GRP (
        PROC_GRP_ID,
        PACKAGE_NO,
        VERSION_NO,
        FACTORY_ID,
        PROCESS_ID,
        PROC_GRP_DESC,
        UPDATE_USER,
        UPDATE_DATE
)
VALUES (
        :PROC_GRP_ID,
        :PACKAGE_NO,
        :VERSION_NO,
        :FACTORY_ID,
        :PROCESS_ID,
        :PROC_GRP_DESC,
        :UPDATE_USER,
        :UPDATE_DATE
)
", new[]{
            OracleHelper.MakeInParam("PROC_GRP_ID",OracleType.VarChar,30,entity.PROC_GRP_ID),
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("PROC_GRP_DESC",OracleType.VarChar,100,entity.PROC_GRP_DESC),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PROC_GRP_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_PROC_GRP SET 
    PROC_GRP_DESC=:PROC_GRP_DESC,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE
 WHERE 
        PROC_GRP_ID=:PROC_GRP_ID AND  
        PACKAGE_NO=:PACKAGE_NO AND  
        VERSION_NO=:VERSION_NO AND  
        FACTORY_ID=:FACTORY_ID AND  
        PROCESS_ID=:PROCESS_ID 
", new[]{
            OracleHelper.MakeInParam("PROC_GRP_ID",OracleType.VarChar,30,entity.PROC_GRP_ID),
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("PROC_GRP_DESC",OracleType.VarChar,100,entity.PROC_GRP_DESC),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
            });
        }

        #endregion

        #region 删除
        public int PostDelete(PACKAGE_PROC_GRP_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_PROC_GRP 
WHERE
    PROC_GRP_ID=:PROC_GRP_ID AND 
    PACKAGE_NO=:PACKAGE_NO AND 
    VERSION_NO=:VERSION_NO AND 
    FACTORY_ID=:FACTORY_ID AND 
    PROCESS_ID=:PROCESS_ID
", new[]{
            OracleHelper.MakeInParam("PROC_GRP_ID",OracleType.VarChar,30,entity.PROC_GRP_ID),
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID)
            });
        }

        public int DeleteByPackageId(PACKAGE_PROC_GRP_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_PROC_GRP 
WHERE    
    PACKAGE_NO=:PACKAGE_NO AND 
    VERSION_NO=:VERSION_NO AND 
    FACTORY_ID=:FACTORY_ID     
", new[]{           
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }
        #endregion



    }
}
