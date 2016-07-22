
namespace Model.BaseInfo
{
    public class PARAMETER_LIST_Entity
    {
        public decimal TOTAL { get; set; }
        public string PARAMETER_ID { get; set; }//
        public string FACTORY_ID { get; set; }//
        public string PRODUCT_TYPE_ID { get; set; }//
        public string PRODUCT_PROC_TYPE_ID { get; set; }//
        public string UPDATE_USER { get; set; }//
        public string UPDATE_DATE { get; set; }//
        public string PARAM_NAME { get; set; }//
        public string PARAM_DESC { get; set; }//
        public string PARAM_TYPE_ID { get; set; }//
        public string PARAM_IO { get; set; }//
        public string SOURCE { get; set; }//
        /// <summary>
        /// 是规格参数
        /// </summary>
        public string IS_SPEC_PARAM { get; set; }//
        public string IS_FIRST_CHECK_PARAM { get; set; }//
        public string IS_PROC_MON_PARAM { get; set; }//
        public string IS_OUTPUT_PARAM { get; set; }//
        public string IS_VERSION_CTRL { get; set; }//
        public string MEASURE_METHOD { get; set; }//
        public string IS_GROUP_PARAM { get; set; }//
        public string PARAM_DATATYPE { get; set; }//
        public string PARAM_UNIT { get; set; }//
        public string TARGET { get; set; }//
        public string USL { get; set; }//
        public string LSL { get; set; }//
        public string VALID_FLAG { get; set; }//
        public string SAMPLING_FREQUENCY { get; set; }//
        public string CONTROL_METHOD { get; set; }//       
    }
}
