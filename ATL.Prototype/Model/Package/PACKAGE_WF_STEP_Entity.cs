namespace Model.Package
{
    public class PACKAGE_WF_STEP_Entity
    {
        public decimal TOTAL { get; set; }
        public decimal PACKAGE_WF_STEP_ID { get; set; }
        public string WF_SET_STEP_ID { get; set; }
        public string PACKAGE_NO { get; set; }
        public string VERSION_NO { get; set; }
        public string FACTORY_ID { get; set; }
        public string UPDATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }
        //关联
        public string WF_SET_STEP_NAME { get; set; }
        public string STEP_FLAG { get; set; }

    }
}
