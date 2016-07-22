namespace Model.Settings
{
    public class PMES_USER_GROUP_INFO_Entity
    {
        public decimal TOTAL { get; set; }
        public string PMES_USER_ID { get; set; }
        public string PMES_USER_GROUP_ID { get; set; }
        public string FACTORY_ID { get; set; }
        public string UPDATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }
        public string REMARK { get; set; }
        public string VALID_FLAG { get; set; }
        //关联
        public string CNNAME { get; set; }
        public string TITLE { get; set; }
        public string DEPARTMENT { get; set; }
        public string MAIL { get; set; }
       
    }
}
