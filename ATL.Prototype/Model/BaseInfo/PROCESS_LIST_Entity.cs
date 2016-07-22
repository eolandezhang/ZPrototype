
namespace Model.BaseInfo
{
    public class PROCESS_LIST_Entity
    {
        public decimal TOTAL { get; set; }
        public string PROCESS_ID { get; set; }//
        public string FACTORY_ID { get; set; }//
        public string PRODUCT_TYPE_ID { get; set; }//
        public string PRODUCT_PROC_TYPE_ID { get; set; }//
        public string VALID_FLAG { get; set; }//
        public string UPDATE_USER { get; set; }//
        public string UPDATE_DATE { get; set; }//
        public string PROCESS_NAME { get; set; }//
        public string PROCESS_DESC { get; set; }//
        public string WORKSTATION_ID { get; set; }//
        public string PROCESS_GROUP_ID { get; set; }//
        public decimal SEQUENCE_NO { get; set; }//
        public decimal ORDER_IN_GROUP { get; set; }//
        public string IS_MULTI_TASK { get; set; }//
        public string PREVIOUS_PROCESS_ID { get; set; }//
        public string NEXT_PROCESS_ID { get; set; }//

        public string PREVIOUS_PROCESS_DESC { get; set; }
        public string NEXT_PROCESS_DESC { get; set; }
    }
}
