
namespace Model.Package
{
    public class PACKAGE_BOM_SPEC_INFO_Entity
    {
        public decimal TOTAL { get; set; }
        public string PACKAGE_NO { get; set; }//
        public string GROUP_NO { get; set; }//
        public string PROCESS_ID { get; set; }//
        public string FACTORY_ID { get; set; }//
        public string VERSION_NO { get; set; }//
        public string P_PART_ID { get; set; }//
        public string C_PART_ID { get; set; }//
        public decimal P_PART_QTY { get; set; }//
        public decimal C_PART_QTY { get; set; }//
        public string IS_VIRTUAL_PART { get; set; }//
        public string IS_IQC_MATERIAL { get; set; }//
        public string IS_SUBSTITUTE { get; set; }//
        public string SYNC_DATE { get; set; }//
        public string UPDATE_USER { get; set; }//
        public string UPDATE_DATE { get; set; }//

        //批量
        public string GROUPS { get; set; }
    }
}
