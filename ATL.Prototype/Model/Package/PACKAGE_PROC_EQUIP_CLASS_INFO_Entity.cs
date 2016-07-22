
namespace Model.Package
{
    public class PACKAGE_PROC_EQUIP_CLASS_INFO_Entity
    {
        public decimal TOTAL { get; set; }
        public string PACKAGE_NO { get; set; }//
        public string GROUP_NO { get; set; }//
        public string VERSION_NO { get; set; }//
        public string PROCESS_ID { get; set; }//        
        public string EQUIPMENT_CLASS_ID { get; set; }//
        public string FACTORY_ID { get; set; }//
        public string REMARK { get; set; }//
        public string UPDATE_USER { get; set; }//
        public string UPDATE_DATE { get; set; }//

        //批量
        public string GROUPS { get; set; }

        //关联
        public string EQUIPMENT_CLASS_DESC { get; set; }
        public string EQUIPMENT_TYPE_ID { get; set; }
    }
}
