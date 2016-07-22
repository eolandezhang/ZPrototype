using DBUtility;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Package
{
    public class PACKAGE_FLOW_INFO
    {

        #region 查询

        public List<PACKAGE_FLOW_INFO_Entity> GetData(string groupNo, string factoryId, string packageNo, string versionNo, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_FLOW_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.FACTORY_ID,
       A.VERSION_NO,
       A.PROCESS_ID,
       A.PROC_SEQUENCE_NO,
       A.PREVIOUS_PROCESS_ID,
       A.NEXT_PROCESS_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       A.PKG_PROC_DESC,
       A.SUB_GROUP_NO,
       B.PROCESS_NAME,
       B.PROCESS_DESC,
       F.PROCESS_NAME PROCESS_NAME_P,
       F.PROCESS_DESC PROCESS_DESC_P,
       I.PROCESS_NAME PROCESS_NAME_N,
       I.PROCESS_DESC PROCESS_DESC_N
  FROM PACKAGE_FLOW_INFO A,
       PROCESS_LIST B,
       PACKAGE_BASE_INFO C,
       (SELECT DISTINCT D.PROCESS_ID, D.PROCESS_NAME, D.PROCESS_DESC
          FROM PROCESS_LIST D, PACKAGE_BASE_INFO E
         WHERE     D.FACTORY_ID = E.FACTORY_ID
               AND D.PRODUCT_TYPE_ID = E.PRODUCT_TYPE_ID
               AND D.PRODUCT_PROC_TYPE_ID = E.PRODUCT_PROC_TYPE_ID) F,
       (SELECT DISTINCT G.PROCESS_ID, G.PROCESS_NAME, G.PROCESS_DESC
          FROM PROCESS_LIST G, PACKAGE_BASE_INFO H
         WHERE     G.FACTORY_ID = H.FACTORY_ID
               AND G.PRODUCT_TYPE_ID = H.PRODUCT_TYPE_ID
               AND G.PRODUCT_PROC_TYPE_ID = H.PRODUCT_PROC_TYPE_ID) I
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.VERSION_NO = :VERSION_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.GROUP_NO=:GROUP_NO
       AND A.FACTORY_ID = B.FACTORY_ID                                   --A,B
       AND A.PROCESS_ID = B.PROCESS_ID                                   --A,B
       AND A.FACTORY_ID = C.FACTORY_ID                                   --A,C
       AND A.PACKAGE_NO = C.PACKAGE_NO                                   --A,C
       AND A.VERSION_NO = C.VERSION_NO                                   --A,C
       AND B.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID                         --B,C
       AND B.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID               --B,C
       AND A.PREVIOUS_PROCESS_ID = F.PROCESS_ID(+)
       AND A.NEXT_PROCESS_ID = I.PROCESS_ID(+)
" + queryStr + "ORDER BY A.PROC_SEQUENCE_NO", new[]{     
     OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,packageNo),
     OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,versionNo),
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,factoryId),
     OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,groupNo)
 });
        }


       
        // Package所有工序        
        public List<PACKAGE_FLOW_INFO_Entity> GetDataByPackageId(string factoryId, string packageNo, string versionNo)
        {
            return OracleHelper.SelectedToIList<PACKAGE_FLOW_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT DISTINCT A.PACKAGE_NO,
                A.FACTORY_ID,
                A.VERSION_NO,
                A.PROCESS_ID,               
                B.PROCESS_DESC,                
                B.SEQUENCE_NO            
  FROM PACKAGE_FLOW_INFO A, PROCESS_LIST B, PACKAGE_BASE_INFO C
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.VERSION_NO = :VERSION_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.PROCESS_ID = B.PROCESS_ID
       AND B.FACTORY_ID = C.FACTORY_ID
       AND B.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID
       AND B.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID
       AND C.FACTORY_ID = :FACTORY_ID
       AND C.PACKAGE_NO = :PACKAGE_NO
       AND C.VERSION_NO = :VERSION_NO
ORDER BY B.SEQUENCE_NO  
", new[]{     
     OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,packageNo),
     OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,versionNo),
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,factoryId)
 });
        }
        public List<PACKAGE_FLOW_INFO_Entity> GetGroupNoByProcessId(string factoryId, string packageNo, string versionNo, string processId,string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_FLOW_INFO_Entity>(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT DISTINCT A.PACKAGE_NO,
                A.GROUP_NO,
                B.GROUP_QTY,                
                A.FACTORY_ID,
                A.VERSION_NO,
                A.PROCESS_ID
  FROM PACKAGE_FLOW_INFO A,PACKAGE_GROUPS B
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.VERSION_NO = :VERSION_NO
       AND A.PROCESS_ID = :PROCESS_ID
       AND A.GROUP_NO=B.GROUP_NO
       AND A.PACKAGE_NO=B.PACKAGE_NO
       AND A.FACTORY_ID=B.FACTORY_ID
       AND A.VERSION_NO=B.VERSION_NO
" + queryStr+" ORDER BY A.GROUP_NO", new[]{
     OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,packageNo),
     OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,versionNo),
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,factoryId),
     OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,processId)
 });
        }

        public List<PACKAGE_FLOW_INFO_Entity> GetDataById(string PACKAGE_NO, string GROUP_NO, string FACTORY_ID, string VERSION_NO, string PROCESS_ID)
        {
            return OracleHelper.SelectedToIList<PACKAGE_FLOW_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        FACTORY_ID,
        VERSION_NO,
        PROCESS_ID,
        PROC_SEQUENCE_NO,
        PREVIOUS_PROCESS_ID,
        NEXT_PROCESS_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        PKG_PROC_DESC,
        SUB_GROUP_NO
FROM PACKAGE_FLOW_INFO
WHERE PACKAGE_NO=:PACKAGE_NO 
    AND GROUP_NO=:GROUP_NO 
    AND FACTORY_ID=:FACTORY_ID 
    AND VERSION_NO=:VERSION_NO 
    AND PROCESS_ID=:PROCESS_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID)
            });
        }

        public List<PACKAGE_FLOW_INFO_Entity> GetDataByProcessId(string PACKAGE_NO, string FACTORY_ID, string VERSION_NO, string PROCESS_ID)
        {
            return OracleHelper.SelectedToIList<PACKAGE_FLOW_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.FACTORY_ID,
       A.VERSION_NO,
       A.PROCESS_ID,
       A.PROC_SEQUENCE_NO,
       A.PREVIOUS_PROCESS_ID,
       A.NEXT_PROCESS_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       A.PKG_PROC_DESC,
       A.SUB_GROUP_NO,
       B.PROCESS_NAME,
       B.PROCESS_DESC,
       F.PROCESS_NAME PROCESS_NAME_P,
       F.PROCESS_DESC PROCESS_DESC_P,
       I.PROCESS_NAME PROCESS_NAME_N,
       I.PROCESS_DESC PROCESS_DESC_N
  FROM PACKAGE_FLOW_INFO A,
       PROCESS_LIST B,
       PACKAGE_BASE_INFO C,
       (SELECT DISTINCT D.PROCESS_ID, D.PROCESS_NAME, D.PROCESS_DESC
          FROM PROCESS_LIST D, PACKAGE_BASE_INFO E
         WHERE     D.FACTORY_ID = E.FACTORY_ID
               AND D.PRODUCT_TYPE_ID = E.PRODUCT_TYPE_ID
               AND D.PRODUCT_PROC_TYPE_ID = E.PRODUCT_PROC_TYPE_ID) F,
       (SELECT DISTINCT G.PROCESS_ID, G.PROCESS_NAME, G.PROCESS_DESC
          FROM PROCESS_LIST G, PACKAGE_BASE_INFO H
         WHERE     G.FACTORY_ID = H.FACTORY_ID
               AND G.PRODUCT_TYPE_ID = H.PRODUCT_TYPE_ID
               AND G.PRODUCT_PROC_TYPE_ID = H.PRODUCT_PROC_TYPE_ID) I
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.VERSION_NO = :VERSION_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.PROCESS_ID=:PROCESS_ID
       --AND A.GROUP_NO=:GROUP_NO
       AND A.FACTORY_ID = B.FACTORY_ID                                   --A,B
       AND A.PROCESS_ID = B.PROCESS_ID                                   --A,B
       AND A.FACTORY_ID = C.FACTORY_ID                                   --A,C
       AND A.PACKAGE_NO = C.PACKAGE_NO                                   --A,C
       AND A.VERSION_NO = C.VERSION_NO                                   --A,C
       AND B.PRODUCT_TYPE_ID = C.PRODUCT_TYPE_ID                         --B,C
       AND B.PRODUCT_PROC_TYPE_ID = C.PRODUCT_PROC_TYPE_ID               --B,C
       AND A.PREVIOUS_PROCESS_ID = F.PROCESS_ID(+)
       AND A.NEXT_PROCESS_ID = I.PROCESS_ID(+)
ORDER BY A.GROUP_NO
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,PACKAGE_NO),           
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID)
            });
        }

        public List<PACKAGE_FLOW_INFO_Entity> GetAllDataByPackageId(string PACKAGE_NO, string FACTORY_ID, string VERSION_NO, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_FLOW_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        FACTORY_ID,
        VERSION_NO,
        PROCESS_ID,
        PROC_SEQUENCE_NO,
        PREVIOUS_PROCESS_ID,
        NEXT_PROCESS_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        PKG_PROC_DESC,
        SUB_GROUP_NO
FROM PACKAGE_FLOW_INFO
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

        public int PostAdd(PACKAGE_FLOW_INFO_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PACKAGE_FLOW_INFO
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND FACTORY_ID=:FACTORY_ID AND VERSION_NO=:VERSION_NO AND PROCESS_ID=:PROCESS_ID
",
                new[]{
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
                    OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
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
INSERT INTO PACKAGE_FLOW_INFO (
        PACKAGE_NO,
        GROUP_NO,
        FACTORY_ID,
        VERSION_NO,
        PROCESS_ID,
        PROC_SEQUENCE_NO,
        PREVIOUS_PROCESS_ID,
        NEXT_PROCESS_ID,
        UPDATE_USER,
        UPDATE_DATE,
        PKG_PROC_DESC,
        SUB_GROUP_NO
)
VALUES (
        :PACKAGE_NO,
        :GROUP_NO,
        :FACTORY_ID,
        :VERSION_NO,
        :PROCESS_ID,
        :PROC_SEQUENCE_NO,
        :PREVIOUS_PROCESS_ID,
        :NEXT_PROCESS_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :PKG_PROC_DESC,
        :SUB_GROUP_NO
)
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("PROC_SEQUENCE_NO",OracleType.Number,0,entity.PROC_SEQUENCE_NO),
            OracleHelper.MakeInParam("PREVIOUS_PROCESS_ID",OracleType.VarChar,15,entity.PREVIOUS_PROCESS_ID),
            OracleHelper.MakeInParam("NEXT_PROCESS_ID",OracleType.VarChar,15,entity.NEXT_PROCESS_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("PKG_PROC_DESC",OracleType.VarChar,100,entity.PKG_PROC_DESC),
            OracleHelper.MakeInParam("SUB_GROUP_NO",OracleType.VarChar,51,string.IsNullOrEmpty(entity.SUB_GROUP_NO)?"":entity.SUB_GROUP_NO.ToUpper())
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_FLOW_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_FLOW_INFO SET 
    PROC_SEQUENCE_NO=:PROC_SEQUENCE_NO,
    PREVIOUS_PROCESS_ID=:PREVIOUS_PROCESS_ID,
    NEXT_PROCESS_ID=:NEXT_PROCESS_ID,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    PKG_PROC_DESC=:PKG_PROC_DESC,
    SUB_GROUP_NO=:SUB_GROUP_NO
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND FACTORY_ID=:FACTORY_ID AND VERSION_NO=:VERSION_NO AND PROCESS_ID=:PROCESS_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("PROC_SEQUENCE_NO",OracleType.Number,0,entity.PROC_SEQUENCE_NO),
            OracleHelper.MakeInParam("PREVIOUS_PROCESS_ID",OracleType.VarChar,15,entity.PREVIOUS_PROCESS_ID),
            OracleHelper.MakeInParam("NEXT_PROCESS_ID",OracleType.VarChar,15,entity.NEXT_PROCESS_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("PKG_PROC_DESC",OracleType.VarChar,100,entity.PKG_PROC_DESC),
            OracleHelper.MakeInParam("SUB_GROUP_NO",OracleType.VarChar,51,string.IsNullOrEmpty(entity.SUB_GROUP_NO)?"":entity.SUB_GROUP_NO.ToUpper())
            });
        }

        #endregion

        #region 删除
        public int PostDelete(PACKAGE_FLOW_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_FLOW_INFO WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND FACTORY_ID=:FACTORY_ID AND VERSION_NO=:VERSION_NO AND PROCESS_ID=:PROCESS_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID)
            });
        }
        public int DeleteByPackageId(PACKAGE_FLOW_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_FLOW_INFO 
WHERE PACKAGE_NO=:PACKAGE_NO     
    AND FACTORY_ID=:FACTORY_ID 
    AND VERSION_NO=:VERSION_NO    
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO)            
            });
        }
        public int DeleteByPackageIdAndGroupNo(PACKAGE_FLOW_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_FLOW_INFO 
WHERE PACKAGE_NO=:PACKAGE_NO 
    AND GROUP_NO=:GROUP_NO 
    AND FACTORY_ID=:FACTORY_ID 
    AND VERSION_NO=:VERSION_NO     
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO)            
            });
        }
        #endregion



    }
}
