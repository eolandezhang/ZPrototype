namespace Model.Settings
{
    public class WF_SET_STEP_Entity
    {
        public decimal TOTAL { get; set; }
        public string WF_SET_STEP_ID { get; set; }
        public string WF_SET_NUM { get; set; }
        public decimal ORDER_NUM { get; set; }
        public string AGREE_STEP_ID { get; set; }
        public string DISAGREE_STEP_ID { get; set; }
        public string PMES_USER_GROUP_ID { get; set; }
        /// <summary>
        /// FST：第一步，MDL：中间步骤，LST：最后一步
        /// </summary>
        public string STEP_FLAG { get; set; }
        public string UPDATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }
        public string VALID_FLAG { get; set; }
        public string FACTORY_ID { get; set; }
        public string WF_SET_STEP_NAME { get; set; }
    }
}
