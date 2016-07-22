
namespace Model.BaseInfo
{
    public class PROCESS_MATERIAL_INFO_Entity
    {
        public decimal TOTAL { get; set; }
        public string PROCESS_ID { get; set; }//
        public string FACTORY_ID { get; set; }//
        public string PRODUCT_TYPE_ID { get; set; }//
        public string PRODUCT_PROC_TYPE_ID { get; set; }//
        public string MATERIAL_TYPE_ID { get; set; }//
        public string UPDATE_USER { get; set; }//
        public string UPDATE_DATE { get; set; }//
        public string VALID_FLAG { get; set; }

        //关联
        public string MATERIAL_TYPE_NAME { get; set; }
        public string MATERIAL_TYPE_DESC { get; set; }
    }
}
