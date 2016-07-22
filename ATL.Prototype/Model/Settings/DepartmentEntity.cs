using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Settings
{
    public class DepartmentEntity
    {
        public string DEPT_NUM { get; set; }
        public string DEPT_NAME { get; set; }
        public decimal DEPT_STATUS { get; set; }
    }
}
//CREATE TABLE EASYWEBAPP.DEPARTMENT
//(
//  DEPT_NUM     NVARCHAR2(20),
//  DEPT_NAME    NVARCHAR2(30),
//  DEPT_STATUS  NUMBER
//)
