
namespace Model.Package
{
    public class PACKAGE_FLOW_INFO_Entity
    {
        public decimal TOTAL { get; set; }
        public string PACKAGE_NO { get; set; }//
        public string GROUP_NO { get; set; }//
        public string FACTORY_ID { get; set; }//
        public string VERSION_NO { get; set; }//
        public string PROCESS_ID { get; set; }//
        public decimal PROC_SEQUENCE_NO { get; set; }//工序序号
        public string PREVIOUS_PROCESS_ID { get; set; }//前一工序编码
        public string NEXT_PROCESS_ID { get; set; }//后一工序编码
        public string UPDATE_USER { get; set; }//
        public string UPDATE_DATE { get; set; }//
        public string PKG_PROC_DESC { get; set; }//工序说明
        public string SUB_GROUP_NO { get; set; }//每个工序的PKG分组归类

        //关联
        public string PROCESS_NAME { get; set; }//工序英文名
        public string PROCESS_DESC { get; set; }//工序中文名
        public string PROCESS_NAME_P { get; set; }//工序英文名
        public string PROCESS_DESC_P { get; set; }//工序中文名
        public string PROCESS_NAME_N { get; set; }//工序英文名
        public string PROCESS_DESC_N { get; set; }//工序中文名
        public decimal SEQUENCE_NO { get; set; }
        public decimal GROUP_QTY { get; set; }

        //批量添加辅助属性
        public string PRODUCT_TYPE_ID { get; set; }
        public string PRODUCT_PROC_TYPE_ID { get; set; }

        public string GROUP_NOS { get; set; }
        public string PROCESS_IDS { get; set; }
    }
}
