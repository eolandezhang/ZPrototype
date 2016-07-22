using DBUtility;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Package
{
    public class PACKAGE_WF_STEP_AUDITOR
    {
        #region 查询

        public List<PACKAGE_WF_STEP_AUDITOR_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PACKAGE_WF_STEP_AUDITOR_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        AUDITOR_ID,
        PACKAGE_WF_STEP_ID,
        PMES_USER_ID,
        IS_AGREED,
        AUDITOR_COMMENT,
        TO_CHAR (AUDIT_AT, 'MM/DD/YYYY HH24:MI:SS') AUDIT_AT,
        IS_CANCELED,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_WF_STEP_AUDITOR
", null);
        }

        public List<PACKAGE_WF_STEP_AUDITOR_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PACKAGE_WF_STEP_AUDITOR_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        AUDITOR_ID,
        PACKAGE_WF_STEP_ID,
        PMES_USER_ID,
        IS_AGREED,
        AUDITOR_COMMENT,
        AUDIT_AT,
        IS_CANCELED,
        UPDATE_USER,
        UPDATE_DATE
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            AUDITOR_ID,
            PACKAGE_WF_STEP_ID,
            PMES_USER_ID,
            IS_AGREED,
            AUDITOR_COMMENT,
            AUDIT_AT,
            IS_CANCELED,
            UPDATE_USER,
            UPDATE_DATE
        FROM (  SELECT 
                    AUDITOR_ID,
                    PACKAGE_WF_STEP_ID,
                    PMES_USER_ID,
                    IS_AGREED,
                    AUDITOR_COMMENT,
                    TO_CHAR (AUDIT_AT, 'MM/DD/YYYY HH24:MI:SS') AUDIT_AT,
                    IS_CANCELED,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
                FROM PACKAGE_WF_STEP_AUDITOR ) T1,          
            (  SELECT COUNT (1) TOTAL FROM PACKAGE_WF_STEP_AUDITOR ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PACKAGE_WF_STEP_AUDITOR_Entity> GetDataById(decimal AUDITOR_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_WF_STEP_AUDITOR_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        AUDITOR_ID,
        PACKAGE_WF_STEP_ID,
        PMES_USER_ID,
        IS_AGREED,
        AUDITOR_COMMENT,
        TO_CHAR (AUDIT_AT, 'MM/DD/YYYY HH24:MI:SS') AUDIT_AT,
        IS_CANCELED,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_WF_STEP_AUDITOR
WHERE AUDITOR_ID=:AUDITOR_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("AUDITOR_ID",OracleType.Number,0,AUDITOR_ID)
            });
        }
        public List<PACKAGE_WF_STEP_AUDITOR_Entity> GetDataByPkgStepId(decimal PACKAGE_WF_STEP_ID)
        {
            return OracleHelper.SelectedToIList<PACKAGE_WF_STEP_AUDITOR_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT AUDITOR_ID,
       PACKAGE_WF_STEP_ID,
       PMES_USER_ID,
       IS_AGREED,
       AUDITOR_COMMENT,
       TO_CHAR (AUDIT_AT, 'MM/DD/YYYY HH24:MI:SS') AUDIT_AT,
       IS_CANCELED,
       UPDATE_USER,
       TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       B.CNNAME,
       B.MAIL,
       B.DEPARTMENT,
       B.TITLE
  FROM PACKAGE_WF_STEP_AUDITOR A, USERS B
 WHERE     PACKAGE_WF_STEP_ID = :PACKAGE_WF_STEP_ID
       AND A.PMES_USER_ID = B.DESCRIPTION(+)
", new[]{
            OracleHelper.MakeInParam("PACKAGE_WF_STEP_ID",OracleType.Number,0,PACKAGE_WF_STEP_ID)
            });
        }
        public bool GetDataValidateId(decimal AUDITOR_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM PACKAGE_WF_STEP_AUDITOR
WHERE AUDITOR_ID=:AUDITOR_ID
", new[]{
            OracleHelper.MakeInParam("AUDITOR_ID",OracleType.Number,0,AUDITOR_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_WF_STEP_AUDITOR_Entity entity)
        {
            entity.AUDITOR_ID = Convert.ToDecimal(OracleHelper.ExecuteScalar(
               PubConstant.ConnectionString,
               CommandType.Text,
               @"SELECT SEQ_PACKAGE_WF_STEP_AUDITOR.NEXTVAL FROM DUAL",
               null
               ));

            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO PACKAGE_WF_STEP_AUDITOR (
        AUDITOR_ID,
        PACKAGE_WF_STEP_ID,
        PMES_USER_ID,
        IS_AGREED,
        AUDITOR_COMMENT,
        AUDIT_AT,
        IS_CANCELED,
        UPDATE_USER,
        UPDATE_DATE
)
VALUES (
        :AUDITOR_ID,
        :PACKAGE_WF_STEP_ID,
        :PMES_USER_ID,
        :IS_AGREED,
        :AUDITOR_COMMENT,
        :AUDIT_AT,
        :IS_CANCELED,
        :UPDATE_USER,
        :UPDATE_DATE
)
", new[]{
            OracleHelper.MakeInParam("AUDITOR_ID",OracleType.Number,0,entity.AUDITOR_ID),
            OracleHelper.MakeInParam("PACKAGE_WF_STEP_ID",OracleType.Number,0,entity.PACKAGE_WF_STEP_ID),
            OracleHelper.MakeInParam("PMES_USER_ID",OracleType.VarChar,10,entity.PMES_USER_ID),
            OracleHelper.MakeInParam("IS_AGREED",OracleType.VarChar,1,entity.IS_AGREED),
            OracleHelper.MakeInParam("AUDITOR_COMMENT",OracleType.VarChar,30,entity.AUDITOR_COMMENT),
            OracleHelper.MakeInParam("AUDIT_AT",OracleType.DateTime,0,string.IsNullOrEmpty(entity.AUDIT_AT)?DateTime.Now: DateTime.ParseExact(entity.AUDIT_AT,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("IS_CANCELED",OracleType.VarChar,1,entity.IS_CANCELED),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_WF_STEP_AUDITOR_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_WF_STEP_AUDITOR SET 
    PACKAGE_WF_STEP_ID=:PACKAGE_WF_STEP_ID,
    PMES_USER_ID=:PMES_USER_ID,
    IS_AGREED=:IS_AGREED,
    AUDITOR_COMMENT=:AUDITOR_COMMENT,
    AUDIT_AT=:AUDIT_AT,
    IS_CANCELED=:IS_CANCELED,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE
 WHERE 
        AUDITOR_ID=:AUDITOR_ID 
", new[]{
            OracleHelper.MakeInParam("AUDITOR_ID",OracleType.Number,0,entity.AUDITOR_ID),
            OracleHelper.MakeInParam("PACKAGE_WF_STEP_ID",OracleType.Number,0,entity.PACKAGE_WF_STEP_ID),
            OracleHelper.MakeInParam("PMES_USER_ID",OracleType.VarChar,10,entity.PMES_USER_ID),
            OracleHelper.MakeInParam("IS_AGREED",OracleType.VarChar,1,entity.IS_AGREED),
            OracleHelper.MakeInParam("AUDITOR_COMMENT",OracleType.VarChar,30,entity.AUDITOR_COMMENT),
            OracleHelper.MakeInParam("AUDIT_AT",OracleType.DateTime,0,string.IsNullOrEmpty(entity.AUDIT_AT)?DateTime.Now: DateTime.ParseExact(entity.AUDIT_AT,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("IS_CANCELED",OracleType.VarChar,1,entity.IS_CANCELED),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
            });
        }
        public int Update_IS_CANCELED(decimal AUDITOR_ID)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_WF_STEP_AUDITOR SET     
    IS_CANCELED=:IS_CANCELED  
 WHERE AUDITOR_ID=:AUDITOR_ID 
", new[]{
            OracleHelper.MakeInParam("AUDITOR_ID",OracleType.Number,0,AUDITOR_ID),            
            OracleHelper.MakeInParam("IS_CANCELED",OracleType.VarChar,1,"1")            
            });
        }
        #endregion

        #region 删除
        public int PostDelete(PACKAGE_WF_STEP_AUDITOR_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_WF_STEP_AUDITOR 
WHERE
    AUDITOR_ID=:AUDITOR_ID
", new[]{
            OracleHelper.MakeInParam("AUDITOR_ID",OracleType.Number,0,entity.AUDITOR_ID)
            });
        }

        #endregion



    }
}
