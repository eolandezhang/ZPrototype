namespace Model.BaseInfo
{
    public class EQUIPMENT_CLASS_PROC_INFO_Entity
    {
        public decimal TOTAL { get; set; }
        public string PROCESS_ID { get; set; }
        public string EQUIPMENT_TYPE_ID { get; set; }
        public string EQUIPMENT_CLASS_ID { get; set; }
        public string FACTORY_ID { get; set; }
        public string UPDATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }
        public string REMARK { get; set; }

        //关联
        public string EQUIPMENT_CLASS_DESC { get; set; }
    }
}
