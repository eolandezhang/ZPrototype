
namespace Model.BaseInfo
{
    public class PROCESS_PARAM_INFO_Entity
    {
        public decimal TOTAL { get; set; }
        public string PROCESS_ID { get; set; }
        public string FACTORY_ID { get; set; }
        public string PRODUCT_TYPE_ID { get; set; }
        public string PRODUCT_PROC_TYPE_ID { get; set; }
        public string PARAMETER_ID { get; set; }
        public string IS_ILLUSTRATION_PARAM { get; set; }
        public string UPDATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }
        /// <summary>
        /// 显示在规格牌上的参数
        /// </summary>
        public string IS_SC_PARAM { get; set; }
        /// <summary>
        /// 参数package上的显示顺序
        /// </summary>
        public decimal PARAM_ORDER_NO { get; set; }
        /// <summary>
        /// 参数规格牌上的显示顺序
        /// </summary>
        public decimal DISP_ORDER_IN_SC { get; set; }

        public string PARAM_DESC { get; set; }
        public string IS_FIRST_CHECK_PARAM { get; set; }
        public string IS_PROC_MON_PARAM { get; set; }
        public string IS_OUTPUT_PARAM { get; set; }
        public string PARAM_UNIT { get; set; }
        public string TARGET { get; set; }
        public string USL { get; set; }
        public string LSL { get; set; }
        public string PARAM_IO { get; set; }
        public string PARAM_TYPE_ID { get; set; }
        public string SAMPLING_FREQUENCY { get; set; }
        public string CONTROL_METHOD { get; set; }
        public string IS_GROUP_PARAM { get; set; }        
        public string PARAM_DATATYPE { get; set; }
    }
}
