using DBUtility;
using Model.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Settings
{
    public class Permission
    {               

        public bool CheckGroupRight(string PMES_USER_ID, string FACTORY_ID, string PMES_TASK_ID)
        {
            return Convert.ToInt32(OracleHelper.ExecuteScalar(
                    PubConstant.ConnectionString,
                    CommandType.Text,
                    @"
SELECT COUNT (1)
  FROM PMES_USER_GROUP_INFO A, PMES_USER_GRP_TASK_INFO B
 WHERE     B.FACTORY_ID = A.FACTORY_ID
       AND B.PMES_USER_GROUP_ID = A.PMES_USER_GROUP_ID
       AND A.PMES_USER_ID = :PMES_USER_ID
       AND A.FACTORY_ID = :FACTORY_ID
       AND B.PMES_TASK_ID = :PMES_TASK_ID
",
                    new[] { 
                    OracleHelper.MakeInParam("PMES_USER_ID",OracleType.VarChar,10,PMES_USER_ID),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
                    OracleHelper.MakeInParam("PMES_TASK_ID",OracleType.VarChar,20,PMES_TASK_ID)
                }
                    )) > 0;
        }

        public bool CheckTaskRight(string PMES_USER_ID, string FACTORY_ID, string PMES_TASK_ID)
        {
            return Convert.ToInt32(OracleHelper.ExecuteScalar(

                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PMES_USER_TASK_INFO A
 WHERE     A.PMES_TASK_ID = :PMES_TASK_ID
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.PMES_USER_ID = :PMES_USER_ID
",
                new[] { 
                    OracleHelper.MakeInParam("PMES_USER_ID",OracleType.VarChar,10,PMES_USER_ID),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
                    OracleHelper.MakeInParam("PMES_TASK_ID",OracleType.VarChar,20,PMES_TASK_ID)
                })) > 0;
        }
                
    }
}
