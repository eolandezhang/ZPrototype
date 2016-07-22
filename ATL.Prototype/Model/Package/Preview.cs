using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Package
{
    public class Preview
    {
        public string CurrentStepName { get; set; }
        public string NextStepName { get; set; }
        public List<Model.Settings.PMES_USER_GROUP_INFO_Entity> Auditors { get; set; }
        public string STEP_FLAG { get; set; }
        public decimal IS_AUDITOR { get; set; }
        public string CURRENT_STEP_FLAG { get; set; }
    }
}
