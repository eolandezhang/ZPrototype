using DBUtility;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Package
{
    public class PACKAGE_WF_STEP
    {
        #region 查询

        public List<PACKAGE_WF_STEP_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PACKAGE_WF_STEP_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        PACKAGE_WF_STEP_ID,
        WF_SET_STEP_ID,
        PACKAGE_NO,
        VERSION_NO,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_WF_STEP
", null);
        }

        public List<PACKAGE_WF_STEP_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PACKAGE_WF_STEP_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        PACKAGE_WF_STEP_ID,
        WF_SET_STEP_ID,
        PACKAGE_NO,
        VERSION_NO,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            PACKAGE_WF_STEP_ID,
            WF_SET_STEP_ID,
            PACKAGE_NO,
            VERSION_NO,
            FACTORY_ID,
            UPDATE_USER,
            UPDATE_DATE
        FROM (  SELECT 
                    PACKAGE_WF_STEP_ID,
                    WF_SET_STEP_ID,
                    PACKAGE_NO,
                    VERSION_NO,
                    FACTORY_ID,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
                FROM PACKAGE_WF_STEP ) T1,          
            (  SELECT COUNT (1) TOTAL FROM PACKAGE_WF_STEP ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PACKAGE_WF_STEP_Entity> GetDataById(decimal PACKAGE_WF_STEP_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_WF_STEP_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_WF_STEP_ID,
        WF_SET_STEP_ID,
        PACKAGE_NO,
        VERSION_NO,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_WF_STEP
WHERE PACKAGE_WF_STEP_ID=:PACKAGE_WF_STEP_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("PACKAGE_WF_STEP_ID",OracleType.Number,0,PACKAGE_WF_STEP_ID)
            });
        }

        public List<PACKAGE_WF_STEP_Entity> GetDataByPkgId(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            return OracleHelper.SelectedToIList<PACKAGE_WF_STEP_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PACKAGE_WF_STEP_ID,
         A.WF_SET_STEP_ID,
         A.PACKAGE_NO,
         A.VERSION_NO,
         A.FACTORY_ID,
         A.UPDATE_USER,
         TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
         B.WF_SET_STEP_NAME
    FROM PACKAGE_WF_STEP A, WF_SET_STEP B, PACKAGE_BASE_INFO C
   WHERE     A.PACKAGE_NO = :PACKAGE_NO
         AND A.VERSION_NO = :VERSION_NO
         AND A.FACTORY_ID = :FACTORY_ID
         AND C.PACKAGE_NO = A.PACKAGE_NO
         AND C.VERSION_NO = A.VERSION_NO
         AND C.FACTORY_ID = A.FACTORY_ID
         AND B.FACTORY_ID = A.FACTORY_ID
         AND B.WF_SET_STEP_ID = A.WF_SET_STEP_ID
         AND B.WF_SET_NUM = C.APPROVE_FLOW_ID
ORDER BY A.UPDATE_DATE
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,PACKAGE_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }
        public List<PACKAGE_WF_STEP_Entity> GetLatest(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            return OracleHelper.SelectedToIList<PACKAGE_WF_STEP_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PACKAGE_WF_STEP_ID,
       A.WF_SET_STEP_ID,
       A.PACKAGE_NO,
       A.VERSION_NO,
       A.FACTORY_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       B.WF_SET_STEP_NAME,
       B.STEP_FLAG
  FROM PACKAGE_WF_STEP A, WF_SET_STEP B, PACKAGE_BASE_INFO C
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.VERSION_NO = :VERSION_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND C.PACKAGE_NO = A.PACKAGE_NO
       AND C.VERSION_NO = A.VERSION_NO
       AND C.FACTORY_ID = A.FACTORY_ID
       AND B.FACTORY_ID = A.FACTORY_ID
       AND B.WF_SET_STEP_ID = A.WF_SET_STEP_ID
       AND B.WF_SET_NUM = C.APPROVE_FLOW_ID
       AND A.UPDATE_DATE =
              (SELECT MAX (UPDATE_DATE)
                 FROM PACKAGE_WF_STEP D
                WHERE     D.PACKAGE_NO = A.PACKAGE_NO
                      AND D.VERSION_NO = A.VERSION_NO
                      AND D.FACTORY_ID = A.FACTORY_ID)
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,PACKAGE_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<PACKAGE_WF_STEP_Entity> GetPrevious(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, decimal PACKAGE_WF_STEP_ID)
        {
            return OracleHelper.SelectedToIList<PACKAGE_WF_STEP_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PACKAGE_WF_STEP_ID,
       A.WF_SET_STEP_ID,
       A.PACKAGE_NO,
       A.VERSION_NO,
       A.FACTORY_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       B.WF_SET_STEP_NAME,
       B.STEP_FLAG
  FROM PACKAGE_WF_STEP A, WF_SET_STEP B, PACKAGE_BASE_INFO C
 WHERE     A.PACKAGE_NO = :PACKAGE_NO
       AND A.VERSION_NO = :VERSION_NO
       AND A.FACTORY_ID = :FACTORY_ID
       AND C.PACKAGE_NO = A.PACKAGE_NO
       AND C.VERSION_NO = A.VERSION_NO
       AND C.FACTORY_ID = A.FACTORY_ID
       AND B.FACTORY_ID = A.FACTORY_ID
       AND B.WF_SET_STEP_ID = A.WF_SET_STEP_ID
       AND B.WF_SET_NUM = C.APPROVE_FLOW_ID
       AND A.PACKAGE_WF_STEP_ID!=:PACKAGE_WF_STEP_ID
       AND A.UPDATE_DATE =
              (SELECT MAX (UPDATE_DATE)
                 FROM PACKAGE_WF_STEP D
                WHERE     D.PACKAGE_NO = A.PACKAGE_NO
                      AND D.VERSION_NO = A.VERSION_NO
                      AND D.FACTORY_ID = A.FACTORY_ID
                      AND D.PACKAGE_WF_STEP_ID!=:PACKAGE_WF_STEP_ID)
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,PACKAGE_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PACKAGE_WF_STEP_ID",OracleType.Number,PACKAGE_WF_STEP_ID)
            });
        }

        public bool GetDataValidateId(decimal PACKAGE_WF_STEP_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM PACKAGE_WF_STEP
WHERE PACKAGE_WF_STEP_ID=:PACKAGE_WF_STEP_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_WF_STEP_ID",OracleType.Number,0,PACKAGE_WF_STEP_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_WF_STEP_Entity entity)
        {
            entity.PACKAGE_WF_STEP_ID = Convert.ToDecimal(OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"SELECT SEQ_PACKAGE_WF_STEP.NEXTVAL FROM DUAL",
                null
                ));
            var result = OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO PACKAGE_WF_STEP (
        PACKAGE_WF_STEP_ID,
        WF_SET_STEP_ID,
        PACKAGE_NO,
        VERSION_NO,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE
)
VALUES (
        :PACKAGE_WF_STEP_ID,
        :WF_SET_STEP_ID,
        :PACKAGE_NO,
        :VERSION_NO,
        :FACTORY_ID,
        :UPDATE_USER,
        :UPDATE_DATE
)
", new[]{
            OracleHelper.MakeInParam("PACKAGE_WF_STEP_ID",OracleType.Number,0,entity.PACKAGE_WF_STEP_ID),
            OracleHelper.MakeInParam("WF_SET_STEP_ID",OracleType.VarChar,20,entity.WF_SET_STEP_ID),
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
        });
            return result == 0 ? 0 : Convert.ToInt32(entity.PACKAGE_WF_STEP_ID);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_WF_STEP_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_WF_STEP SET 
    WF_SET_STEP_ID=:WF_SET_STEP_ID,
    PACKAGE_NO=:PACKAGE_NO,
    VERSION_NO=:VERSION_NO,
    FACTORY_ID=:FACTORY_ID,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE
 WHERE 
        PACKAGE_WF_STEP_ID=:PACKAGE_WF_STEP_ID 
", new[]{
            OracleHelper.MakeInParam("PACKAGE_WF_STEP_ID",OracleType.Number,0,entity.PACKAGE_WF_STEP_ID),
            OracleHelper.MakeInParam("WF_SET_STEP_ID",OracleType.VarChar,20,entity.WF_SET_STEP_ID),
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
            });
        }

        #endregion

        #region 删除
        public int PostDelete(PACKAGE_WF_STEP_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_WF_STEP 
WHERE
    PACKAGE_WF_STEP_ID=:PACKAGE_WF_STEP_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_WF_STEP_ID",OracleType.Number,0,entity.PACKAGE_WF_STEP_ID)
            });
        }

        #endregion



    }
}
