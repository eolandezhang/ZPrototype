using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using DBUtility;
using Model.Settings;

namespace DAL.Settings
{
    public class Department
    {
        public List<DepartmentEntity> GetData(string status)
        {

            return OracleHelper.SelectedToIList<DepartmentEntity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT DEPT_NUM, DEPT_NAME, DEPT_STATUS FROM DEPARTMENT WHERE DEPT_STATUS LIKE :DEPT_STATUS
", new[] { OracleHelper.MakeInParam("DEPT_STATUS", OracleType.NVarChar, status) });
        }
    }
}
