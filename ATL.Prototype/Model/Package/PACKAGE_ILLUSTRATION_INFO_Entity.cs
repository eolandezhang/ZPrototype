
namespace Model.Package
{
    public class PACKAGE_ILLUSTRATION_INFO_Entity
    {
        public decimal TOTAL { get; set; }
        public string PACKAGE_NO { get; set; }//
        public string GROUP_NO { get; set; }//
        public string VERSION_NO { get; set; }//
        public string FACTORY_ID { get; set; }//
        public string PROCESS_ID { get; set; }//
        public string ILLUSTRATION_DESC { get; set; }//插图描述
        public byte[] ILLUSTRATION_DATA { get; set; }//插图
        public string VALID_FLAG { get; set; }//
        public string UPDATE_USER { get; set; }//
        public string UPDATE_DATE { get; set; }//
        public string ILLUSTRATION_ID { get; set; }//插图ID

        public string PRODUCT_TYPE_ID { get; set; }
        public string PRODUCT_PROC_TYPE_ID { get; set; }

        public byte[] UploadImg { get; set; }

        //批量
        public string GROUPS { get; set; }

        //关联
        public decimal IMG_LENGTH { get; set; }
    }
}
