namespace Model.Package
{
    public class PACKAGE_PROC_GRP_Entity
    {
        public decimal TOTAL { get; set; }
        public string PROC_GRP_ID { get; set; }
        public string PACKAGE_NO { get; set; }
        public string VERSION_NO { get; set; }
        public string FACTORY_ID { get; set; }
        public string PROCESS_ID { get; set; }
        public string PROC_GRP_DESC { get; set; }
        public string UPDATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }

        //辅助
        public string GROUPS { get; set; }
    }
}
