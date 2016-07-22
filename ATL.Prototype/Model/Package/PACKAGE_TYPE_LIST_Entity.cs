
namespace Model.Package
{
    public class PACKAGE_TYPE_LIST_Entity
    {
        public decimal TOTAL { get; set; }
        public string PACKAGE_TYPE_ID { get; set; }//PTP/ENG/RDP/PRP/FAE
        public string FACTORY_ID { get; set; }//
        public string PACKAGE_TYPE_DESC { get; set; }//
        public string PACKAGE_CODE { get; set; }//01/02
        public string VALID_FLAG { get; set; }//
        public string UPDATE_USER { get; set; }//
        public string UPDATE_DATE { get; set; }//
        public decimal GROUPS_LIMIT { get; set; }//0:不允许分组；其他分组数：允许的最大分组数
    }
}
