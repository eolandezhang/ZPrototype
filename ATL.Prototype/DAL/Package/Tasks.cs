using DBUtility;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Package
{
    public class Tasks
    {
        public List<PACKAGE_BASE_INFO_Entity> GetTasks(string PMES_USER_ID)
        {
            return OracleHelper.SelectedToIList<PACKAGE_BASE_INFO_Entity>(
                PubConstant.ConnectionString, CommandType.Text,
@"
SELECT A.PACKAGE_NO,
       A.VERSION_NO,
       A.FACTORY_ID,
       TO_CHAR (B.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,       
       C.PRODUCT_PROC_TYPE_ID,
       C.PRODUCT_TYPE_ID,
       E.WF_SET_STEP_NAME,
       C.PURPOSE
  FROM PACKAGE_WF_STEP A,
       PACKAGE_WF_STEP_AUDITOR B,
       PACKAGE_BASE_INFO C,
       WF_SET D,
       WF_SET_STEP E
 WHERE     B.PACKAGE_WF_STEP_ID = A.PACKAGE_WF_STEP_ID
       AND B.PMES_USER_ID = :PMES_USER_ID
       AND B.IS_AGREED = '0'
       AND B.IS_CANCELED = '0'
       AND A.PACKAGE_NO = C.PACKAGE_NO
       AND A.VERSION_NO = C.VERSION_NO
       AND A.FACTORY_ID = C.FACTORY_ID
       AND C.APPROVE_FLOW_ID = D.WF_SET_NUM
       AND C.APPROVE_FLOW_ID = D.WF_SET_NUM
       AND E.WF_SET_STEP_ID = A.WF_SET_STEP_ID
       AND E.WF_SET_NUM = D.WF_SET_NUM
       AND E.FACTORY_ID = A.FACTORY_ID
       AND A.UPDATE_DATE =
              (SELECT MAX (T.UPDATE_DATE)
                 FROM PACKAGE_WF_STEP T
                WHERE     T.PACKAGE_NO = A.PACKAGE_NO
                      AND T.VERSION_NO = A.VERSION_NO
                      AND T.FACTORY_ID = A.FACTORY_ID)
ORDER BY B.UPDATE_DATE
", new[] {
     OracleHelper.MakeInParam("PMES_USER_ID",OracleType.VarChar,10,PMES_USER_ID)});
        }
    }
}
