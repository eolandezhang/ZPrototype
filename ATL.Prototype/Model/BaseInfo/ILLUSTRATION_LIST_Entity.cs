
namespace Model.BaseInfo
{
    public class ILLUSTRATION_LIST_Entity
    {
        public decimal TOTAL { get; set; }
        public string ILLUSTRATION_ID { get; set; }
        public string FACTORY_ID { get; set; }
        public string PRODUCT_TYPE_ID { get; set; }
        public string PRODUCT_PROC_TYPE_ID { get; set; }
        public string UPDATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }
        public string PROCESS_ID { get; set; }
        public string ILLUSTRATION_DESC { get; set; }
        //public object ILLUSTRATION_DATA { get; set; }
        public string VALID_FLAG { get; set; }
        public decimal IMG_LENGTH { get; set; }

        public byte[] UploadImg { get; set; }
    }
}
