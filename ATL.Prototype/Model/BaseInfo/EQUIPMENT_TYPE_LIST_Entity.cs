
namespace Model.BaseInfo
{
    public class EQUIPMENT_TYPE_LIST_Entity
    {
        public decimal TOTAL { get; set; }
        public string EQUIPMENT_TYPE_ID { get; set; }//MC:加工设备；TESTER:测试仪器；FIXTURE:工装夹具
        public string FACTORY_ID { get; set; }//
        public string EQUIPMENT_TYPE_NAME { get; set; }//
        public string EQUIPMENT_TYPE_DESC { get; set; }//
        public string UPDATE_USER { get; set; }//
        public string UPDATE_DATE { get; set; }//
        public string VALID_FLAG { get; set; }//
    }
}
