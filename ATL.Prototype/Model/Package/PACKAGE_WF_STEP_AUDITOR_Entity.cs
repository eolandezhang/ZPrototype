using System;

namespace Model.Package
{
    public class PACKAGE_WF_STEP_AUDITOR_Entity
    {
        public decimal TOTAL { get; set; }
        public decimal AUDITOR_ID { get; set; }
        public decimal PACKAGE_WF_STEP_ID { get; set; }
        public string PMES_USER_ID { get; set; }
        public string IS_AGREED { get; set; }
        public string AUDITOR_COMMENT { get; set; }
        public string AUDIT_AT { get; set; }
        public string IS_CANCELED { get; set; }
        public string UPDATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }

        //关联
        public string CNNAME { get; set; }
        public string MAIL { get; set; }
        public string DEPARTMENT { get; set; }
        public string TITLE { get; set; }
    }
}
