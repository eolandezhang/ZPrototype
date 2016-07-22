namespace Model.BaseInfo
{
    public class EQUIPMENT_CLASS_LIST_Entity
    {
        public decimal TOTAL { get; set; }
        /// <summary>
        /// 设备或仪器的分类：如搅拌机按照供应商可分为HY或ROSS；按照容量分5L，35L，100L等等
        /// </summary>
        public string EQUIPMENT_CLASS_ID { get; set; }
        public string FACTORY_ID { get; set; }
        public string UPDATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }
        public string EQUIPMENT_CLASS_NAME { get; set; }
        public string EQUIPMENT_CLASS_DESC { get; set; }
        public string CLASSIFIED_BY { get; set; }
        public string VALID_FLAG { get; set; }
    }
}
